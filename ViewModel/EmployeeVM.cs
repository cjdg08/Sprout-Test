using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class EmployeeVM
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string TIN { get; set; }
        public int EmployeeType { get; set; }
        public string EmployeeTypeDescription { get; set; }
    }

    public class EmployeeTypeVM
    {
        public int EmployeeTypeID { get; set; }
        public string EmployeeTypeDescription { get; set; }
        public int SalaryType { get; set; }
        public double WorkDaysPerMonth { get; set; }
        public double TaxPercentage { get; set; }
    }
}
