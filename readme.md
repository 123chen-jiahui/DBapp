## 望周知

后端目前的进展：

1. 登录和注册

2. 初步的挂号（挂号时，病人会选择科室，这时候要显示科室里面所有医生的信息，供病人选择，实现了该接口）

3. 门诊医生给病人开具处方（从“购物车”中添加删除药品）

4. 生成订单

5. 查看历史订单

假想病人注册过程：输入一堆信息（身份证号等）以及密码，注册成功后返回一个病人Id作为账号，和密码配合使用可以登录。返回的病人Id按照顺序生成，是以1开头的七位数。这么做是考虑到，医院不是社交网站，不需要用户有花里胡哨的账号，最好做到格式统一，便于识别。由于它是按照顺序生成的，所以是整型。

假想的病人登录过程：使用病人Id和密码登录

员工注册过程：同病人注册过程一样，输入一堆信息（身份证号等）以及密码，注册成功后返回一个员工Id作为账号。返回的员工Id按照顺序生成，是以2开头的七位数。员工注册操作只能由管理员完成

员工登录：使用Id和密码登录

病人和员工的区别在于：1、病人的Id受限，员工不受限；2、病人注册由自己完成，员工注册由管理员完成

## 后端同学须知

请尽快学习我在群里发的[视频](https://www.bilibili.com/video/BV1p34y1L7f1?p=1&vd_source=99812009bbdcd07164cc7839b276ff53)，因为我们的项目的后端是仿照这个视频的，并且如果你没有看过，你可能看不懂我后面在讲什么。

后端架构是.net core 3.1，开发的API是RESTful风格（即面向资源）的，而数据库的构建则是使用Entity Framework，这么做的好处是可以尽量避免在数据库中建表、创建函数和序列、数据库编程等，这些工作都可以在Visual Studio中直接完成，[这里](https://docs.microsoft.com/zh-cn/ef/core/)是Entity Framework的官方文档，如果遇到困难可以查看。

为了大家都可以访问同一个数据库，我已经把数据库部署到服务器了，大家可以连接上去看看，连接的方法详见”数据库原理与应用-第3次实践课-2022-06-14（穆斌）“，该视频在软院公盘上能找到，在视频的前十分钟有详细的教程。不过视频里是连接本地的Oracle数据库，为了连接服务器的数据库，需要将”数据库主机名“改为公网IP：118.31.108.144；”服务名“改为：lhrcdb1；”用户名“为：C##TEST；”口令“为：123456。

截止目前项目依赖nuget程序包（可能有单词拼写错误，后续持续更新）：

AutoMapper.Extensions.MicroSoft.DependencyInjection(11.0.0)

Microsoft.AspNetCore.Authentication.JwtBearer(3.1.27)

Microsoft.AspNetCore.JsonPatch(6.0.7)

Microsoft.AspNetCore.Mvc.NewtonsoftJson(3.1.27)

Microsoft.EntityFrameworkCore(5.0.1)

Microsoft.EntityFrameworkCore.Design(5.0.1)

Microsoft.EntityFrameworkCore.Tools(5.0.1)

Microsoft.VisualStudio.Azure.Containers.Tools.Targets(1.10.13)

Newtonsoft.Json(13.0.1)

Oracle.EntityFrameworkCore(5.21.1)

Stateless(5.1.1)

我大致说明一下我写的这部分代码，希望大家能了解一下我想象中的结构。为获得良好的阅读体验，请切换到Back_End分支第二次commit完成时的代码查看（哈希号为`d33c50f`），因为当前后端进展已经超出这篇文章时很多了。

### Models文件夹

放的是模型，可以理解为数据库中的表。比如`Staff.cs`，Patient类中含有很多属性，就相当于数据库中表的column。我们可以看到在属性上面有用中括号括起来的东西，这是**数据注解**，例如`[Key]`表明这条属性是主键；`[Required]`表明这条属性非空，即数据库中的not null……数据注解有很多，更多详情请自行了解。有关数据注解有一个坑，阴间的Oracle数据库中啥东西都是全大写，如果是小写会带引号，这可能会出问题，所以需要在“每一个”属性上面都加了`[Column]`数据注解。

如果阅读仔细的话，你可能注意到了一个问题：并不是每个属性上面都有`[Column]`数据注解，比如`Staff.cs`中的`public Department Department { get; set;}`。这是为什么？它会是数据库Staff表中的一个Column么？（提示：想想对数据库不做任何操作的情况下，数据库支持Department这种自定义的数据类型么？）如果不是，为什么它要出现在这里？看看`Department.cs`，你是否也看到有关Staff的奇怪的属性？（提示：这份[文档]([关系 - EF Core | Microsoft Docs](https://docs.microsoft.com/zh-cn/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key))可以帮助你解决上面的问题，不需要非常详细的阅读，有个概念现查现用即可）所有问题解决完后，你是否能回答为什么`Ward.cs`和`Department.cs`之间也有这种奇怪的属性？你是否知道在Entity Framework中如果通过数据注解表示**外键**？表示外键的方法只有通过数据注解么？哪种方式更通用？

`Gender.cs`、`Role.cs`、`WardType.cs`并不是model，它们存在的意义我想看了代码就能懂（提示：枚举类型）

注意model的命名规范，全部大写开头，且不要加复数。

### Database文件夹

模型毕竟只是模型，还需要将其映射到数据库中，`AppDbContext.cs`中形如`public DbSet<Patient> Patients`的代码完成的就是映射工作，注意命名规范，映射的表都是复数。

`protected override void OnModelCreating(ModelBuilder modelBuilder)`函数的作用是添加数据，并对表添加一些约束，比如我添加了一个自增序列，自动生成病人Id（**注意**，视频中实现自增序列的方法是在model中使用数据注解，但是这对阴间的Oracle数据库无效，我踩了很多坑，觉得这个方法比较好）。另外，数据可以从json中导入，将json文件放在Database文件夹下，并按照模板来导入数据。

写好代码后，怎么将数据迁移到数据库呢，看过视频的自然知道，dotnet ef add migrations...，我不多说了。

这里提一下，如果大家使用本地的Oracle12c数据库，那么会出现一个巨大无比的坑，阴间的Oracle12c支持的最大标识符长度只有30位，Entity Framework自动生成的标识符很容易就超出限制，并且改也不好改。Oracle12c R2支持128位的标识符，我将它部署到服务器上了，也算是填了这个坑。

其他文件夹没有需要特殊说明的地方，如有哪些代码不理解，请联系我。

目前前端正在开发当中，并且任务比较重，**第二组**可以先进行后端的开发工作，即有关病人的挂号、收费、查询等API。第二组的同学在我的代码的基础上进行开发。

## 前端同学须知

前端使用**vue+axios**，俺也不知道有哪些好的学习资源QAQ，所以请大家抓紧学呀，目前前端滞后于后端，需要加快速度。

目前的API接口如下（持续更新中），`{{host}}`用服务器IP地址118.31.108.144替代。如果某接口不能用，或者出现Http状态码500，Internal Server Error，则是服务器的问题，出现这种情况请告知我修复服务器。 另外，如果前端需要某种功能的API，请与后端联系，添加之。

有的API需要授权（**Authorization**）才能访问，比如为病人开具处方、生成订单等API只有医生才能访问，而登录等API则是谁都可以访问，不进行权限验证。这是通过**jwt token**来实现的。jwt token相当于指明了某个用户拥有什么权限，拿我们的项目举例子，如果是病人登录，调用登录api，发送httppost请求，在请求体里面写病人的账号和密码，后端会返回一串加密过得字符，就是jwt token，这个东西表示了该用户是谁，有什么权限，哪些api可以调，哪些不能调（这些我都处理好了）。jwt token由浏览器/前端保存，当这个用户要访问一些api时，在http请求header中加入这串字符，后端可以知道这个用户是否有权限访问。怎么在http请求中加入jwt token请自行解决。

1. (POST)http://{{host}}/auth/auth/login_patient
   
   Authorization: 无（不进行权限验证）
   
   作用：病人登录
   
   Body请求参数（json格式）：
   
   | 参数名      | 类型       | 必填  | 说明   |
   | -------- | -------- | --- | ---- |
   | Id       | [number] | 是   | 病人Id |
   | Password | [string] | 是   | 病人密码 |
   
   返回参数：jwt token
   
   Body请求示例：
   
   ```json
   {
       Id: 1000000,
       Password: 'cjh010315'
   }
   ```
   
   返回示例：状态码200OK
   
   ```
   eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMDAwMDAwIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiUGF0aWVudCIsIm5iZiI6MTY1OTA1ODU4MiwiZXhwIjoxNjU5MTQ0OTgyLCJpc3MiOiJUb25namlIb3NwaXRhbC5jb20iLCJhdWQiOiJUb25namlIb3NwaXRhbC5jb20ifQ.LLev7Cd_dSVa2w5vm0IYbQo6hJksxZDng6gxxVhz35k
   ```
   
   错误Body示例：
   
   ```json
   {
       Id: 1000000,
       Password: '513010hjc'
   }
   ```
   
   返回：状态码400BadRequest，并返回字符串”用户名或密码错误“

2. (POST)http://{{host}}/auth/auth/login_staff
   
   Authorization: 无
   
   作用：员工登录
   
   Body请求参数（json格式）:
   
   | 参数名      | 类型       | 必填  | 说明   |
   | -------- | -------- | --- | ---- |
   | Id       | [string] | 是   | 员工Id |
   | Password | [string] | 是   | 员工密码 |
   
   返回参数：jwt token
   
   Body请求示例：
   
   ```json
   {
       Id: 2000000,
       Password: '123456'
   }
   ```
   
   返回示例：状态码200OK
   
   ```
   eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkb2N0b3Jfd2FuZyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik1lZGljaW5lVG9rZW4iLCJuYmYiOjE2NTkwNjA4NjQsImV4cCI6MTY1OTE0NzI2NCwiaXNzIjoiVG9uZ2ppSG9zcGl0YWwuY29tIiwiYXVkIjoiVG9uZ2ppSG9zcGl0YWwuY29tIn0.jhjOIyNcIc2LGsNNr-DBuSZRGZpRwn1qbkCMihxlyNI
   ```
   
   错误的情况同1

3. (POST)http://{{host}}/auth/auth/register_patient
   
   Authorization: 无
   
   作用：病人注册
   
   Body请求参数（json格式）
   
   | 参数名             | 类型           | 必填  | 说明     |
   | --------------- | ------------ | --- | ------ |
   | GlobalId        | [string]     | 是   | 病人身份证号 |
   | Password        | [string]     | 是   | 病人密码   |
   | ConfirmPassword | [string]     | 是   | 确认密码   |
   | Name            | [string]     | 是   | 病人姓名   |
   | Gender          | [**number**] | 是   | 病人性别   |
   | Birthday        | [**string**] | 是   | 病人出生日期 |
   | Phone           | [string]     | 是   | 病人电话号码 |
   
   返回参数：int（病人Id）
   
   Body请求示例：
   
   ```json
   {
       GlobalId: '012345678901234567',
       Password: '123456',
       ConfirmPassword: '123456',
       Name: '张三',
       Gender: 1,
       Birthday: '1988-05-16',
       Phone: '01234567890'
   }
   ```
   
   返回示例：Http状态码200OK，并返回分配的Id（整型），如1000003
   
   注：后端会对密码和确认密码进行检查，身份证号、手机号码等在前端进行合法性检查。同时，前端请确保Gender范围限定在0或1，这一点可以用选项实现。
   
   错误Body示例①：重复注册
   
   返回：Http状态码400BadRequest，并返回字符串“病人已存在，请勿重复注册”
   
   错误Body示例②：密码和确认密码不一致
   
   返回：Http状态码400BadRequest，并返回json
   
   ```json
   {
       "errors": {
           "confirmPassword": [
               "密码输入不一致"
           ]
       },
       "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
       "title": "One or more validation errors occurred.",
       "status": 400,
       "traceId": "|844f90cd-4f1b028c4781ffc6."
   }
   ```

4. (POST)http://{{host}}/auth/auth/register_staff
   
   Authorization: Admin（只有管理员才能访问）
   
   作用：员工注册
   
   Body请求参数：（json格式）
   
   | 参数名             | 类型       | 必填  | 说明     |
   | --------------- | -------- | --- | ------ |
   | GlobalId        | [string] | 是   | 员工身份证号 |
   | Password        | [string] | 是   | 员工密码   |
   | ConfirmPassword | [string] | 是   | 确认密码   |
   | Role            | [number] | 是   | 员工职务   |
   | Name            | [string] | 是   | 员工姓名   |
   | Gender          | [number] | 是   | 员工性别   |
   | Birthday        | [string] | 是   | 员工生日   |
   | Address         | [string] | 是   | 员工地址   |
   | Phone           | [string] | 是   | 员工电话号码 |
   | DepartmentId    | int      | 是   | 员工所在部门 |
   
   注：目前有10个科室，前端确保DepartmentId在1-10之间
   
   注：Role目前有0,1,2三种，分别表示Admin、Doctor、MedicineToken
   
   Body请求示例：
   
   ```json
   {
       GlobalId: '555555555555555555',
       Password: '12345678',
       ConfirmPassword: '12345678',
       Role: 2,
       Name: '李四',
       Gender: 2,
       Birthday: '2013-08-30',
       Address: '火星',
       Phone: '09876543210',
       Department: 5
   ```
   
   返回：Http状态码200OK，并返回分配的Id（整型），如2000009
   
   错误Body示例：身份证号已被注册
   
   返回：Http状态码400BadRequest，并返回字符串“身份证号已存在”

5. (GET)http://\{{host}}/staff/{DepartmentId}
   
   Authorization: Patient
   
   作用：根据科室Id来找到所有医生（挂号时有用）
   
   示例：https://{{host}}/staff/1
   
   返回：Http状态码200OK，并返回json
   
   ```json
   [
       {
           "name": "王明",
           "age": 23
       },
       {
           "name": "陈星睿",
           "age": 8
       }
   ]
   ```
   
   如果科室Id超出限制，则返回Http状态码404NotFound

6. (POST)http://{{host}}/patients/guahao（**该API还不完善**）
   
   Authorization: Patient
   
   作用：挂号
   
   Body请求参数：（json格式）
   
   | 参数名       | 类型       | 必填  | 说明           |
   | --------- | -------- | --- | ------------ |
   | Time      | [string] | 是   | 时间           |
   | fee       | [double] | 是   | 挂号费用（最多两位小数） |
   | PatientId | [number] | 是   | 病人Id         |
   | StaffId   | [number] | 是   | 员工Id         |
   
   返回：Http状态码200Ok

7. (GET)http://{{host}}/api/shoppingCart/{patientId}
   
   Authorization: Doctor
   
   作用：门诊医生看病时，获取病人当前的处方
   
   示例：http://{{host}}/api/shoppingCart/1000000
   
   返回：Http状态码200Ok，并返回病人当前处方，例如
   
   ```json
   {
       "id": "c131a5be-d954-49b7-94a5-ed7e49c78816",
       "patientId": 1000000,
       "shoppingCartItems": [
           {
               "id": 29,
               "medicineId": "Z20040063",
               "medicine": {
                   "id": "Z20040063",
                   "name": "连花清瘟胶囊",
                   "price": 15.0,
                   "inventory": 200,
                   "indications": "用于治疗流行性感冒属热毒袭肺症"
               },
               "shoppingCartId": "c131a5be-d954-49b7-94a5-ed7e49c78816",
               "price": 15.0
           }
       ]
   }
   ```

8. (POST)http://{{host}}/api/shoppingCart/items/{patientId}
   
   Authorization: Doctor
   
   作用：门诊医生看病时，将某一种药品添加进处方
   
   Body请求参数：（json格式）
   
   | 参数名 | 类型       | 必填  | 说明   |
   | --- | -------- | --- | ---- |
   | Id  | [string] | 是   | 药品Id |
   
   示例：http://{{host}}/api/shoppingCart/items/1000000
   
   示例请求体：
   
   ```json
   {
       Id: 'Z20040063'
   }
   ```
   
   返回：Http状态码200Ok，并返回病人当前处方（同7）

9. (DELETE)http://{{host}}/api/shoppingCart/items/{patientId}/{lineItemId}
   
   Authorization: Doctor
   
   作用：门诊医生看病时，将某一种药品从处方中删除
   
   示例：http://{{host}}/api/shoppingCart/items/1000000/29
   
   成功：Http状态码204NoContent
   
   失败①：Http状态码404Notfound，并返回字符串“当前处方中无该项”
   
   失败②：Http状态码400BadRequest，并返回字符串“不能删除其他病人的处方”

10. (DELETE)http://{{host}}/api/shoppingCart/items/{patientId}/({lineItemId})
    
    Authorization: Doctor
    
    作用：门诊医生看病时，批量删除处方
    
    示例：http://{{host}}/api/shoppingCart/items/1000000/(18, 19, 20)
    
    返回：Http状态码204NoContent

11. (POST)http://{{host}}/api/shoppingCart/checkout/{patientId}
    
    Authorization: Doctor
    
    作用：将处方生成订单
    
    示例：http://{{host}}/api/shoppingCart/checkout/1000000
    
    返回：Http状态码200Ok，并返回订单信息json，例如：
    
    ```json
    {
        "id": "ec1d7e24-f81d-4b1a-8236-47dedbcfae23",
        "patientId": 1000000,
        "orderItems": [
            {
                "id": 29,
                "medicineId": "Z20040063",
                "medicine": {
                    "id": "Z20040063",
                    "name": "连花清瘟胶囊",
                    "price": 15.0,
                    "inventory": 200,
                    "indications": "用于治疗流行性感冒属热毒袭肺症"
                },
                "shoppingCartId": null,
                "price": 15.0
            },
            {
                "id": 30,
                "medicineId": "Z50020615",
                "medicine": {
                    "id": "Z50020615",
                    "name": "急支糖浆",
                    "price": 25.0,
                    "inventory": 350,
                    "indications": "用于外感风热所致的咳嗽"
                },
                "shoppingCartId": null,
                "price": 25.0
            }
        ],
        "state": "Pending",
        "createDateUTC": "2022-08-06T02:30:57.8596092Z",
        "transactionMetadata": "what"
    }
    ```
