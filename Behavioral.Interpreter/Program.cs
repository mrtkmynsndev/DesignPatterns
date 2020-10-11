using System;
using System.Collections.Generic;

namespace Behavioral.Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Interpreter Pattern!");

            Context context = new Context { Input = "ACSSDDDD" };
            RunExpression(context);

        }
        static void RunExpression(Context context)
        {
            foreach (var roleExpression in CreateExpressionTree(context.Input))
            {
                roleExpression.Interpret(context);
            }

            Console.WriteLine($"{context.Input} için maliyet {context.Total}");
        }

        private static IEnumerable<RoleExpression> CreateExpressionTree(string format)
        {
            List<RoleExpression> roleExpressions = new List<RoleExpression>();

            foreach (char item in format)
            {
                RoleExpression expression = item switch
                {
                    'A' => new ArchitectExpression(),
                    'C' => new ConstultantExpression(),
                    'S' => new SeniorExpressiın(),
                    'D' => new DeveloperExpresion(),
                    _ => throw new ArgumentException()
                };

                roleExpressions.Add(expression);
            }

            return roleExpressions;
        }
    }


    /// <summary>
    /// The 'Context' class
    /// </summary>
    class Context
    {
        public string Input { get; set; }
        public int Total { get; set; }
    }

    /// <summary>
    /// The 'AbstractExpression' abstract class
    /// </summary>
    abstract class RoleExpression
    {
        public abstract void Interpret(Context context);
    }

    /// <summary>
    /// The 'TerminalExpress' class
    /// </summary>
    class ArchitectExpression : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Input.Contains("A"))
                context.Total += 5;
        }
    }

    /// <summary>
    /// The 'TerminalExpress' class
    /// </summary>
    class ConstultantExpression : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Input.Contains("C"))
                context.Total += 10;
        }
    }

    /// <summary>
    /// The 'TerminalExpress' class
    /// </summary>
    class SeniorExpressiın : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Input.Contains("S"))
                context.Total += 15;
        }
    }

    /// <summary>
    /// The 'TerminalExpress' class
    /// </summary>
    class DeveloperExpresion : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Input.Contains("D"))
                context.Total += 20;
        }
    }
}
