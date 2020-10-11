using System;
using System.Collections;
using System.Collections.Generic;

namespace Structural.Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Composite Pattern!");

            // Composite root = new Composite("The Root");
            // Component child = new Leaf("Leaf 1");
            // root.Add(child);

            // Component child2 = new Leaf("The Leaf 2");
            // root.Add(child2);

            // Composite composite = new Composite("The Composite");
            // composite.Add(new Leaf("The Leaf 3"));
            // composite.Add(new Leaf("The Leaf 4"));

            // root.Add(composite);

            // root.Display();

            Person rootPerson = new Person("Root Person");

            Person compositePerson = new Person("John Manager");
            compositePerson.Add(new Employee("Employee 1"));
            compositePerson.Add(new Employee("Employee 2"));
            compositePerson.Add(new Employee("Employee 3"));

            Person compositePerson2 = new Person("Julia Manager");
            compositePerson2.Add(new Employee("Employee 4"));
            compositePerson2.Add(new Employee("Employee 5"));

            rootPerson.Add(compositePerson);
            rootPerson.Add(compositePerson2);
            rootPerson.Display();

            Console.WriteLine();

            Console.WriteLine(rootPerson.Name);

            foreach (Person person in rootPerson)
            {
               Console.WriteLine($"# {person.Name}");

               foreach (var child in person)
               {
                 Console.WriteLine($"  - {child.Name}");
               }
            }
        }
    }

    #region Structural

    /// <summary>
    /// The 'Component' abstract
    /// </summary>
    internal abstract class Component
    {
        protected readonly string _name;

        protected Component(string name)
        {
            _name = name;
        }

        public abstract void Display();
    }

    /// <summary>
    /// The 'Composite' class
    /// </summary>
    internal class Composite : Component
    {
        private readonly List<Component> _child = new List<Component>();

        public Composite(string name) : base(name)
        {
        }

        public virtual void Add(Component c)
        {
            _child.Add(c);
        }

        public virtual void Delete(Component c)
        {
            _child.Remove(c);
        }

        public override void Display()
        {
            Console.WriteLine($"{_name}");

            _child.ForEach(x =>
            {
                x.Display();
            });
        }

    }

    /// <summary>
    /// The 'Leaf' class
    /// </summary>
    internal class Leaf : Component
    {
        public Leaf(string name) : base(name)
        {
        }

        public override void Display()
        {
            Console.WriteLine($"- {_name}");
        }
    }
    #endregion

    #region Read-World


    /// <summary>
    /// The 'Component' interface
    /// </summary>
    internal interface IPerson
    {
        string Name { get; set; }
        void Display();
    }

    /// <summary>
    /// The 'Composite' class
    /// </summary>
    internal class Person : IPerson, IEnumerable<IPerson>
    {
        private readonly List<IPerson> _children = new List<IPerson>();

        public Person(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public void Add(IPerson person)
        {
            _children.Add(person);
        }

        public void Remove(IPerson person)
        {
            _children.Remove(person);
        }

        public IPerson GetPerson(int index)
        {
            if (index >= 0 && index < _children.Count)
            {
                return _children[index];
            }

            return default(IPerson);
        }

        public void Display()
        {
            Console.WriteLine(Name);

            _children.ForEach(x =>
            {
                x.Display();
            });
        }

        public IEnumerator<IPerson> GetEnumerator()
        {
            foreach (var child in _children)
            {
                yield return child;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

    /// <summary>
    /// The 'Leaf' class
    /// </summary>
    class Employee : IPerson
    {
        public Employee(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public void Display()
        {
            Console.WriteLine($"- {Name}");
        }
    }
    #endregion
}
