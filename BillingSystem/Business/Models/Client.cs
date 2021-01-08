using BillingSystem.Business.Interfaces;
using System;

namespace BillingSystem.Business.Models
{
    public class Client : IUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
