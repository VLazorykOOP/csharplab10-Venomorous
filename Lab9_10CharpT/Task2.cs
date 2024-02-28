using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9_10CharpT
{
    public delegate void ConveyorEventHandler(object sender, EventArgs e);

    public class Conveyor
    {
        public event ConveyorEventHandler Start;
        public event ConveyorEventHandler Stop;
        public event EventHandler Break;

        Random random = new Random();

        public void Run()
        {
            Console.WriteLine("Conveyor is running.");
            OnStart(EventArgs.Empty);
        }

        public void Halt()
        {
            Console.WriteLine("Conveyor is stopping.");
            OnStop(EventArgs.Empty);
        }

        public void BreakDown()
        {
            Console.WriteLine("Conveyor is broken!");
            OnBreak(EventArgs.Empty);
        }

        // Event invocation methods
        protected virtual void OnStart(EventArgs e)
        {
            if (random.Next(0, 10) < 8) // 80% chance of firing the event
                Start?.Invoke(this, e);
        }

        protected virtual void OnStop(EventArgs e)
        {
            if (random.Next(0, 10) < 8) // 80% chance of firing the event
                Stop?.Invoke(this, e);
        }

        protected virtual void OnBreak(EventArgs e)
        {
            if (random.Next(0, 10) < 0) // 30% chance of firing the event
                Break?.Invoke(this, e);
        }
    }

    public class Logger
    {
        public void LogStart(object sender, EventArgs e)
        {
            Console.WriteLine("Logger: Conveyor started.");
        }

        public void LogStop(object sender, EventArgs e)
        {
            Console.WriteLine("Logger: Conveyor stopped.");
        }

        public void LogBreak(object sender, EventArgs e)
        {
            Console.WriteLine("Logger: Conveyor broken!");
        }
    }

    class ProgramTask2
    {
        public static void Task()
        {
            //Conveyor conveyor = new Conveyor();
            //Logger logger = new Logger();

            //// Subscribe to events
            //conveyor.Start += logger.LogStart;
            //conveyor.Stop += logger.LogStop;

            //// Simulate conveyor operation
            //conveyor.Run();
            //Console.WriteLine("Working on the conveyor...");
            //conveyor.Halt();

            Conveyor conveyor = new Conveyor();
            Logger logger = new Logger();

            // Subscribe to events
            conveyor.Start += logger.LogStart;
            conveyor.Stop += logger.LogStop;
            conveyor.Break += logger.LogBreak;

            // Simulate conveyor operation
            Console.WriteLine("Press any key to start the conveyor...");
            Console.ReadKey();
            conveyor.Run();

            Console.WriteLine("Working on the conveyor... Press any key to break it.");
            Console.ReadKey();
            conveyor.BreakDown();

            conveyor.Halt();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
