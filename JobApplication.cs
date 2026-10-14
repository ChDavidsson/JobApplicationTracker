namespace JobApplicationTracker
{
    public enum ApplicationStatus
    {
        Applied = 1,
        Interviewed = 2,
        Offer = 3,
        Rejected = 4
    }

    public class JobApplication
    {
        public string? CompanyName { get; set; }
        public string? PositionTitle { get; set; }
        public DateTime ApplicationDate { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime? ResponseDate { get; set; }
        public int SalaryExpectation { get; set; }

        public int GetDaysSinceApplied()
        {
            return (DateTime.Now - ApplicationDate).Days;
        }

        public string GetSummary()
        {
            string response = ResponseDate.HasValue ? ResponseDate.Value.ToString("yyyy-mm-dd") : "N/A";
            return $"Company: {CompanyName} as {PositionTitle}, Applied: {ApplicationDate.ToString("yyyy-mm-dd")}, Status: {Status}, Response Date: {response}, Salary Expectation: {SalaryExpectation}kr";
        }
    }
}