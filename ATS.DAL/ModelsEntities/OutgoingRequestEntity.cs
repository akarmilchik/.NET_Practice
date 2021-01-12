using ATS.DAL.Constants;

namespace ATS.DAL.ModelsEntities
{
    public class OutgoingRequestEntity
    {
        public int Id { get; set; }
        public string SourcePhoneNumber { get; set; }
        public string TargetPhoneNumber { get; set; }
        public RequestState State { get; set; }
    }
}