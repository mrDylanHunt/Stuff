using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string a = null, b = null, operation = null, result = null;
        int pointcount = 0;

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Calculator";
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }
        private void Number(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string temp = button.Content.ToString();
            if (operation == null && a == null)
            {
                a = temp;
                output.Text = a;
            }
            else if (operation == null && a != null)
            {
                a += temp;
                output.Text = a;
            }
            else if (operation != null && b == null)
            {
                b = temp;
                output.Text = a  + operation + b;
            }
            else
            {
                b += temp;
                output.Text = a  + operation + b;
            }

        }


        private void Clear(object sender, RoutedEventArgs e)
        {
            a = null; b = null; operation = null;
            output.Text = "0";
            pointcount = 0;
        }

        private void Operation(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string temp = button.Content.ToString();
            pointcount = 0;
            if (operation == null & a != null){
                operation = temp;
                output.Text = a + operation;
            }
            else if(operation == null && a == null && temp == "-")
            {
                a = temp;
                output.Text = a;
            }
            else if(operation == null && a == null && temp != "-")
            {
                output.Text = "0";
            }
            else
            {
                Equals(sender, e);
                operation = temp;
                output.Text = a + operation;
            }
        }

        private void UnaryOperation(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string temp = button.Content.ToString();
            pointcount--;
            if (operation == null & a != null)
            {
                operation = temp;
                Equals(sender, e);
            }
            else if (operation == null && a == null)
            {
                output.Text = "0";
            }
        }

        private void Backspace(object sender, RoutedEventArgs e)
        {
            
            if(a!=null && operation != null && b != null){
                if(b.Length <= 1)
                {
                    if (a[a.Length - 1] == ',')
                    {
                        pointcount = 0;
                    }
                    b = null;
                    output.Text = a + operation;
                }
                else
                {
                    if (a[a.Length - 1] == ',')
                    {
                        pointcount = 0;
                    }
                    b = b.Remove(b.Length - 1);
                    output.Text = a + operation + b;
                }
                
            }
            else if(a != null && operation != null && b == null)
            {
                operation = null;
                output.Text = a;
            }
            else if(a != null && operation == null && b == null) 
            {
                if (a.Length <= 1)
                {
                    if (a[a.Length - 1] == ',')
                    {
                        pointcount = 0;
                    }
                    a = null;
                    output.Text = output.Text = "0";
                }
                else
                {
                    if (a[a.Length - 1] == ',')
                    {
                        pointcount = 0;
                    }
                    a = a.Remove(a.Length - 1);
                    output.Text = a;
                }
            }
            else
            {
                output.Text = "0";
            }
        }

        private void Point(object sender, RoutedEventArgs e)
        {
            if (pointcount == 0)
            {
                if (b != null)
                {
                    b += ',';
                    output.Text = a + operation + b;
                    pointcount = 1;
                }
                else if (operation != null && a != null && b == null)
                {
                    output.Text = a + operation;
                }
                else if (operation == null && a != null && b == null)
                {
                    a += ',';
                    output.Text = a;
                    pointcount = 1;
                }
                else
                {
                    output.Text = "0";
                }
            }
            else {
                if (b != null)
                {
                    output.Text = a + operation + b;
                }
                else if (operation != null && a != null && b == null)
                {
                    output.Text = a + operation;
                }
                else if (operation == null && a != null && b == null)
                {
                    output.Text = a;
                }
                else
                {
                    output.Text = "0";
                }
            }
        }

        private void Equals(object sender, RoutedEventArgs e)
        {
            pointcount = 0;
            if(operation != null && a != null && b != null)
            {
                switch (operation) {

                    case "+" :
                        result = (double.Parse(a) + double.Parse(b)).ToString();
                        break;
                    case "-":
                        result = (double.Parse(a) - double.Parse(b)).ToString();
                        break;
                    case "*":
                        result = (double.Parse(a) * double.Parse(b)).ToString();
                        break;
                    case "/":
                        if (double.Parse(b) == 0)
                        {
                            a = null; b = null; operation = null; result = "Undefined";
                            pointcount = 0;
                        }
                        else
                        {
                            result = (double.Parse(a) / double.Parse(b)).ToString();
                        }
                        break;
                    case "^":
                        result = (Math.Pow(double.Parse(a), double.Parse(b))).ToString();
                        break;
                }
                output.Text = result;
                a = (result != "Undefined") ? result : null;
                b = null; operation = null; result = null;
                if (a != null)
                {
                    int hasapoint = a.IndexOf(",");
                    if (hasapoint == -1)
                    { pointcount = 0; }
                    else { pointcount = 1; }
                }
            }
            else if(operation != null && a != null && b == null)
            {
                switch (operation)
                {

                    case "√":
                        if (double.Parse(a) < 0)
                        {
                            a = null; operation = null; result = "Imaginary";
                            pointcount = 0;
                        }
                        else
                        {
                            result = (Math.Sqrt(double.Parse(a))).ToString();
                        }
                        break;
                    case "1/x":
                        if (double.Parse(a) == 0)
                        {
                            a = null;operation = null; result = "Undefined";
                            pointcount = 0;
                        }
                        else { result = (1.0 / double.Parse(a)).ToString(); }
                        break;
                    case "sin":
                        if (Radian.IsChecked == false)
                        {
                            double rad = (double.Parse(a) * Math.PI) / 180;

                            rad = Math.Sin(rad);
                            result = (rad).ToString();
                        }
                        else
                        {

                            result = (Math.Sin(double.Parse(a))).ToString();
                        }
                        break;
                    case "cos":
                        if (Radian.IsChecked == false)
                        {
                            double rad = (double.Parse(a) * Math.PI) / 180;

                            rad = Math.Cos(rad);
                            result = (rad).ToString();
                        }
                        else
                        {

                            result = (Math.Cos(double.Parse(a))).ToString();
                        }
                        break;
                    case "tan":
                        if (Radian.IsChecked == false)
                        {
                            double rad = (double.Parse(a) * Math.PI) / 180;

                            rad = Math.Tan(rad);
                            result = (rad).ToString();
                        }
                        else
                        { 
                            result = (Math.Tan(double.Parse(a))).ToString();
                        }
                        break;
                    default:
                        a = null; operation = null; result = "Undefined";
                        break;

                }
                output.Text = result;
                a = (result != "Undefined" && result !="Imaginary") ? result : null;
                operation = null; result = null;
                if (a != null)
                {
                    int hasapoint = a.IndexOf(",");
                    if (hasapoint == -1)
                    { pointcount = 0; }
                    else { pointcount = 1; }
                }
            }
            else
            {
                output.Text = "0";
                a = null; b = null; operation = null; result = null;
                pointcount = 0;
            }
        }
    }
}
