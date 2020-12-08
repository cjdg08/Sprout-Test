using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace BusinessLibrary.BusinesLogic.BLServices
{
    public interface IEmployeeProcessor
    {
        string SaveEmployee(EmployeeVM Employee);
        List<EmployeeVM> GetEmployeeList();
        string UpdateEmployee(EmployeeVM Employee);
        List<EmployeeTypeVM> GetEmployeeTypeList();
        string SaveEmployeeType(EmployeeTypeVM EmployeeType);
    }
}
