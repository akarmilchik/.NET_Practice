namespace BillingSystem.Business.Interfaces
{
    public interface IUser: IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
