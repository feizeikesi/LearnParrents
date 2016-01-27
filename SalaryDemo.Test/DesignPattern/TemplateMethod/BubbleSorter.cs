using System.Diagnostics;

namespace SalaryDemo.Test.DesignPattern.TemplateMethod
{
    public class BubbleSorter
    {
        private static int operations = 0;

        public static int Sort(int[] array)
        {
            for (var i = 0; i < array.Length - 1; i++)
            {
                for (var j = 0; j < array.Length - i - 1; j++)
                {
                    CompareAndSwap(array, j);
                }
            }
            return operations;
        }


        private static void Swap(int[] array, int index)
        {
            var temp = array[index];
            array[index] = array[index + 1];
            array[index + 1] = temp;
        }

        private static void CompareAndSwap(int[] array, int index)
        {
            if (array[index] > array[index + 1])
            {
                Swap(array, index);
                operations++;
            }
        }


        public abstract class TemplateMothed
        {
            protected int Length = 0;

            protected int DoSort()
            {
                for (var i = 0; i < Length - 1; i++)
                {
                    for (var j = 0; j < Length - i - 1; j++)
                    {
                        if (OutOfOrder(j))
                        {
                            Swap(j);
                            operations++;
                        }
                    }
                }
                return operations;
            }

            protected abstract bool OutOfOrder(int index);

            protected abstract void Swap(int index);
        }
    }

    public class IntBubbleSorter:BubbleSorter.TemplateMothed
    {
        private int[] _array = null;

        public int Sort(int[] array)
        {
            _array = array;
            Length = array.Length;
          return DoSort();
        }

        protected override bool OutOfOrder(int index)
        {
            return _array[index] > _array[index + 1];
        }

        protected override void Swap(int index)
        {
            int temp = _array[index];
            _array[index] = _array[index + 1];
            _array[index + 1] = temp;
           
        }
    }
}