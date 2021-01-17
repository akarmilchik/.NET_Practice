using ATS.Core.Interfaces;
using ATS.Core.Services;
using ATS.DAL;
using ATS.DAL.Constants;
using ATS.Helpers;
using System.Linq;

namespace ATS
{
    public class Program
    {
        public static IPrintService printService = new PrintService();

        public static IInputService inputService = new InputService();

        public static IDataService dataService;

        public static MainMenuItems mainMenuItems;

        public static IMainMenuService mainMenuService;

        public static DataContext context;

        static void Main()
        {
            bool isWorking = true;

            CreateContext();

            InitDataBase();

            InitDataService(context);

            InitMenuService();

            while (isWorking)
            {
                printService.PrintWelcome();

                printService.PrintMainMenu();

                var selectedMenuItemId = inputService.ReadInputKey();

                mainMenuItems = (MainMenuItems)selectedMenuItemId;

                if (selectedMenuItemId >= 0)
                {
                    switch (mainMenuItems)
                    {
                        case MainMenuItems.CloseApp:
                            isWorking = false;
                            printService.PrintExit();
                            break;

                        case MainMenuItems.PrintClientsData:
                            printService.PrintLine();
                            dataService.GetClients().ToList().ForEach(c => printService.PrintItemValue(c.ToString()));
                            break;

                        case MainMenuItems.OpenStationMenu:
                            mainMenuService.StationMenuHandler();
                            break;

                        case MainMenuItems.OpenClientMenu:
                            mainMenuService.ClientMenuHandler();
                            break;

                        default:
                            printService.PrintIncorrectChoose();
                            break;
                    }
                }
            }
        }

        public static void InitDataService(DataContext context)
        {
            dataService = new DataService(context);
        }

        public static void InitMenuService()
        {
            mainMenuService = new MainMenuService(printService, inputService, dataService);
        }

        public static void CreateContext()
        {
            var contextFactory = new DataContextFactory();

            context = contextFactory.CreateDbContext(null);
        }

        public static void InitDataBase()
        {
            context.Database.EnsureCreated();

            InitData.InitializeData(context);
        }
    }
}