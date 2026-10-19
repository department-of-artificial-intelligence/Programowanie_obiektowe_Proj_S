namespace Gemini.dataModel
{
    public class Prescription
    {
        public int Id { get; set; }
        public string PrescriptionNumber { get; set; } = string.Empty;
        public string PatientPesel { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public bool IsFulfilled { get; set; }
    }
}