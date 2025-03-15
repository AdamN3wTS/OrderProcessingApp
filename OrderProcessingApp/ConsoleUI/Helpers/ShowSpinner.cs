using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingApp.ConsoleUI.Helpers
{
    public static class SpinnerHelper
    {
        public static void ShowSpinner()
        {
            char[] spinnerChars = { '|', '/', '-', '\\' };
            int spinnerIndex = 0;
            DateTime endTime = DateTime.Now.AddMilliseconds(5000);

            while (DateTime.Now < endTime)
            {
                Console.Write(spinnerChars[spinnerIndex]);
                spinnerIndex = (spinnerIndex + 1) % spinnerChars.Length;
                Thread.Sleep(100);
                Console.Write("\b");
            }
        }
    }
}
