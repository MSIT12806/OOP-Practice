
using System;
using System.Collections.Generic;

namespace Payment.Models.BasicDataMaintenece
{
    public class Employee
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        //[SelectOptionsGetterByMainType(Options = new string[] { })] //// 這個要想辦法找一個機制去生成
        public string PayWay { get; set; }
    }

}
