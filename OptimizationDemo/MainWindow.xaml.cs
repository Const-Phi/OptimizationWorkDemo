﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using OptimizationLibrary;

namespace OptimizationDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Model model = new Model();

        public MainWindow()
        {
#if DEBUG
            Debug.WriteLine($"model.Range is {model.Range}");
#endif

            try
            {
                InitializeComponent();

                BindTextBoxWithModel("Range.Left", LeftBoundInput);
                BindTextBoxWithModel("Range.Right", RightBoundInput);
                BindTextBoxWithModel("Range.Length", LenghtOutput, BindingMode.OneWay);
                BindTextBoxWithModel("Factor", Factor);

                // for more degree of encapsulation we should use
                // model object as entry point to work with
                // event-subscription mechanism
                model.PropertyChanged += UserInput_OnLostFocus;
                
                // but if we use one entry point to subscriptions on events
                // we have some complications of reflection algorithm
                // model.Range.PropertyChanged += UserInput_OnLostFocus;
            }
            catch (Exception exception)
            {
                var message = exception.ToString();
#if DEBUG
                Debug.WriteLine(message);
#else
                MessageBox.Show(message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
#endif
            }
        }

        private void BindTextBoxWithModel(String path, FrameworkElement target,
            BindingMode mode = BindingMode.TwoWay)
        {
            SetBinding(model, path, target, TextBox.TextProperty, mode);
        }

        private static void SetBinding(Object source, String path, FrameworkElement target,
            DependencyProperty targetProperty,
            BindingMode mode = BindingMode.Default)
        {
            // see example in MacDonald M. - Pro WPF 4.5 in C#
            // page 231 - section "Creating Bindings With Code"
            var binding = new Binding
            {
                Source = source,
                Path = new PropertyPath(path),
                Mode = mode
            };
            target.SetBinding(targetProperty, binding);
        }

        private static String GetInfo(Object sender, String propertyName)
        {
            // prepare to recursion part of reflection algorithm
            if (sender is SenderWrapper wrapper)
            {
                var outerSenderType = wrapper.OuterSender.GetType();
                var innerSenderType = wrapper.InnerSender.GetType();
                var changedValue = innerSenderType.GetProperty(propertyName)?.GetValue(wrapper.InnerSender);

                return
                    $"{outerSenderType.FullName} / {innerSenderType.FullName} / property \"{propertyName}\" changed.{Environment.NewLine}New value is [{changedValue}].{Environment.NewLine}Object is {sender}.";
            }

            // base part of reflection algorithm
            var senderType = sender.GetType();
            var senderTypeName = senderType.FullName;
            var changedProperty = senderType.GetProperty(propertyName);
            var changedPropertyValue = changedProperty?.GetValue(sender);

            return
                $"{senderTypeName} property \"{propertyName}\" changed.{Environment.NewLine}New value is [{changedPropertyValue}].{Environment.NewLine}Object is {sender}.";
        }

        private static void UserInput_OnLostFocus(Object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            try
            {
                MessageBox.Show(
                    GetInfo(sender, propertyChangedEventArgs.PropertyName),
                    "Reflection Info",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
#if DEBUG
                Debug.WriteLine(exception.ToString());
#endif
            }
        }
    }
}