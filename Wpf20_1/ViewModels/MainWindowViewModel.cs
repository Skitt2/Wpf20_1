using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf20_1.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Wpf20_1.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        private Ariph calculation;
        private string lastOperation;
        private bool newDisplayRequired = false;
        private string display;




        public MainWindowViewModel()
        {
            calculation = new Ariph();

            display = "0";
            FirstOperand = string.Empty;
            SecondOperand = string.Empty;
            Operation = string.Empty;
            lastOperation = string.Empty;

            AriphCommand = new RelayCommand(OnAriphCommandExecute, CanAriphCommandExecuted);
            PushButton = new RelayCommand(OnPushButtonExecute, CanPushButtonExecuted);
        }

        public string FirstOperand
        {
            get => calculation.FirstOper;
            set { calculation.FirstOper = value; }
        }

        public string SecondOperand
        {
            get => calculation.SecondOper;
            set { calculation.SecondOper = value; }
        }

        public string Operation
        {
            get => calculation.Operation;
            set { calculation.Operation = value; }
        }

        public string LastOperation
        {
            get => lastOperation;
            set { lastOperation = value; }
        }

        public string Result
        {
            get => calculation.Result;
        }

        public string Display
        {
            get => display;
            set => Set(ref display, value);
        }


        public ICommand AriphCommand { get; }

        private void OnAriphCommandExecute(object p)
        {
            if (FirstOperand == string.Empty || LastOperation == "=")
            {
                FirstOperand = display;
                LastOperation = p.ToString();
            }
            else
            {
                SecondOperand = display;
                Operation = lastOperation;
                calculation.CalculateResult();

                if (Operation == "/" && SecondOperand == "0")
                {
                    Display = "Ошибка";
                    newDisplayRequired = true;
                    return;
                }
                else
                {
                    LastOperation = p.ToString();
                    Display = Result;
                    FirstOperand = display;
                }
            }
            newDisplayRequired = true;
        }

        private bool CanAriphCommandExecuted(object p) => true;

        //кнопки
        public ICommand PushButton { get; }

        private void OnPushButtonExecute(object p)
        {
            switch (p)
            {
                case "C":
                    Display = "0";
                    FirstOperand = string.Empty;
                    SecondOperand = string.Empty;
                    Operation = string.Empty;
                    LastOperation = string.Empty;
                    break;
                case "CE":
                    Display = "0";
                    SecondOperand = string.Empty;
                    break;
                case "←":
                    Display = display.Length > 1 ? display.Substring(0, display.Length - 1) : "0";
                    break;
                case "+/-":
                    Display = display.Contains("-") ? display.Remove(display.IndexOf("-"), 1) : "-" + display;
                    break;
                case ",":
                    if (newDisplayRequired)
                    {
                        Display = "0,";
                    }
                    else
                    {
                        if (!display.Contains(","))
                        {
                            Display = display + ",";
                        }
                    }
                    break;
                default:
                    Display = display == "0" || newDisplayRequired ? p.ToString() : display + p.ToString();
                    break;
            }
            newDisplayRequired = false;
        }

        private bool CanPushButtonExecuted(object p) => true;
    }
}
