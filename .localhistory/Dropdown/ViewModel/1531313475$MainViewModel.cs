namespace Dropdown.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using Model;
    using MVVM;

    public class MainViewModel : ViewModelBase
    {
        private PatientsModel patientsModel;
        private PatientModel patientModel;

        public MainViewModel()
        {
            UpdatePatientsList();
        }

        public event EventHandler<object> OnWarningMessage;

        public PatientsModel PatientsModel
        {
            get => patientsModel;
            set
            {
                if (patientsModel == value)
                    return;

                patientsModel = value;
                NotifyPropertyChanged();
            }
        }

        public PatientModel PatientModel
        {
            get => patientModel;
            set
            {
                if (patientModel == value)
                    return;

                patientModel = value;
                NotifyPropertyChanged();
            }
        }

        public override void UnLoad()
        {
            base.UnLoad();
        }

        public void UpdatePatientsList()
        {
            PatientsModel = new PatientsModel
            {
                Patients = new ObservableCollection<PatientModel>
                {
                    new PatientModel
                    {
                        School = "School 1",
                        Teacher = "Teacher 1",
                        StudentName = "Person 1",
                        StudentBirthDate = "01-01-2010",
                    },
                    new PatientModel
                    {
                        School = "School 1",
                        Teacher = "Teacher 1",
                        StudentName = "Person 2",
                        StudentBirthDate = "01-01-2010",
                    },
                    new PatientModel
                    {
                        School = "School 1",
                        Teacher = "Teacher 2",
                        StudentName = "Person 3",
                        StudentBirthDate = "01-01-2010",
                    },
                    new PatientModel
                    {
                        School = "School 1",
                        Teacher = "Teacher 2",
                        StudentName = "Person 4",
                        StudentBirthDate = "01-01-2010",
                    },
                    new PatientModel
                    {
                        School = "School 2",
                        Teacher = "Teacher 3",
                        StudentName = "Person 5",
                        StudentBirthDate = "01-01-2010",
                    },
                    new PatientModel
                    {
                        School = "School 2",
                        Teacher = "Teacher 3",
                        StudentName = "Person 6",
                        StudentBirthDate = "01-01-2010",
                    },
                    new PatientModel
                    {
                        School = "School 2",
                        Teacher = "Teacher 3",
                        StudentName = "Person 7",
                        StudentBirthDate = "01-01-2010",
                    }
                }
            };
        }

        public void RaiseOnWarningMessage(string key)
        {
            OnWarningMessage?.Invoke(this, key);
        }

        public void RaiseOnWarningMessage(int code)
        {
            OnWarningMessage?.Invoke(this, code);
        }

        protected override void OnDispose()
        {
        }
    }
}
