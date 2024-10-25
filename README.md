# OOP-Practice

本專案用以實作《Agile Priciples, Patterns, and Practices in C#》中 Section 3 的 「薪水支付案例」。

## 20204/10/15

今天是實作這練習的第一天，我要先閱讀一遍該案例的需求文件，然後規劃出系統。

### 需求文件

系統必須按照規定的方法準時支付所有員工正確數目的薪水。同時，必須從員工的薪水中減掉各種款項。

- 有些員工是時薪工，會按照他們員工紀錄中時薪的欄位的值對他們進行支付。他們每天會提交工作出勤卡，其中紀錄了日期以及工作小時數。如果他們每天工作超過 8 小時，那麼超過的部分會按照正常報酬的 1.5 倍進行支付。每周五是支付日。
 
- 有些員工的薪水是月薪進行支付。每個月的最後一個工作日是支付日。在他們的員工紀錄中有一個月薪的欄位。

- 有些員工是銷售員，他們的薪水是基本薪水加上一定比例的銷售額，是為傭金(commission)。他們會提交銷售單據，其中紀錄了銷售的日期和數量；並且員工紀錄中有一個佣金率的欄位。每隔一周的周五是支付日。

- 有些員工是公會成員，在他們的員工紀錄中有一個每周會費的欄位。這些會費會直接從他們的薪水中扣除。公會有時也會針對單個工會成員徵收服務費用。公會每周會提交這些服務費用，服務費用必須從相對應的員工的下個月薪水總額中扣除。

- 薪水支付系統每個工作日運行一次，並在當天為相關員工進行支付。系統會被告知員工的支付日期，這樣她會計算從員工上次支付日期到規定的本次支付日期間所應支付的金額。


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
	
1. 薪水支付-2

	上次完成了 Payday Service 的架構，以及 PaydayController 的實作，現在要來實作 Adapter 層，我發現我漏掉了 員工的薪資的欄位。
	
	雖然匆匆忙忙的實作完畢了，但是這涉及了一個很有趣的問題：薪水收入是否還算是員工的主要欄位？還是應該要屬於薪水支付的子系統？
	
	我決定修改一下，把薪水收入的欄位另外放在一個資料表。
	
1. 薪水支付 -3

	多調整了一些東西，比方說，mapper 不使用介面去處理，而是用靜態方法。
	
	以及發現發薪水這件事，其實不用問過 EmpService。這就有點怪怪的，因為如果兩邊沒有同步得很好，不就有可能不小心發薪水給離職員工之類的？但目前實作起來就是這樣，哈哈。如果真的要強烈綁定，可能還是要考慮寫在同一張資料表？
	
1. 第二次迭代

	一個大略的功能已經開發完成，現在要進行第二次的功能開發，這次要加入一個附屬功能：有銷售單據的員工，將要可以進行抽成。經過分析後可以得知，這次主要邏輯的調整在 Payday 服務，要去多考慮一張儲存了銷售單據的資料表，這張表裡面存放每位員工的銷售單據等資訊。

	1. 薪水計算公式魂歸何處
		
		當我開始涉及到不同的薪水計算公式，我認為這是他們公司的核心邏輯，因此需要聚合到 Domain 層當中，但是目前 domain 層的東西都還只是非常簡單的純資料結構的物件。
		
		因此我想到了將 PaydayResult 提升為領域物件，因為在實務上，通常不會單單結算薪資而已，這些資料應該也會被要求進行儲存。
		
		Payday 資料結構上也應該要加上抽成的薪資部分，這樣才能夠在 PaydayService 中進行計算。

		問題一，這樣是否違反開放/封閉原則？我認為是的，這樣的改動勢必會影響原本不需要抽成的員工薪水的計算方式，但如果我要避免這樣的改動，我就要先進行重構。

		問題二，銷售單據的資料從哪裡來？有可能我會需要從一個訂單系統去進行界接，所以在這裡我還是要選擇用介面隔離的方式來做，並且在實作層用自己的邏輯來處理。
	
	1. 重回 Context 的設置

		Context 的設置真的是一個讓我很頭痛的事情，每個 table 要做的事情都是一樣的，所以重複率很高，而且我也很容易忘記，我想要挑戰使用設計方法，將重複的以及容易遺漏的問題解決掉。

		然後...因為一些考量，後來沒有去做，因為重複的程式碼主要也只有 HasKey。

	1. 加入服務是否有時序耦合的問題？

		目前看起來沒有時序耦合的問題。
	
	1. 加入測試
			
	    原因很簡單，只是我覺得重複測試太麻煩了，哈哈哈，然後也是每次迭代，都會回頭修改很多程式碼，所以加上測試比較保險。
	
	    我就簡單產生一些測試的成功案例。不過 AI 助手生成給我的是單元測試，但我想要直接使用 ATDD 的驗收測試，因此就寫了一個算是整合測試的案例。

		然後為了規避使用到相同變數的問題，我就用了 if 區塊，哈哈，我想這是一個不好的方法，但是目前就先這樣了。

		然後為了讓測試通過，還是有很多程式碼的問題被我處理，都是很基礎的關於 dbContext 的應用、IEnumerable 在迴圈中可不可以被修改的問題。


1. 重整需求

	這次大概就是我要做的進度了，但是其實需求還有很多，因此這次我要再度整理一次需求。在重寫需求的過程中，有一些體悟，這讓我整理了一篇關於怎麼撰寫需求文件給軟體工程師的文件。

	整理完需求文件，就可以盤點這個小小的需求，到底描述了那些需求和邏輯，以下整理成表格：

	|員工|薪水計算方式| 支付頻率 | 
	|---|---|---|
    |時薪員工|時薪 + 超時津貼|每週五|
    |月薪員工|固定月薪|每月最後一個工作日|
    |銷售員|銷售抽成|隔週週五|
    |公會員|固定周費 + 額外費用|從下個月薪水中扣除|

	這樣整理完以後，發現其實東西還不少，新的需求整理如下：

    **新的業務邏輯**
	1. 發薪日的檢查
	1. 時薪員工的薪水計算
	1. 銷售抽成的計算
	1. 公會員的周費扣除

	**新的應用程式邏輯**
	1. 發薪日的檢查
	1. 時薪員工的薪水計算
		1. 新增設定員工是時薪員工還是月薪員工的功能
	1. 銷售抽成的計算
		1. 修改銷售單據建檔功能
	1. 公會員的周費扣除
		1. 模擬公會費用請求的功能
	
	**第三次迭代**		
	- 時薪員工的薪水計算
	
	**第四次迭代**
	- 銷售抽成的計算
	
	**第五次迭代**
	- 公會員的周費扣除
	
1. 第三次迭代
	
	**Outside-In 與 測試驅動的糾結**
	> 他們每天會提交工作出勤卡(TimeCard)，其中紀錄了日期以及工作小時數。如果他們每天工作超過 8 小時，那麼超過的部分會按照正常報酬的 1.5 倍進行支付。

	需要一個新的資料表"TimeCard"，我打算放在 Payday 系統內，因為這個資料表只會被 Payday 系統使用。我一樣使用 Outside-In 的方式進行開發，首先要建立一個新的 ViewModel，然後建立一個新的 Controller，然後建立一個新的 Service、Core Model，最後建立一個新的 Repository。但是現在遇到一個困難，就是我同時也要寫測試腳本，到底是先寫測試腳本，再用Outside-In 的方式開發，還是先用Outside-In 的方式開發，再寫測試腳本？我決定先使用 Outside-In 的方式開發，完成畫面以及 Controller，再來寫測試腳本，再用測試腳本去驅動功能的完成。

	**ViewModel 與 Controller 的多型**
	
	這次我還想要嘗試使用繼承關係處理 ViewModel 很重複的問題。然後修改一下命名規則：資料-動作-ViewModel，例如 TimeCard-Add-ViewModel。

	也嘗試用多型處理 Controller，並且重新思考Controller 的命名問題，一個 Service 應該對應若干 Controller，因此 Controller 命名不應該直接只用 Service 的名稱。然後我也在父類別註冊的欄位或方法都使用 protected 命名前綴，這樣子類別就可以直接打 protected 查看父類別的欄位或方法。
	
1. 重整案例

	1. 經過第三次迭代的重新分析，我發現書本中與我自己對於使用案例的重新理解，為了可以更好的呈現系統的使用，我決定重新整理一下使用案例。

	**案例1：新增員工**

	Emp.AddEmp

	**案例2：刪除員工**

	Emp.DelEmp

	**案例3：設定員工給薪**

	Pay.SetEmpSalary

	**案例4：登記出缺勤卡**

	TimeCard.SaveTimeCard

	**案例5：登記銷售單據**

	SalesReceipt.SaveSalesReceipt

	**案例6：登記公會服務費用**

	Guild.SaveServiceCharge

	**案例7：修改員工資料**

	Emp.ChgEmp

	**案例8：執行支付薪水**

	Pay.Payday

1. 建立 Gherkin Language

	經過上面使用案例的重新整理，我也重新處理了系統的布局，也應該要撰寫 Gherkin Language 的語法，這樣可以更好的呈現系統的使用。

	我就把最複雜的案例寫成驗收標準。

	另外介紹好用的單元測試擴充套件[Fine Code Coverage](https://www.youtube.com/watch?v=CvrySUcTi7I)


1. 第三次迭代 -2
	
	我要來處理工會費用的邏輯，突然覺得由外部傳入 ServiceChargeCore 挺不合理的，因為領域物件應該要在領域內部生成，而不是由外部傳入。由外部傳入會不知道這個領域物件是從系統內部而來，還是從外部傳來，以及各種物件的屬性是否都有填妥等。

	**查詢邏輯要在哪裡實現**

	在撰寫薪水支付的時候，因為要去結算該月的銷售業績和服務費等資料，因此要寫特定的查詢邏輯，問過 chatGPT，他認為寫在 repository 和 service 都可以，看我考量的是甚麼，寫在 repository 或許可以追求比較好的效能表現，而寫在 service 層則是能處理比較多的邏輯。我目前先寫在 service 層，讓 repository 職責乾淨一些，如果需要進行效能調教，在看情況實現部分查詢在 repository 裡面。

## 20204/10/21

### 朝著 DDD 重構

上述功能大致上完成，接下來要朝著 DDD 的架構進行重構。

首先，一個很明顯的聚合根，就是 Emp，所有的其他資料都依附於它，但如果這樣做，我就必須把 IRepository 的層級提升到 領域邏輯內。

然後我還要重寫測試邏輯。

1. 臃腫的 Repository

	經過分析，我發現這些實體都與 Emp 有強烈的依賴性，所以我決定將這些實體的操作都聚合在 Emp 裡面，但是這樣就變成 EmpRepository 顯得十分臃腫。經過與GPT 的討論，他的回應如下：
	``` markdown
	你提到的問題很常見。在 DDD 中，聚合根的 repository 可能會因為需要處理多個子實體而變得臃腫。這裡有幾個方法可以考慮，幫助保持 empRepository 的簡潔性：
	
	方法分離：將 empRepository 的方法分離，確保每個方法都專注於單一職責。可以將一些複雜的邏輯提取到服務類中，這樣 empRepository 就只負責 CRUD 操作。
	
	使用領域服務：如果某些操作需要涉及多個子實體，考慮將這些操作提取到領域服務中。這樣可以避免在 empRepository 中直接處理多個子實體的情況，使其保持簡潔。
	
	聚合設計：確保聚合的設計是合理的。如果某些子實體之間的關聯不強，可以考慮將它們獨立出來，形成新的聚合根。這樣可以減少 emp 聚合的複雜度。
	
	界面和實現分離：可以為 empRepository 定義一個接口，然後將具體的實現放在不同的類中。這樣可以提高可測試性和維護性。
	
	分層架構：根據應用的大小和複雜度，可以考慮使用 CQRS（Command Query Responsibility Segregation）模式，將查詢和命令操作分開，這樣可以減少 empRepository 的責任。
	
	這些方法可以幫助你控制 empRepository 的臃腫問題，保持代碼的整潔和可維護性。
	```

1. 再論 Service

	Service 承載的是 User Story，也就是從業務邏輯的角度去看系統的行為，所以 Service 之間不應該有依賴關係。然後像 Controller 這類 UI 的接口，都應該透過 Service 進行行為，不能直接使用 Domain 物件。
	
	Service 的概念是一組操作行為，所以這物件本身是沒有狀態的。
	
	那，像是 ChgEmp ，在 Service 層應該要分解成多個方法，因為一個 ChgEmp 的 request，裡面蘊含修改多個欄位的可能的使用者故事，但是這又遇到一個問題，如果每一個欄位，我就去訪問一次資料庫，這樣會不會太浪費資源？

## 20204/10/24

### 加入新的員工

我發現自己其實沒有實作時薪工的部分，所以我決定先實作時薪工的部分。

其實一個系統怎樣設計都沒有錯，只要能滿足使用者的需求，但是如果我的系統的概念盡可能的與使用者一致，那在該業務中要怎麼擴充、那些地方要修改、那些地方可能會有錯誤，將會更容易的發現。

在該情境中，我發現這個業務區分不同發薪水方式是非常直觀的，甚至他們不會去把月薪員工和時薪員工混為一談，也就是說，他們的行為是互相獨立的。因此我在寫測試的時候，也就直接分開寫，不用特別去考慮他們之間混合的情形。

**小插曲：處理 SaveChanges** 

因為要一直寫 SaveChanges 真的很煩，我想讓 SaveChanges 自動在每一次的 request 結束的時候執行，這樣使用者的每一次操作就會自動變成一個單元，如果中間有任何異常，應該就可以自己回滾，然後如果有些資料要先寫入，就可以自己再調用 SaveChanges。

於是我把 EmpRepository 繼承 IDisposable，並且在 Dispose裡面實作 SaveChanges ，結果執行時碰到 cannot access a disposed object 的問題，因為 dbContext 物件是使用依賴注入，所以我不能控制他的生命週期。

經過查詢後，我猜測問題是發生在，我並沒有使用非同步的寫法，這樣可能導致非同步的 dbContext 判斷已經沒有在等待的任務，就會自動回收。

解決了，問題是發生在我應該繼承非同步的 IAsyncDisposable，而不是 IDisposable。

1. 重新建模

	這次發現，其實寫程式要對業務邏輯具有描述性，比方說，時薪員工、月薪員工，這些都是業務領域中實實在在的名詞，並不是像 薪水這種某員工的內秉屬性，所以如果要更貼砌業務邏輯，應該要把這些屬性提升為一個獨立的物件。

	現在我要重新對需求進行分析，名詞我會用粗體字，動詞我會用反灰，希望用這種簡單粗暴的方法，可以初步分解出本次要重構的模組。

	**系統**必須按照規定的方法`準時支付`所有**員工**正確數目的**薪水**。同時，必須從員工的薪水中`減掉`各種**款項**。

	- 有些員工是**時薪工**，會按照他們員工紀錄中`時薪`的欄位的值對他們進行`支付`。他們每天會`提交`**工作出勤卡**，其中`紀錄`了**日期以及工作小時數**。如果他們每天工作超過 8 小時，那麼超過的部分會按照正常報酬的 1.5 倍進行支付。每周五是支付日。
 
	- 有些員工的薪水是**月薪**進行支付。每個月的最後一個工作日是支付日。在他們的員工紀錄中有一個月薪的欄位。

	- 有些員工是**銷售員**，他們的薪水是**基本薪水**加上**一定比例的銷售額**，是為**傭金**(commission)。他們會`提交`**銷售單據**，其中`紀錄`了**銷售的日期和數量**；並且員工紀錄中有一個**佣金率**的欄位。每隔一周的周五是支付日。

	- 有些員工是**公會成員**，在他們的員工紀錄中有一個**每周會費**的欄位。這些會費會直接從他們的薪水中`扣除`。公會有時也會針對單個工會成員徵收服務費用。公會每周會提交這些服務費用，服務費用必須從相對應的員工的下個月薪水總額中扣除。

	- 薪水支付系統每個**工作日**`運行`一次，並在當天為相關員工進行支付。**系統**會被告知員工的**支付日期**，這樣她會計算從員工上次支付日期到規定的本次支付日期間所應支付的金額。

	畫成 UML 圖如下：

	``` mermaid
	
	---
	title: 圖一：系統透過員工資訊支付薪水
	---
	classDiagram 
	   
	      系統 --> 員工
	      員工 --> 支付日期
	      支付日期 --|> 工作日
	
	      class 系統{
	        薪水 支付(員工)
	      }
	
	```
	``` mermaid
	
	---
	title: 圖二：月薪員工關係圖
	---
	classDiagram 
	   
	   class 薪水{
	void 減掉(款項)
	   }
	   class 款項{
	
	   }
	員工 <|--月薪員工
	員工 <|--銷售員
	員工 <|--工會成員
	月薪 --|>薪水
	基本薪水--|>薪水
	傭金--|>薪水
	每周會費 --|> 款項
	銷售員 -->基本薪水
	銷售員 -->傭金 : 算出
	銷售員 "1"-->"0..*"銷售單據
	月薪員工-->月薪
	工會成員 --> 每周會費
	   
	   class 月薪{
	
	   }
	   class 銷售員{
	int 傭金率
	銷售單據 提交()
	   }
	   class 基本薪水{
	
	   }
	   class 傭金{
	
	   }
	   class 銷售單據{
	date 銷售日期
	int 銷售數量
	   }
	
	      
	```
	``` mermaid
	---
	title: 圖三：時薪員工關係圖
	---
	classDiagram 
	   
	時薪工 --|>員工
	時薪 --|>薪水
	時薪工 -->時薪
	時薪工 "1"-->"0..*"工作出勤卡
	    
	   class 薪水{
	void 減掉(款項)
	   }
	   
	   class 時薪工{
	工作出勤卡 提交()
	   }
	
	   class 工作出勤卡{
	date 工作日期
	int 工作小時
	   }
	   
	
	```
	
	和 Amber 討論後，他說時薪員工不應該可以被扣除款項，所以修改圖二如下：
	
	``` mermaid
	---
	title: 圖二：月薪員工關係圖
	---
	classDiagram 
	   
	   class 薪水{
	void 減掉(款項)
	   }
	   class 款項{
	
	   }
	員工 <|--月薪員工
	員工 <|--銷售員
	月薪員工 <|--工會成員
	月薪 --|>薪水
	基本薪水--|>薪水
	傭金--|>薪水
	每周會費 --|> 款項
	銷售員 -->基本薪水
	銷售員 -->傭金 : 算出
	銷售員 "1"-->"0..*"銷售單據
	月薪員工-->月薪
	工會成員 --> 每周會費
	   
	   class 月薪{
	
	   }
	   class 銷售員{
	int 傭金率
	銷售單據 提交()
	   }
	   class 基本薪水{
	
	   }
	   class 傭金{
	
	   }
	   class 銷售單據{
	date 銷售日期
	int 銷售數量
	   }
	
	```
	然後我們把原本是 系統要做的「算薪水」拿來給員工自己算
	
	``` mermaid
	---
	title: 圖二：月薪員工關係圖
	---
	classDiagram 
	   
	   class 薪水{
	void 減掉(款項)
	   }
	   class 款項{
	
	   }
	
	   class 員工{
	    abstract 算薪水()
	   }
	
	   class 月薪員工{
	    override 算薪水()
	   }
	
	   class 工會成員{
	    override 算薪水()
	   }
	員工 <|--月薪員工
	員工 <|--銷售員
	月薪員工 <|--工會成員
	月薪 --|>薪水
	基本薪水--|>薪水
	傭金--|>薪水
	每周會費 --|> 款項
	
	銷售員 -->基本薪水
	銷售員 -->傭金 : 算出
	銷售員 "1"-->"0..*"銷售單據
	月薪員工-->月薪
	工會成員 --> 每周會費
	   
	   class 月薪{
	
	   }
	   class 銷售員{
	int 傭金率
	銷售單據 提交()
	override 算薪水()
	   }
	   class 基本薪水{
	
	   }
	   class 傭金{
	
	   }
	   class 銷售單據{
	date 銷售日期
	int 銷售數量
	   }
	
	```
	最後加上支付日，以及把圖三時薪員工的部分也加進來，就完成了
	
	``` mermaid
	---
	title: 支付薪水領域模型
	---
	classDiagram 
	    
	   class 薪水{
	void 減掉(款項)
	   }
	   class 款項{
	
	   }
	
	   class 員工{
	    abstract 算薪水()
	    abstract 支付日()
	   }
	
	   class 月薪員工{
	    override 算薪水()
	    override 支付日
	   }
	
	   class 工會成員{
	    override 算薪水()
	    override 支付日
	   }
	員工 <|--月薪員工
	員工 <|--銷售員
	月薪員工 <|--工會成員
	月薪 --|>薪水
	基本薪水--|>薪水
	傭金--|>薪水
	每周會費 --|> 款項
	
	銷售員 -->基本薪水
	銷售員 -->傭金 : 算出
	銷售員 "1"-->"0..*"銷售單據
	月薪員工-->月薪
	工會成員 --> 每周會費
	   
	   class 月薪{
	
	   }
	   class 銷售員{
	int 傭金率
	銷售單據 提交()
	override 算薪水()
	override 支付日
	   }
	   class 基本薪水{
	
	   }
	   class 傭金{
	
	   }
	   class 銷售單據{
	date 銷售日期
	int 銷售數量
	   }
	
	   
	   
	員工 <|--時薪工
	時薪 --|>薪水
	時薪工 -->時薪
	時薪工 "1"-->"0..*"工作出勤卡
	    
	   class 薪水{
	void 減掉(款項)
	   }
	   
	   class 時薪工{
	工作出勤卡 提交()
	override 算薪水()
	override 支付日
	   }
	
	   class 工作出勤卡{
	date 工作日期
	int 工作小時
	   }
	
	```
	接下來的工作，就是把既有的程式碼，進行重構，讓他們符合這個模型。

1. 重構 EmpCore
	
	忘記為什麼要在領域模型上加上Core字尾，或許是為了避免跟命名空間重疊到，但現在我重新把這個類別改成Emp，然後要把裡面的職責劃分開來。

	首先是關於 Emp 資料面的維護，我希望這部分的資訊就聚合在 Emp 這個父類別當中，接下來各種薪水的計算方式，會由子類別進行實作。

	在重構的過程，我發現支付薪水的方法，如果沒有讓子類別自己實作，可能會跑去繼承父類別的方法，這是一個風險，因此我決定使用 specification pattern 來處理這個問題。

	示意圖如下：

	``` mermaid
	---
	title: 支付薪水領域模型
	---
	classDiagram 
	    
	
	月薪員工 <|--工會成員
	工會成員 --> 每周會費
	每周會費 --|> 款項
	工會成員結算方式 --|>薪水結算方式
	工會成員 --> 工會成員結算方式
	
	員工 <|--月薪員工
	月薪結算方式 --|>薪水結算方式
	月薪員工-->月薪結算方式
	
	員工 <|--銷售員
	銷售員結算方式--|>薪水結算方式
	銷售員 -->銷售員結算方式
	銷售員 "1"-->"0..*"銷售單據
	   
	員工 <|--時薪工
	時薪結算方式 --|>薪水結算方式
	時薪工 -->時薪結算方式
	時薪工 "1"-->"0..*"工作出勤卡
	
	   class 薪水結算方式{
	    abstract 算薪水()
	   }
	   class 款項{
	
	   }
	
	   class 員工{
	    abstract date 支付日()
	   }
	
	   class 月薪員工{
	    override date 支付日
	   }
	
	   class 工會成員{
	    override date 支付日
	   }
	   
	   class 銷售員{
	    int 基本薪水
	int 傭金率
	銷售單據 提交()
	override 支付日
	   }
	   class 銷售單據{
	date 銷售日期
	int 銷售數量
	   }
	
	   
	   class 時薪工{
	工作出勤卡 提交()
	override 算薪水()
	override 支付日
	   }
	
	   class 工作出勤卡{
	date 工作日期
	int 工作小時
	   }
	```

	但是，真的有必要嗎？如果需要做到這個程度，應該要考慮的是，這薪水結算方式的變動性、擴充性，或是這規則是否有需要透過額外抽出一個類別集合來凸顯？


### 其他想要做的練習
1. 基本型別依賴