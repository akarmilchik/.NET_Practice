using ATS.Core.Interfaces;
using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using ATS.DAL.Models;
using System;
using System.Collections.Generic;

namespace ATS.Core.Services
{
    public class PrintService : IPrintService
    {
        public void PrintWelcome()
        {
            Console.Title = "ATS and Billing system";
            Console.WriteLine("\n\nWelcome to ATS And Billing system, please choose what you would like to do!");
        }

        public void PrintMainMenu()
        {
            Console.WriteLine("\nActions:\n 1. See all data about clients and tariffs \n 2. Open station menu \n 3. Open clients menu \n 0. Close");
            Console.Write("Input: ");
        }

        public void PrintClientsMenu()
        {
            Console.WriteLine("\n\nActions:\n 1. Show current client \n 2. Choose client as current \n 3. Connect to station port \n 4. Disconnect from station port \n 5. Make call to another client \n 6. Drop an incoming call \n 7. Answer an incoming call \n 8. Show calls report \n 0. Back to main menu ");
            Console.Write("Input: ");
        }

        public void PrintStationMenu()
        {
            Console.WriteLine("\n\nActions:\n 1. Conclude contract with client \n 0. Back to main menu");
            Console.Write("Input: ");
        }

        public void PrintChooseClient()
        {
            Console.Write("\nChoose client number from the list: ");
            Console.Write("Input: ");
        }

        public void PrintInputProposal()
        {
            Console.Write("Input: ");
        }

        public void PrintChooseTerminalToCall()
        {
            Console.WriteLine("\nChoose terminal number from the list: ");
        }

        public void PrintChooseClientToConcludeContract()
        {
            Console.WriteLine("\nChoose client to coclude a contract: ");
        }

        public void PrintIncorrectChoose()
        {
            Console.WriteLine("\n\nPlease input correct number.");
        }

        public void PrintDataArray(IEnumerable<IClient> clients)
        {
            foreach (var client in clients)
            {
                Console.WriteLine($"Client №{client.Id}: {client.FirstName} {client.LastName}");
            }
        }

        public void PrintDataArray(IEnumerable<ITerminal> terminals)
        {
            foreach (var terminal in terminals)
            {
                Console.WriteLine($"Terminal №{terminal.Id}. Number:{terminal.PhoneNumber}");
            }
        }

        public void PrintExit()
        {
            Console.Write("Exit...");
        }
    }
}