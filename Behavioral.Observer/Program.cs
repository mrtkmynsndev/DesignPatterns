using System;
using System.Collections.Generic;

namespace Behavioral.Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Observer Pattern!");

            var ibmStock = new IBM("IBM", 10.50m);

            var appleStock = new Apple("Apple", 20.90m);

            ibmStock.Attach(new Investor("Me"));

            appleStock.Attach(new Investor("You"));

            for (int i = 0; i < 10; i++)
            {
                ibmStock.Value = ibmStock.Value + i;
                appleStock.Value = ibmStock.Value + i;
            }
        }
    }



    /// <summary>
    /// The 'Subject' abstract class
    /// </summary>
    internal abstract class Stock
    {
        private decimal _value;
        private string _symbol;

        private List<InvestorBase> _investors = new List<InvestorBase>();

        protected Stock(decimal value, string symbol)
        {
            _value = value;
            _symbol = symbol;
        }

        public decimal Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    Notify();
                }
            }
        }

        public string Symbol => _symbol;

        public void Attach(InvestorBase investor)
        {
            this._investors.Add(investor);
        }

        public void Detach(InvestorBase investor)
        {
            this._investors.Remove(investor);
        }

        public void Notify()
        {
            _investors.ForEach(x => x.Update(this));
        }
    }

    /// <summary>
    /// The 'Subject' concrete class
    /// </summary>
    internal class IBM : Stock
    {
        public IBM(string symbol, decimal value) : base(value, symbol)
        {
        }
    }

    /// <summary>
    /// The 'Subject' concrete class
    /// </summary>
    internal class Apple : Stock
    {
        public Apple(string symbol, decimal value) : base(value, symbol)
        {
        }
    }

    /// <summary>
    /// The 'Observer' abstract class
    /// </summary>
    internal abstract class InvestorBase
    {
        public abstract void Update(Stock stock);
    }

    internal class Investor : InvestorBase
    {
        string name;

        public Investor(string name)
        {
            this.name = name;
        }

        public override void Update(Stock stock)
        {
            Console.WriteLine($"{name}, {stock.Symbol} stock changed to {stock.Value}");
        }
    }
}
