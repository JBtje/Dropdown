namespace Dropdown.ViewModel
{
    using System;
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
            PatientsModel = new PatientsModel();
            PatientsModel Patients = new PatientsModel();
            PatientModel x = new PatientModel
            {
                Practice = "School 1",
                GeneralPractitioner = "Teacher 1",
                PatientName = "Person 1",
                PatientBirthDate = "01-01-2010",
                PatientCode = "ABCD"
            };

            Patients.Patients.Add(x);


            PatientsModel.Patients.Add(new PatientModel
            {
                Practice = "School 1",
                GeneralPractitioner = "Teacher 1",
                PatientName = "Person 2",
                PatientBirthDate = "01-01-2010",
                PatientCode = "ABCD"
            });
            PatientsModel.Patients.Add(new PatientModel
            {
                Practice = "School 1",
                GeneralPractitioner = "Teacher 2",
                PatientName = "Person 3",
                PatientBirthDate = "01-01-2010",
                PatientCode = "ABCD"
            });
            PatientsModel.Patients.Add(new PatientModel
            {
                Practice = "School 1",
                GeneralPractitioner = "Teacher 2",
                PatientName = "Person 4",
                PatientBirthDate = "01-01-2010",
                PatientCode = "ABCD"
            });
            PatientsModel.Patients.Add(new PatientModel
            {
                Practice = "School 2",
                GeneralPractitioner = "Teacher 3",
                PatientName = "Person 5",
                PatientBirthDate = "01-01-2010",
                PatientCode = "ABCD"
            });
            PatientsModel.Patients.Add(new PatientModel
            {
                Practice = "School 2",
                GeneralPractitioner = "Teacher 3",
                PatientName = "Person 6",
                PatientBirthDate = "01-01-2010",
                PatientCode = "ABCD"
            });
            PatientsModel.Patients.Add(new PatientModel
            {
                Practice = "School 2",
                GeneralPractitioner = "Teacher 3",
                PatientName = "Person 7",
                PatientBirthDate = "01-01-2010",
                PatientCode = "ABCD"
            });

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
