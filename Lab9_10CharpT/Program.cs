using System.Threading.Tasks;
using Lab9_10CharpT;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("=========================================================");
            Console.WriteLine("Select a Task:");
            Console.WriteLine("1. Task 1");
            Console.WriteLine("2. Task 2");
            Console.WriteLine("3. Exit");

            Console.Write("Enter your choice: ");
            string? choice = Console.ReadLine();
            Console.WriteLine("---------------------------------------------------------");

            switch (choice)
            {
                case "1":
                    VectorDecimal.Task();
                    break;

                case "2":
                    ProgramTask2.Task();
                    break;

                case "3":
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }
}
