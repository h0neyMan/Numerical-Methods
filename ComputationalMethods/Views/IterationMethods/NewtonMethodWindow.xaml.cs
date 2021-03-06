﻿using ComputationalMethods.Helpers;
using RPN;
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
using System.Windows.Shapes;
using Numerics.Approximation;

namespace ComputationalMethods.Views.IterationMethods
{
    public partial class NewtonMethodWindow : Window
    {
        public NewtonMethodWindow()
        {
            InitializeComponent();
        }

        private void EvaluateButton_Click(object sender, RoutedEventArgs e)
        {
            var a = Parser.ParseInput(LeftIntervalSideTextBox.Text);
            var b = Parser.ParseInput(RightIntervalSideTextBox.Text);
            var epselon = Parser.ParseInput(EpselonTextBox.Text);
            if (a != null && b != null && epselon != null)
            {
                Function expression = new Function(new[] { "x" });
                expression.Parse(Function.Text);
                //Function expression1StDerative = new Function(new[] { "x" });
                //expression1StDerative.Parse(Function1StDerative.Text);
                //Function expression2NdDerative = new Function(new[] { "x" });
                //expression2NdDerative.Parse(Function2NdDerative.Text);

                Function expression1StDerative = expression.Derive("x");
                Function expression2NdDerative = expression1StDerative.Derive("x");
                NewtonMethod expr = new NewtonMethod((x) =>
                    {
                        return expression.Evaluate(new [] { x });
                    },
                    (x) =>
                    {
                        return expression1StDerative.Evaluate(new[] { x });
                    },
                    (x) =>
                    {
                        return expression2NdDerative.Evaluate(new[] { x });
                    });
                try
                {
                    MessageBox.Show("Result: " + expr.Evaluate((double)a, (double)b, (double)epselon).ToString()
                        + "\nIterations: " + expr.NumOfIteration, "Result");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!");
                }
            }
        }

        private void LeftIntervalSideTextBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("a");
            tb.LostFocus += Handler.GetLostFocusEventHandler("a");
            Handler.GetLostFocusEventHandler("a")(sender, null);
        }

        private void RightIntervalSideTextBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("b");
            tb.LostFocus += Handler.GetLostFocusEventHandler("b");
            Handler.GetLostFocusEventHandler("b")(sender, null);
        }

        private void EpselonTextBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("epselon");
            tb.LostFocus += Handler.GetLostFocusEventHandler("epselon");
            Handler.GetLostFocusEventHandler("epselon")(sender, null);
        }

        private void Function_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("function");
            tb.LostFocus += Handler.GetLostFocusEventHandler("function");
            Handler.GetLostFocusEventHandler("function")(sender, null);
        }

        private void Function1StDerative_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("1st derivative");
            tb.LostFocus += Handler.GetLostFocusEventHandler("1st derivative");
            Handler.GetLostFocusEventHandler("1st derivative")(sender, null);
        }

        private void Function2NdDerative_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("2nd derivative");
            tb.LostFocus += Handler.GetLostFocusEventHandler("2nd derivative");
            Handler.GetLostFocusEventHandler("2nd derivative")(sender, null);
        }
    }
}
