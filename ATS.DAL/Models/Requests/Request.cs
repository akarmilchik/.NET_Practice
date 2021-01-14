using ATS.DAL.Constants;

namespace ATS.DAL.Models.Requests
{
    public class Request
    {
        public int Id { get; set; }
        public string SourcePhoneNumber { get; set; }
        public RequestRespondState State { get; set; }
    }
}