using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderMatching
{
    public interface ICommandHandler
    {
        void HandleCommand(string command);
        IList<Order> GetResults();
    }

    public class CommandHandler : ICommandHandler
    {
        private IDictionary<int, Order> _buys = new SortedDictionary<int, Order>();
        private IDictionary<int, Order> _sells = new SortedDictionary<int, Order>();
        private IList<Order> _trades = new List<Order>();

        public IList<Order> GetResults()
        {
            var res = new List<Order>();
            res.AddRange(_buys.Values);
            res.AddRange(_sells.Values);
            res.AddRange(_trades);
            return res;
        }

        public void HandleCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                throw new ArgumentException("command");

            var parameters = command.Split(' ');

            string order = parameters[0];
            int price = int.Parse(parameters[1]);
            int count = int.Parse(parameters[2]);

            if (order.Equals(Commands.BUY, StringComparison.CurrentCulture))
                HandleBuyOrder(price, count);
            else if (order.Equals(Commands.SELL, StringComparison.CurrentCulture))
                HandleSellOrder(price, count);
            else
                throw new ArgumentException("Invalid command");
        }

        private void HandleBuyOrder(int price, int count)
        {
            HandleOrder(true, _sells, price, count);
        }

        private void HandleSellOrder(int price, int count)
        {
            HandleOrder(false, _buys, price, count);
        }

        private void HandleOrder(bool buy,
            IDictionary<int, Order> opposite,
            int price, 
            int count)
        {
            var cheaperSells = opposite.Keys.Where(x => x < price).ToArray();

            if (cheaperSells.Length == 0)
            {
                if(buy) AddBuyOrder(price, count);
                else AddSellOrder(price, count);
                return;
            }

            foreach (var cheaper in cheaperSells)
            {
                var curr = opposite[cheaper];
                if (curr.Count > count)
                {
                    curr.Count -= count;
                    _trades.Add(new Order(Commands.TRADE, curr.Price, count));
                    return;
                }
                else if (curr.Count < count)
                {
                    count -= curr.Count;
                    opposite.Remove(curr.Price);
                    _trades.Add(new Order(Commands.TRADE, curr.Price, curr.Count));
                }
                else if (curr.Count == count)
                {
                    opposite.Remove(curr.Price);
                    _trades.Add(new Order(Commands.TRADE, curr.Price, curr.Count));
                    return;
                }
            }

            if (count > 0 && buy)
                AddBuyOrder(price, count);
            else if(count > 0 && !buy)
                AddSellOrder(price, count);
        }

        private void AddBuyOrder(int price, int count)
        {
            if (_buys.ContainsKey(price))
                _buys[price].Count += count;
            else
                _buys.Add(price, new Order(Commands.BUY, price, count));
        }

        private void AddSellOrder(int price, int count)
        {
            if (_sells.ContainsKey(price))
                _sells[price].Count += count;
            else
                _sells.Add(price, new Order(Commands.SELL, price, count));
        }
    }
}
