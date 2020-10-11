using System;

namespace Structural.Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Decorator Pattern!");

            ConcreteComponent concreteComponent = new ConcreteComponent();

            ConcreteDecoratorA concreteDecoratorA = new ConcreteDecoratorA(concreteComponent);
            concreteDecoratorA.Operation();

            AnimalShelter animalShelter = new AnimalShelter();
            animalShelter.Feed("meat");

            AnimalActivityShelter animalActivityShelter = new AnimalActivityShelter(animalShelter);
            animalActivityShelter.Feed("fish");
        }
    }

    #region Structural

    /// <summary>
    /// The 'Component' abstract class
    /// </summary>
    internal abstract class Component
    {
        public abstract void Operation();
    }

    /// <summary>
    /// The 'ConcreteComponent' class
    /// </summary>
    internal class ConcreteComponent : Component
    {
        public override void Operation()
        {
            Console.WriteLine($"{nameof(ConcreteComponent)}.Operation()");
        }
    }

    /// <summary>
    /// The 'Decorator' abstract class
    /// </summary>
    internal abstract class Decorator : Component
    {
        private readonly Component _component;
        protected Decorator(Component component)
        {
            _component = component;
        }

        public override void Operation()
        {
            if (_component != null)
            {
                _component.Operation();
            }
        }
    }

    internal class ConcreteDecoratorA : Decorator
    {
        public ConcreteDecoratorA(Component component) : base(component)
        {
        }

        public override void Operation()
        {
            base.Operation();
            Console.WriteLine($"{nameof(ConcreteDecoratorA)}.Operation()");
        }
    }

    internal class ConcreteDecoratorB : Decorator
    {
        public ConcreteDecoratorB(Component component) : base(component)
        {
        }

        public override void Operation()
        {
            base.Operation();
            AddBehaviour();
            Console.WriteLine($"{nameof(ConcreteDecoratorB)}.Operation()");
        }

        public void AddBehaviour()
        {

        }
    }
    #endregion

    #region Read-World

    /// Component
    internal abstract class Shelter
    {
        public abstract void Feed(string name);
    }

    internal class AnimalShelter : Shelter
    {
        public override void Feed(string name)
        {
            Console.WriteLine($"{nameof(AnimalShelter)} is eating {name}");
        }
    }

    internal abstract class AnimalShelterDecorator : Shelter
    {
        private readonly Shelter _shelter;
        protected AnimalShelterDecorator(Shelter shelter)
        {
            _shelter = shelter;
        }

        public override void Feed(string name)
        {
            if (_shelter != default(Shelter)) _shelter.Feed(name);
        }
    }

    internal class AnimalActivityShelter : AnimalShelterDecorator
    {
        public AnimalActivityShelter(Shelter shelter) : base(shelter)
        {
        }

        public override void Feed(string name)
        {
            RunActivity(10); // add custom behaviour
            base.Feed(name);
            PoolActivity(2);  // add custom behaviour
        }

        public void RunActivity(int km)
        {
            Console.WriteLine($"{nameof(AnimalActivityShelter)}: Animal is running about {km} km");
        }

        public void PoolActivity(int hour)
        {
            Console.WriteLine($"{nameof(AnimalActivityShelter)}: Animal is swimming about {hour} hours");
        }
    }
    
    #endregion
}
