using Registration.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Registration.ViewModels
{
    class MainViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Person Person;

        private string _repeatPassword;
        public string RepeatPassword
        {
            get => _repeatPassword;
            set
            {
                _repeatPassword = value;
                TriggerPropertyChange("RepeatPassword");
                TriggerPropertyChange("Password");
                TriggerPropertyChange("RenderString");
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                Person.Firstname = value;
                TriggerPropertyChange("FirstName");
                TriggerPropertyChange("RenderString");
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                Person.Lastname = value;
                TriggerPropertyChange("LastName");
                TriggerPropertyChange("RenderString");
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                Person.Password = value;
                TriggerPropertyChange("Password");
                TriggerPropertyChange("RepeatPassword");
                TriggerPropertyChange("RenderString");
            }
        }

        private Gender _gender;
        public Gender Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                Person.Gender = value;
                TriggerPropertyChange("Gender");
                TriggerPropertyChange("RenderString");
            }
        }

        private DateTime _birthday;
        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                Person.Birthday = value;
                TriggerPropertyChange("Birthday");
                TriggerPropertyChange("RenderString");
            }
        }

        private DateTime _firstSchoolday;
        public DateTime FirstSchoolDay
        {
            get => _firstSchoolday;
            set
            {
                _firstSchoolday = value;
                Person.FirstSchoolDay = value;
                TriggerPropertyChange("FirstSchoolDay");
                TriggerPropertyChange("RenderString");
            }
        }

        public string RenderString
        {
            get => $"{Person.Firstname} {Person.Lastname} {Person.Password} {RepeatPassword} {Person.Birthday.Day}/{Person.Birthday.Month}/{Person.Birthday.Year} {Person.FirstSchoolDay.Day}/{Person.FirstSchoolDay.Month}/{Person.FirstSchoolDay.Year} {Person.Gender}";
        }

        public ICommand ChangeGender { get; }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string res = String.Empty;
                if (columnName == "RepeatPassword")
                {
                    if (Password != null && RepeatPassword != null && !Password.Equals(RepeatPassword))
                    {
                        res = "Passwords must match";
                    }
                }
                if (columnName == "Password")
                {
                    if (Password != null && RepeatPassword != null && !Password.Equals(RepeatPassword))
                    {
                        res = "Passwords must match";
                    }
                }
                if (columnName == "FirstSchoolDay")
                {
                    if (Birthday >= FirstSchoolDay)
                    {
                        res = "FirstSchoolDay must be after birthdate";
                    }
                }
                return res;
            }
        }

        public MainViewModel()
        {
            Person = new Person();
            ChangeGender = new RelayCommand(
                (gender) =>
                {
                    switch (gender)
                    {
                        case "MALE":
                            Person.Gender = Gender.MALE;
                            break;
                        case "FEMALE":
                            Person.Gender = Gender.FEMALE;
                            break;
                        case "OTHER":
                            Person.Gender = Gender.OTHER;
                            break;
                    }
                    TriggerPropertyChange("RenderString");
                },
                (gender) => true
            );
        }

        private void TriggerPropertyChange(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
