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
            
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        private void HeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsInitialized)
            {
                double heightVal = SliderChange(HeightSlider.Value, HeightNum, " cm");

                //position
                PositionChange(heightVal, Height, H_Cav);

                //BMI
                BMI();
            }
        }

        private void WeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsInitialized)
            {
                double weightVal = SliderChange(WeightSlider.Value, WeightNum," kg");

                //position
                PositionChange(weightVal, Weight, W_Cav);

                //BMI
                BMI();
            }
        }

        double SliderChange(double value, TextBlock block, string unit)
        {
            double Val = Math.Round(value, 1);
            if (Val < 240)
            {
                block.Text = Val.ToString() + unit;
                block.Width = block.Text.Length * 10;
            }
            else
            {
                Val = Math.Round(value, 0);
                block.Width = 60;
                block.Text = Val.ToString() + " cm";
            }
            return Val;
        }

        void PositionChange(double changeValue, Border displayBorder, Canvas displayCanvas) {
            double a = ((changeValue - 50) / 200) * (displayCanvas.ActualWidth - 60);
            Canvas.SetLeft(displayBorder, a);
        }

        void BMI() {
            //BMI
            double height = Math.Round(HeightSlider.Value, 1);
            double weight = Math.Round(WeightSlider.Value, 1);
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

            // different messages
            if (bmi < 24)
            {
                Reminder.FontSize = 14;
                if (bmi < 18.5)
                {
                    Reminder.Foreground = Brushes.Aqua;
                    Reminder.Text = "What have you been eating???";
                }
                else
                {
                    Reminder.Foreground = Brushes.ForestGreen;
                    Reminder.Text = "Healthy guy, keep it up!!!";
                }
            }
            else if (bmi < 35)
            {
                Reminder.Foreground = Brushes.Red;
                Reminder.FontSize = 18;
                if (bmi < 30)
                {
                    Reminder.FontWeight = FontWeights.Bold;
                    Reminder.Text = "Watchout for Obesity!!!";

                    if (bmi < 27)
                    {
                        Reminder.FontWeight = FontWeights.Normal;
                        Reminder.Text = "You're a bit overweight!!!";
                    }
                }
                else
                {
                    Reminder.FontWeight = FontWeights.Bold;
                    Reminder.FontSize = 26;
                    Reminder.Text = "U Fatty LOL";
                }
            }
            else {
                Reminder.Foreground = Brushes.Red;
                Reminder.FontWeight = FontWeights.Bold;
                Reminder.FontSize = 30;
                Reminder.Text = "ＯＭＧ Ｕ Ｒ ２ ＦＡＴＴＴ";
                // Automatically resize height and width relative to content
                this.SizeToContent = SizeToContent.WidthAndHeight;
            }
            
        }

        // responsive gimmick
        void ChangeWindowSize(object sender, SizeChangedEventArgs e)
        {
            PositionChange(SliderChange(HeightSlider.Value, HeightNum, " cm"), Height, H_Cav);
            PositionChange(SliderChange(WeightSlider.Value, WeightNum, " kg"), Weight, W_Cav);
            
        }

        
    }
}
