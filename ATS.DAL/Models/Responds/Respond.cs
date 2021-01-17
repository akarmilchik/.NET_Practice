using ATS.DAL.Constants;
using ATS.DAL.Models.Requests;

namespace ATS.DAL.Models.Responds
{
    public class Respond
    {
        public int Id { get; set; }

        public Request Request { get; set; }

        public string SourcePhoneNumber { get; set; }

        public RequestRespondState State { get; set; }
    }
}