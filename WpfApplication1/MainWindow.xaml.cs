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

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsInitialized)
            {
                //number
                double heightVal = Math.Round(HeightSlider.Value, 1); ;
                if (heightVal < 240)
                {
                    HeightNum.Width = HeightNum.Text.Length * 10;
                }
                else {
                    heightVal = Math.Round(HeightSlider.Value, 0);
                    HeightNum.Width = 60;
                }
                HeightNum.Text = heightVal.ToString() + " cm";
                //get actual width
                double a = ((heightVal - 50) / 200) * (H_Cav.ActualWidth - 60);

                //position
                Canvas.SetLeft(Height, a);

                //BMI
                double height = heightVal;
                double weight = Math.Round(WeightSlider.Value, 1);
                double bmi;
                bmi = weight / Math.Pow(height / 100, 2);

                string[] part = Math.Round(bmi,2).ToString().Split('.');
                BMInum.Text = part[0];
                if (part.Length > 1)
                {
                    BMIdecimal.Text = "."+part[1];
                }
                else {
                    BMIdecimal.Text = ".00";
                }
            }
        }

        private void WeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsInitialized)
            {
                //number
                double weightVal = Math.Round(WeightSlider.Value, 1);
                
                if (weightVal < 240)
                {
                    WeightNum.Width = WeightNum.Text.Length * 10;
                }
                else
                {
                    weightVal = Math.Round(WeightSlider.Value, 0);
                    WeightNum.Width = 60;
                }
                WeightNum.Text = weightVal.ToString() + " kg";
                WeightNum.Width = WeightNum.Text.Length * 10;
                //get actual width
                double a = ((weightVal - 50) / 200) * (H_Cav.ActualWidth - 60);

                //position
                Canvas.SetLeft(Weight, a);

                //BMI
                double height = Math.Round(HeightSlider.Value, 1);
                double weight = weightVal;
                double bmi;
                bmi = weight / Math.Pow(height / 100, 2);

                string[] part = Math.Round(bmi, 2).ToString().Split('.');
                BMInum.Text = part[0];
                if (part.Length > 1)
                {
                    BMIdecimal.Text = "." + part[1];
                }
                else
                {
                    BMIdecimal.Text = ".00";
                }
            }
        }
    }
}
