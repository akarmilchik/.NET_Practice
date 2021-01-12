﻿using ATS.DAL.Interfaces.Billing;

namespace ATS.DAL.Models.Billing
{
    public class Client : IClient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}