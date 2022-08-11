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

API接口信息转移到了postman中，下载`TongjiHospital.postman_collection.json` ，并将文件拖入postman中即可产生API信息。如果某接口不能用，或者出现Http状态码500，Internal Server Error，则是服务器的问题，出现这种情况请告知我修复服务器。 另外，如果前端需要某种功能的API，请与后端联系，添加之。

有的API需要授权（**Authorization**）才能访问，比如为病人开具处方、生成订单等API只有医生才能访问，而登录等API则是谁都可以访问，不进行权限验证。这是通过**jwt token**来实现的。jwt token相当于指明了某个用户拥有什么权限，拿我们的项目举例子，如果是病人登录，调用登录api，发送httppost请求，在请求体里面写病人的账号和密码，后端会返回一串加密过得字符，就是jwt token，这个东西表示了该用户是谁，有什么权限，哪些api可以调，哪些不能调（这些我都处理好了）。jwt token由浏览器/前端保存，当这个用户要访问一些api时，在http请求header中加入这串字符，后端可以知道这个用户是否有权限访问。怎么在http请求中加入jwt token请自行解决。

`longhu`分支实现了登录注册功能，扒下来跑就能看到登录界面了

以下是来自龙胡志远同学的Vuetify经验：

+ 增加新的页面
  
  + 在`src\views`路径中添加页面
  
  + 在`src\router\index.js`文件的router对象的成员，数组对象routes中，有layout函数，其中第二个参数是一个数组对象，模仿其它页面，在第二个参数中添加对应的页面名称（第一个参数）和路径（第三个参数）。
  
  + 在`src\store\modules\app.js`文件中state对象的items成员中，模仿该数组的其它对象，添加自己的页面按钮（显示在页面的最左侧），图标（第二个参数）可以参考这个网站 `https://materialdesignicons.com/icon/view-dashboard`

+ tips
  
  + 获取用户的`jwt token`:模仿`src\views\Login.vue`中获取`src\store\modules\app.js`的jwt的方式，需要使用的时候jwt的时候在自己页面的js代码中添加对应代码以添加jwt
  
  + 页面跳转推荐使用这种方式，因为可能最后要统一路径格式，这样写改起来可能会方便一点
    
    ```js
    this.$router.push({ name: 'Dashboard' })
    ```
  
  + 可以注释掉`src\router\index.js`的24-27行来跳过登录（但jwt token就是空的）
