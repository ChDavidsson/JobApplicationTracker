namespace JobApplicationTracker
{
    //Enum status
    public enum ApplicationStatus
    {
        Applied = 1,
        Interviewed = 2,
        Offer = 3,
        Rejected = 4
    }

    public class JobApplication
    {
        //Attributer
        public string? CompanyName { get; set; }
        public string? PositionTitle { get; set; }
        public DateTime ApplicationDate { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime? ResponseDate { get; set; }
        public int SalaryExpectation { get; set; }

        //Metod för att få antal dagar sen ansökan skickades 
        public int GetDaysSinceApplied()
        {
            return (DateTime.Today - ApplicationDate).Days;
        }
        //Metod för utskrift som anropas i JobManager
        public string GetSummary()
        {
            string response = ResponseDate.HasValue ? ResponseDate.Value.ToString("yyyy-MM-dd") : "No response";
            return $"Company: {CompanyName}, Position: {PositionTitle}, Applied: {ApplicationDate:yyyy-MM-dd}, Status: {Status}, Response Date: {response}, Salary Expectation: {SalaryExpectation}kr";
        }
    }
}