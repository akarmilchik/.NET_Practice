﻿using ATS.Core.Interfaces;
using System;

namespace ATS.Core.Services
{
    public class PrintService : IPrintService
    {
        public void PrintWelcome()
        {
            Console.Title = "ATS and Billing system";
            Console.Write("Welcome to ATS And Billing system, please choose what you would like to do!");
        }

        public void PrintMainMenu()
        {
            Console.WriteLine("Actions:\n 1. Show all clients \n 2. Open station menu \n 3. Open clients menu \n 0. Close");
            Console.Write("Input: ");
        }

        public void PrintClientsMenu()
        {
            Console.WriteLine("\n\nClients menu\nActions:\n 1. Show current client \n 2. Choose client as current \n 3. Connect to station port \n 4. Disconnect from station port \n 5. Make call to another client \n 6. Drop outgoing call \n 7. Drop an incoming call \n 8. Answer an incoming call \n 9. Show calls report \n 0. Back to main menu ");
            Console.Write("Input: ");
        }

        public void PrintStationMenu()
        {
            Console.WriteLine("\n\nStation menu\nActions:\n 1. Conclude contract with client \n 2. Show all concluded contracts\n 0. Back to main menu");
            Console.Write("Input: ");
        }

        public void PrintChooseProposal(string value)
        {
            Console.Write($"\nChoose {value} from the list: ");
        }

        public void PrintItemValue(string value)
        {
            Console.WriteLine(value);
        }

        public void PrintTotalCost(decimal value)
        {
            Console.WriteLine($"Total amount of charges for period: {value}$");
        }

        public void PrintClientSelected()
        {
            Console.WriteLine("Client selected!");
        }

        public void PrintInputStartDate(string value)
        {
            Console.Write($"\n\nInput the start date of the {value} in format (dd.mm.yy): ");
        }

        public void PrintInputCloseDate(string value)
        {
            Console.Write($"\n\nInput the closing date of the {value} in format (dd.mm.yy): ");
        }

        public void ContractConcluded()
        {
            Console.WriteLine("Contract was concluded!");
        }

        public void PrintInputProposal()
        {
            Console.Write("Input: ");
        }

        public void PrintIncorrectChoose()
        {
            Console.WriteLine("\n\nPlease input correct number.");
        }

        public void PrintSuccessConnect()
        {
            Console.WriteLine("Succesfully connected terminal to port!");
        }

        public void PrintSuccessDisonnect()
        {
            Console.WriteLine("Succesfully disconnected terminal from port!");
        }

        public void PrintCallState(string state)
        {
            Console.WriteLine($"Call successfully {state}!");
        }

        public void PrintLine()
        {
            Console.WriteLine(Environment.NewLine);
        }

        public void PrintExit()
        {
            Console.Write("Exit...");
        }
    }
}