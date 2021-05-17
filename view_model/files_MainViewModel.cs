using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FileStatistics.Model;
using FileStatistics.BusinessLogic;
using System.Windows;
using Microsoft.Win32;

namespace FileStatistics.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private File _file;
        public File File
        {
            get => _file;
            set
            {
                _file = value;
                SyncProperty("File");
            }
        }

        public ICommand ExitCommand { get; }
        public ICommand OpenFileCommand { get; }
        public ICommand AboutCommand { get; }

        public FileLoader FileLoader { get; }

        public string CurrentFilePath { get; set; }

        public MainViewModel()
        {
            ExitCommand = new RelayCommand(
                () =>
                {
                    System.Windows.Application.Current.Shutdown();
                }
            );
            OpenFileCommand = new RelayCommand(
                () =>
                {
                    try
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        if (openFileDialog.ShowDialog() == true)
                        {
                            CurrentFilePath = openFileDialog.FileName;
                        }
                        File = FileLoader.LoadFile(CurrentFilePath);
                    }
                    catch (Exception e)
                    {

                    }
                },
                () => true
            );
            AboutCommand = new RelayCommand(
                () =>
                {
                    MessageBox.Show("Version: 1.0", "FileStatistics - About");
                }
            );
        }

        private void SyncProperty(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
