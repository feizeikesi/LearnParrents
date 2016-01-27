using System.Security.Cryptography.X509Certificates;

namespace SalaryDemo.Test.DesignPattern.Strategy
{
    public interface IApplication
    {
        void Init();
        void Idle();
        void Cleanup();
        bool Done();
    }
}