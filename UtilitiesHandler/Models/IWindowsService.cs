using System;
using System.Collections.Generic;

namespace UtilitiesHandler
{
    public interface IWindowsService
    {
        void ShowWindow();
        void HideWindow();
        void ExitApp();
        void AddMessageToLogger(string message);
    }
}