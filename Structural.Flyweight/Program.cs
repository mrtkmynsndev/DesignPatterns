using System;
using System.Collections.Generic;

namespace Structural.Flyweight
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Flwweight Pattern!");

            Console.WriteLine(typeof(Character).Namespace);

            
            string document = "ABZABZ";
            char[] documentChars = document.ToCharArray();

            var characterFactory = new CharacterFactory();

            int pointSize = 10;

            foreach (var @char in documentChars)
            {
                var character = characterFactory[@char];
                character.Display(++pointSize);
            }
        }
    }

    #region .Net Optimized

    /// <summary>
    /// The 'FlyweightFactory' class
    /// </summary>
    internal class CharacterFactory
    {
        private static readonly object _obj = new object();
        private Dictionary<char, Character> _characters = new Dictionary<char, Character>();

        public Character this[char key]
        {
            get
            {
                Character _character = null;

                if (_characters.ContainsKey(key))
                {
                    _character = _characters[key];
                }
                else
                {
                    var name = $"{this.GetType().Namespace}.{nameof(Character)}{key.ToString()}";

                    lock (_obj)
                    {
                        _character = (Character)Activator.CreateInstance(Type.GetType(name));
                    }
                }

                return _character;
            }
        }
    }


    /// <summary>
    /// The 'Flyweight' class
    /// </summary>
    internal abstract class Character
    {
        protected char symbol;
        protected int width;
        protected int height;
        protected int ascent;
        protected int descent;
        protected int pointSize;

        public abstract void Display(int pointSize);
    }

    /// <summary>
    /// A 'ConcreteFlyweight' class
    /// </summary>
    internal class CharacterA : Character
    {
        public CharacterA()
        {
            this.symbol = 'A';
            this.height = 100;
            this.width = 120;
            this.ascent = 70;
            this.descent = 0;
        }

        public override void Display(int pointSize)
        {
            this.pointSize = pointSize;

            Console.WriteLine($"{symbol} pointsize: {pointSize}");
        }
    }

    /// <summary>
    /// A 'ConcreteFlyweight' class
    /// </summary>
    internal class CharacterB : Character
    {
        public CharacterB()
        {
            this.symbol = 'B';
            this.height = 100;
            this.width = 140;
            this.ascent = 72;
            this.descent = 0;
        }

        public override void Display(int pointSize)
        {
            this.pointSize = pointSize;

            Console.WriteLine($"{symbol} pointsize: {pointSize}");
        }
    }

    // ... C, D, E, etc.

    /// <summary>
    /// A 'ConcreteFlyweight' class
    /// </summary>
    class CharacterZ : Character
    {
        // Constructor
        public CharacterZ()
        {
            this.symbol = 'Z';
            this.height = 100;
            this.width = 100;
            this.ascent = 68;
            this.descent = 0;
        }

        public override void Display(int pointSize)
        {
            this.pointSize = pointSize;

            Console.WriteLine($"{symbol} pointsize: {pointSize}");
        }
    }

    #endregion

}
