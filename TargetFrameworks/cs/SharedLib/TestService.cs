using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Xarial.XCad.Samples.SharedLib
{
    public class TestService
    {
        public void SayHello(string name) 
        {
            MessageBox.Show($"Hello {name} from TestService v2.0.0");
        }
    }
}
