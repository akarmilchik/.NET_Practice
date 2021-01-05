using ATS.App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.App.Requests
{
    public abstract class Request
    {
        public PhoneNumber Source { get; set; }
    }
}
