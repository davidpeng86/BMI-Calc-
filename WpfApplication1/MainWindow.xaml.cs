﻿using System;
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
            // Automatically resize height relative to content
            this.SizeToContent = SizeToContent.Height;
        }

        private void HeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsInitialized)
            {
                double heightVal = SliderChange(HeightSlider.Value, HeightNum, " cm");

                // Position
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

                // Position
                PositionChange(weightVal, Weight, W_Cav);

                // BMI
                BMI();
            }
        }

        // Text border changes dynamically to fit text length
        // Changes display value to a whole number if value > 240
        // to not let the value box get outta our window
        // returns slider value 
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
                block.Text = Val.ToString() + unit;
            }
            return Val;
        }

        // Changes slider value text border position 
        // changeValue - minimun value, ActualWidth - text border width
        void PositionChange(double changeValue, Border displayBorder, Canvas displayCanvas) {
            double a = ((changeValue - 50) / 200) * (displayCanvas.ActualWidth - 60);
            Canvas.SetLeft(displayBorder, a);
        }

        // Calculate BMI
        void BMI() {
            //BMI
            double height = Math.Round(HeightSlider.Value, 1);
            double weight = Math.Round(WeightSlider.Value, 1);
            double bmi;
            bmi = weight / Math.Pow(height / 100, 2);

            // Outputs BMI value
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

            // Changes text color as BMI gets greater and greater
            if (bmi > 24)
            {
                int R, G = Convert.ToInt32((77 - (bmi - 24) * (77 / 76))), B = Convert.ToInt32((57 - (bmi - 24) * (57 / 76))) ;
                R = 255;
                if (G <= 0)
                {
                    G = 0;
                    B = 0;
                }
                
                SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, (byte)R, (byte)G, (byte)B));   
                BMInum.Foreground = brush;
                BMIdecimal.Foreground = brush;

                // Changes BMI boldness as BMI grows
                double boldness = 100 + (bmi - 15) * 20;
                if (boldness > 999)
                {
                    boldness = 999;
                }
                BMInum.FontWeight = FontWeight.FromOpenTypeWeight(Convert.ToInt32(boldness));
                BMIdecimal.FontWeight = FontWeight.FromOpenTypeWeight(Convert.ToInt32(boldness));

            }

            // Different reminder messages in different weight range
            // Normal weight and too light
            if (bmi < 24)
            {
                BMInum.FontWeight = FontWeights.Light;
                BMIdecimal.FontWeight = FontWeights.Light;
                Reminder.FontSize = 14;
                if (bmi < 18.5)
                {
                    Reminder.Foreground = Brushes.DarkBlue;
                    Reminder.Text = "What have you been eating???";
                }
                else
                {
                    Reminder.Foreground = Brushes.ForestGreen;
                    Reminder.Text = "Healthy guy, keep it up!!!";
                }
            }
            // A bit heavy
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
            // Too fat
            else {
                Reminder.Foreground = new SolidColorBrush(Color.FromRgb(230, 58, 58));
                Reminder.FontWeight = FontWeights.Bold;
                Reminder.FontSize = 30;
                Reminder.Text = "ＯＭＧ Ｕ Ｒ ２ ＦＡＴ";
                
            }
            
        }

        // Content size changes as window changes size
        void ChangeWindowSize(object sender, SizeChangedEventArgs e)
        {
            PositionChange(SliderChange(HeightSlider.Value, HeightNum, " cm"), Height, H_Cav);
            PositionChange(SliderChange(WeightSlider.Value, WeightNum, " kg"), Weight, W_Cav);
        }

        // Resets everything
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            HeightSlider.Value = 150;
            WeightSlider.Value = 50;
            BMInum.Text = "22";
            BMIdecimal.Text = ".22";
            Reminder.FontWeight = FontWeights.Light;
            Reminder.FontSize = 14;
            Reminder.Foreground = Brushes.DarkSlateGray;
            Reminder.Text = "An apple a day keeps the doctor away.";
        }
    }
}
