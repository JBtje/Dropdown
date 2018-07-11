namespace Dropdown.Model
{
    using System.Collections.ObjectModel;

    public class PatientModel
    {
        public string School { get; set; }
        public string GeneralPractitioner { get; set; }
        public string PatientName { get; set; }
        public string PatientBirthDate { get; set; }
    }

    public class PatientsModel
    {
        public ObservableCollection<PatientModel> Patients { get; set; }
    }
}
