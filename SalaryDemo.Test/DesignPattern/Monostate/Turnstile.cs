using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryDemo.Test.DesignPattern.Monostate
{
    /*
     * 
     */
    public class Turnstile
    {
        /// <summary>
        /// 锁住
        /// </summary>
        private static bool _isLocked = true;
        /// <summary>
        /// 警报
        /// </summary>
        private static bool _isAlarming = false;

        private static int _itsCoins = 0;
        private static int _itsRefunds = 0;

        protected static readonly Turnstile LOCKED = new Locked();
        protected static readonly Turnstile UNLOCKED = new UnLocked();

        protected static Turnstile ItsState = LOCKED;


        public int Coins
        {
            get { return _itsCoins; }
        }

        public int Refunds
        {
            get { return _itsRefunds; }
        }


        public void Reset()
        {
            Lock(true);
            Alarm(false);
            _itsCoins = 0;
            _itsRefunds = 0;
            ItsState = LOCKED;
        }

        public void Lock(bool shouldLock)
        {
            _isLocked = shouldLock;
        }
        public void Alarm(bool shouldAlarm)
        {
            _isAlarming = shouldAlarm;
        }
        public bool Locked()
        {
           return _isLocked; 
        }

        public  bool Alarm()
        {
           return _isAlarming;
        }

        /// <summary>
        /// 投币发生在Locked 和UnLocked状态，两个状态都有不同的处理
        /// </summary>
        public virtual  void Coin()
        {
            ItsState.Coin();
        }

        public virtual void Pass()
        {
             ItsState.Pass();
        }

        public void Deposit()
        {
            _itsCoins++;
        }

        public void Refund()
        {
            _itsRefunds++;
        }
    }

    public class Locked : Turnstile
    {
        
        public override void Coin()
        {
            /*Locked状态，如果投入一枚硬币（Coin)
             *它就迁移到UnLocked状态
             *重置可能出现的任何警告（Alarm）状态
             *硬币放到投币箱（Deposit）中
             */
            ItsState = UNLOCKED;
            Lock(false);
            Alarm(false);
            Deposit();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Pass()
        {
            //如果乘客没有投币就想通过旋转门，那么会发出警报，并且旋转门保持Locked状态
            Alarm(true);
        }
    }

    public class UnLocked : Turnstile
    {
        public override void Coin()
        {
         // 如果乘客在通过旋转门投入过多的硬币，则多余的硬币被退还（Refund），并且旋转门保持在Unlocked状态
          Refund();
        }

        public override void Pass()
        {
            Lock(true);
            ItsState = LOCKED; 
        }
    }
}
