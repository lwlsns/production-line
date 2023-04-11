namespace ProductionLine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int beltLength = 5;
            int iterations = 100;
            //start the simulation  
            Simulation simulation = new Simulation(beltLength, iterations);
            simulation.Run();
        }
    }
}