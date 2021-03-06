﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace XamlCSS.WPF.TestApp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel
            {
                Message = "Hello World from DataContext!"
            };

            InitializeComponent();
        }

        private void SwitchLayout()
        {
            var app = Application.Current as App;

            app.currentStyle = app.currentStyle == app.cssStyle1 ? app.cssStyle2 : app.cssStyle1;

            var sheet = XamlCSS.CssParsing.CssParser.Parse(app.currentStyle);

            Css.SetStyleSheet(thegrid, sheet);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SwitchLayout();
        }

        private int count = 0;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var b = new Button() { Content = "Abc" + count++, Name = "B" + Guid.NewGuid().ToString("N") };
            b.Click += B_Click;
            stack.Children.Add(b);
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            stack.Children.Remove(sender as Button);
        }
    }
}
