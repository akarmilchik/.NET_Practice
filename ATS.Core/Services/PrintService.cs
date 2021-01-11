using ATS.Core.Interfaces;
using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using ATS.DAL.Models;
using System;

namespace ATS.Core.Services
{
    public class PrintService : IPrintService
    {
        public void PrintWelcome()
        {
            Console.WriteLine("Welcome to ATS And Billing Station, please choose what you would like to do!");
        }

        public void PrintMainMenu()
        {
            Console.WriteLine("\nActions:\n 1. See all data about clients and tariffs \n 2. Open clients menu \n 3. Open station menu \n 0. Close");
            Console.Write("Input: ");
        }

        public void PrintClientsMenu()
        {
            Console.WriteLine("\nActions:\n 1. See all data about clients and tariffs \n 2. Open clients menu \n 3. Open station menu \n 0. Close");
            Console.Write("Input: ");
        }

        public void PrintStationMenu()
        {
            Console.WriteLine("\nActions:\n 1. Conclude contract with client \n 0. Close");
            Console.Write("Input: ");
        }

        public void PrintEmptyString()
        {
            Console.WriteLine("\nEmpty input string.");
        }

        public void PrintChooseClient()
        {
            Console.Write("\nChoose client number from the list: ");
            Console.Write("Input: ");
        }

        public void PrintIncorrectChoose()
        {
            Console.WriteLine("\n\nPlease choose correct menu item.");
        }

        public void PrintData(IUser user)
        {
            Console.WriteLine($"Client name: {user.FirstName} {user.LastName}");
        }
        public void PrintData(ITerminal terminal)
        {
            Console.WriteLine($"Number: {terminal.PhoneNumber}");
        }

        public void PrintData(IPort port)
        {
            Console.WriteLine($"Port number: {port.Id}. State: {port.PortState}");
        }

        public void PrintData(IContract contract)
        {
            Console.WriteLine($"Contract with client: {contract.Client.FirstName} {contract.Client.LastName}. Duration: from {contract.ContractStartDate.Date} to {contract.ContractCloseDate.Value.Date}.");
        }

        public void PrintData(IStation station)
        {
            Console.WriteLine($"Station: {station.Name}");
        }

        public void PrintData(DataModel data)
        {
            foreach (var station in data.Stations)
            {
                Console.WriteLine($"Station: {station.Name}");
               // Console.WriteLine($"Station: {}");
            }
        }

        public void PrintExit()
        {
            Console.Write("Exit...");
        }

    }
}