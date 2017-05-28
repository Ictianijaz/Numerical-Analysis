using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace NA_PROJECT
{
    public partial class project : Form
    {
        int num_of_term;
        List<double> allX = new List<double>();
        List<double> allY = new List<double>();
        static int count_x = 0;
        static int count_y = 0;
        public project()
        {
            InitializeComponent();
            num_of_term = 2;
            //
            //lagrange
            //
            lagrange_lable_1.Visible = false;
            lagrange_lable_3.Visible = false;
            lagrange_lable_5.Visible = false;
            lagrange_resultant_value_of_y.Visible = false;
            lagrange_value_of_x.Visible = false;
            lagrange_calculate_f.Visible = false;
            //
            //newtonDD
            //
            newtonDD_result_label.Visible = false;
            newtonDD_resultant_value_of_y_label.Visible = false;
            newtonDD_value_of_y_label.Visible = false;
            newtonDD_value_of_x_to_be_interpolate_label.Visible = false;
            newtonDD_value_of_x_to_be_interpolate_numeric_up_down.Visible = false;
            newtonDD_calculate_fx_btn.Visible = false;
            //
            //bisection
            //
            bisection_resultant_label.Visible = false;
            bisection_resultant_root_label.Visible = false;
            //
            //regula falsi
            //
            regula_falsi_resultant_label.Visible = false;
            regula_falsi_resultant_root_label.Visible = false;
            //
            //iterative
            //
            iterative_resultant_label.Visible = false;
            iterative_resultant_root_label.Visible = false;
            iterative_function_list_box.Items.Add("x^3+x^2-1");
            iterative_function_list_box.Items.Add("x^3-2x+5");
            iterative_function_list_box.Items.Add("x^3-9x+1");
            iterative_function_list_box.Items.Add("x^3+x^2-x-5");
            iterative_function_list_box.Items.Add("x^3-3x-5");
            iterative_function_list_box.Items.Add("x^3+x^2-100");
            iterative_function_list_box.Items.Add("2x-log(x)-7");
            iterative_function_list_box.Items.Add("x-sin(x)+1/2");
            iterative_function_list_box.Items.Add("2^x-x-3");
            iterative_function_list_box.Items.Add("cos(x)-3x+1");
            //
            //newton Raphson
            //
            newton_resultant_lebel.Visible = false;
            newton_resultant_root_label.Visible = false;
        }
       
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void lagrange_no_of_term_ValueChanged(object sender, EventArgs e)
        {
            num_of_term = (int)lagrange_no_of_term.Value;
            count_x = 0;
            count_y = 0;
            allX.Clear();
            allY.Clear();
            lagrange_x_list_box.Items.Clear();
            lagrange_y_list_box.Items.Clear();
        }

        private void lagrange_add_x_Click(object sender, EventArgs e)
        {

            if (count_x < num_of_term && check_value((double)lagrange_x_value.Value))
            {
                allX.Add((double)lagrange_x_value.Value);
                lagrange_x_list_box.Items.Add(lagrange_x_value.Value);
                count_x++;
            }
            else
            {
                MessageBox.Show("Can't add value! It's because, either total values exceed number of terms or invalid order", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (count_x == num_of_term)
            {
                lagrange_value_of_x.Visible = true;
                lagrange_lable_1.Visible = true;
                lagrange_calculate_f.Visible = true;
            }
        }
        bool check_value(double value)
        {

            if (count_x <= 1)
            {
                if (count_x == 0)
                    return true;
                else if (allX.Contains(value))
                    return false;
                else
                    return true;
            }
            if (allX[0] > allX[1])
            {
                if (allX[allX.Count - 1] < value)
                    return false;
                else
                    return true;
            }
            else if (allX[0] < allX[1])
            {
                if (allX[allX.Count - 1] > value)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }
        private void lagrange_add_y_Click(object sender, EventArgs e)
        {
            if (count_y < num_of_term)
            {
                allY.Add((double)lagrange_y_value.Value);
                lagrange_y_list_box.Items.Add(lagrange_y_value.Value);
                count_y++;
            }
            else
            {
                MessageBox.Show("Can't add value! It's because total values exceed number of terms", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lagrange_delete_x_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(lagrange_x_list_box);
            selectedItems = lagrange_x_list_box.SelectedItems;

            if (lagrange_x_list_box.SelectedIndex != -1)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                {
                    object x = selectedItems[i];
                    lagrange_x_list_box.Items.Remove(x);
                    allX.Remove(Convert.ToDouble(x));
                    count_x--;
                }
            }
            else
                MessageBox.Show("No value is selected!");
        }

        private void lagrange_delete_y_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(lagrange_y_list_box);
            selectedItems = lagrange_y_list_box.SelectedItems;

            if (lagrange_y_list_box.SelectedIndex != -1)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                {
                    object x = selectedItems[i];
                    lagrange_y_list_box.Items.Remove(x);
                    allY.Remove(Convert.ToDouble(x));
                    count_y--;
                }
            }
            else
                MessageBox.Show("No value is selected!");     
        }

        private void lagrange_calculate_f_Click(object sender, EventArgs e)
        {
            if (allX.Count == num_of_term && allY.Count == num_of_term)
            {
                double value_of_inter = (double)lagrange_value_of_x.Value;
                double result = logic.calculate_lagrange_value(allX, allY, value_of_inter);
                lagrange_lable_3.Visible = true;
                lagrange_lable_5.Visible = true;
                lagrange_resultant_value_of_y.Text = result.ToString();
                lagrange_resultant_value_of_y.Visible = true;
            }
            else
            {
                MessageBox.Show("Total values of x or y are not equal to the number of terms!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tab_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            count_x = 0;
            count_y = 0;
            allX.Clear();
            allY.Clear();
            //
            //lagrange
            //
            lagrange_lable_1.Visible = false;
            lagrange_lable_3.Visible = false;
            lagrange_lable_5.Visible = false;
            lagrange_resultant_value_of_y.Visible = false;
            lagrange_value_of_x.Visible = false;
            lagrange_calculate_f.Visible = false;
            lagrange_no_of_term.Value = 2;
            lagrange_resultant_value_of_y.Text = "";
            lagrange_value_of_x.Value = 0;
            lagrange_x_list_box.Items.Clear();
            lagrange_x_value.Value = 0;
            lagrange_y_list_box.Items.Clear();
            lagrange_y_value.Value = 0;
            //
            //bisection
            //
            bisection_resultant_label.Visible = false;
            bisection_resultant_root_label.Visible = false;
            bisection_decimal_places_numeric_up_down.Value = 0;
            bisection_function_text_box.Text = "";
            //
            //regula falsi
            //
            regula_falsi_resultant_label.Visible = false;
            regula_falsi_resultant_root_label.Visible = false;
            regula_falsi_decimal_places_numeric_up_down.Value = 0;
            regula_falsi_function_text_box.Text = "";
            //
            //iterative
            //
            iterative_resultant_label.Visible = false;
            iterative_resultant_root_label.Visible = false;
            iterative_decimal_places_numeric_up_down.Value = 0;
            //
            //newton Raphson
            //
            newton_resultant_lebel.Visible = false;
            newton_resultant_root_label.Visible = false;
            newton_decimal_places_numeric_up_down.Value = 0;
            newton_function_text_box.Text = "";
            //
            //newtonDD
            //
            newtonDD_result_label.Visible = false;
            newtonDD_resultant_value_of_y_label.Visible = false;
            newtonDD_value_of_y_label.Visible = false;
            newtonDD_value_of_x_to_be_interpolate_label.Visible = false;
            newtonDD_value_of_x_to_be_interpolate_numeric_up_down.Visible = false;
            newtonDD_calculate_fx_btn.Visible = false;
            newtonDD_x_values_list_box.Items.Clear();
            newtonDD_y_values_list_box.Items.Clear();
            newtonDD_value_of_x_to_be_interpolate_numeric_up_down.Value = 0;
            newtonDD_value_of_add_y_numeric_up_down.Value = 0;
            newtonDD_value_of_add_x_numeric_up_down.Value = 0;
            newtonDD_no_of_terms_numeric_up_down.Value = 2;
        }

        private void bisection_calculate_root_btn_Click(object sender, EventArgs e)
        {
            string s = bisection_function_text_box.Text;
            if (logic.checkPoly(s))
            {
                int pre = (int)bisection_decimal_places_numeric_up_down.Value;
                double x0 = (double)bisection_lower_bound_numeric_up_down.Value;
                double x1 = (double)bisection_upper_bound_numeric_up_down.Value;
                if (!(checkBounds(x0, x1)))
                    MessageBox.Show("Upper and lower bound should be different!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    bool fal = true;
                    double root = logic.Bisection_solution(s, x0, x1, pre, ref fal);
                    if (fal)
                    {
                        bisection_resultant_root_label.Text = root.ToString();
                        bisection_resultant_label.Visible = true;
                        bisection_resultant_root_label.Visible = true;
                    }
                    else
                        MessageBox.Show("Limits are not correct!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Polynomial should be in this format 1x^2+2x^1+1!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        bool checkBounds(double l, double u)
        {
            if (l == u)
                return false;
            else
                return true;
        }
        private void iterative_calculate_root_btn_Click(object sender, EventArgs e)
        {
            if (iterative_function_list_box.SelectedIndex >= 0)
            {
                int index = iterative_function_list_box.SelectedIndex;
                int pre = (int)iterative_decimal_places_numeric_up_down.Value;
                double root = logic.iterative_solution(index, pre);
                iterative_resultant_label.Visible = true;
                iterative_resultant_root_label.Visible = true;
                iterative_resultant_root_label.Text = root.ToString();
            }
        }

        private void regula_falsi_calculate_root_btn_Click(object sender, EventArgs e)
        {
            string s = regula_falsi_function_text_box.Text;
            if (logic.checkPoly(s))
            {
                int pre = (int)regula_falsi_decimal_places_numeric_up_down.Value;
                double x0 = (double)regula_falsi_lower_bound_numeric_up_down.Value;
                double x1 = (double)regula_falsi_upper_bound_numeric_up_down.Value;
                if (!(checkBounds(x0, x1)))
                    MessageBox.Show("Upper and lower bound should be different!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    bool fal = true;
                    double root = logic.regula_falsi_solution(s, x0, x1, pre, ref fal);
                    if (fal)
                    {
                        regula_falsi_resultant_root_label.Text = root.ToString();
                        regula_falsi_resultant_label.Visible = true;
                        regula_falsi_resultant_root_label.Visible = true;
                    }
                    else
                        MessageBox.Show("Limits are not correct!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Polynomial should be in this format x^2+2x+1!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void newton_calculate_root_btn_Click(object sender, EventArgs e)
        {
            string s = newton_function_text_box.Text;
            if (logic.checkPoly(s))
            {
                int pre = (int)newton_decimal_places_numeric_up_down.Value;
                double x0 = (double)newton_upper_bound_numeric_up_down.Value;
                bool fal = true;
                double root = logic.newton_solution(s, x0, pre, ref fal);
                if (fal)
                {
                    newton_resultant_root_label.Text = root.ToString();
                    newton_resultant_lebel.Visible = true;
                    newton_resultant_root_label.Visible = true;
                }
                else
                    MessageBox.Show("First approximation is not correct!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
                MessageBox.Show("Polynomial should be in this format x^2+2x+1!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void newtonDD_calculate_fx_btn_Click(object sender, EventArgs e)
        {
            if (allY.Count == num_of_term && allX.Count == num_of_term)
            {
                double value_of_inter = (double)newtonDD_value_of_x_to_be_interpolate_numeric_up_down.Value;
                double result = logic.calculate_newtonDD_value(allX, allY, value_of_inter);
                newtonDD_result_label.Visible = true;
                newtonDD_resultant_value_of_y_label.Visible = true;
                newtonDD_value_of_y_label.Visible = true;
                newtonDD_resultant_value_of_y_label.Text = result.ToString();
            }
            else
                MessageBox.Show("Total values of x or y are not equal to the number of terms!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void newtonDD_remove_selected_x_btn_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(newtonDD_x_values_list_box);
            selectedItems = newtonDD_x_values_list_box.SelectedItems;

            if (newtonDD_x_values_list_box.SelectedIndex != -1)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                {
                    object x = selectedItems[i];
                    newtonDD_x_values_list_box.Items.Remove(x);
                    allX.Remove(Convert.ToDouble(x));
                    count_x--;
                }
            }
            else
                MessageBox.Show("No value is selected!");
        }

        private void newtonDD_remove_selected_y_btn_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(newtonDD_y_values_list_box);
            selectedItems = newtonDD_y_values_list_box.SelectedItems;

            if (newtonDD_y_values_list_box.SelectedIndex != -1)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                {
                    object x = selectedItems[i];
                    newtonDD_y_values_list_box.Items.Remove(x);
                    allY.Remove(Convert.ToDouble(x));
                    count_y--;
                }
            }
            else
                MessageBox.Show("No value is selected!"); 
        }

        private void newtonDD_value_of_add_x_btn_Click(object sender, EventArgs e)
        {
            if (count_x < num_of_term && check_value((double)newtonDD_value_of_add_x_numeric_up_down.Value))
            {
                allX.Add((double)newtonDD_value_of_add_x_numeric_up_down.Value);
                newtonDD_x_values_list_box.Items.Add(newtonDD_value_of_add_x_numeric_up_down.Value);
                count_x++;
            }
            else
            {
                MessageBox.Show("Can't add value! It's because, either total values exceed number of terms or invalid order", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (count_x == num_of_term)
            {
                newtonDD_value_of_x_to_be_interpolate_label.Visible = true;
                newtonDD_value_of_x_to_be_interpolate_numeric_up_down.Visible = true;
                newtonDD_calculate_fx_btn.Visible = true;
            }
        }

        private void newtonDD_value_of_add_y_btn_Click(object sender, EventArgs e)
        {
            if (count_y < num_of_term)
            {
                allY.Add((double)newtonDD_value_of_add_y_numeric_up_down.Value);
                newtonDD_y_values_list_box.Items.Add(newtonDD_value_of_add_y_numeric_up_down.Value);
                count_y++;
            }
            else
            {
                MessageBox.Show("Can't add value! It's because total values exceed number of terms", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void newtonDD_no_of_terms_numeric_up_down_ValueChanged(object sender, EventArgs e)
        {
            num_of_term = (int)newtonDD_no_of_terms_numeric_up_down.Value;
            count_x = 0;
            count_y = 0;
            allX.Clear();
            allY.Clear();
            newtonDD_x_values_list_box.Items.Clear();
            newtonDD_y_values_list_box.Items.Clear();
        }
       
    }
}

