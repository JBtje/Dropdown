namespace Dropdown.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using Model;
    using MVVM;

    public class MainViewModel : ViewModelBase
    {
        private StudentsModel studentsModel;
        private StudentModel patientModel;

        public MainViewModel()
        {
            UpdatePatientsList();
        }

        public event EventHandler<object> OnWarningMessage;

        public StudentsModel StudentsModel
        {
            get => studentsModel;
            set
            {
                if (studentsModel == value)
                    return;

                studentsModel = value;
                NotifyPropertyChanged();
            }
        }

        public StudentModel StudentModel
        {
            get => studentModel;
            set
            {
                if (studentModel == value)
                    return;

                studentModel = value;
                NotifyPropertyChanged();
            }
        }

        public override void UnLoad()
        {
            base.UnLoad();
        }

        public void UpdatePatientsList()
        {
            StudentsModel = new StudentsModel
            {
                Students = new ObservableCollection<StudentModel>
                {
                    new StudentModel
                    {
                        School = "School 1",
                        Teacher = "Teacher 1",
                        StudentName = "Person 1",
                        StudentBirthDate = "01-01-2010",
                    },
                    new StudentModel
                    {
                        School = "School 1",
                        Teacher = "Teacher 1",
                        StudentName = "Person 2",
                        StudentBirthDate = "01-01-2010",
                    },
                    new StudentModel
                    {
                        School = "School 1",
                        Teacher = "Teacher 2",
                        StudentName = "Person 3",
                        StudentBirthDate = "01-01-2010",
                    },
                    new StudentModel
                    {
                        School = "School 1",
                        Teacher = "Teacher 2",
                        StudentName = "Person 4",
                        StudentBirthDate = "01-01-2010",
                    },
                    new StudentModel
                    {
                        School = "School 2",
                        Teacher = "Teacher 3",
                        StudentName = "Person 5",
                        StudentBirthDate = "01-01-2010",
                    },
                    new StudentModel
                    {
                        School = "School 2",
                        Teacher = "Teacher 3",
                        StudentName = "Person 6",
                        StudentBirthDate = "01-01-2010",
                    },
                    new StudentModel
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
