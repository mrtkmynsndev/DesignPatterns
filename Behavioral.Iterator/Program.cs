using System;
using System.Collections;
using System.Collections.Generic;

namespace Behavioral.Iterator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Iterator Pattern!");

            ProductCollection products = new ProductCollection();
            products.Add(new Product(){Name = "Macbook"});
            products.Add(new Product(){Name = "Windows"});
            products.Add(new Product(){Name = "IBM"});

            foreach (var product in products)
            {
                Console.WriteLine(product.Name);
            }
        }
    }

    class ProductCollection : IEnumerable<Product>
    {
        private List<Product> products = new List<Product>();

        public ProductCollection()
        {
        }

        public Product this[int index]
        {
            get => products[index];
            set => products[index] = value;
        }

        public int Count => products.Count;

        public void Add(Product product)
        {
            products.Add(product);
        }

        public IEnumerator<Product> GetEnumerator()
        {
            foreach (var product in products)
            {
                yield return product;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    class ProductEnumerator : IEnumerator<Product>
    {
        private readonly ProductCollection collection;
        private Product _product;
        private int index = -1;

        public ProductEnumerator(ProductCollection collection)
        {
            this.collection = collection;
        }

        public Product Current => _product;

        object IEnumerator.Current => Current;

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (++index > collection.Count)
            {
                return false;
            }
            else
            {
                _product = collection[index];
            }

            return false;
        }

        public void Reset()
        {
            index = -1;
            _product = default(Product);
        }
    }

    class Product
    {
        public string Name { get; set; }
    }
}
