namespace SalaryDemo.Test.DesignPattern.Strategy
{
    public class ApplicationRunner
    {
        private IApplication _application = null;
        public ApplicationRunner(IApplication application)
        {
            _application = application;
        }

        public void Run()
        {
            _application. Init();
            while (!_application.Done())
            {
                _application.Idle();
            }
            _application.Cleanup();
        }
    }
}