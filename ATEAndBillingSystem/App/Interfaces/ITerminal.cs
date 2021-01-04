using ATEAndBillingSystem.App.Models;

namespace ATEAndBillingSystem.App.Interfaces
{
    public interface ITerminal
    {
        PhoneNumber PhoneNumber { get; set; }
        void Call(string phoneNumber);
    }
}
