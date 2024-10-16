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
	
	另外就是 ChgEmp 如果傳入的 empId 是無效的，就應該回傳錯誤畫面