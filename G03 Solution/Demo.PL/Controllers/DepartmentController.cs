using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Security.Cryptography.Xml;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository departmentRepository; // attribute or field
        public IDepartmentRepository departmentRepository { get; } // property
        public DepartmentController(IDepartmentRepository _departmentRepository)
        {
            //this.departmentRepository = _departmentRepository;
            departmentRepository = _departmentRepository;
        }
        public IActionResult Index()
        {
   //         -viewData : Is Dictionary Object { Key , Value } (Introduces In Asp .Net Framwork 3.5 )
   //=> It Helps Us To Transfer Data From Controller To View.
   //=> It Enforce Type Safety(ViewData Is Strongly Typed)
            ViewData["Message"] = "Hello View Data";

   //         -viewBag : Is Dynamic Property(Introduced In Asp.Net Framework 4.0 and Used dynamic Features)
   //=> It Helps Us To Transfer Data From Controller to View. 
   //=> It Doesn't Enforce Type Safety ( ViewBag Is Loosly Typed )
            ViewBag.Message = "Hello View Bag";


            return View(departmentRepository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                departmentRepository.Add(department);
                TempData["Message"] = "Department is Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(department);

        }
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var Department = departmentRepository.Get(id);
            if (Department == null)
                return NotFound();
            return View(ViewName, Department);
        }
        public IActionResult Edit(int? id)
        {
            //if (id == null)
            //    return NotFound();
            //var Department = departmentRepository.Get(id);
            //if (Department == null)
            //    return NotFound();
            //return View(Department);

            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int? id, Department department)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid) // Server Side Validation
            { 
                try
                {
                    departmentRepository.Update(department);
                    return RedirectToAction(nameof (Index));
                }
                catch(Exception ex)
                {
                    return View(department);
                }
            }
            return View(department);
        }
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete([FromRoute]int? id, Department department)
        {
            if (id != department.Id)
                return BadRequest();
            try
            {
                departmentRepository.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(department);
            }
        }
    }
}
