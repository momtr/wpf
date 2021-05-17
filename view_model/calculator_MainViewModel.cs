using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _a;
        private int _b;
        public int A {
            get => _a;
            set
            {
                _a = value;
                UpdateProperty("Result");
            }
        }
        public int B
        {
            get => _b;
            set
            {
                _b = value;
                UpdateProperty("Result");
            }
        }
        public string Result
        {
            get
            {
                if (OpCode == "+")
                {
                    return (A + B).ToString();
                }
                else if (OpCode == "-")
                {
                    return (A - B).ToString();
                }
                else if (OpCode == "/")
                {
                    if (B == 0) return "ERROR";
                    return (A / B).ToString();
                }
                else if (OpCode == "*")
                {
                    return (A * B).ToString();
                }
                else
                {
                    return (A + B).ToString();
                }
            }
        }

        private string _opCode;
        public string OpCode {
            get => _opCode;
            set
            {
                _opCode = value;
                UpdateProperty("Result");
            }
        }

        public ICommand CalcCommand { get; }

        public MainViewModel()
        {

        }

        private void UpdateProperty(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
