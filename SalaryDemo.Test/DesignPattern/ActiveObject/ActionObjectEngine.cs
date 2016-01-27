using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryDemo.Test.DesignPattern.ActiveObject
{
   public class ActionObjectEngine
    {
       List<ICommand> _itsCommands=new List<ICommand>();

       public void AddCommand(ICommand cmd)
       {
           _itsCommands.Add(cmd);
       }

       public void Run()
       {
           while (_itsCommands.Count>0)
           {
               ICommand cmd = _itsCommands[0];
               _itsCommands.Remove(cmd);
               cmd.Execute();
           }
       }
    }
}
