using System;

namespace OrderMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            ICommandChecker checker = new CommandChecker();

            while (true)
            {
                var command = Console.ReadLine();

                if (!checker.CommandCorrect(command))
                {
                    Console.WriteLine("ERR");
                    continue;l
                }
            }
        }
    }
}
