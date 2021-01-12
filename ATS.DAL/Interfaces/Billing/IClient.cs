namespace ATS.DAL.Interfaces.Billing
{
    public interface IClient : IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}