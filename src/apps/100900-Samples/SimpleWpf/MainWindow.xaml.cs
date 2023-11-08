using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// MainWindow() Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            var result = new string[] { "Bob", "Ned", "Amy", "Bill" }.All(n => n.StartsWith("B"));
            result = new string[] { "Bob", "Ned", "Amy", "Bill" }.Any(n => n.StartsWith("B"));

        }
        /// <summary>
        /// Sample_Aggregate_Lambda_Simple() Method.
        /// </summary>
        private static void Sample_Aggregate_Lambda_Simple()
        {
            //Now is the time for all good people to come to the aid of their country!
            var result = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.Aggregate((a, b) => a * b);

            Console.WriteLine("Aggregated numbers by multiplication:");
            Debug.WriteLine(result);
        }
        /// <summary>
        /// Sample_Distinct_Lambda() Method.
        /// </summary>
        static void Sample_Distinct_Lambda()
        {
            int[] numbers = { 1, 2, 2, 3, 5, 6, 6, 6, 8, 9 };

            var result = numbers.Distinct();

            Console.WriteLine("Distinct removes duplicate elements:");
            foreach (int number in result)
                Console.WriteLine(number);
        }
        static void Sample_Except_Linq()
        {
            int[] numbers1 = { 1, 2, 3 };
            int[] numbers2 = { 3, 4, 5 };

            var result = (from n in numbers1.Except(numbers2)
                          select n);

            Debug.WriteLine("Except creates a single sequence from numbers1 and removes the duplicates found in numbers2:");
            foreach (int number in result)
                Debug.WriteLine(number);
        }
        static void Sample_ElementAt_Lambda()
        {

            string[] words = { "One", "Two", "Three" };

            var result = words.ElementAt(1);

            Debug.WriteLine("Element at index 1 in the array is:");
            Debug.WriteLine(result);
        }
        static void Sample_Where_Lambda_Indexed()
        {
            var result = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.Where((n, i) => n % 3 == 0 && i >= 5);

            Debug.WriteLine("Numbers divisible by 3 and indexed >= 5:");
            foreach (var number in result)
                Debug.WriteLine(number);
        }
        static void Sample_OrderBy_Lambda_Dates()
        {
            //This works now!
            var dates = new DateTime[] {
                new DateTime(2015, 2, 15),
                new DateTime(2015, 3, 25),
                new DateTime(2015, 1, 5)
            };

            var results = dates.OrderBy(d => d);

            Debug.WriteLine("Ordered list of dates:");
            foreach (DateTime dt in results)
                Debug.WriteLine(dt.ToString("yyyy/MM/dd"));
        }
        static void Sample_Where_Linq_Numbers()
        {
            int[] numbers = { 5, 10, 15, 20, 25, 30 };

            var results = from n in numbers
                          where n >= 15 && n <= 25
                          select n;

            Debug.WriteLine("Numbers being >= 15 and <= 25:");
            foreach (var number in results)
                Debug.WriteLine(number);
        }
        static void Sample_Zip_Lambda()
        {
            int[] numbers1 = { 1, 2, 3 };
            int[] numbers2 = { 10, 11, 12 };

            var result = numbers1.Zip(numbers2, (a, b) => (a * b));

            Debug.WriteLine("Using Zip to combine two arrays into one (1*10, 2*11, 3*12):");
            foreach (int number in result)
                Debug.WriteLine(number);
        }
        static void Sample_Union_Lambda()
        {
            int[] numbers1 = { 1, 2, 3 };
            int[] numbers2 = { 3, 4, 5 };

            var results = numbers1.Union(numbers2);

            Debug.WriteLine("Union creates a single sequence and eliminates the duplicates:");
            foreach (int number in results)
                Debug.WriteLine(number);
        }

        private void EnterDebuggerBreak_Click(object sender, RoutedEventArgs e)
        {
            Debugger.Break();
        }

        private void ThrowExceptionHandled_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                throw new Exception("Here we go");
            }
            catch (Exception excption)
            {

                throw;
            }

        }

        private void ThrowExceptionUnhandled_Click(object sender, RoutedEventArgs e)
        {
            throw new Exception("Here we go");
        }
    }
}