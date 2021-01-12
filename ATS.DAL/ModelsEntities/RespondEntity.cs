using ATS.DAL.Constants;

namespace ATS.DAL.ModelsEntities
{
    public class RespondEntity
    {
        public int Id { get; set; }
        public int Request_Id { get; set; }
        public string SourcePhoneNumber { get; set; }
        public RespondState State { get; set; }
    }
}