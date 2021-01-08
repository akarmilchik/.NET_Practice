using ATS.App.Constants;
using ATS.App.Requests;

namespace ATS.App.Responds
{
    public class Respond
    {
        public Request Request;
        public string SourcePhoneNumber { get; set; }
        public RespondState State { get; set; }
    }
}
