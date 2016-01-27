namespace SalaryDemo.Test.DesignPattern.Monostate.V1
{

    /*
 * 旋转门(Turnstile)开始时处于Locked状态，如果投入一枚硬币（Coin），它就迁移到UnLocked状态，
 * 开启旋转门，重置可能出现的任何警告（Alarm）状态，并把硬币放到投币箱（Deposit）中
 * 如果此时乘客通过（Pass）了旋转门，旋转门就迁移回Locked状态并且把门锁上
 * 如果乘客在通过旋转门投入过多的硬币，则多余的硬币被退还（Refund），并且旋转门保持在Unlocked状态
 * 如果乘客没有投币就想通过旋转门，那么会发出警报，并且旋转门保持Locked状态
 */

   public class Turnstile
   {
       protected bool locked = true;//旋转门(Turnstile)开始时处于Locked状态，false为UnLocked状态，
       protected bool alerted = false;//默认旋转门不发出警告，true为警告
       protected int _Coins = 0;//投币箱内硬币数

       public int Coins
       {
           get { return _Coins; }
       }

       protected int _Refunds =0;
       public int Refunds
       {
           get { return _Refunds; }
       }

       public void Coin()
       {
           
           if (locked)
           {
               _Coins += 1;
               _Refunds = 0;
           }
           else
           {
               _Refunds += 1;
           }

           locked = false;
           alerted = false;
       }

       //如果此时乘客通过（Pass）了旋转门，旋转门就迁移回Locked状态并且把门锁上
       public void Pass()
       {
           if (_Coins > 0 && _Refunds == 0)
           {
               locked = true;
               alerted = false;
           }
           else
           {
               
               locked = true;
               alerted = true;
           }
           
          // _Coins = 0;
       }

       //重置
       public void Reset()
       {
           locked = true;
           alerted = false;
       }


       public bool Locked()
       {
           return locked;
       }

       public bool Alarm()
       {
           return alerted;
       }
   }
}
