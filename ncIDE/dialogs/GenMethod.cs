using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ncIDE.dialogs
{
    public partial class GenMethod : Form
    {
        public GenMethod()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.None;
        }

        public string Gen
        {
            set
            {
                label3.Text = value + " Name:";
                Text = "Generate " + value;
            }
        }

        public string AccessLvl
        {
            get
            {
                return comboBox1.Items[comboBox1.SelectedIndex] as string;
            }
        }

        public string ReturnType
        {
            get
            {
                return textBox2.Text;
            }
        }

        public string MethodName
        {
            get
            {
                return textBox1.Text;
            }
        }

        public bool IsStatic
        {
            get
            {
                return checkBox1.Checked;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void GenMethod_Load(object sender, EventArgs e)
        {

        }
    }
}
