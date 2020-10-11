using System;

namespace Behavioral.TemplateMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Template Method!");

            var generator = new ClassGenerator();
            generator.Generate();
        }
    }

    #region .Net Optimized

    internal abstract class Generator
    {
        public void Generate()
        {
            CreateDefaultUsing();

            var className = CreateName();
            var properties = CreateProperties();
            var methods = CreateMethods();

            var tmpClass = GetClassTemplate();

            tmpClass = tmpClass.Replace("[ClassName]", className);
            tmpClass = tmpClass.Replace("[property]", properties);
            tmpClass = tmpClass.Replace("[method]", methods);

            Console.WriteLine(tmpClass);
        }

        public virtual void CreateDefaultUsing()
        {
            Console.WriteLine("using System;");
        }

        private string GetClassTemplate()
        {
            return @"class [ClassName]
            {
                [property]

                [method]
            }";
        }

        public abstract string CreateName();
        public abstract string CreateProperties();
        public abstract string CreateMethods();
    }

    internal class ClassGenerator : Generator
    {
        public override string CreateMethods()
        {
            return "public void FirstMethod(){}";
        }

        public override string CreateName()
        {
            return "FirstClass";
        }

        public override string CreateProperties()
        {
            return @"public string FirstProperty {get; set;}";
        }
    }

    #endregion
}
