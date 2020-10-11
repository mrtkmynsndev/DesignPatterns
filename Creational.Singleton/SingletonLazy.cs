using System;

namespace Creational.Singleton
{
    public class SingletonLazy
    {
        // eagerly initializing with lazy
        private static readonly Lazy<SingletonLazy> _instance = new Lazy<SingletonLazy>(() => new SingletonLazy());

        public SingletonLazy()
        {
        }

        public static SingletonLazy Instance => _instance.Value;
    }
}