using System;

namespace Behavioral.ChainOfResponsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Chain of Responsibility!");

            Approver mert = new Director();
            Approver hulya = new VicePresident();
            Approver munir = new President();

            mert.Successor = hulya;
            hulya.Successor = munir;


            // Generate and process purchase requests
            var purchase = new Purchase { Number = 2034, Amount = 350.00, Purpose = "Supplies" };
            mert.ProcessRequest(purchase);

            purchase = new Purchase { Number = 2035, Amount = 32590.10, Purpose = "Project X" };
            mert.ProcessRequest(purchase);

            purchase = new Purchase { Number = 2036, Amount = 122100.00, Purpose = "Project Y" };
            mert.ProcessRequest(purchase);

            Console.WriteLine("***************** Overview *****************");

            Worker softwareWorker = new SoftwareWorker();
            Worker dbWorker = new DBWorker();
            Worker productWorker = new ProductWorker();

            productWorker.Successor = softwareWorker;
            softwareWorker.Successor = dbWorker;

            productWorker.ProcessRequest(WorkerType.Software);
            productWorker.ProcessRequest(WorkerType.DB);

        }
    }

    #region .Net Optimized

    public class Purchase
    {
        public double Amount { get; set; }
        public string Purpose { get; set; }
        public int Number { get; set; }
    }

    // Purchase event argument holds purchase info
    internal class PurchaseEventArgs : EventArgs
    {
        public Purchase Purchase { get; set; }
    }


    /// <summary>
    /// The 'Handler' abstract class
    /// </summary>
    internal abstract class Approver
    {
        public EventHandler<PurchaseEventArgs> Purchase;

        protected Approver()
        {
            Purchase += PurchaseHandler;
        }

        public abstract void PurchaseHandler(object sender, PurchaseEventArgs eventArgs);

        public void ProcessRequest(Purchase purchase)
        {
            OnPurchase(this, new PurchaseEventArgs() { Purchase = purchase });
        }

        public virtual void OnPurchase(object sender, PurchaseEventArgs eventArgs)
        {
            if (Purchase != null)
                Purchase(this, eventArgs);
        }

        public Approver Successor { get; set; }
    }

    /// <summary>
    /// The 'ConcreteHandler' class
    /// </summary>
    internal class Director : Approver
    {
        public override void PurchaseHandler(object sender, PurchaseEventArgs eventArgs)
        {
            if (eventArgs.Purchase.Amount < 10000.0)
            {
                Console.WriteLine($"{this.GetType().Name} aproved for request #{eventArgs.Purchase.Number}");
            }
            else if (Successor != null)
            {
                Successor.PurchaseHandler(sender, eventArgs);
            }
        }
    }

    /// <summary>
    /// The 'ConcreteHandler' class
    /// </summary>
    internal class VicePresident : Approver
    {
        public override void PurchaseHandler(object sender, PurchaseEventArgs eventArgs)
        {
            if (eventArgs.Purchase.Amount < 25000.0)
            {
                Console.WriteLine($"{this.GetType().Name} aproved for request #{eventArgs.Purchase.Number}");
            }
            else if (Successor != null)
            {
                Successor.PurchaseHandler(sender, eventArgs);
            }
        }
    }

    /// <summary>
    /// The 'ConcreteHandler' clas
    /// </summary>
    internal class President : Approver
    {
        public override void PurchaseHandler(object sender, PurchaseEventArgs eventArgs)
        {
            if (eventArgs.Purchase.Amount < 100000.0)
            {
                Console.WriteLine($"{this.GetType().Name} aproved for request #{eventArgs.Purchase.Number}");
            }
            else if (Successor != null)
            {
                Successor.PurchaseHandler(sender, eventArgs);
            }
            else
            {

                Console.WriteLine(
                    "Request# {0} requires an executive meeting!",
                    eventArgs.Purchase.Number);
            }
        }
    }
    #endregion

    #region Overview

    public enum WorkerType
    {
        Software,
        DB,
        Product
    }

    public class WorkerEventArgs : EventArgs
    {
        public WorkerType WorkerType { get; set; }
    }

    public abstract class Worker
    {
        public EventHandler<WorkerEventArgs> Business;

        protected Worker()
        {
            Business += BusinessHandler;
        }

        public abstract void BusinessHandler(object sender, WorkerEventArgs args);

        public void ProcessRequest(WorkerType workerType)
        {
            OnBusiness(this, new WorkerEventArgs()
            {
                WorkerType = workerType
            });
        }

        protected virtual void OnBusiness(object sender, WorkerEventArgs args)
        {
            if (Business != null)
                Business(sender, args);
        }

        public Worker Successor { get; set; }
    }

    public class SoftwareWorker : Worker
    {
        public override void BusinessHandler(object sender, WorkerEventArgs args)
        {
            if (args == default(WorkerEventArgs))
                throw new ArgumentException();

            if (args.WorkerType == WorkerType.Software)
            {
                Console.WriteLine("This work for software and I handled it :)");
            }
            else if (Successor != default(SoftwareWorker))
            {
                Successor.BusinessHandler(sender, args);
            }
        }
    }

    public class DBWorker : Worker
    {
        public override void BusinessHandler(object sender, WorkerEventArgs args)
        {
            if (args == default(WorkerEventArgs))
                throw new ArgumentException();

            if (args.WorkerType == WorkerType.DB)
            {
                Console.WriteLine("This work for DB and I handled it :)");
            }
            else if (Successor != default(DBWorker))
            {
                Successor.BusinessHandler(sender, args);
            }
        }
    }

    public class ProductWorker : Worker
    {
        public override void BusinessHandler(object sender, WorkerEventArgs args)
        {
            if (args == default(WorkerEventArgs))
                throw new ArgumentException();

            if (args.WorkerType == WorkerType.Product)
            {
                Console.WriteLine("This work for Product and I handled it :)");
            }
            else if (Successor != default(ProductWorker))
            {
                Successor.BusinessHandler(sender, args);
            }
            else
            {
                Console.WriteLine("I am final worker :)");
            }
        }
    }

    #endregion
}
