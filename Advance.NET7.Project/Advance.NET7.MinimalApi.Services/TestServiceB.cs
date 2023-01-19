using Advance.NET7.MinimalApi.Interfaces;

namespace Advance.NET7.MinimalApi.Services
{
    public class TestServiceB : ITestServiceB
    {
        private ITestServiceA _testServiceA { get; set; }

        public TestServiceB(ITestServiceA testServiceA)
        {
            _testServiceA = testServiceA;
            Console.WriteLine($"{GetType().Name}被构造~~");
        }

        public string ShowB()
        {
            return $"This is from {GetType().FullName} ShowB 调用 {_testServiceA.ShowA()}";
        }
    }
}