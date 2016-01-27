using System;

namespace SalaryDemo.Test.DesignPattern.ActiveObject
{
    public class SleepCommand:ICommand
    {
        private long _sleepTime;
        private ActionObjectEngine _action;
        private ICommand _cmd;
        private DateTime _startTime;
        private bool _started = false;
        public SleepCommand(long sleepTime,ActionObjectEngine action,ICommand cmd)
        {
            this._sleepTime = sleepTime;
            this._action = action;
            this._cmd = cmd;
        }



        public void Execute()
        {
            DateTime currentTime=DateTime.Now;

            if (!_started)
            {
                _started = true;
                _startTime = currentTime;
                _action.AddCommand(this);
            }
            else
            {
                _action.AddCommand((currentTime - _startTime).TotalMilliseconds < _sleepTime ? this : _cmd);
            }
        }
    }
}