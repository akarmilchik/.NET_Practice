using System;

namespace ATS.Core.Interfaces
{
    public interface IInputService
    {
        DateTime ReadInputDate();
        int ReadInputKey();
        string ReadInputLine();
    }
}