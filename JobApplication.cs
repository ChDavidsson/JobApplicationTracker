namespace JobApplicationTracker
{
    public enum ApplicationStatus
    {
        Applied,
        Interviewed,
        Offer,
        Rejected
    }

    public class JobApplication
    {
        // CompanyName | string 
        public string CompanyName { get; set; }
        // PositionTitle | string
        public string PositionTitle { get; set; }
        // ApplicationDate | DateTime - Datum när ansökan skickades
        public DateTime ApplicationDate { get; set; }
        public ApplicationStatus Status { get; set; }
        // ResponseDate | DateTime? - Datum när svar mottogs
        public DateTime? ResponseDate { get; set; }
        // SalaryExpectation | int - Önskad lön i kronor
        public int SalaryExpectation { get; set; }

        public int GetDaysSinceApplied()
        {
            return (DateTime.Now - ApplicationDate).Days;
        }

        public string GetSummary()
        {
            string response = ResponseDate.HasValue ? ResponseDate.Value.ToString("yyyy-MM-dd") : "N/A";
            return $"Company: {CompanyName} as {PositionTitle}, Applied: {ApplicationDate.ToShortDateString("yyyy-MM-dd")}, Status: {Status}, Response Date: {response}, Salary Expectation: {SalaryExpectation}kr";
        }
    }
}