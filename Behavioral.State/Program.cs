using System;
using System.Collections.Generic;

namespace Behavioral.State
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Behavioral State!");

            SnackMachineContext context = new SnackMachineContext();
            context.Request("Chocalate", 20);

            Console.ReadLine();
        }
    }

    #region .Net Optimized
    
    /// <summary>
    /// The 'Snack' class
    /// </summary>
    internal class Snack
    {
        public string Name { get; set; }
        public int Total { get; set; }
        public decimal Price { get; set; }
    }

    /// <summary>
    /// The 'State' abstract class
    /// </summary>
    internal abstract class SnackMachineState
    {
        abstract public void HandleState(SnackMachineContext snackMachineContext);
    }

    /// <summary>
    /// The 'Concrete Sate' class
    /// </summary>
    internal class InitializeState : SnackMachineState
    {
        public InitializeState()
        {
            Console.WriteLine("Initialize State");
        }

        public override void HandleState(SnackMachineContext snackMachineContext)
        {
            Console.WriteLine("SnackMachine is initialized");
            System.Threading.Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// The 'Concrete Sate' class
    /// </summary>
    internal class CoinInsertedState : SnackMachineState
    {
        public CoinInsertedState()
        {
            Console.WriteLine("Coin Inserted State");
        }

        public override void HandleState(SnackMachineContext snackMachineContext)
        {
            Console.WriteLine("Coin inserted: The coin is verifying");
            System.Threading.Thread.Sleep(1000);

            snackMachineContext.State = new PreparingState();
        }
    }

    /// <summary>
    /// The 'Concrete Sate' class
    /// </summary>
    internal class PreparingState : SnackMachineState
    {
        public PreparingState()
        {
            Console.WriteLine("Preparing State");
        }

        public override void HandleState(SnackMachineContext snackMachineContext)
        {
            Console.WriteLine("Snack is prepering...");
            System.Threading.Thread.Sleep(1000);

            snackMachineContext.State = new DeliveryState();
        }
    }

    /// <summary>
    /// The 'Concrete Sate' class
    /// </summary>
    internal class DeliveryState : SnackMachineState
    {
        public DeliveryState()
        {
            Console.WriteLine("Delivery State");
        }

        public override void HandleState(SnackMachineContext snackMachineContext)
        {
            Console.WriteLine("Snack is check in...");
            System.Threading.Thread.Sleep(1000);

            snackMachineContext.State = new InitializeState();
        }
    }

    /// <summary>
    /// The 'Context' class
    /// </summary>
    internal class SnackMachineContext
    {
        private readonly List<Snack> _snacks;
        private SnackMachineState _state;

        public SnackMachineContext()
        {
            _snacks = new List<Snack>() {
                new Snack(){Name = "Chocalate", Price = 1, Total = 10},
                new Snack(){Name = "Cips", Price = 2, Total = 10},
                new Snack(){Name = "Water", Price = 1, Total = 10}
            };
        }

        public void Request(string name, decimal money)
        {
            Console.WriteLine($"Snack request is came. Requested snack name: {name} Inserted coin: {money}");

            var avaliableSnack = _snacks.Find(x => x.Name == name && x.Price <= money && x.Total >= 1);

            if (avaliableSnack != default)
            {
                avaliableSnack.Total--;

                State = new CoinInsertedState();
            }
            else
            {
                State = new InitializeState();
            }
        }

        public SnackMachineState State
        {
            get => _state;
            set
            {
                _state = value;

                _state.HandleState(this);
            }
        }
    }
    #endregion
}
