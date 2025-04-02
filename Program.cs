using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using EmployeePensionApp;

public class Program
{
    static void Main()
    {
        List<Employee> employees = GetSampleData();

        // Feature 1: Print all employees sorted
        var sortedEmployees = employees
            .OrderByDescending(e => e.YearlySalary)
            .ThenBy(e => e.LastName)
            .ToList();

        JsonSerializerOptions options = new()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        Console.WriteLine("All Employees:");
        Console.WriteLine(JsonSerializer.Serialize(sortedEmployees, options));

        // Feature 2: Quarterly Upcoming Enrollees
        DateTime currentDate = new(2025, 4, 1); // Hardcoded to match lab's context
        var nextQuarter = GetNextQuarterDates(currentDate);

        var upcomingEnrollees = employees
            .Where(e => e.PensionPlan == null &&
                        e.EmploymentDate.AddYears(3) >= nextQuarter.start &&
                        e.EmploymentDate.AddYears(3) <= nextQuarter.end)
            .OrderByDescending(e => e.EmploymentDate)
            .ToList();

        Console.WriteLine("\nQuarterly Upcoming Enrollees:");
        Console.WriteLine(JsonSerializer.Serialize(upcomingEnrollees, options));
    }

    static List<Employee> GetSampleData()
    {
        return new List<Employee>
        {
            new()
            {
                EmployeeId = 1,
                FirstName = "Daniel",
                LastName = "Agar",
                EmploymentDate = new DateTime(2018, 1, 17),
                YearlySalary = 105945.50m,
                PensionPlan = new PensionPlan
                {
                    PlanReferenceNumber = "EX1089",
                    EnrollmentDate = new DateTime(2023, 1, 17),
                    MonthlyContribution = 100.00m
                }
            },
            new()
            {
                EmployeeId = 2,
                FirstName = "Benard",
                LastName = "Shaw",
                EmploymentDate = new DateTime(2022, 9, 3),
                YearlySalary = 197750.00m,
                PensionPlan = null
            },
            new()
            {
                EmployeeId = 3,
                FirstName = "Carly",
                LastName = "Agar",
                EmploymentDate = new DateTime(2014, 5, 16),
                YearlySalary = 842000.75m,
                PensionPlan = new PensionPlan
                {
                    PlanReferenceNumber = "SM2307",
                    EnrollmentDate = new DateTime(2019, 11, 4),
                    MonthlyContribution = 1555.50m
                }
            },
            new()
            {
                EmployeeId = 4,
                FirstName = "Wesley",
                LastName = "Schneider",
                EmploymentDate = new DateTime(2022, 7, 21),
                YearlySalary = 74500.00m,
                PensionPlan = null
            },
            new()
            {
                EmployeeId = 5,
                FirstName = "Anna",
                LastName = "Wilford",
                EmploymentDate = new DateTime(2022, 6, 15),
                YearlySalary = 85750.00m,
                PensionPlan = null
            },
            new()
            {
                EmployeeId = 6,
                FirstName = "Yosef",
                LastName = "Tesfalem",
                EmploymentDate = new DateTime(2022, 10, 31),
                YearlySalary = 100000.00m,
                PensionPlan = null
            }
        };
    }

    static (DateTime start, DateTime end) GetNextQuarterDates(DateTime currentDate)
    {
        int currentQuarter = (currentDate.Month - 1) / 3 + 1;
        int nextQuarter = currentQuarter + 1;
        int year = currentDate.Year;

        if (nextQuarter > 4)
        {
            nextQuarter = 1;
            year++;
        }

        int startMonth = (nextQuarter - 1) * 3 + 1;
        DateTime start = new(year, startMonth, 1);
        DateTime end = start.AddMonths(3).AddDays(-1);

        return (start, end);
    }
}