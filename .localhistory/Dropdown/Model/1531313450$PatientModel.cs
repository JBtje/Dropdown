namespace Dropdown.Model
{
    using System.Collections.ObjectModel;

    public class PatientModel
    {
        public string School { get; set; }
        public string Teacher { get; set; }
        public string StudentName { get; set; }
        public string PatientBirthDate { get; set; }
    }

    public class PatientsModel
    {
        public ObservableCollection<PatientModel> Patients { get; set; }
    }
}
