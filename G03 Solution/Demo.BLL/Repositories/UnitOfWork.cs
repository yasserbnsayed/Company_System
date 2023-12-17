﻿using Demo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }

        public UnitOfWork(IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository)
        {
            DepartmentRepository = departmentRepository;
            EmployeeRepository = employeeRepository;
            
        }
    }
}
