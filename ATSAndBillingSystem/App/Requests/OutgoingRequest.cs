using ATS.App.Models;

namespace ATS.App.Requests
{
    public class OutgoingRequest : Request
    {
        public PhoneNumber Target { get; set; }
    }
}
