using System;
using System.Collections.Generic;

namespace Structural.Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Adapter Pattern!");

            // // Non-Adapted logger class
            // ILogger logger = new FileLogger();
            // logger.Log("File Logger");

            // // Adapted Log4Net class
            // ILogger logger1 = new Log4NetLoggerAdapter();
            // logger1.Log("Log4Net Logger");

            var loggers = new List<ILogger>() { new FileLogger(), new Log4NetLoggerAdapter() };
            ProductManager productManager = null;
            loggers.ForEach(x => {
                productManager = new ProductManager(x);
                productManager.Add("Some Log");
            });
        }


        /// <summary>
        /// The 'Target' class
        /// </summary>ƒ
        class Compound
        {

        }

        #region Log

        private interface IProductService
        {
            void Add(string productName);
        }

        class ProductManager : IProductService
        {
            private readonly ILogger logger;

            public ProductManager(ILogger logger)
            {
                this.logger = logger;
            }

            public void Add(string productName)
            {
                logger.Log("Product Added");
                // other stuff
            }
        }

        /// <summary>
        /// The 'Target' class
        /// </summary>
        interface ILogger
        {
            void Log(string message);
        }

        class FileLogger : ILogger
        {
            public void Log(string message)
            {
                Console.WriteLine("\nFileLogger: s ------ ");
            }
        }


        /// <summary>
        /// The 'Adapter' class
        /// </summary>
        class Log4NetLoggerAdapter : ILogger
        {
            private ILog4Net _logNet;
            public Log4NetLoggerAdapter()
            {
                // some required instance
                _logNet = new Log4Net();
            }

            public void Log(string message)
            {
                _logNet.Log(message);
            }
        }

        interface ILog4Net
        {
            void Log(string message);
        }


        /// <summary>
        /// The 'Adeptee' class
        /// </summary>
        class Log4Net : ILog4Net
        {
            public void Log(string message)
            {
                Console.WriteLine("\nLog4Net: s ------ ");
            }
        }
        #endregion
    }
}
