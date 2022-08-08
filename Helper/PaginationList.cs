using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Helper
{
    public class PaginationList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public PaginationList(int currentPage, int pageSize, List<T> items)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            AddRange(items);
        }

        // 设计模式（工厂模式）
        public static async Task<PaginationList<T>> CreateAsync(
            int currentPage, 
            int pageSize,
            IQueryable<T> result   // items,第三个参数是真正需要输出的数据列表
        )
        {
            // pagination
            // skip
            var skip = (currentPage - 1) * pageSize;
            result = result.Skip(skip);
            // 以pageSize为标准显示一定量的数据
            result = result.Take(pageSize);

            var items = await result.ToListAsync();

            return new PaginationList<T>(currentPage, pageSize, items);
        }
    }
}
