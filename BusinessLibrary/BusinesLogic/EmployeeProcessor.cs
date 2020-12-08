using AutoMapper;
using BusinessLibrary.BusinesLogic.BLServices;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel;

namespace BusinessLibrary.BusinesLogic
{
    public class EmployeeProcessor : IEmployeeProcessor
    {
        private IMemoryCache _cache;
        public EmployeeProcessor(IMemoryCache _cache)
        {
            this._cache = _cache;
        }

        public string SaveEmployee(EmployeeVM Employee)
        {
            try
            {
                int EmpID = 0;
                List<EmployeeVM> lstEmp = new List<EmployeeVM>();
                if (_cache.TryGetValue("EmployeeList", out lstEmp))
                {
                    if (lstEmp != null)
                    {
                        EmpID = lstEmp.Select(x => x.EmployeeID).LastOrDefault() + 1;
                    }
                    else
                    {
                        lstEmp = new List<EmployeeVM>();
                        EmpID = 1;
                    }

                    lstEmp.Add(new EmployeeVM
                    {
                        EmployeeID = EmpID,
                        Name = Employee.Name,
                        BirthDate = Employee.BirthDate,
                        TIN = Employee.TIN,
                        EmployeeType = Employee.EmployeeType
                    });

                    _cache.Set("EmployeeList", lstEmp);
                }
                else
                {
                    lstEmp = new List<EmployeeVM>();
                    lstEmp.Add(new EmployeeVM
                    {
                        EmployeeID = 1,
                        Name = Employee.Name,
                        BirthDate = Employee.BirthDate,
                        TIN = Employee.TIN,
                        EmployeeType = Employee.EmployeeType
                    });

                    _cache.Set("EmployeeList", lstEmp);
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }

        public List<EmployeeVM> GetEmployeeList()
        {
            List<EmployeeVM> lstEmp = new List<EmployeeVM>();
            if (_cache.TryGetValue("EmployeeList", out lstEmp))
            {
                if (lstEmp == null)
                {
                    lstEmp = new List<EmployeeVM>();
                }
            }
            else
            {
                lstEmp = new List<EmployeeVM>();
            }
            return lstEmp;
        }

        public string UpdateEmployee(EmployeeVM Employee)
        {
            try
            {
                List<EmployeeVM> lstEmp = new List<EmployeeVM>();
                if (_cache.TryGetValue("EmployeeList", out lstEmp))
                {
                    if (lstEmp != null)
                    {
                        foreach(var item in lstEmp)
                        {
                            if(item.EmployeeID == Employee.EmployeeID)
                            {
                                item.Name = Employee.Name;
                                item.BirthDate = Employee.BirthDate;
                                item.TIN = Employee.TIN;
                                item.EmployeeType = Employee.EmployeeType;
                            }
                        }
                        _cache.Set("EmployeeList", lstEmp);
                        return "Success";
                    }
                    else
                    {
                        return "Error";
                    }
                }
                else
                {
                    return "Error";
                }
            }
            catch(Exception ex)
            {
                return "Error";
            }
        }

        public List<EmployeeTypeVM> GetEmployeeTypeList()
        {
            List<EmployeeTypeVM> lstEmpType = new List<EmployeeTypeVM>();
            if (_cache.TryGetValue("EmployeeTypeList", out lstEmpType))
            {
                if (lstEmpType == null)
                {
                    lstEmpType = new List<EmployeeTypeVM>();
                }
            }
            else
            {
                lstEmpType = new List<EmployeeTypeVM>();
            }
            return lstEmpType;
        }

        public string SaveEmployeeType(EmployeeTypeVM EmployeeType)
        {
            try
            {
                int EmpID = 0;
                List<EmployeeTypeVM> lstEmpType = new List<EmployeeTypeVM>();
                if (_cache.TryGetValue("EmployeeTypeList", out lstEmpType))
                {
                    if (lstEmpType != null)
                    {
                        EmpID = lstEmpType.Select(x => x.EmployeeTypeID).LastOrDefault() + 1;
                    }
                    else
                    {
                        lstEmpType = new List<EmployeeTypeVM>();
                        EmpID = 1;
                    }

                    lstEmpType.Add(new EmployeeTypeVM
                    {
                        EmployeeTypeID = EmpID,
                        EmployeeTypeDescription = EmployeeType.EmployeeTypeDescription,
                        SalaryType = EmployeeType.SalaryType,
                        WorkDaysPerMonth = EmployeeType.WorkDaysPerMonth,
                        TaxPercentage = EmployeeType.TaxPercentage
                    });

                    _cache.Set("EmployeeTypeList", lstEmpType);
                }
                else
                {
                    lstEmpType = new List<EmployeeTypeVM>();
                    lstEmpType.Add(new EmployeeTypeVM
                    {
                        EmployeeTypeID = 1,
                        EmployeeTypeDescription = EmployeeType.EmployeeTypeDescription,
                        SalaryType = EmployeeType.SalaryType,
                        WorkDaysPerMonth = EmployeeType.WorkDaysPerMonth,
                        TaxPercentage = EmployeeType.TaxPercentage
                    });

                    _cache.Set("EmployeeTypeList", lstEmpType);
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }
    }
}
