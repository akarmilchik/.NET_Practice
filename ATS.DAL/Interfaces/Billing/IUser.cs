namespace ATS.DAL.Interfaces.Billing
{
    public interface IUser : IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}