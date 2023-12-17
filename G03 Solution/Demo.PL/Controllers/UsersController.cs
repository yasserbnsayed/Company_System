using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Entities;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userManager) 
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
                return View(userManager.Users);

            else
            {
                var user = await userManager.FindByEmailAsync(SearchValue);
                return View(new List<ApplicationUser>() { user });
            }

        }

        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(ViewName, user);
        }
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, ApplicationUser updatedUser)
        {
            if (id != updatedUser.Id)
                return BadRequest();
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    // we should first get the user from the database before updating it
                    var user = await userManager.FindByIdAsync(id);

                    // then we update the data that we want to make the user can update
                    user.UserName = updatedUser.UserName;
                    user.NormalizedUserName = updatedUser.UserName.ToUpper();
                    user.PhoneNumber = updatedUser.PhoneNumber;

                    // if we want to make the user can update its Email, we should update the SecurityStamp too 
                    //user.Email = updatedUser.Email;
                    //user.SecurityStamp = updatedUser.SecurityStamp;

                    // then we execute the operation of update on it
                    var result = await userManager.UpdateAsync(user);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return View(updatedUser);
        }
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]string id, ApplicationUser deletedUser)
        {
            if (id != deletedUser.Id)
                return BadRequest();
            try
            {
                // we should first get the user from the database before deleting it
                var user = await userManager.FindByIdAsync(deletedUser.Id);

                // then we execute the operation of delete on it
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

                foreach (var error in result.Errors) 
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(deletedUser);
            }
            catch (Exception ex)
            {
                throw;
            }
            

        }
    }
}