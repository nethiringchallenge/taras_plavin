using System;
using System.Collections.Generic;
using System.Text;

namespace OrderMatching
{
    public interface ICommandChecker
    {
        bool CommandCorrect(string command);
    }

    public class CommandChecker : ICommandChecker
    {
        public bool CommandCorrect(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                return false;

            var parameters = command.Split(' ');

            if (parameters.Length == 1 && parameters[0].Equals(Commands.LIST, StringComparison.CurrentCulture))
                return true;

            if (parameters.Length != 3)
                return false;

            if (!parameters[0].Equals(Commands.BUY, StringComparison.CurrentCulture) || !parameters[0].Equals(Commands.BUY, StringComparison.CurrentCulture))
                return false;

            bool firstParam = int.TryParse(parameters[1], out int temp1);
            bool secondParam = int.TryParse(parameters[1], out int temp2);

            if (!firstParam || !secondParam)
                return false;

            return true;
        }
    }
}
