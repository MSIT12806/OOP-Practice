﻿using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.Infrastructure.ORM
{
    public class EmpDbModel
    {
        public string EmpId { get; internal set; }

        public string Name { get; internal set; }

        public string Address { get; internal set; }
    }
}
