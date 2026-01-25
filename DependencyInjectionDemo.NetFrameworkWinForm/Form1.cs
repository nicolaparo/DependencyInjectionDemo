using Microsoft.Extensions.DependencyInjection;
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
    public partial class Form1 : Form
    {
        private readonly IServiceProvider serviceProvider;

        public Form1(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = serviceProvider.GetRequiredService<Form2>();
            form.Show();
        }
    }
}
