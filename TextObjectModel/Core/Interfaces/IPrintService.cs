using TextObjectModel.App.Interfaces;
using TextObjectModel.App.Models;

namespace TextObjectModel.Core.Interfaces
{
    public interface IPrintService
    {
        void PrintConsonantLetterToDeleteWords();
        void PrintIncorrectChoose();
        void PrintInputWordsLength();
        void PrintMainMenu();
        void PrintNumberOfSentence();
        void PrintSentence(ISentence sentence);
        void PrintSentenceItem(ISentenceItem item);
        void PrintSentences(Text data);
        void PrintSentencesByOrderOfWords(Text data);
        void PrintSortingMenu();
        void PrintSubstringToReplaceWords();
        void PrintWelcome();
        void PrintWrongInput();
    }
}
