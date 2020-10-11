using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Creational.Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Prototype Pattern!");

            var colorManager = new ColorManager();

            colorManager["red"] = new Color(255, 0, 0);
            colorManager["green"] = new Color(0, 255, 0);
            colorManager["blue"] = new Color(0, 0, 255);

            Color red = colorManager["red"].Clone() as Color;
            Color green = colorManager["green"].Clone() as Color;
            Color blue = colorManager["blue"].Clone() as Color;

            Person person = new Person() { Name = "Mert", LastName = "Kimyonşen" };
            Person person1 = person.Clone(true) as Person;
            Person person2 = person.Clone(false) as Person;

            Cell cell = new Cell();
            cell.Detect = "First Cell";

            Cell cellCopy = (Cell) cell.Clone();
            cellCopy.Detect = "Copy Cell";

            Console.WriteLine(cell.Detect);
            Console.WriteLine(cellCopy.Detect);

            Cell cellDeepCopy = (Cell) cell.CloneDeep();

        }

        #region Overview

        [Serializable]
        public class Cell : ICloneable
        {
            public object Clone()
            {
                return this.MemberwiseClone();
            }

            public string Detect {get; set;}

            public object CloneDeep()
            {
                MemoryStream memory = new MemoryStream();

                var binFormatter = new BinaryFormatter();
                binFormatter.Serialize(memory, this);

                memory.Seek(0, SeekOrigin.Begin);

                var copy = binFormatter.Deserialize(memory);
                memory.Close();

                return copy;
            }
        }
        #endregion

        /// <summary>
        /// The 'Prototype' abstract class
        /// </summary>
        abstract class ColorPrototype
        {
            public abstract ColorPrototype Clone();
        }

        /// <summary>
        /// The 'Prototype' concrete class
        /// </summary>
        class Color : ColorPrototype
        {
            private readonly int red;
            private readonly int blue;
            private readonly int green;

            public Color(int red, int blue, int green)
            {
                this.red = red;
                this.blue = blue;
                this.green = green;
            }

            public override ColorPrototype Clone()
            {
                Console.WriteLine($"red: {red}, blue: {blue}, green: {green}");
                return (ColorPrototype)this.MemberwiseClone();
            }
        }

        /// <summary>
        /// Prototype manager
        /// </summary>
        class ColorManager
        {
            private Dictionary<string, ColorPrototype> _colors = new Dictionary<string, ColorPrototype>();

            public ColorPrototype this[string name]
            {
                get => _colors[name];
                set => _colors[name] = value;
            }
        }


        /// <summary>
        /// The 'ConcretePrototype' class
        /// </summary>
        [Serializable]
        class Person : ICloneable
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public object Clone(bool shallow)
            {
                return shallow ? Clone() : DeepCopy();
            }

            public object Clone()
            {

                Console.WriteLine(
                    $"{Name}, {LastName}"
                );

                return this.MemberwiseClone();
            }

            private object DeepCopy()
            {
                var stream = new MemoryStream();

                var formatter = new BinaryFormatter();

                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);

                var copy = formatter.Deserialize(stream);
                stream.Close();

                Console.WriteLine(
                    $"Deep copy: {(copy as Person).Name}, {(copy as Person).LastName}"
                );

                return copy;
            }
        }
    }
}
