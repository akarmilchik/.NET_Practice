using ATS.App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.App.Responds
{
    public class Respond
    {
        public Requests.Request Request;
        public PhoneNumber Source { get; set; }
        public RespondState State { get; set; }
    }
}
