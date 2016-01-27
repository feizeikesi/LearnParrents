using System;
using System.Collections;

namespace SalaryDemo.Test.DesignPattern.Strategy
{
    public interface ISortHandle<T>
    {
        void Swap(int index);
        bool OutOfOrder(int index);

        int Length();

        void SetArray(T[] arrry);

    }

    public class BubbleSorter
    {
        private static int _operations = 0;


        public static int Sort<T>(T[] array) where T : IComparable
        {
            return Sort<T>(array, new DefaultBubbleSorter<T>());
        }

        public static int Sort<T>(T[] array, ISortHandle<T> sortHandle)
        {
            sortHandle.SetArray(array);
            int length = sortHandle.Length();
            bool thisPassInOrder = false;
            for (var i = 0; i < length - 1 && !thisPassInOrder; i++)
            {
                thisPassInOrder = true; 
                for (var j = 0; j < length - i - 1; j++)
                {
                    if (sortHandle.OutOfOrder(j))
                    {
                       sortHandle.Swap(j);
                       thisPassInOrder = false;
                        _operations++;
                    }
                }
                Console.WriteLine(string.Join(",",array));
            }
            return _operations;
        }

        public class DefaultBubbleSorter<T> : ISortHandle<T> where T : IComparable
        {
            private T[] _array = null;
            public void Swap(int index)
            {
                T temp = _array[index];
                _array[index] = _array[index + 1];
                _array[index + 1] = temp;
            }

            public bool OutOfOrder(int index)
            {
                return _array[index].CompareTo( _array[index + 1])>0;
            }

            public int Length()
            {
                return _array.Length;
            }

            public void SetArray(T[] arrry)
            {
                _array = arrry;
            }
        }
    }
}