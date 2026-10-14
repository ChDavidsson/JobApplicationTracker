namespace JobApplicationTracker
{
    public class JobManager
    {
        List<JobApplication> JobApplications = new List<JobApplication>();

        public void TestData()
        {
            JobApplications.Add(new JobApplication
            {
                CompanyName = "Gekås",
                PositionTitle = "IT-support",
                Status = ApplicationStatus.Applied,
                ApplicationDate = new DateTime(2026, 04, 20),
                SalaryExpectation = 32000
            });

            JobApplications.Add(new JobApplication
            {
                CompanyName = "IKEA",
                PositionTitle = "IT-tekniker",
                Status = ApplicationStatus.Interviewed,
                ApplicationDate = new DateTime(2026, 03, 30),
                SalaryExpectation = 38000
            });

            JobApplications.Add(new JobApplication
            {
                CompanyName = "Discord",
                PositionTitle = "Operator",
                Status = ApplicationStatus.Applied,
                ApplicationDate = new DateTime(2024, 08, 12),
                SalaryExpectation = 29000
            });
        }

        public void AddJob()
        {
            Console.WriteLine("Company name: ");
            string companyName = Console.ReadLine()!;
            Console.WriteLine("Position: ");
            string position = Console.ReadLine()!;
            Console.WriteLine("Status (1: Applied, 2: Interview, 3: Offer, 4: Rejected): ");
            int statusNumber = int.Parse(Console.ReadLine()!);
            ApplicationStatus status = (ApplicationStatus)statusNumber;
            Console.WriteLine("Application date (yyyy-mm-dd): ");
            DateTime date = DateTime.Parse(Console.ReadLine()!);
            Console.WriteLine("Salary expectation: ");
            int salary = int.Parse(Console.ReadLine()!);
            
            JobApplication job = new JobApplication
            {
                CompanyName = companyName,
                PositionTitle = position,
                Status = status,
                ApplicationDate = date,
                SalaryExpectation = salary,
            };

            JobApplications.Add(job);
            Console.WriteLine("Job application added!");

        }

        public void UpdateStatus()
        {
            if (JobApplications.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Currently, there are no jobs to update.");
                    Console.ResetColor();
                    return;
                }
                
            bool run = true;
            while(run)
            {
                for (int number = 0; number < JobApplications.Count; number++)
                    Console.WriteLine($"{JobApplications[number].GetSummary()}");

                Console.WriteLine("Enter the company name of the application you want to update: ");
                string companyName = Console.ReadLine()!;
                JobApplication job = JobApplications.FirstOrDefault(job => job.CompanyName!.Equals(companyName, StringComparison.OrdinalIgnoreCase))!;
                
                if (job != null)
                {
                    Console.WriteLine("Enter the new status (1: Applied, 2: Interview, 3: Offer, 4: Rejected): ");
                    int statusNumber = int.Parse(Console.ReadLine()!);
                    job.Status = (ApplicationStatus)statusNumber;
                    if (job.Status != ApplicationStatus.Applied)
                    {
                        job.ResponseDate = DateTime.Today;
                    }
                    Console.WriteLine($"Status for {companyName} updated!");
                    run = false;
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Job application not found.");
                    Console.ResetColor();
                }
            }
    
        }

        public void ShowAll()
        {
            if (JobApplications.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No available application");
                Console.ResetColor();
                return;
            }

            List<JobApplication> sortedby = JobApplications.OrderByDescending(app => app.ApplicationDate).ToList();

            foreach(var application in sortedby)
            {
                StatusColor(application.Status);
                Console.WriteLine(application.GetSummary());
                Console.ResetColor();
                Console.WriteLine($"{application.GetDaysSinceApplied()} days since applied ");
            }

        }

        public void ShowByStatus()
        {

            Console.WriteLine("Status: 1: Applied 2: Interview 3: Offer 4: Rejected");
            
            if (!int.TryParse(Console.ReadLine(), out int statusNumber))
            {
                Console.WriteLine("Choose an option (1-4)");
                return;
            }

            ApplicationStatus status = (ApplicationStatus)statusNumber;
            
            List<JobApplication> sortedlist = JobApplications.Where(app => app.Status == status).ToList();

            if(sortedlist.Count == 0)
            {
                Console.WriteLine("No application found");
                return;
            }

            foreach (var application in sortedlist)
            {
                StatusColor(application.Status);
                Console.WriteLine(application.GetSummary());
                Console.ResetColor();
            }
        }

        public void ShowStatistics()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("///STATS///");
            Console.WriteLine($"Total applications: {JobApplications.Count}");

            int applied = JobApplications.Count(job => job.Status == ApplicationStatus.Applied);
            int interview = JobApplications.Count(job => job.Status == ApplicationStatus.Interviewed);
            int offer = JobApplications.Count(job => job.Status == ApplicationStatus.Offer);
            int rejected = JobApplications.Count(job => job.Status == ApplicationStatus.Rejected);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Applied: {applied}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Interviewed: {interview}");   //Färger visas beroende på positivt eller negativ statistik
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Offer: {offer}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Rejected: {rejected}");
            Console.ResetColor();

            //LINQ tagen från inlämingsuppgiften
            var averageResponseTime = JobApplications
                .Where(job => job.ResponseDate != null)
                .Average(job => (job.ResponseDate - job.ApplicationDate)?.TotalDays);

            if (averageResponseTime.HasValue)
            {
                Console.WriteLine($"Average response time: {averageResponseTime.Value:F1} days");
            }
            else
            {
                Console.WriteLine("Currently no average response time.");
            }
        }

        public void SortByDate()
        {
            List<JobApplication> sortedByDate = JobApplications.OrderBy(job => job.ApplicationDate).ToList();

            if (sortedByDate.Count == 0)
            {
                Console.WriteLine("Currently no applications in list");
                return;
            }

            foreach (var application in sortedByDate)
            {
                StatusColor(application.Status);
                Console.WriteLine(application.GetSummary());
            }
        }

        public void RemoveApplication()
        {
            if (JobApplications.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No applications to remove.");
                Console.ResetColor();
                return;
            }

            for (int number = 0; number < JobApplications.Count; number++)
                Console.WriteLine($"{number + 1}.{JobApplications[number].GetSummary()}");

            Console.WriteLine("Number of application you want to remove: ");
            int inputNumber = int.Parse(Console.ReadLine()!);

            if (inputNumber < 1 || inputNumber > JobApplications.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid number.");
                Console.ResetColor();
                return;
            }

            JobApplications.RemoveAt(inputNumber - 1);
            Console.WriteLine("Application removed successfully!");
        }

        public void StatusColor(ApplicationStatus status)
        {
            switch (status)
            {
                case ApplicationStatus.Applied:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                case ApplicationStatus.Interviewed:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case ApplicationStatus.Offer:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                
                case ApplicationStatus.Rejected:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
        }
    }
}