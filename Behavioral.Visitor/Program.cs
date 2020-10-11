using System;
using System.Collections.Generic;

namespace Behavioral.Visitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Visitor Pattern!");

            Car car = new Car();
            car.Add(new Body());
            car.Add(new Wheel());
            car.Add(new Engine());

            var visitors = new List<ICarElementVisitor>() { new PrintCarElementVisitor(), new MaintainCarElementVisitor() };

            foreach (var visitor in visitors)
            {
                car.Accept(visitor);
            }

        }

        #region .Net Optimized

        /// <summary>
        /// A 'Element' interface
        /// </summary>
        internal interface ICarElement
        {
            void Accept(ICarElementVisitor visitor);
        }

        /// <summary>
        /// A 'Visitor' interface
        /// </summary>
        internal interface ICarElementVisitor
        {
            void Visit(Body body);
            void Visit(Wheel wheel);
            void Visit(Engine engine);
        }


        /// <summary>
        /// A 'Element' class
        /// </summary>
        internal class Body : ICarElement
        {
            public void Accept(ICarElementVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        /// <summary>
        /// A 'Element' class
        /// </summary>
        internal class Wheel : ICarElement
        {
            public void Accept(ICarElementVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        /// <summary>
        /// A 'Element' class
        /// </summary>
        internal class Engine : ICarElement
        {
            public void Accept(ICarElementVisitor visitor)
            {
                visitor.Visit(this);
            }
        }


        /// <summary>
        /// A 'Object Structures' class
        /// </summary>
        internal class Car
        {
            private List<ICarElement> _cars;

            public Car()
            {
                _cars = new List<ICarElement>();
            }

            public void Add(ICarElement carElement)
            {
                _cars.Add(carElement);
            }

            public void Remove(ICarElement carElement)
            {
                _cars.Remove(carElement);
            }

            public void Accept(ICarElementVisitor visitor)
            {
                foreach (var car in _cars)
                {
                    car.Accept(visitor);
                }
            }
        }

        /// <summary>
        /// A 'Concrete Visitor' class
        /// </summary>
        internal class MaintainCarElementVisitor : ICarElementVisitor
        {
            public void Visit(Body body)
            {
                Console.WriteLine($"{body.GetType().Name} is maintaned");
            }

            public void Visit(Wheel wheel)
            {
                Console.WriteLine($"{wheel.GetType().Name} is maintaned");
            }

            public void Visit(Engine engine)
            {
                Console.WriteLine($"{engine.GetType().Name} is maintaned");
            }
        }

        /// <summary>
        /// A 'Concrete Visitor' class
        /// </summary>
        internal class PrintCarElementVisitor : ICarElementVisitor
        {
            public void Visit(Body body)
            {
                Console.WriteLine($"{body.GetType().Name} is printed");
            }

            public void Visit(Wheel wheel)
            {
                Console.WriteLine($"{wheel.GetType().Name} is printed");
            }

            public void Visit(Engine engine)
            {
                Console.WriteLine($"{engine.GetType().Name} is printed");
            }
        }
        #endregion
    }
}
