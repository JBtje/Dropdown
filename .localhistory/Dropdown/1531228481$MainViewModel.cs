namespace Dropdown
{
    using System;
    using System.Timers;
    using Model;
    using MVVM;

    public class MainViewModel : ViewModelBase
    {
        private string[] comPorts;
        private string selectedComPort = string.Empty;
        private PatientsModel patientsModel;
        private PatientModel patientModel;
        private bool configuringDone;
        private bool isBusy = true;

        public MainViewModel()
        {
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

        public override void Load()
        {
            base.Load();
            UpdatePatientsList();
        }

        public override void UnLoad()
        {
            base.UnLoad();
        }

        public async void UpdatePatientsList()
        {
            PatientsModel = new PatientsModel();
            PatientsModel.Patients.Add(new PatientModel(
            {

            }))
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
