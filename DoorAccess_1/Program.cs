using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoorAccess_1
{
    internal static class Program
    {
        private static object AppRole;

        [STAThread]
        static void Main()
        {

            Application.Run(new StartForm());


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

        }
    }
}
