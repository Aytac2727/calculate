using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private Button btn;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtDisplay.Text);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            previousOperations = Operations.None;
            txtDisplay.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text.Length > 0)
            {
                double d;
                if(! double.TryParse(txtDisplay.Text[txtDisplay.Text.Length-1].ToString(),out d))
                {
                    previousOperations = Operations.None;
                }
                
                txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.Text.Length - 1, 1);
            }
        }

        private void btnNum_Click(object btn, EventArgs e)
        {
            txtDisplay.Text += (btn as Button).Text;
        }

        enum Operations
        {
            Add,
            Sub,
            Mut,
            Div,
            None
        }
        
        static Operations previousOperations = Operations.None;

        private void btnDiv_Click(object sender, EventArgs e)
        {
            if (previousOperations != Operations.None)
            {
                PerformCalculation(previousOperations);
                
            }         
            previousOperations = Operations.Div;         
            txtDisplay.Text += (sender as Button).Text;
        }

        private void btnMut_Click(object sender, EventArgs e)
        {
            if (previousOperations != Operations.None)
            {
                PerformCalculation(previousOperations);
               
            }
           
            previousOperations = Operations.Mut;         
            txtDisplay.Text += (sender as Button).Text;
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            if (previousOperations != Operations.None)
            {
                PerformCalculation(previousOperations);
            }
            
            previousOperations = Operations.Sub;             
            txtDisplay.Text += (sender as Button).Text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (previousOperations != Operations.None)
            {
                PerformCalculation(previousOperations);
                
            }
            previousOperations = Operations.Add;
            txtDisplay.Text += (sender as Button).Text;
        }

        private void PerformCalculation(Operations previousOperations)
        {
            List<double> lstNums = new List<double>();
            switch (previousOperations)
            {
                case Operations.Add:
                    lstNums = txtDisplay.Text.Split('+').Select(double.Parse).ToList();
                    txtDisplay.Text = (lstNums[0] + lstNums[1]).ToString();
                    break;
                case Operations.Sub:
                    lstNums = txtDisplay.Text.Split('-').Select(double.Parse).ToList();
                    txtDisplay.Text = (lstNums[0] - lstNums[1]).ToString();
                    break;
                case Operations.Mut:
                    lstNums = txtDisplay.Text.Split('*').Select(double.Parse).ToList();
                    txtDisplay.Text = (lstNums[0] * lstNums[1]).ToString();
                    break;
                case Operations.Div:
                    try
                    {
                        lstNums = txtDisplay.Text.Split('/').Select(double.Parse).ToList();
                        txtDisplay.Text = (lstNums[0] / lstNums[1]).ToString();
                    }
                    catch (DivideByZeroException)
                    {

                        txtDisplay.Text = "EEEEEEE";
                    }
                    break;

                case Operations.None:
                    break;

                default:
                    break;
            }
        }

        private void btnRes_Click(object sender, EventArgs e)
        {
            if (previousOperations == Operations.None)
                return;
            else
            {
                PerformCalculation(previousOperations);
            }
          
        }
    }
}
