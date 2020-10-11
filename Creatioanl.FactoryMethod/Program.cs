using System;
using System.Collections.Generic;
using Creatioanl.FactoryMethod.FlowSnacks;

namespace Creatioanl.FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Factory Method Pattern!");

            /*
                En Temel olay fabrika mantığıdır. 
                Oluşturacağımız objenin tipini önceden belirlemek istemiyorsak. 
                Oluşturulacak obje factory methodun içinde runtime olarak üretilecek. 
            */

            // Örnek: Factory Method Pattern kullanılarak esnek bir Document yaratma
            // yapısı oluşturma

            // Product (Page)
            // ConcreteProduct (SkillsPage, EducationPage, ExperiencePage)
            // Creator (Document)
            // ConcreteCreator (Report, Resume)

            var documents = new List<Document> { new Report(), new Resume() };

            foreach (var document in documents)
            {
                Console.WriteLine($"{document}---");

                foreach (var page in document.Pages)
                {
                    Console.WriteLine(" " + page);
                }

                Console.WriteLine();
            }

            Console.WriteLine("************** End **************");

            var faculities = new List<IFaculity>() { new EngineeringFaculty(), new IndustryialFaculity() };

            FaculityManager faculityManager = null;

            foreach (var faculity in faculities)
            {
                faculityManager = new FaculityManager(faculity);
                faculityManager.AllSchedule();
            }

            var departmentFactory = new DepartmentFactory();
            IDepartment computerDepartment = departmentFactory.Create<ComputerDepartment>();

            
            Console.WriteLine("************** End **************");

            IFlow flow = new Flow();
            flow.CreateStates();
            flow.ShowFlowAbility();
        }
    }

    #region Documents

    /// <summary>
    /// The 'Product' abstract class
    /// </summary>
    abstract class Page
    {
        public override string ToString()
        {
            return this.GetType().Name;
        }
    }

    /// <summary>
    /// The 'ConcreteProduct' class
    /// </summary>
    class SkillsPage : Page
    {

    }

    /// <summary>
    /// The 'ConcreteProduct' class
    /// </summary>
    class EducationPage : Page
    {

    }

    /// <summary>
    /// The 'ConcreteProduct' class
    /// </summary>
    class ExperiencePage : Page
    {

    }

    /// <summary>
    /// The 'ConcreteProduct' class
    /// </summary>
    class IntroductionPage : Page
    {

    }

    /// <summary>
    /// The 'ConcreteProduct' class
    /// </summary>
    class ResultsPage : Page
    {

    }

    /// <summary>
    /// A 'ConcreteProduct' class
    /// </summary>
    class ConclusionPage : Page
    {
    }

    /// <summary>
    /// A 'ConcreteProduct' class
    /// </summary>
    class SummaryPage : Page
    {
    }

    /// <summary>
    /// A 'ConcreteProduct' class
    /// </summary>
    class BibliographyPage : Page
    {
    }

    /// <summary>
    /// The 'Creator' abstract class
    /// </summary>
    abstract class Document
    {
        protected Document()
        {
            this.CreatePages();
        }

        public abstract void CreatePages();

        public List<Page> Pages { get; protected set; }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }

    /// <summary>
    /// The 'Creator' concrete class
    /// </summary>
    class Resume : Document
    {
        public override void CreatePages()
        {
            Pages = new List<Page>
            {
                new SkillsPage(),
                new EducationPage(),
                new ExperiencePage(),
            };
        }
    }

    /// <summary>
    /// The 'Creator' concrete class
    /// </summary>
    class Report : Document
    {
        public override void CreatePages()
        {
            Pages = new List<Page>
                {
                    new IntroductionPage(),
                    new ResultsPage(),
                    new ConclusionPage(),
                    new SummaryPage(),
                    new BibliographyPage()
                };
        }
    }

    #endregion

    #region University
    interface IDepartment
    {
        string Scedule { get; }
    }

    class ComputerDepartment : IDepartment
    {
        public string Scedule => this.GetType().Name;
    }

    class ElectronicDepartment : IDepartment
    {
        public string Scedule => this.GetType().Name;
    }

    class MathDepartment : IDepartment
    {
        public string Scedule => this.GetType().Name;
    }

    class HistoryDepartment : IDepartment
    {
        public string Scedule => this.GetType().Name;
    }

    class DepartmentFactory
    {
        public DepartmentFactory()
        {
        }

        public IDepartment Create<T>() where T : IDepartment
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }

    interface IFaculity
    {
        void CretaDepartments();
        List<IDepartment> Departments { get; set; }
    }

    class EngineeringFaculty : IFaculity
    {
        public EngineeringFaculty()
        {
            this.CretaDepartments();
        }

        public List<IDepartment> Departments { get; set; }

        public void CretaDepartments()
        {
            Departments = new List<IDepartment>()
           {
               new ComputerDepartment(),
               new ElectronicDepartment()
           };
        }
    }

    class IndustryialFaculity : IFaculity
    {
        public IndustryialFaculity()
        {
            this.CretaDepartments();
        }

        public List<IDepartment> Departments { get; set; }

        public void CretaDepartments()
        {
            Departments = new List<IDepartment>()
           {
               new MathDepartment(),
               new HistoryDepartment()
           };
        }
    }

    class FaculityManager
    {
        private readonly IFaculity _faculity;

        public FaculityManager(IFaculity faculity)
        {
            _faculity = faculity;
        }

        public void AllSchedule()
        {
            foreach (var department in _faculity.Departments)
            {
                Console.WriteLine(department.GetType().Name);
                Console.WriteLine("Scheudle: " + department.Scedule);
            }
        }

    }

    #endregion

}
