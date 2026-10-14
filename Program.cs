namespace JobApplicationTracker;

class Program
{
    static void Main(string[] args)
    {
        JobManager test = new JobManager();
        test.AddJob();
        test.UpdateStatus();
        test.ShowByStatus();
        test.ShowAll();
        test.ShowStatistics();
    }
}
