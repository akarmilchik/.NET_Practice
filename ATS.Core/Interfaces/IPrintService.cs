﻿using ATS.DAL.Interfaces.Billing;

namespace ATS.Core.Interfaces
{
    public interface IPrintService
    {
        void PrintChooseClient();

        void PrintClientsMenu();

        void PrintEmptyString();
        void PrintExit();
        void PrintIncorrectChoose();

        void PrintMainMenu();

        void PrintStationMenu();

        void PrintWelcome();
    }
}