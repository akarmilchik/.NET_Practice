namespace ATS.Core.Extensions
{
    public static class TypeConversionExtension
    {
        public static int ToInt(this char inputChar)
        {
            if (int.TryParse(inputChar.ToString(), out var resultInt))
            {
                return resultInt;
            }

            resultInt = -1;
            return resultInt;
        }
    }
}