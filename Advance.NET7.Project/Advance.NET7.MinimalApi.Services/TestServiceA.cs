using Advance.NET7.MinimalApi.Interfaces;

namespace Advance.NET7.MinimalApi.Services
{
    public class TestServiceA:ITestServiceA
    {
        public TestServiceA() 
        {
            Console.WriteLine($"{GetType().Name}被构造了~~");
        }

        public string ShowA()
        {
            return $"This is from {GetType().FullName} ShowA";
        }
    }
}