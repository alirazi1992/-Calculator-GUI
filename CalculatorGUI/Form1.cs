using System;
using System.Windows.Forms;

namespace CalculatorGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Ensure operator items exist (in case designer items were not added)
            if (cmbOp.Items.Count == 0)
            {
                cmbOp.Items.AddRange(new object[] { "+", "-", "*", "/" });
            }
            if (cmbOp.SelectedIndex < 0 && cmbOp.Items.Count > 0)
            {
                cmbOp.SelectedIndex = 0;
            }

            // If you didn't wire events via the ⚡ tab, uncomment these:
            // this.btnCalc.Click += btnCalc_Click;
            // this.btnClear.Click += btnClear_Click;
            // this.cmbOp.SelectedIndexChanged += cmbOp_SelectedIndexChanged;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            double a, b;

            if (!double.TryParse(txtA.Text, out a))
            {
                MessageBox.Show("Please enter a valid number for A.", "Invalid Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtA.Focus();
                return;
            }

            if (!double.TryParse(txtB.Text, out b))
            {
                MessageBox.Show("Please enter a valid number for B.", "Invalid Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtB.Focus();
                return;
            }

            if (cmbOp.SelectedItem == null)
            {
                MessageBox.Show("Please choose an operator.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string op = cmbOp.SelectedItem.ToString();
            double result;

            try
            {
                if (op == "+")
                {
                    result = a + b;
                }
                else if (op == "-")
                {
                    result = a - b;
                }
                else if (op == "*")
                {
                    result = a * b;
                }
                else if (op == "/")
                {
                    if (b == 0)
                        throw new DivideByZeroException();
                    result = a / b;
                }
                else
                {
                    throw new InvalidOperationException("Unknown operator");
                }

                lblResult.Text = "Result: " + result;
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("Cannot divide by zero.", "Math Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblResult.Text = "Result: —";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblResult.Text = "Result: —";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtA.Clear();
            txtB.Clear();
            if (cmbOp.Items.Count > 0) cmbOp.SelectedIndex = 0;
            lblResult.Text = "Result: —";
            txtA.Focus();
        }

        private void cmbOp_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optional: auto-calc when operator changes
            if (!string.IsNullOrWhiteSpace(txtA.Text) &&
                !string.IsNullOrWhiteSpace(txtB.Text))
            {
                btnCalc.PerformClick();
            }
        }
    }
}
