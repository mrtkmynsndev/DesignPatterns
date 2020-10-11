using System;
using System.Collections;
using System.Collections.Generic;

namespace Behavioral.Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Strategy Pattern!");

            var sortableCollection = new List<string>(){
                "Apple",
                "Windows",
                "Tesla",
                "IBM",
                "Spotify",
                "Yemeksepeti",
                "Hepsiburada",
                "Trendyol"
            };

            var sortStrategies = new List<ISortStrategy>(){
                new QuickSort(),
                new BubbleSort(),
                new MergeSort()
            };

            foreach (var sort in sortStrategies)
            {
                SortedList sortedList = new SortedList(sort);
                sortedList.Sort(sortableCollection);
            }
        }
    }

    #region .Net Optimized

    /// <summary>
    /// The 'Strategy' interface
    /// </summary>
    interface ISortStrategy
    {
        void Sort(List<string> collection);
    }

    /// <summary>
    /// The 'ConcreteStrategy' class
    /// </summary>
    internal class QuickSort : ISortStrategy
    {
        public void Sort(List<string> collection)
        {
            collection.Sort(); // default sort
            Console.WriteLine($"count: {collection.Count} is sorted by {nameof(QuickSort)}");
        }
    }

    /// <summary>
    /// The 'ConcreteStrategy' class
    /// </summary>
    internal class BubbleSort : ISortStrategy
    {
        public void Sort(List<string> collection)
        {
            // todo: implement bubble sort
            Console.WriteLine($"count: {collection.Count} is sorted by {nameof(BubbleSort)}");
        }
    }

    /// <summary>
    /// The 'ConcreteStrategy' class
    /// </summary>
    internal class MergeSort : ISortStrategy
    {
        public void Sort(List<string> collection)
        {
            // todo: implement merge sort
            Console.WriteLine($"count: {collection.Count} is sorted by {nameof(MergeSort)}");
        }
    }

    internal class SortedList
    {
        private readonly ISortStrategy _sortStrategy;
        public SortedList(ISortStrategy sortStrategy)
        {
            _sortStrategy = sortStrategy;
        }

        public void Sort(List<string> collection)
        {
            _sortStrategy.Sort(collection);

            foreach (var item in collection)
            {
                Console.WriteLine(" " + item);
            }

            Console.WriteLine();
        }
    }
    #endregion
}
