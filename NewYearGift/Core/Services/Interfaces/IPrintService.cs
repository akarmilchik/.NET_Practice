using NewYearGift.App.Models.Sweets;
using NewYearGift.Models.Gifts;
using System.Collections.Generic;

namespace NewYearGift.Core.Services.Interfaces
{
    public interface IPrintService
    {
        void PrintMainMenu();
        void PrintSweetParametersMenu();
        void PrintSweetRangeParametersMenu();
        void PrintStartRangeText();
        void PrintEndRangeText();
        void PrintInputText();
        void PrintChoosePresenteeMenu();
        void PrintSortingMenu();
        void PrintSweetsMenu();
        void PrintGiftWeight(int weight);
        void PrintGift(Gift gift);
        void PrintSweets(List<Sweet> sweets);
    }
}
