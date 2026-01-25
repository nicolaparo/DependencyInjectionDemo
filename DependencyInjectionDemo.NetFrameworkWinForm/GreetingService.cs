using System.Windows.Forms;

namespace DependencyInjectionDemo.NetFrameworkWinForm
{
    public class GreetingService
    {
        public void Greet(string name)
        {
            MessageBox.Show($"Hello, {name}!");
        }
    }
}
