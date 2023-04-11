using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionLine
{
    internal class Simulation
    {
        private Worker[] workers;
        private ConveyorBelt belt;
        private int simulationLength;
        private int productCount = 0;
        private int missedComponentACount = 0;
        private int missedComponentBCount = 0;
        private int iteration = 0;

        public Simulation(int beltLength, int simulationLength)
        {
            this.simulationLength = simulationLength;
            IRandomGenerator randomGenerator = new DefaultRandomGenerator();
            belt = new ConveyorBelt(beltLength, randomGenerator);
            workers = new Worker[beltLength*2];
            //create workers and assign two to each slot of the belt
            for (int i = 0; i < workers.Length; i++)
            {
                workers[i] = new Worker(belt, i % beltLength, i);
            }

        }
        public void Run()
        {
            for (iteration = 0; iteration < simulationLength; iteration++)
            {
                // Move the conveyor
                belt.MoveBelt();

                // Update the workers
                UpdateWorkers();   
                
                // Count the items coming off the belt
                CountItems();

                // Print the conveyor belt state
                PrintBelt(belt.GetBelt());
                PrintWorkers();
                Thread.Sleep(100);
            }
        }

        // Count the number of products and missed components at the end of the belt
        private void CountItems()
        {
            char? lastBeltSlotItem = belt.GetBelt()[belt.Length -1];

            switch (lastBeltSlotItem)
            {
                case 'A':
                    missedComponentACount++;
                    return;
                case 'B':
                    missedComponentBCount++;
                    return;
                case 'P':
                    productCount++;
                    return;
                default:
                    return;
            }

        }

        private void UpdateWorkers()
        {
            for (int i = 0; i < workers.Length; i++)
            {   
                workers[i].Update();
            }
        }

        // Print the simulation
        private void PrintBelt(char?[] belt)
        {
            Console.SetCursorPosition(0, Console.WindowTop);
            Console.WriteLine("  v   v   v   v   v  ");
            Console.WriteLine("---------------------");
            Console.Write("|");
            for (int i = 0; i < belt.Length; i++)
            {
                char? component = belt[i];
                if (component == null)
                {
                    component = ' ';
                }
                Console.Write($" {component} |");
            }
            Console.Write('\n');
            Console.WriteLine("---------------------");
            Console.WriteLine("  ^   ^   ^   ^   ^  ");
            Console.WriteLine($"Iteration:{iteration}");
            Console.WriteLine($"Product Count:{productCount}");
            Console.WriteLine($"Component 'A':{missedComponentACount}");
            Console.WriteLine($"Component 'B':{missedComponentBCount}");
        }

        private void PrintWorkers()
        {
            for (int i = 0; i < workers.Length; i++)
            {
                Console.Write($"Worker {i} has " );
                workers[i].PrintHands();
                Console.WriteLine();
            }
        }
    }
}
