using System;

namespace Structural.Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Proxy Pattern!");

            // Create math proxy
            var proxy = new MathProxy();

            // Do math 
            Console.WriteLine($"Add: {proxy.Add(20, 10)}");
            Console.WriteLine($"Sub: {proxy.Sub(20, 10)}");
            Console.WriteLine($"Mul: {proxy.Mul(20, 10)}");
            Console.WriteLine($"Div: {proxy.Div(20, 10)}");

            var car = new CarProxy(new Driver(){Age = 15});
            car.DriveCar();

            var car2 = new CarProxy(new Driver(){Age = 17});
            car2.DriveCar();
        }
    }

    #region .Net Optimized

    /// <summary>
    /// The 'Subject' interface
    /// </summary>
    internal interface IMath
    {
        double Add(double x, double y);
        double Sub(double x, double y);
        double Mul(double x, double y);
        double Div(double x, double y);
    }

    /// <summary>
    /// The 'RealSubject' class
    /// </summary>
    internal class Math : MarshalByRefObject, IMath
    {
        public double Add(double x, double y) => x + y;

        public double Div(double x, double y) => x / y;

        public double Mul(double x, double y) => x * y;

        public double Sub(double x, double y) => x - y;
    }

    /// <summary>
    /// The 'Proxy Object' class
    /// </summary>
    internal class MathProxy : IMath
    {
        private readonly Math _math;

        public MathProxy()
        {
            // var ad = AppDomain.CreateDomain(AppDomain.CurrentDomain.FriendlyName);

            // var o = ad.CreateInstance("Structural.Proxy", "Structural.Proxy.Math");

            // _math = (Math)o.Unwrap();

            _math = new Math();
        }

        public double Add(double x, double y) => _math.Add(x, y);

        public double Div(double x, double y) => _math.Div(x, y);

        public double Mul(double x, double y) => _math.Mul(x, y);

        public double Sub(double x, double y) => _math.Sub(x, y);
    }

    #endregion

    #region Car Sample

    /// <summary>
    /// The 'Subject' interface
    /// </summary>
    internal interface ICar
    {
        void DriveCar();
    }

    /// <summary>
    /// The 'Real Subject' class
    /// </summary>
    internal class Car : ICar
    {
        public void DriveCar()
        {
            Console.WriteLine("The car has been driven!");
        }
    }

    internal class CarProxy : ICar
    {
        private readonly Driver _driver;
        private readonly ICar _car;

        public CarProxy(Driver driver)
        {
            _driver = driver;
            _car = new Car();

        }
        public void DriveCar()
        {
            if (_driver.Age < 16)
                Console.WriteLine("Sorry, the driver is too young to drive.");
            else
                this._car.DriveCar();
        }
    }

    class Driver
    {
        public int Age { get; set; }
    }
    #endregion
}
