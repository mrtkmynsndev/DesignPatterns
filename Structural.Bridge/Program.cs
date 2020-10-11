using System;
using System.Collections.Generic;

namespace Structural.Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Structural Patterns!");

            // İstemci tarafından abstraction yapıyı ayırdık. 
            // Artık client tarafını değiştirmeye gerek kalmayacak.
            // Bu sayede SOLID'in Open Closed prensibine de uymuş olduk.

            CustomerBase customer = new Customer();
            customer.DataObject = new CustomerData();
            customer.Add("Mert");
            customer.Add("Melin");

            customer.Next();
            customer.ShowAll();
            customer.Prior();
            customer.ShowAll();
        }
    }

    #region Sample 1
    
    /// <summary>
    /// The 'Abstraction' class
    /// </summary>
    internal abstract class CustomerBase
    {
        public IDataObject<string> DataObject { get; set; }

        public virtual void Next()
        {
            DataObject.Next();
        }

        public virtual void Prior()
        {
            DataObject.Previous();
        }

        public virtual void Add(string name)
        {
            DataObject.Add(name);
        }

        public virtual void Delete(string name)
        {
            DataObject.Delete(name);
        }

        public virtual void ShowAll()
        {
            DataObject.ShowAll();
        }
    }

    /// <summary>
    /// The 'RefinedAbstract' class
    /// </summary>
    internal class Customer : CustomerBase
    {
        public override void ShowAll()
        {
            base.ShowAll();
        }
    }

    /// <summary>
    /// The 'Implementor' interface
    /// </summary>
    internal interface IDataObject<T>
    {
        void Next();
        void Previous();
        void Add(T model);
        void Delete(T model);
        T GetCurrent();
        void ShowAll();
    }

    /// <summary>
    /// The 'ConcreteImplementor' class
    /// </summary>
    class CustomerData : IDataObject<string>
    {
        int current = 0;
        List<string> customers = new List<string>();

        public void Add(string model)
        {
            customers.Add(model);
        }

        public void Delete(string model)
        {
            customers.Remove(model);
        }

        public string GetCurrent()
        {
            return customers[current];
        }

        public void Next()
        {
            if (current < customers.Count)
            {
                current++;
            }
        }

        public void Previous()
        {
            if (current > 0)
            {
                current--;
            }
        }

        public void ShowAll()
        {
            customers.ForEach(customer =>
               Console.WriteLine(" " + customer));
        }
    }
    
    #endregion
}
