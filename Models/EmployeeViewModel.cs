using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VCSSystem_Task1.Models
{
    public class EmployeeViewModel
    {
        public EmployeeModel NewEmployee { get; set; }
        public List<EmployeeModel> Employees { get; set; }
    }
}