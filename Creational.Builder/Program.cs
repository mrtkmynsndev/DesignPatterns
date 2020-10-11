using System;
using System.Collections.Generic;

namespace Creational.Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Builder Pattern!");

            var builders = new List<VehicleBuilder>() { new MotoCycleBuilder(), new CarBuilder(), new ScooterBuilder() };

            foreach (var builder in builders)
            {
                var shop = new Shop(builder);
                shop.Construct();
                shop.Show();
            }

            // Another usage
            var pizzaa = new PizzaBuilder("Mixed", "xl").WithOptionalOil("oil").WithOptionalCheese("cheese").Build();

            // Overview

            Console.WriteLine("******************** END ********************");

            var villaBuilder = new VillaConstructorBuilder();
            var architectOffice = new ArchitectOffice(villaBuilder);
            architectOffice.Construct();
            architectOffice.Show();
        }
    }

    #region Vehicle
    /// <summary>
    /// The 'Director' class
    /// </summary>
    internal class Shop
    {
        private readonly VehicleBuilder _builder;

        public Shop(VehicleBuilder builder)
        {
            _builder = builder;
        }

        public void Construct()
        {
            _builder.BuildFrame();
            _builder.BuildEngine();
            _builder.BuildWheels();
            _builder.BuildDoors();
        }

        public void Show()
        {
            Console.WriteLine(_builder.Vehicle);
        }
    }

    /// <summary>
    /// The 'Builder' abstract class
    /// </summary>
    internal abstract class VehicleBuilder
    {
        public VehicleBuilder(VehicleType type)
        {
            Vehicle = new Vehicle(type);
        }
        public Vehicle Vehicle { get; private set; }

        abstract public void BuildFrame();
        abstract public void BuildEngine();
        abstract public void BuildWheels();
        abstract public void BuildDoors();
    }

    /// <summary>
    /// The 'Builder' concrete class
    /// </summary>
    internal class MotoCycleBuilder : VehicleBuilder
    {
        public MotoCycleBuilder() : base(VehicleType.Motor)
        {
        }

        public override void BuildDoors()
        {
            Vehicle[PartType.Doors] = "0";
        }

        public override void BuildEngine()
        {
            Vehicle[PartType.Engine] = "1200 cc";
        }

        public override void BuildFrame()
        {
            Vehicle[PartType.Frame] = "MotoCycle Frame";
        }

        public override void BuildWheels()
        {
            Vehicle[PartType.Wheels] = "2";
        }
    }

    /// <summary>
    /// The 'Builder' concrete class
    /// </summary>
    internal class CarBuilder : VehicleBuilder
    {
        public CarBuilder() : base(VehicleType.Car)
        {
        }

        public override void BuildDoors()
        {
            Vehicle[PartType.Doors] = "4";
        }

        public override void BuildEngine()
        {
            Vehicle[PartType.Engine] = "5000 cc";
        }

        public override void BuildFrame()
        {
            Vehicle[PartType.Frame] = "Car Frame";
        }

        public override void BuildWheels()
        {
            Vehicle[PartType.Wheels] = "4";
        }
    }

    /// <summary>
    /// The 'Builder' concrete class
    /// </summary>
    internal class ScooterBuilder : VehicleBuilder
    {
        public ScooterBuilder() : base(VehicleType.Scooter)
        {
        }

        public override void BuildDoors()
        {
            Vehicle[PartType.Doors] = "0";
        }

        public override void BuildEngine()
        {
            Vehicle[PartType.Engine] = "500 cc";
        }

        public override void BuildFrame()
        {
            Vehicle[PartType.Frame] = "Scooter Frame";
        }

        public override void BuildWheels()
        {
            Vehicle[PartType.Wheels] = "2";
        }
    }

    internal class Vehicle
    {
        private readonly VehicleType _type;
        private Dictionary<PartType, string> _parts = new Dictionary<PartType, string>();

        public Vehicle(VehicleType type)
        {
            _type = type;
        }

        public string this[PartType partType]
        {
            get => _parts[partType];
            set => _parts[partType] = value;
        }

        public override string ToString()
        {
            return $"Vehicle Type: {_type} \n Frame: {_parts[PartType.Frame]} \n Engine: {_parts[PartType.Engine]} \n " +
            $"Wheels: {_parts[PartType.Wheels]} \n Doors: {_parts[PartType.Doors]}";
        }
    }

    internal enum VehicleType
    {
        Motor,
        Scooter,
        Car
    }

    internal enum PartType
    {
        Frame,
        Engine,
        Wheels,
        Doors
    }
    #endregion

    #region Pizza

    internal class Pizzaa
    {
        private readonly string _name;
        private readonly string _cheese;
        private readonly string _oil;
        private readonly string _size;

        // public Pizzaa(string name, string cheese, string oil, string size)
        // {
        //     _size = size;
        //     _oil = oil;
        //     _cheese = cheese;
        //     _name = name;
        // }

        public Pizzaa(PizzaBuilder builder)
        {
            _size = builder._size;
            _oil = builder._oil;
            _cheese = builder._cheese;
            _name = builder._name;
        }
    }

    internal class PizzaBuilder
    {
        public string _name;
        public string _cheese;
        public string _oil;
        public string _size;

        public PizzaBuilder(string name, string size)
        {
            _name = name;
            _size = size;
        }

        public PizzaBuilder WithOptionalCheese(string cheese)
        {
            _cheese = cheese;
            return this;
        }

        public PizzaBuilder WithOptionalOil(string oil)
        {
            _oil = oil;
            return this;
        }

        public Pizzaa Build()
        {
            return new Pizzaa(this);
        }
    }
    #endregion

    #region Constructor
    
    public abstract class ConstructorBuilder
    {
        private ConstructorType _constructorType;

        protected ConstructorBuilder(ConstructorType constructorType)
        {
            Constructor = new Constructor(constructorType);
        }

        public Constructor Constructor { get; private set; }

        public abstract void BuildWindow();
        public abstract void BuildFrame();
        public abstract void BuildDoor();
    }

    public class VillaConstructorBuilder : ConstructorBuilder
    {
        public VillaConstructorBuilder() : base(ConstructorType.Villa)
        {
        }

        public override void BuildDoor()
        {
            Constructor[ConstructorPartType.Door] = "5 Door";
        }

        public override void BuildFrame()
        {
            Constructor[ConstructorPartType.Frame] = "One Frame";
        }

        public override void BuildWindow()
        {
            Constructor[ConstructorPartType.Window] = "10 Window";
        }
    }


    // Director class
    public class ArchitectOffice
    {
        private readonly ConstructorBuilder constructorBuilder;

        public ArchitectOffice(ConstructorBuilder constructorBuilder)
        {
            this.constructorBuilder = constructorBuilder;
        }

        public void Construct()
        {
            this.constructorBuilder.BuildFrame();
            this.constructorBuilder.BuildDoor();
            this.constructorBuilder.BuildWindow();
        }

        public void Show()
        {
            System.Console.WriteLine(constructorBuilder.Constructor);
        }
    }

    public class Constructor
    {
        private readonly Dictionary<ConstructorPartType, string> _parts = new Dictionary<ConstructorPartType, string>();
        private readonly ConstructorType constructorType;

        public Constructor(ConstructorType constructorType)
        {
            this.constructorType = constructorType;
        }

        public string this[ConstructorPartType type]
        {
            get => _parts[type];
            set => _parts[type] = value;
        }

        public override string ToString()
        {
            return $"{constructorType}: {_parts[ConstructorPartType.Door]}, {_parts[ConstructorPartType.Frame]}, {_parts[ConstructorPartType.Window]}";
        }
    }

    public enum ConstructorType
    {
        Villa,
        Apartment,
        Triplex
    }

    public enum ConstructorPartType
    {
        Window,
        Frame,
        Door
    }
    #endregion
}
