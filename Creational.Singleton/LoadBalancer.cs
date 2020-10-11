using System;
using System.Collections.Generic;

namespace Creational.Singleton
{
    // Real World Example
    public class LoadBalancer
    {
        // Eagerly initializing
        // Static members are 'eagerly initialized', that is, 
        // immediately when class is loaded for the first time.
        // .NET guarantees thread safety for static initialization
        private readonly static LoadBalancer _instance = new LoadBalancer();

        private readonly List<string> server = new List<string>();

        private Random random = new Random();
        
        // If you mark constructor as a static
        // .Net guarantees this class has only one instance
        static LoadBalancer()
        {
            
        }

        private LoadBalancer()
        {
            // online virtual servers
            server.Add("Server1");
            server.Add("Server2");
            server.Add("Server3");
            server.Add("Server4");
        }

        public static LoadBalancer Instance => _instance;
        
        // think that this methods know about web state authorative knowledge
        // Each request must go through
        public string Server => server[random.Next(server.Count)];
    }

    public class LoadBalancerThread
    {
        static LoadBalancerThread _instance;

        private static object _lock = new object();

        private readonly List<string> server = new List<string>();

        private Random random = new Random();

        private LoadBalancerThread()
        {
            server.Add("Server1");
            server.Add("Server2");
            server.Add("Server3");
            server.Add("Server4");
        }

        // Support multithreaded applications through
        // 'Double checked locking' pattern which (once
        // the instance exists) avoids locking each
        // time the method is invoked
        public static LoadBalancerThread Instance
        {
            get
            {

                // Uses lazy initialization.
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new LoadBalancerThread();
                    }
                }

                return _instance;
            }
        }
        public string Server => server[random.Next(server.Count)];
    }
}