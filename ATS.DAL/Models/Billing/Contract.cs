﻿using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using System;

namespace ATS.DAL.Models.Billing
{
    public class Contract : IContract
    {
        public int Id { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime? ContractCloseDate { get; set; }
        public IUser Client { get; set; }
        public ITerminal Terminal { get; set; }
    }
}