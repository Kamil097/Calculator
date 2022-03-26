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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResultText.Text = String.Empty;
            Operation.Text = String.Empty;
        }
        private void AddNo(object sender, RoutedEventArgs e)
        {
            if(ResultText.Text!=string.Empty)
            {
                ResultText.Text=string.Empty;
                Operation.Text=string.Empty;
            }
            var button = sender as Button;
            var currentNumber = button.Name[button.Name.Length - 1];
            ResultText.Text=string.Empty;
            if (!(IsHere(Operation.Text, ',')))
            {
                if (ZeroChange(Operation.Text))
                {
                    var len = Operation.Text.Length;
                    Operation.Text = Operation.Text.Remove(len - 1, 1);
                    Operation.Text += currentNumber;
                }
                else
                    Operation.Text += currentNumber;
            }
            else
            { Operation.Text += currentNumber; }
        }
        private void Zero(object sender, RoutedEventArgs e)
        {
            if (IsHere(Operation.Text, ','))
            { Operation.Text += '0'; }
            else if (CanZero(Operation.Text, '0'))
                Operation.Text += '0';
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            if(!ResultText.Text.Contains('!'))
            {
                Operation.Text = calc(Operation.Text, '+');
                Operation.Text=ResultCalc(Operation.Text,ResultText.Text,'+');
                ResultText.Text=String.Empty;
            }
        }
        private void Minus(object sender, RoutedEventArgs e)
        {
            if(!ResultText.Text.Contains('!'))
            {
            Operation.Text = calc(Operation.Text, '-');
            Operation.Text=ResultCalc(Operation.Text,ResultText.Text,'-');
            ResultText.Text=String.Empty;
            }
        }
        private void Multiply(object sender, RoutedEventArgs e)
        {
            if(!ResultText.Text.Contains('!'))
            {
            Operation.Text = calc(Operation.Text, '*');
            Operation.Text=ResultCalc(Operation.Text,ResultText.Text,'*');
            ResultText.Text=String.Empty;
            }
        }
        private void Divide(object sender, RoutedEventArgs e)
        {
            if(!ResultText.Text.Contains('!'))
            {
            Operation.Text = calc(Operation.Text, '/');
            Operation.Text=ResultCalc(Operation.Text,ResultText.Text,'/');
            ResultText.Text=String.Empty;
            }
        }
        private void Coma(object sender, RoutedEventArgs e)
        {
            if(!ResultText.Text.Contains('!'))
            {
            Operation.Text = calc(Operation.Text, ',');
            Operation.Text=Operation.Text +',';
            ResultText.Text=String.Empty;
            }
        }
        private void Result(object sender, RoutedEventArgs e)
        {
            var len = Operation.Text.Length-1;
            if(!(Operation.Text!=String.Empty&&ResultText.Text!=String.Empty))
            {
                if(!(Operation.Text[len]==('+')||Operation.Text[len]==('-')||Operation.Text[len]==('*')||Operation.Text[len]==('/')))
                {
                ResultText.Text = Calculation(Operation.Text);
                }
            }
            else
            {
                var r = Operation.Text;
                char p = 'x';
                if(r.Contains('*')){p='*';}
                else if(r.Contains('/')){p='/';}
                else if(r.Contains('+')){p='+';}
                else if(r.Contains('-')){p='-';}
                var calc = r.Split(p);
                r = ResultText.Text+p+calc[1];
                Operation.Text=r;
                ResultText.Text = Calculation(r);
            }

        }
        
        private void AC(object sender, RoutedEventArgs e)
        {
            ResultText.Text = String.Empty;
            Operation.Text = String.Empty;
        }
        private Boolean CanZero(string operation, char x)
        {
            var len = operation.Length;
            char[] tab = operation.ToCharArray();
            if (len == 0)
                return true;

            for (int i = len - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    if (tab[i] == x)
                        return false;
                    else
                        return true;
                }
                else if (tab[i] == '/' || tab[i] == '*' || tab[i] == '+' || tab[i] == '-')
                {
                    if (!(i == len - 1))
                    {
                        if (tab[i + 1] == x)
                            return false;
                        else
                            return true;
                    }
                    else { return true; }
                }
            }
            return false;
        }
        private Boolean IsHere(string operation, char x)
        {
            var len = operation.Length;
            char[] tab = operation.ToCharArray();
            if (len == 0)
                return false;

            for (int i = len - 1; i >= 0; i--)
            {
                if (tab[i] == x)
                {
                    return true;
                }
                else if (tab[i] == '/' || tab[i] == '*' || tab[i] == '+' || tab[i] == '-')
                {
                    return false;
                }
            }
            return false;

        }
        private string calc(string operation, char x)
        {
            if(operation.Length!=0)
            {
            char[] tab = operation.ToCharArray();
            var i = tab.Length - 1;
            if (tab[i] == '/' || tab[i] == '*' || tab[i] == '+' || tab[i] == '-' || tab[i]==',')
            {
                tab[i] = x;
                return new string(tab);
            }
            return new string(tab);
            }
            return operation;
        }
        private string ResultCalc(string operation,string result,char x) 
        {
            var ret="";
            if(!(result!=string.Empty && operation!=string.Empty)) //jeżeli nic nie ma w konsoli, znak nie doda się
            {
                if(result==string.Empty) //jedynie znajduje się coś w operacji
                {
                    if(operation[0]=='-')
                    {
                        var operation2 = operation.Remove(0,1);
                        var len = operation2.Length-1;
                        if (operation2.Contains('+')||operation2.Contains('-')||operation2.Contains('*')||operation2.Contains('/'))
                        {
                            if(!(operation2[len]==('+')||operation2[len]==('-')||operation2[len]==('*')||operation2[len]==('/')))
                            {
                                ret = Calculation(operation2);
                                ret+=x;
                            }
                            else
                            {
                                ret = operation;
                            }
                        }
                        else
                        {
                            ret = operation+x;
                        }
                    }
                    else
                    {
                        var len = operation.Length-1;
                        if (operation.Contains('+')||operation.Contains('-')||operation.Contains('*')||operation.Contains('/'))
                        {
                           if(!(operation[len]==('+')||operation[len]==('-')||operation[len]==('*')||operation[len]==('/')))
                            {
                                ret = Calculation(operation);
                                ret+=x;
                            }
                            else
                            {
                                ret = operation;
                            }
                        }
                        else
                        {
                            ret = operation+x;
                        }
                    }
                }
            }
            else
            {
                ret = result+x;
            }
            return ret;
            
        }

        private Boolean ZeroChange(string operation)
        {
            var len = operation.Length;
            char[] tab = operation.ToCharArray();
            if (len != 0)
            {
                if (tab[len-1] == '0')
                { return true; }

                else
                {
                    for (int i = len - 1; i >= 0; i--)
                    {
                        if (tab[i] == '/' || tab[i] == '*' || tab[i] == '+' || tab[i] == '-')
                        {
                            if (!(i == len - 1))
                            {
                                if (tab[i + 1] == '0')
                                { return true; }
                                else
                                { return false; }
                            }
                        }

                    }
                    return false;
                }
            }
            return false;
        }

        private string Calculation(string operation)
        {
            if (operation.Contains('*'))
            {
                var tab = operation.Split('*');
                return (double.Parse(tab[0]) * double.Parse(tab[1])).ToString();
            }
            else if (operation.Contains('/'))
            {
                var tab = operation.Split('/');
                if (tab[1] == "0")
                {
                    return "You tried to divide by 0!";
                }
                else
                {
                    return (double.Parse(tab[0]) / double.Parse(tab[1])).ToString();
                }
            }
            else if (operation.Contains('+'))
            {
                var tab = operation.Split('+');
                return (double.Parse(tab[0]) + double.Parse(tab[1])).ToString();
            }
            else if (operation.Contains('-'))
            {
                if (operation[0] == '-')
                {
                    operation.Remove(0, 1);
                    var tab = operation.Split('-');
                    return (-double.Parse(tab[0]) - double.Parse(tab[1])).ToString();

                }
                else
                {
                    var tab = operation.Split('-');
                    return (double.Parse(tab[0]) - double.Parse(tab[1])).ToString();
                }
            }
            else return default;


        }
        //private string CharChange(string operation, )
        //{
        //    char[] tab = operation.ToCharArray();
        //}
    }

}
