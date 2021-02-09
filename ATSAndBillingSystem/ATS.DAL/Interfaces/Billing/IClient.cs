namespace ATS.DAL.Interfaces.Billing
{
    public interface IClient
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}