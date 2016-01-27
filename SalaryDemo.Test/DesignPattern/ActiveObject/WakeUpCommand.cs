namespace SalaryDemo.Test.DesignPattern.ActiveObject
{
    public class  WakeUpCommand:ICommand
    {
        private bool _executed = false;

        public bool Executed
        {
            get { return _executed; }
            set { _executed = value; }
        }

        public void Execute()
        {
            _executed = true;
        }
    }
}