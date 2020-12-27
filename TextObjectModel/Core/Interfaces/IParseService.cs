namespace TextObjectModel.Core.Interfaces
{
    public interface IParseService
    {
        string ClearSentenceStringLine(string stringLine);
        string FindSeparator(string currentString, ref int separatorOccurence);
    }
}
