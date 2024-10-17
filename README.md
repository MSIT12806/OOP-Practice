# OOP-Practice

本專案用以實作《Agile Priciples, Patterns, and Practices in C#》中 Section 3 的 「薪水支付案例」。

## 20204/10/15

今天是實作這練習的第一天，我要先閱讀一遍該案例的需求文件，然後規劃出系統。

### 需求文件
需求：系統必須按照規定的方法準時支付所有員工正確數目的薪水。同時，必須從員工的薪水中減掉各種款項。

**使用案例**

1. AddEmp 增加員工
1. DelEmp 刪除員工
1. TimeCard 登記出缺勤卡
1. SalesReceipt 登記銷售單據
1. ServiceCharge 登記公會服務費
1. ChgEmp 更改員工明細
1. Payday 在今日執行薪水支付系統

我預期使用三次迭代，將這些案例逐一完成

**第一次迭代**

- AddEmp
- DelEmp
- ChgEmp
- ServiceCharge
- Payday

**第二次迭代**

- SalesReceipt

**第三次迭代**

- TimeCard

### 第一次迭代

1. 建置專案

	我預期使用 Outside-In 的方式進行開發，所以我先來個 MVC 專案，然後很快速的把專案架起來，畢竟是 OOP ，所以還是遵照 clean architecture 原則來跑。

1. 呈現功能列表

	我預計在首頁陳列我的所有功能，因此做了一張 table，並且預先寫好所有網址對接的Action。
	
	雖然規格上並沒有要求每個功能都要一個獨立的畫面，但是一方面這樣比較好在首頁呈現。
	
	另外，我依照可能的領域切分 Controller，以避免後續可能會有安全性等議題的劃分。
	
	前端的 URL 的部分，我盡量善用 MVC 的特性，把 C# 強型別的寫法溶入進來，使用 nameof 運算式，取得 Controller 和 action 的名字。
	
1. 建立畫面

	首先得要建立 各種功能需要的 ViewModel
	
	- AddEmp: EmpId, name, address
	- DelEmp: EmpId
	- ChgEmp: EmpId, name, address // , hourly, salaried, commissioned, hold, direct, mail, member, dues, nomember // 規格書上說要這些欄位，但目前不懂為什麼，所以也先不加上。
	- ServiceCharge: memberId, amount
	- Payday

	目前會碰到的問題：
	
	1. AddEmp 的 view model 跟 ChgEmp 的 view model 長得一樣，在開發上就會想要借用，但是在此時需要謹慎。
	1. view model 檔案的管理：我目前是用 Controller 的階層去處理，但我不確定這樣的放法是否是最好的。
	
	建置好 view model 之後，就可以用 .net core 自動新增模板畫面。
	
	然後 View model 可以用 Attribute 語法，快速加入驗證機制。
	
	還可以快速加入 防偽造標籤，作為資安的需求。
	
	在撰寫 Controller 的過程中，還碰到 return View, Redirect 還是 RedirectToAction 的問題。
	
	另外就是 ChgEmp 如果傳入的 empId 是無效的，就應該回傳錯誤畫面，需要注意回傳錯誤畫面的語法。
	
1. 建立畫面 -2

	產生 案例五的 View model 的時候，我發現好像漏掉 和 Emp 建立關聯的設定，我先擅自加上去，不知道之後書本的內容會不會提到。
	
1. 資料儲存

	我預期使用 Entity Framework 來處理，首先要做一個 Infrastructure 層，用來處理跟外部套件，例如 Entity Framework 等工具的互動。
	
	然後在 Program.cs 加入 DbContext 服務，並註冊為 InMemory 的機制。
	
	```csharp
	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.
	builder.Services.AddControllersWithViews();

	#region 加入各種服務

	// 加入 Entity Framework Core 服務(需要 using Microsoft.EntityFrameworkCore 以及 Microsoft.EntityFrameworkCore.InMemory)
	builder.Services.AddDbContext<AppDbContext>(o=> o.UseInMemoryDatabase("InMemoryDb"));

	#endregion

	var app = builder.Build();

	// and so on...

	```
	
	接著，我終於碰觸到核心業務邏輯，我在 Model 層，加入了第一個類別，「Emp.cs」，並撰寫了第一個方法。
	
	Model 層對外需要使用 interface 介接，為了儲存員工的資料，我宣告了 IEmpRepository 的介面。
	
	介面的實作則放在 Application 層，將由 Application 層負責與 Infrastructure 進行互動。
	
	為了滿足 Entity Framework 的行為，需要為 DbModel 定義一些屬性，有些要寫在 OnModelCreating 方法，有些可以用 Attribute 的方法寫在 DbModel 裡面，目前還在權衡哪一種寫法比較好。
	
1. 資料儲存 -2

	為了方便人工測試，我還是需要生成 List 表單的畫面。
	
	然後我突然意識到，我到目前其實都還沒有碰到資料的核心邏輯，因為資料的 crud ，其實還是算 application 的東西。
	
	因此寫到這裡，我突然發現我還沒為我真正的業務物件，也就是 Emp 進行建模，而我先前寫的 Emp，因為要對 Emp 資料進行 crud ，因此並不算 Emp 本身，我已經將其改名為 EmpServiece。
	
	建立了 Emp 模型，現在依賴關係就變得比較清楚了，核心業務邏輯，也就是 Emp，本身不可以有任何依賴。應用程式層，則依賴核心模型，
	
	然後我觸碰到了 Adapter 層的物件，才發現 Repository 、 View Model 都屬於 Adapter 層，也就是所謂的實作層。
	
1. 資料儲存 -3

	實作 ServiceCharge 的儲存。
	應該要驗證兩個規則：確認員工編號確有其人、確認公會編號確有其人。另外，可能也要找個方法去驗證這兩個人的某些資料是一致的，不過目前就先不糾結在這些邏輯裡面。
	
	為了要驗證是否該員工編號有效，我的 ServiceChargeService，就要跟 EmpService 進行互動，不過，兩個 Service 不能有依賴關係，所以，我選擇用介面去處理隔離。
	
1. 薪水支付

	其實不是很知道薪水支付應該要做的行為是甚麼，畢竟這是練習的專案，不可能真的有甚麼帳戶可以扣款。所以我選擇回傳金額數目。
	
	然後薪水支付要依賴 EmpService 和 ServiceCharge 的資料，但是我又不能直接使用這兩個領域的 domain model，所以必須要自己建模。
	
	所以為了維持邊界，真的是一個很麻煩的事情，不過在前公司經歷了邊界模糊的折磨，我想我會繼續努力堅持。
	
	如果薪水支付這功能順利完成，我就要算要完成第一次迭代了。
	