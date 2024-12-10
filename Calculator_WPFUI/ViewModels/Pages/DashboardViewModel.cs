using Calculator_WPFUI.Models;
using Calculator_WPFUI.Services;
using Calculator_WPFUI.Views.Controls;
using Gma.System.MouseKeyHook;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using System.Windows.Input;
using Wpf.Ui;

namespace Calculator_WPFUI.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly CalculatorService _calculatorService;

        private readonly IContentDialogService _contentDialogService;

        private IKeyboardEvents _globalHook;




        [ObservableProperty]
        private bool _IsInputTextBoxFocused = false;

        [ObservableProperty]
        private string _inputString = string.Empty;

        //partial void OnInputStringChanged(string? oldValue, string newValue)
        //{
        //    if(!string.IsNullOrEmpty(newValue))
        //    {
        //        DataTable dt = new DataTable();

        //        try
        //        {
        //            dt.Compute(newValue, "");
        //        }
        //        catch (Exception)
        //        {
        //            InputString = oldValue;
        //        }
        //    }
        //}

        [ObservableProperty]
        private string _outputString = string.Empty;




        [ObservableProperty]
        private bool _isHistoryControlEnabled = false;

        [ObservableProperty]
        private HistoryControl _historyControl = new HistoryControl();

        [ObservableProperty]
        private ObservableCollection<CalculatorHistory> _historyList = new ObservableCollection<CalculatorHistory>();

        [ObservableProperty]
        private CalculatorHistory _selectedHistory = new CalculatorHistory();

        partial void OnSelectedHistoryChanged(CalculatorHistory value)
        {
            if (value != null)
            {
                InputString = value.Input;
                OutputString = value.Output;
            }
        }



        public DashboardViewModel(CalculatorService calculatorService, IContentDialogService contentDialogService)
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyUp += GlobalHookKeyUp;

            _calculatorService = calculatorService;
            _contentDialogService = contentDialogService;

            HistoryControl.DataContext = this;
        }




        [RelayCommand]
        private void Clear()
        {
            InputString = string.Empty;
            OutputString = string.Empty;
        }

        [RelayCommand]
        private void RemoveLast()
        {
            if (InputString.Length > 0)
            {
                InputString = InputString.Remove(InputString.Length - 1);
            }
        }

        // 현재 InputNumber()와 InputOperator() 메서드가 사실상 중복, 
        [RelayCommand]
        private void InputNumber(string numParam)
        {
            InputString += numParam;
        }

        [RelayCommand]
        private void InputOperator(string opParam)
        {
            InputString += opParam;
        }

        [RelayCommand]
        private void Calc()
        {
            try
            {
                var tokenList = _calculatorService.Tokenization(InputString);
                var postfix = _calculatorService.ConvertToPostfix(tokenList);

                OutputString = _calculatorService.CalcPostfix(postfix);

                HistoryList.Insert(0, new CalculatorHistory
                {
                    Input = InputString,
                    Output = OutputString,
                });
            }
            catch (Exception)
            {
                _contentDialogService.ShowAsync(new Wpf.Ui.Controls.ContentDialog()
                {
                    Title = "오류",
                    Content = $"올바른 수식을 입력해주세요.",
                    CloseButtonText = "확인",
                    IsPrimaryButtonEnabled = false,
                    IsSecondaryButtonEnabled = false,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                }, CancellationToken.None);
            }
        }

        [RelayCommand]
        private void ShowHistory()
        {
            IsHistoryControlEnabled = true;
        }

        [RelayCommand]
        private void HideHistory()
        {
            IsHistoryControlEnabled = false;
        }


        [RelayCommand]
        private void InputTextBox_GotFocus()
        {
            IsInputTextBoxFocused = true;
        }


        [RelayCommand]
        private void InputTextBox_LostFocus()
        {
            IsInputTextBoxFocused = false;
        }




        private void GlobalHookKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Calc();
            }

            if (IsInputTextBoxFocused)
            {
                return;
            }



            if (e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.D0)
            {
                InputNumber("0");
            }

            else if (e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.D1)
            {
                InputNumber("1");
            }

            else if (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2)
            {
                InputNumber("2");
            }

            else if (e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.D3)
            {
                InputNumber("3");
            }

            else if (e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.D4)
            {
                InputNumber("4");
            }

            else if (e.KeyCode == Keys.NumPad5 || e.KeyCode == Keys.D5)
            {
                InputNumber("5");
            }

            else if (e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.D6)
            {
                InputNumber("6");
            }

            else if (e.KeyCode == Keys.NumPad7 || e.KeyCode == Keys.D7)
            {
                InputNumber("7");
            }

            else if (e.KeyCode == Keys.NumPad8 || e.KeyCode == Keys.D8)
            {
                InputNumber("8");
            }

            else if (e.KeyCode == Keys.NumPad9 || e.KeyCode == Keys.D9)
            {
                InputNumber("9");
            }

            else if (e.KeyCode == Keys.Decimal)
            {
                InputNumber(".");
            }




            else if (e.KeyCode == Keys.Add)
            {
                InputOperator("+");
            }

            else if (e.KeyCode == Keys.Subtract)
            {
                InputOperator("-");
            }

            else if (e.KeyCode == Keys.Multiply)
            {
                InputOperator("*");
            }

            else if (e.KeyCode == Keys.Divide)
            {
                InputOperator("/");
            }

            else if (e.KeyCode == Keys.D9 && e.Shift)
            {
                InputOperator("(");
            }

            else if (e.KeyCode == Keys.D0 && e.Shift)
            {
                InputOperator(")");
            }



            else if (e.KeyCode == Keys.Back)
            {
                RemoveLast();
            }
        }
    }
}
