using ATS.DAL.Constants;
using ATS.DAL.Models.Requests;

namespace ATS.DAL.Models.Responds
{
    public class Respond
    {
        public Request Request;
        public string SourcePhoneNumber { get; set; }
        public RespondState State { get; set; }
    }
}