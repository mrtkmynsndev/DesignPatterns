using System;
using System.Collections.Generic;

namespace Creational.AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Abstract Factory!");

            // Kıtalarda yaşayan etçil ve otçul hayvanların besin zincirini karşıalyacak
            // yapıyı abstract factory ile yapılması.

            // AbstractFactory (ContinentFactory)
            // ConcreteFactory (AfricaFactory, AmericaFactory)
            // AbstractProduct (Herbivore, Carnivore)
            // Product (Wildebeest, Lion, Bison, Wolf)
            // Client (AnimalWorld)

            // IContinentFactory africaFactory = new AfricaFactory();

            // // Real World basic usage
            // // AnimalWorld animalWorld = new AnimalWorld();
            // // animalWorld.RunFoodChain(africaFactory);

            // // .Net Optimized usag
            // IAnimalWorld animalWorld = new AnimalWorld<AfricaFactory>();
            // animalWorld.RunFoodChain();

            // AbstractFactory (CharacterFactory)
            // ConcreteFactory (AssianCharacterFactory, EuropeCharacterFactory)
            // AbstractProduct (IAbility, ISuperPower)
            // Product (FastAbility, FireAbility, FireSuperPower, GuardSuperPower)
            // Client (CharacterManager)

            List<ICharacterFactory> characterFactories = new List<ICharacterFactory>()
            {
                new AssianCharacterFactory(),
                new EuropoCharacterFactory()
            };

            ICharacterService characterManager = null;

            foreach (var factory in characterFactories)
            {
                characterManager = new CharacterManager(factory);
                characterManager.BuildCharacter();
            }

        }

        #region Continent Sample
        // Abstract Factory
        interface IContinentFactory
        {
            IHerbivore CreateHerbivore();
            ICarnivore CreateCarnivor();
        }

        // Concrete Factory 1
        public class AfricaFactory : IContinentFactory
        {
            public ICarnivore CreateCarnivor()
            {
                return new Lion();
            }

            public IHerbivore CreateHerbivore()
            {
                return new Wildebeest();
            }
        }

        // Concrete Factory 2
        public class AmericaFactory : IContinentFactory
        {
            public ICarnivore CreateCarnivor()
            {
                return new Wolf();
            }

            public IHerbivore CreateHerbivore()
            {
                return new Bison();
            }
        }

        // Abstract Product 
        public interface IHerbivore
        {

        }

        // Abstract Product 
        public interface ICarnivore
        {
            void Eat(IHerbivore herbivore);
        }

        /// <summary>
        /// The 'ProductA!' class
        /// </summary>
        class Wildebeest : IHerbivore
        {

        }

        /// <summary>
        /// The 'ProductB1' class
        /// </summary>
        class Lion : ICarnivore
        {
            public void Eat(IHerbivore herbivore)
            {
                Console.WriteLine($"{this.GetType().Name}  eats {herbivore.GetType().Name}");
            }
        }

        /// <summary>
        /// The 'ProductA2' class
        /// </summary>
        class Bison : IHerbivore
        {
        }

        /// <summary>
        /// The 'ProductB2' class
        /// </summary>
        class Wolf : ICarnivore
        {
            public void Eat(IHerbivore herbivore)
            {
                // Eat Bison
                Console.WriteLine(this.GetType().Name +
                    " eats " + herbivore.GetType().Name);
            }
        }

        // Client Interface
        interface IAnimalWorld
        {
            void RunFoodChain();
        }

        // Client class
        class AnimalWorld<T> : IAnimalWorld where T : IContinentFactory, new()
        {
            IHerbivore _herbivore;
            ICarnivore _carnivore;

            public AnimalWorld(IContinentFactory factory)
            {
                _carnivore = factory.CreateCarnivor();
                _herbivore = factory.CreateHerbivore();
            }

            public AnimalWorld()
            {
                T factory = new T();

                _carnivore = factory.CreateCarnivor();
                _herbivore = factory.CreateHerbivore();
            }

            public void RunFoodChain()
            {
                _carnivore.Eat(_herbivore);
            }
        }
    }
    #endregion

    #region Gaming


    /// <summary>
    /// The 'AbstractFactory' interface
    /// </summary>
    interface ICharacterFactory
    {
        IAbilitiy CreateAbility();
        ISuperPower CreateSuperPower();
    }

    /// <summary>
    /// The 'AbstractProductA' interface
    /// </summary>
    interface IAbilitiy
    {
        string GetAbility();
    }

    /// <summary>
    /// The 'AbstractProductB' interface
    /// </summary>
    interface ISuperPower
    {
        string GetSuperPower();
    }

    /// <summary>
    /// The 'ProductA1' class
    /// </summary>
    class FastAbility : IAbilitiy
    {
        public string GetAbility()
        {
            return $"{this.GetType().Name}: Catch me, if you can";
        }
    }

    /// <summary>
    /// The 'ProductA2' class
    /// </summary>
    class JumpAbility : IAbilitiy
    {
        public string GetAbility()
        {
            return $"{this.GetType().Name}: I'm in the sky";
        }
    }

    /// <summary>
    /// The 'ProductB1' class
    /// </summary>
    class FireSuperPower : ISuperPower
    {
        public string GetSuperPower()
        {
            return $"{this.GetType().Name}: I'm like a dragon";
        }
    }

    /// <summary>
    /// The 'ProductB2' class
    /// </summary>
    class GuardSuperPower : ISuperPower
    {
        public string GetSuperPower()
        {
            return $"{this.GetType().Name}: Dark night come back";
        }
    }

    /// <summary>
    /// The 'ConcreteFactory1' class.
    /// </summary>
    class AssianCharacterFactory : ICharacterFactory
    {
        public IAbilitiy CreateAbility()
        {
            return new JumpAbility();
        }

        public ISuperPower CreateSuperPower()
        {
            return new FireSuperPower();
        }
    }

    /// <summary>
    /// The 'ConcreteFactory2' class.
    /// </summary>
    class EuropoCharacterFactory : ICharacterFactory
    {
        public IAbilitiy CreateAbility()
        {
            return new FastAbility();
        }

        public ISuperPower CreateSuperPower()
        {
            return new GuardSuperPower();
        }
    }

    /// <summary>
    /// The 'Client' interface
    /// </summary>

    interface ICharacterService
    {
        void BuildCharacter();
    }

    /// <summary>
    /// The 'Client' class
    /// </summary>
    class CharacterManager : ICharacterService
    {
        private readonly IAbilitiy _ability;
        private readonly ISuperPower _superPower;

        public CharacterManager(ICharacterFactory characterFactory)
        {
            _ability = characterFactory.CreateAbility();
            _superPower = characterFactory.CreateSuperPower();
        }

        public void BuildCharacter()
        {
            Console.WriteLine($"Abilities-> {_ability.GetAbility()}");
            Console.WriteLine($"SuperPower-> {_superPower.GetSuperPower()}");
        }
    }

    #endregion
}
