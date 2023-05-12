using CalculatorManager.Interfaces;

namespace CalculatorManager
{
    public class OtherFeatures : IOtherFeatures
    {
        public async Task DoSomething()
        {
            await Task.Delay(1000);
            Console.WriteLine($"{DateTime.Now} Done...");
        }
    }
}
