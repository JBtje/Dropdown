namespace Dropdown.Model
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonConverter(typeof(JsonDataConverter))]
    public class PatientModel
    {
        [JsonProperty("practice")]
        public string Practice { get; set; }

        [JsonProperty("gp")]
        public string GeneralPractitioner { get; set; }

        [JsonProperty("name")]
        public string PatientName { get; set; }

        [JsonProperty("birthdate")]
        public string PatientBirthDate { get; set; }

        [JsonProperty("code")]
        public string PatientCode { get; set; }
    }

    [JsonConverter(typeof(JsonDataConverter))]
    public class PatientsModel
    {
        [JsonProperty("data.patients")]
        public ObservableCollection<PatientModel> Patients { get; set; }
    }
}
