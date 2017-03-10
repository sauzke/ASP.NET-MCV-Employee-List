using System;
using System.Data.Entity;

namespace Employee_List.Models
{
    public class Employee
    {        
        public int ID { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public DateTime start_date { get;  set; }
        public DateTime end_date { get; set; }
        public byte[] image { get; set; }
    }

    public class EmployeeDisplay
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string imageString { get; set; }
    }

    public class EmployeeContext: DbContext
    {
        public EmployeeContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}