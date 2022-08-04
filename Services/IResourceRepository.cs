﻿using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IResourceRepository
    {
        Medicine GetMedicine(string medicineId);
        void AddShoppingCartItem(LineItem lineItem);
    }
}