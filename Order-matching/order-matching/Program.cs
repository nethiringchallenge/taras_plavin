using System;

namespace OrderMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            ICommandChecker checker = new CommandChecker();
            ICommandHandler handler = new CommandHandler();

            while (true)
            {
                try
                {
                    var command = Console.ReadLine();

                    if (!checker.CommandCorrect(command))
                    {
                        Console.WriteLine("ERR");
                        continue;
                    }

                    if (command.Contains(Commands.TRADE))
                    {
                        var orders = handler.GetResults();
                        foreach (var order in orders)
                        {
                            Console.WriteLine($"{order.Command} {order.Price} {order.Count}");
                        }
                    }
                    else
                        handler.HandleCommand(command);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERR");
                }
            }
        }
    }
}
