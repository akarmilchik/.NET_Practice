using ATEAndBillingSystem.App.Interfaces;

namespace ATEAndBillingSystem.App.Models
{
    public class Terminal : ITerminal
    {
        public PhoneNumber PhoneNumber { get; set; }

        public void Call(string phoneNumber)
        {
            throw new System.NotImplementedException();
        }
    }
}
