namespace EmployeePensionApp;

public class Employee
{
    public long EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime EmploymentDate { get; set; }
    public decimal YearlySalary { get; set; }
    public PensionPlan PensionPlan { get; set; }
}