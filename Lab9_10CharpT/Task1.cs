using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9_10CharpT
{
    public class VectorDecimalException : Exception
    {
        public VectorDecimalException(string message)
            : base(message) { }

        public VectorDecimalException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class VectorArrayTypeMismatchException : VectorDecimalException
    {
        public VectorArrayTypeMismatchException(string message)
            : base(message) { }
    }

    public class VectorDivideByZeroException : VectorDecimalException
    {
        public VectorDivideByZeroException(string message)
            : base(message) { }
    }

    public class VectorIndexOutOfRangeException : VectorDecimalException
    {
        public VectorIndexOutOfRangeException(string message)
            : base(message) { }
    }

    public class VectorInvalidCastException : VectorDecimalException
    {
        public VectorInvalidCastException(string message)
            : base(message) { }
    }

    public class VectorOutOfMemoryException : VectorDecimalException
    {
        public VectorOutOfMemoryException(string message)
            : base(message) { }
    }

    public class VectorOverflowException : VectorDecimalException
    {
        public VectorOverflowException(string message)
            : base(message) { }

        public VectorOverflowException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class VectorStackOverflowException : VectorDecimalException
    {
        public VectorStackOverflowException(string message)
            : base(message) { }
    }

    public class VectorDecimal
    {
        protected decimal[] ArrayDecimal;
        protected uint num;
        protected int codeError;
        protected static uint num_vec;

        public VectorDecimal()
        {
            ArrayDecimal = new decimal[1];
            num = 1;
            codeError = 0;
            num_vec++;
        }

        public VectorDecimal(uint size)
        {
            ArrayDecimal = new decimal[size];
            num = size;
            codeError = 0;
            num_vec++;
        }

        public VectorDecimal(uint size, decimal initValue)
        {
            ArrayDecimal = new decimal[size];
            num = size;
            codeError = 0;

            for (uint i = 0; i < size; i++)
            {
                ArrayDecimal[i] = initValue;
            }

            num_vec++;
        }

        ~VectorDecimal()
        {
            Console.WriteLine($"Destructor: {this}");
        }

        public void Input()
        {
            try
            {
                for (uint i = 0; i < num; i++)
                {
                    Console.Write($"Enter element at index {i}: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal value))
                    {
                        ArrayDecimal[i] = checked(value); // Check for overflow
                    }
                    else
                    {
                        throw new VectorInvalidCastException("Invalid input format.");
                    }
                }
            }
            catch (OverflowException ex)
            {
                throw new VectorOverflowException("Overflow occurred during input.", ex);
            }
        }

        public decimal this[uint index]
        {
            get
            {
                try
                {
                    if (index < 0 || index >= num)
                    {
                        throw new VectorIndexOutOfRangeException($"Index {index} is out of range.");
                    }

                    return ArrayDecimal[index];
                }
                catch (Exception ex)
                {
                    throw new VectorDecimalException("Error accessing vector element.", ex);
                }
            }
            set
            {
                try
                {
                    if (index < 0 || index >= num)
                    {
                        throw new VectorIndexOutOfRangeException($"Index {index} is out of range.");
                    }

                    ArrayDecimal[index] = checked(value); // Check for overflow
                }
                catch (OverflowException ex)
                {
                    throw new VectorOverflowException("Overflow occurred during assignment.", ex);
                }
                catch (Exception ex)
                {
                    throw new VectorDecimalException(
                        "Error assigning value to vector element.",
                        ex
                    );
                }
            }
        }

        public void Display()
        {
            Console.Write("Vector elements: ");
            for (uint i = 0; i < num; i++)
            {
                Console.Write($"{ArrayDecimal[i]} ");
            }
            Console.WriteLine();
        }

        public void AssignValue(decimal value)
        {
            for (uint i = 0; i < num; i++)
            {
                ArrayDecimal[i] = value;
            }
        }

        public static uint CountVectors()
        {
            return num_vec;
        }

        public int Dimension
        {
            get { return (int)num; }
        }

        public int CodeError
        {
            get { return codeError; }
            set { codeError = value; }
        }

        public static void Task()
        {
            try
            {
                Console.WriteLine("Creating vector 1");
                VectorDecimal vector1 = new VectorDecimal(3, 10);
                Console.WriteLine($"Vector 1 created. Dimension: {vector1.Dimension}");

                Console.WriteLine("\nCreating vector 2");
                VectorDecimal vector2 = new VectorDecimal(2, 5);
                Console.WriteLine($"Vector 2 created. Dimension: {vector2.Dimension}");

                Console.WriteLine("\nVector 1 Elements:");
                vector1.Display();

                Console.WriteLine("\nVector 2 Elements:");
                vector2.Display();

                Console.WriteLine("\nAssigning value to Vector 1");
                vector1.AssignValue(7);
                Console.WriteLine("Vector 1 Elements after assignment:");
                vector1.Display();

                Console.WriteLine("\nAccessing Vector 2 Element at Index 3");
                decimal value = vector2[3];
                Console.WriteLine($"Value at index 3: {value}");

                Console.WriteLine("\nTrying to access Vector 1 Element at Index 5");
                value = vector1[5]; // This will throw VectorIndexOutOfRangeException
            }
            catch (VectorDecimalException ex)
            {
                Console.WriteLine($"Exception: {ex.GetType().Name}\nMessage: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine(
                        $"Inner Exception: {ex.InnerException.GetType().Name}\nInner Exception Message: {ex.InnerException.Message}"
                    );
                }
            }

            Console.WriteLine($"\nTotal Vectors created: {VectorDecimal.CountVectors()}");
        }
    }
}
