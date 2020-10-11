using System;

namespace Structural.Facade
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Structural Pattern!");

            // Facede
            Mortagage mortagage = Mortagage.Instace;

            var customer = new Customer()
            {
                Name = "Mert Kimyonşen"
            };

            var isEligible = mortagage.IsEligible(customer, 5000);

            Console.WriteLine($"The customer {customer.Name} has been {(isEligible ? "applied" : "rejected")}");
        }
    }

    #region .Net Optimized

    /// <summary>
    /// The 'Subsystem ClassA' class
    /// </summary>
    class Bank
    {
        public bool HasSufficientSavings(Customer customer, int amount)
        {
            Console.WriteLine($"Checking bank for: {customer.Name}");
            return true;
        }
    }

    /// <summary>
    /// The 'Subsystem ClassB' class
    /// </summary>
    class Credit
    {
        public bool HasGoodCreditScore(Customer customer)
        {
            Console.WriteLine($"Checking credit score for: {customer.Name}");

            return true;
        }
    }

    /// <summary>
    /// The 'Subsystem ClassC' class
    /// </summary>
    class Loan
    {

        public bool HasNoBadLoan(Customer customer)
        {
            Console.WriteLine($"Checking loans for: {customer.Name}");

            return true;
        }
    }

    /// <summary>
    /// The 'Facade' class
    /// </summary>
    class Mortagage
    {
        private static readonly Lazy<Mortagage> _lazy = new Lazy<Mortagage>(() => new Mortagage());

        private readonly Bank _bank;
        private readonly Credit _credit;
        private readonly Loan _loan;

        public Mortagage()
        {
            _bank = new Bank();
            _credit = new Credit();
            _loan = new Loan();
        }

        public static Mortagage Instace => _lazy.Value;

        public bool IsEligible(Customer customer, int amount)
        {
            Console.WriteLine($"{customer.Name} applies for {amount:C}");

            bool isEligible = true;

            if (!_bank.HasSufficientSavings(customer, amount))
            {
                isEligible = false;
            }
            else if (!_credit.HasGoodCreditScore(customer))
            {
                isEligible = false;
            }
            else if (!_loan.HasNoBadLoan(customer))
            {
                isEligible = false;
            }

            return isEligible;
        }
    }

    /// <summary>
    /// The 'Customer' class
    /// </summary>
    class Customer
    {
        public string Name { get; set; }
    }
    #endregion
}
