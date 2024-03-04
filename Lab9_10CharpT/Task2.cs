using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9_10CharpT
{
    public delegate void ConveyorEventHandler(object sender, ConveyorEventArgs e);

    public class ConveyorEventArgs : EventArgs
    {
        public string ConveyorName { get; }
        public double CurrentSpeed { get; }
        public string Message { get; }

        public ConveyorEventArgs(string conveyorName, double currentSpeed, string message)
        {
            ConveyorName = conveyorName;
            CurrentSpeed = currentSpeed;
            Message = message;
        }
    }

    public class Conveyor
    {
        public event ConveyorEventHandler SpeedingUp; // Event for speeding up
        public event ConveyorEventHandler SlowingDown; // Event for slowing down

        private string conveyorName;
        private double currentSpeed;
        private Random random;

        public Conveyor(string name, double initialSpeed)
        {
            conveyorName = name;
            currentSpeed = initialSpeed;
            random = new Random();
        }

        protected virtual void OnSpeedingUp(ConveyorEventArgs e)
        {
            SpeedingUp?.Invoke(this, e);
        }

        protected virtual void OnSlowingDown(ConveyorEventArgs e)
        {
            SlowingDown?.Invoke(this, e);
        }

        public void SimulateConveyorChanges()
        {
            for (int i = 0; i < 5; i++)
            {
                // Simulate random changes in conveyor speed
                double percentageChange = (random.NextDouble() - 0.5) * 2; // From -1 to 1
                currentSpeed *= (1 + percentageChange);

                // Determine whether the speed is increasing or decreasing
                if (percentageChange > 0)
                {
                    OnSpeedingUp(new ConveyorEventArgs(conveyorName, currentSpeed, "Speeding up!"));
                }
                else
                {
                    OnSlowingDown(
                        new ConveyorEventArgs(conveyorName, currentSpeed, "Slowing down!")
                    );
                }

                System.Threading.Thread.Sleep(1000); // Delay for visualization
            }
        }
    }

    public interface IConveyorOperator
    {
        void Subscribe(Conveyor conveyor);
        void Unsubscribe(Conveyor conveyor);

        void HandleConveyorEvent(object sender, ConveyorEventArgs e);
    }

    public class Flash : IConveyorOperator
    {
        public void Subscribe(Conveyor conveyor)
        {
            conveyor.SpeedingUp += HandleConveyorEvent;
        }

        public void Unsubscribe(Conveyor conveyor)
        {
            conveyor.SpeedingUp -= HandleConveyorEvent;
        }

        public void HandleConveyorEvent(object sender, ConveyorEventArgs e)
        {
            Console.WriteLine($"The Flash: {e.ConveyorName}, Speed: {e.CurrentSpeed}, {e.Message}");
        }
    }

    public class Slowpoke : IConveyorOperator
    {
        public void Subscribe(Conveyor conveyor)
        {
            conveyor.SlowingDown += HandleConveyorEvent;
        }

        public void Unsubscribe(Conveyor conveyor)
        {
            conveyor.SlowingDown -= HandleConveyorEvent;
        }

        public void HandleConveyorEvent(object sender, ConveyorEventArgs e)
        {
            Console.WriteLine($"Slowpoke: {e.ConveyorName}, Speed: {e.CurrentSpeed}, {e.Message}");
        }
    }

    public class ProgramTask2
    {
        public static void Task()
        {
            Conveyor conveyor = new Conveyor("Assembly Line", 10.0);
            Flash flash = new Flash();
            Slowpoke slowpoke = new Slowpoke();

            flash.Subscribe(conveyor);
            slowpoke.Subscribe(conveyor);

            conveyor.SimulateConveyorChanges();

            flash.Unsubscribe(conveyor);
            slowpoke.Unsubscribe(conveyor);
        }
    }

    //public delegate void ConveyorEventHandler(object sender, EventArgs e);

    //public class Conveyor
    //{
    //    public event ConveyorEventHandler Start;
    //    public event ConveyorEventHandler Stop;
    //    public event EventHandler Break;

    //    Random random = new Random();

    //    public void Run()
    //    {
    //        Console.WriteLine("Conveyor is running.");
    //        OnStart(EventArgs.Empty);
    //    }

    //    public void Halt()
    //    {
    //        Console.WriteLine("Conveyor is stopping.");
    //        OnStop(EventArgs.Empty);
    //    }

    //    public void BreakDown()
    //    {
    //        Console.WriteLine("Conveyor is broken!");
    //        OnBreak(EventArgs.Empty);
    //    }

    //    // Event invocation methods
    //    protected virtual void OnStart(EventArgs e)
    //    {
    //        if (random.Next(0, 10) < 8) // 80% chance of firing the event
    //            Start?.Invoke(this, e);
    //    }

    //    protected virtual void OnStop(EventArgs e)
    //    {
    //        if (random.Next(0, 10) < 8) // 80% chance of firing the event
    //            Stop?.Invoke(this, e);
    //    }

    //    protected virtual void OnBreak(EventArgs e)
    //    {
    //        if (random.Next(0, 10) < 0) // 30% chance of firing the event
    //            Break?.Invoke(this, e);
    //    }
    //}

    //public class Logger
    //{
    //    public void LogStart(object sender, EventArgs e)
    //    {
    //        Console.WriteLine("Logger: Conveyor started.");
    //    }

    //    public void LogStop(object sender, EventArgs e)
    //    {
    //        Console.WriteLine("Logger: Conveyor stopped.");
    //    }

    //    public void LogBreak(object sender, EventArgs e)
    //    {
    //        Console.WriteLine("Logger: Conveyor broken!");
    //    }
    //}

    //class ProgramTask2
    //{
    //    public static void Task()
    //    {
    //        //Conveyor conveyor = new Conveyor();
    //        //Logger logger = new Logger();

    //        //// Subscribe to events
    //        //conveyor.Start += logger.LogStart;
    //        //conveyor.Stop += logger.LogStop;

    //        //// Simulate conveyor operation
    //        //conveyor.Run();
    //        //Console.WriteLine("Working on the conveyor...");
    //        //conveyor.Halt();

    //        Conveyor conveyor = new Conveyor();
    //        Logger logger = new Logger();

    //        // Subscribe to events
    //        conveyor.Start += logger.LogStart;
    //        conveyor.Stop += logger.LogStop;
    //        conveyor.Break += logger.LogBreak;

    //        // Simulate conveyor operation
    //        Console.WriteLine("Press any key to start the conveyor...");
    //        Console.ReadKey();
    //        conveyor.Run();

    //        Console.WriteLine("Working on the conveyor... Press any key to break it.");
    //        Console.ReadKey();
    //        conveyor.BreakDown();

    //        conveyor.Halt();

    //        Console.WriteLine("Press any key to exit.");
    //        Console.ReadKey();
    //    }
    //}
}
