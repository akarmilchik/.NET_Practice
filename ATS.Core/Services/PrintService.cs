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
            Console.Write("\nChoose terminal number from the list: ");
        }

        public void PrintIncorrectChoose()
        {
            Console.WriteLine("\n\nPlease choose correct menu item.");
        }

        public void PrintData(ITerminal terminal)
        {
            Console.WriteLine($"   Terminal\n      Number: {terminal.PhoneNumber}");
        }

        public void PrintData(IPort port)
        {
            Console.WriteLine($"   Provided port\n      №: {port.Id}\n      State: {port.PortState}");
        }

        public void PrintData(IContract contract)
        {
            Console.WriteLine($"   Contract\n      Start date: {contract.ContractStartDate}\n      Close date: {contract.ContractCloseDate}");
        }

        public void PrintData(ITariffPlan tariffPlan)
        {
            Console.WriteLine($"   Tariff plan\n      Name: {tariffPlan.Name}\n      Minute cost: {tariffPlan.MinuteCost}$");
        }

        public void PrintData(IStation station)
        {
            Console.WriteLine($"Station: {station.Name}");
        }
        public void PrintData(IClient client)
        {
            Console.WriteLine($"Client №{client.Id}: {client.FirstName} {client.LastName}");
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