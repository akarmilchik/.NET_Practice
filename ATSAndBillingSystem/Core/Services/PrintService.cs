using ATS.Core.Interfaces;
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

        public void PrintNumberOfSentence()
        {
            Console.WriteLine("\nInput number of sentence for replace words:");
            Console.Write("Input: ");
        }

        public void PrintSuccessSave()
        {
            Console.WriteLine("\n\nText object model successfully saved!");
        }

        public void PrintIncorrectChoose()
        {
            Console.WriteLine("\n\nPlease choose correct menu item.");
        }
        
        public void PrintClient(IUser client)
        {
            

           Console.WriteLine(client.FirstName);
            

          
        }

    }
}
