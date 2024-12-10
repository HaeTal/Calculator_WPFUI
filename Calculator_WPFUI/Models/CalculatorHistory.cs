using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_WPFUI.Models
{
    public partial class CalculatorHistory : ObservableObject
    {
        [ObservableProperty]
        private string _input;

        [ObservableProperty]
        private string _output;
    }
}
