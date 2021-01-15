namespace ATS.DAL.ModelsEntities
{
    public class TerminalEntity
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsOnline { get; set; }
        public int ProvidedPort_Id { get; set; }
    }
}