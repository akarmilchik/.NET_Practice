using ATS.App.Requests;
using System;

namespace ATS.App.Models
{
    public class MainTerminal : Terminal
    {
        public MainTerminal(string phoneNumber) : base(phoneNumber)
        {
            this.IncomingRequest += this.OnIncomingRequest;
            this.Online += (sender, args) => { Console.WriteLine("Terminal {0} turned to online mode", phoneNumber); };
            this.Offline += (sender, args) => { Console.WriteLine("Terminal {0} turned to offline mode", phoneNumber); };
        }

        protected override void OnIncomingRequest(object sender, IncomingRequest request)
        {
            base.OnIncomingRequest(sender, request);
            Console.WriteLine("{0} received request for incoming connection from {1}", this.PhoneNumber, request.SourcePhoneNumber);
        }
    }
}
