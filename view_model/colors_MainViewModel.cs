using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPanel.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _r;
        private int _g;
        private int _b;

        public int R
        {
            get => _r;
            set
            {
                _r = value;
                UpdateColors();
            }
        }

        public int G
        {
            get => _g;
            set
            {
                _g = value;
                UpdateColors();
            }
        }

        public int B
        {
            get => _b;
            set
            {
                _b = value;
                UpdateColors();
            }
        }

        public string Color
        {
            get => $"#{ToHex(R)}{ToHex(G)}{ToHex(B)}";
        }

        public string ContrastColor
        {
            get => $"#{ToContrastHex(R)}{ToContrastHex(G)}{ToContrastHex(B)}";
        }

        public MainViewModel()
        {

        }

        private void ChangeProperty(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private string ToHex(int num)
        {
            string hex = num.ToString("X");
            if (hex.Length == 2)
                return hex;
            return "0" + hex;
        }

        private string ToContrastHex(int num)
        {
            string hex = (255 - num).ToString("X");
            if (hex.Length == 2)
                return hex;
            return "0" + hex;
        }

        private void UpdateColors()
        {
            ChangeProperty("Color");
            ChangeProperty("ContrastColor");
            ChangeProperty("R");
            ChangeProperty("G");
            ChangeProperty("B");
        }

    }
}
