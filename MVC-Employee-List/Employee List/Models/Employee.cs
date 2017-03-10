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

    public class Login
    {
        public int ID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    public class EmployeeContext: DbContext
    {
        public EmployeeContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Login> Logins { get; set; }
    }
}