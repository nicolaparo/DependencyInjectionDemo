using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DependencyInjectionDemo.NetFrameworkWinForm
{
    public partial class Form2 : Form
    {
        private readonly Form1 form1;
        private readonly GreetingService greetingService;

        public Form2(Form1 form1, GreetingService greetingService)
        {
            InitializeComponent();
            this.form1 = form1;
            this.greetingService = greetingService;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            greetingService.Greet("User");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form1.Show();
        }
    }
}
