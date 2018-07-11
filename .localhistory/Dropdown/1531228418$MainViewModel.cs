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

#pragma warning disable IDISP006 // Implement IDisposable.
        private Timer updatePortTimer;
#pragma warning restore IDISP006 // Implement IDisposable.

        public MainViewModel()
        {
        }

        public event EventHandler<object> OnWarningMessage;

        public string[] ComPorts
        {
            get => comPorts;
            set
            {
                if (comPorts == value)
                    return;

                comPorts = value;
                NotifyPropertyChanged();
            }
        }

        public string SelectedComPort
        {
            get => selectedComPort;
            private set
            {
                if (selectedComPort == value)
                    return;
                selectedComPort = value;
                NotifyPropertyChanged();
            }
        }

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

        public bool ConfiguringDone
        {
            get => configuringDone;
            set
            {
                if (configuringDone == value)
                    return;

                configuringDone = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (isBusy == value)
                    return;

                isBusy = value;
                NotifyPropertyChanged();
            }
        }

#pragma warning disable IDISP006 // Implement IDisposable.
        public RefreshComPortsCommand RefreshComPortsCommand { get; }

        public RefreshPatientsCommand RefreshPatientsCommand { get; }

        public ConfigureDeviceCommand ConfigureDeviceCommand { get; }
#pragma warning restore IDISP006 // Implement IDisposable.

        public override void Load()
        {
            base.Load();
            UpdatePatientsList();

            ComPorts = Com.GetPorts();

            updatePortTimer?.Dispose();
            updatePortTimer = new Timer
            {
                Interval = 2000,
            };
            updatePortTimer.Elapsed += UpdatePortTimer_Elapsed;
            updatePortTimer.Start();
        }

        public override void UnLoad()
        {
            base.UnLoad();

            updatePortTimer.Stop();
            updatePortTimer.Dispose();
        }

        public async void UpdatePatientsList()
        {
            SpinnerViewModel.IsVisible = IsBusy = true;
            MessageModel message;
            try
            {
                // Make a call to the server, return the response.
                string result = await Api.Patients(UserViewModel.User);

                // Parse the response to a model
                PatientsModel = Api.ParseJson<PatientsModel>(result);
            }
            catch (FlurlHttpTimeoutException)
            {
                WarningViewModel.Set(nameof(Properties.Resources.ServerTimeoutError));
            }
            catch (FlurlHttpException e)
            {
                try
                {
                    message = await e.GetResponseJsonAsync<MessageModel>();
                    WarningViewModel.Set(message.Code);
                }
                catch (Exception ee)
                {
                    WarningViewModel.Set(nameof(Properties.Resources.ServerMessage_500));
                    WarningViewModel.ServerMessage = "CDVM-1: error: " + ee.Message + "\r\nResponse:\r\n" + Api.Instance.LastResponse;
                }

                SpinnerViewModel.IsVisible = IsBusy = false;
                return;
            }
            catch (Exception e)
            {
                WarningViewModel.Set(nameof(Properties.Resources.ServerMessage_500));
                WarningViewModel.ServerMessage = "CDVM-2: error: " + e.Message + "\r\nResponse:\r\n" + Api.Instance.LastResponse;

                SpinnerViewModel.IsVisible = IsBusy = false;
                return;
            }

            SpinnerViewModel.IsVisible = IsBusy = false;
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
            updatePortTimer?.Dispose();

            RefreshComPortsCommand.Dispose();
            RefreshPatientsCommand.Dispose();
            ConfigureDeviceCommand.Dispose();
        }

        private void UpdatePortTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ComPorts = Com.GetPorts();
        }
    }
}
