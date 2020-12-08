using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLibrary.BusinesLogic.BLServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Sprout_Software_Dev_Test.Models;
using ViewModel;

namespace Sprout_Software_Dev_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMemoryCache _cache;
        private IEmployeeProcessor iEmployee;

        public HomeController(ILogger<HomeController> logger, IMemoryCache _cache, IEmployeeProcessor iEmployee)
        {
            _logger = logger;
            this._cache = _cache;
            this.iEmployee = iEmployee;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveEmployee(string Name, DateTime BirthDate, string TIN, int EmployeeType)
        {
            EmployeeVM emp = new EmployeeVM
            {
                EmployeeID = 1,
                Name = Name,
                BirthDate = BirthDate,
                TIN = TIN,
                EmployeeType = EmployeeType
            };
            string result = iEmployee.SaveEmployee(emp);
            return Json(result);
        }

        public JsonResult GetEmployeeList()
        {
            var data = iEmployee.GetEmployeeList();
            var empType = iEmployee.GetEmployeeTypeList();
            foreach(var item in data)
            {
                item.EmployeeTypeDescription = empType.Where(x => x.EmployeeTypeID == item.EmployeeType).FirstOrDefault().EmployeeTypeDescription;
            }
            return Json(data);
        }

        public IActionResult AddEmployee()
        {
            var ddlEmpType = iEmployee.GetEmployeeTypeList();
            List<SelectListItem> ddl = new List<SelectListItem>();
            ddl.Add(new SelectListItem
            {
                Value = "",
                Text = "- Select Employee Type -"
            });
            foreach (var item in ddlEmpType)
            {
                ddl.Add(new SelectListItem
                {
                    Value = item.EmployeeTypeID.ToString(),
                    Text = item.EmployeeTypeDescription
                });
            }
            ViewBag.EmployeeType = ddl;

            return View();
        }

        public IActionResult EditEmployee(int ID)
        {
            var emp = iEmployee.GetEmployeeList().Where(x => x.EmployeeID == ID).FirstOrDefault();

            var ddlEmpType = iEmployee.GetEmployeeTypeList();
            List<SelectListItem> ddl = new List<SelectListItem>();
            ddl.Add(new SelectListItem
            {
                Value = "",
                Text = "- Select Employee Type -"
            });
            foreach (var item in ddlEmpType)
            {
                ddl.Add(new SelectListItem
                {
                    Value = item.EmployeeTypeID.ToString(),
                    Text = item.EmployeeTypeDescription,
                    Selected = (item.EmployeeTypeID == emp.EmployeeType ? true : false)
                });
            }
            ViewBag.EmployeeType = ddl;

            return View(emp);
        }

        public JsonResult UpdateEmployee(int EmployeeID, string Name, DateTime BirthDate, string TIN, int EmployeeType)
        {
            EmployeeVM emp = new EmployeeVM
            {
                EmployeeID = EmployeeID,
                Name = Name,
                BirthDate = BirthDate,
                TIN = TIN,
                EmployeeType = EmployeeType
            };
            string result = iEmployee.UpdateEmployee(emp);
            return Json(result);
        }

        public IActionResult ProcessPayroll(int ID)
        {
            var emp = iEmployee.GetEmployeeList().Where(x => x.EmployeeID == ID).FirstOrDefault();
            var ddlEmpType = iEmployee.GetEmployeeTypeList();
            var empType = ddlEmpType.Where(x => x.EmployeeTypeID == emp.EmployeeType).FirstOrDefault();
            ViewBag.SalaryType = empType.SalaryType;
            ViewBag.WorkDaysPerMonth = empType.WorkDaysPerMonth;
            ViewBag.TaxPercentage = empType.TaxPercentage;

            List<SelectListItem> ddl = new List<SelectListItem>();
            ddl.Add(new SelectListItem
            {
                Value = "",
                Text = "- Select Employee Type -"
            });
            // test
            foreach (var item in ddlEmpType)
            {
                ddl.Add(new SelectListItem
                {
                    Value = item.EmployeeTypeID.ToString(),
                    Text = item.EmployeeTypeDescription,
                    Selected = (item.EmployeeTypeID == emp.EmployeeType ? true : false)
                });
            }
            ViewBag.EmployeeType = ddl;

            return View(emp);
        }

        public IActionResult EmployeeType()
        {
            return View();
        }

        public JsonResult GetEmployeeTypeList()
        {
            var data = iEmployee.GetEmployeeTypeList();
            return Json(data);
        }

        public JsonResult SaveEmployeeType(string EmployeeTypeDescription, int SalaryType, double WorkDaysPerMonth, double TaxPercentage)
        {
            EmployeeTypeVM empType = new EmployeeTypeVM
            {
                EmployeeTypeID = 1,
                EmployeeTypeDescription = EmployeeTypeDescription,
                SalaryType = SalaryType,
                WorkDaysPerMonth = WorkDaysPerMonth,
                TaxPercentage = TaxPercentage
            };
            string result = iEmployee.SaveEmployeeType(empType);
            return Json(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
