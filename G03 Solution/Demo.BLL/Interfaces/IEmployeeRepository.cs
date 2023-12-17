﻿using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IEnumerable<Employee> GetEmployeesByDepartmentName(string departmentName);
        //Employee Get(int? id);
        //IEnumerable<Employee> GetAll();
        //int Add(Employee Employee);
        //int Update(Employee Employee);
        //int Delete(Employee Employee);

        Task<IEnumerable<Employee>> SearchEmployee(string value);
    }
}
