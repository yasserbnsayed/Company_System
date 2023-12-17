using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Entities;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
        private readonly IMapper mapper;
        public IUnitOfWork UnitOfWork { get; }

        public EmployeeController( IUnitOfWork unitOfWork , IMapper mapper) // [ctrl+.]
        {
            UnitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {

                var mappedEmps = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(await UnitOfWork.EmployeeRepository.GetAll());
                return View(mappedEmps);
            }

            else
            {
                var mappedEmps = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(await UnitOfWork.EmployeeRepository.SearchEmployee(SearchValue));
                return View(mappedEmps);
            }

        }

        public IActionResult Create()
        {
            //ViewBag.Departments = DepartmentRepository.GetAll();

            return View();
        }
        [HttpPost]
        //[ActionName("Submit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                //// Manual Mapping
                //var mappedEmp = new Employee()
                //{
                //    Id = employeeVM.Id,
                //    Address = employeeVM.Address,
                //    Age = employeeVM.Age,
                //    DepartmentId = employeeVM.DepartmentId,
                //    Email = employeeVM?.Email,
                //    IsActive = employeeVM.IsActive,
                //    HireDate = employeeVM.HireDate,
                //    Name = employeeVM.Name,
                //    PhoneNumber = employeeVM.PhoneNumber,
                //    Salary = employeeVM.Salary,
                //};

                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Imgs");
                ////  Mapping using autoMapper package
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                await UnitOfWork.EmployeeRepository.Add(mappedEmp);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = UnitOfWork.DepartmentRepository.GetAll();

            return View(employeeVM);

        }
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var employee = await UnitOfWork.EmployeeRepository.Get(id);
            if (employee == null)
                return NotFound();
            var employeeVM = mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(ViewName, employeeVM);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //    return NotFound();
            //var Employee= EmployeeRepository.Get(id);
            //if (Employee== null)
            //    return NotFound();
            //return View(Employee);
            ViewBag.Departments = UnitOfWork.DepartmentRepository.GetAll();

            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    var employee = mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                    await UnitOfWork.EmployeeRepository.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return View(employeeVM);
                }
            }
            ViewBag.Departments = UnitOfWork.DepartmentRepository.GetAll();
            return View(employeeVM);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            try
            {
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                DocumentSettings.DeleteFile(mappedEmp.ImageName, "Imgs");
                await UnitOfWork.EmployeeRepository.Delete(mappedEmp);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(employeeVM);
            }

        }
    }
}
