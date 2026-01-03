using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IdentityApp.Controllers
{
    [Authorize(Roles ="admin")]
    public class UsersController:Controller
    {
        private UserManager<AppUser>_UserManager;
        private RoleManager<AppRole> _roleManager;
        public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _UserManager = userManager;
            _roleManager=roleManager;
        }
       // [AllowAnonymous] her hangi biri görebilsin diye yazabiliriz
        public IActionResult Index()
        {
            
            return View(_UserManager.Users);
        }
        public async Task<IActionResult> Edit( string id)
        {
            if(id==null)
            {
                return RedirectToAction("Index");
            }
            var user= await _UserManager.FindByIdAsync(id);
            if(user!=null)
            {
                ViewBag.Roles=await _roleManager.Roles.Select(i=>i.Name).ToListAsync();
                return View(new EditViewModels
                {
                    UserId=id, 
                    FullName=user.FullName,
                    Email=user.Email,
                    SelectedRoles= await _UserManager.GetRolesAsync(user)
                                    });
            }
            return RedirectToAction("Index");
            
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditViewModels model)
        {
            if(id!=model.UserId)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                var user=await _UserManager.FindByIdAsync(id);
                if(user!=null)
                {
                    user.Email=model.Email;
                    user.FullName=model.FullName;
                    var result=await _UserManager.UpdateAsync(user);
                    if(result.Succeeded && !string.IsNullOrEmpty(model.Password))
                    {
                        await _UserManager.RemovePasswordAsync(user);
                        await _UserManager.AddPasswordAsync(user, model.Password);
                    }
                    if(result.Succeeded)
                    {
                        await _UserManager.RemoveFromRolesAsync(user, await _UserManager.GetRolesAsync(user));
                        if(model.SelectedRoles!=null)
                        {
                            await _UserManager.AddToRolesAsync(user, model.SelectedRoles);
                        }
                        return RedirectToAction("Index");
                    }
                    foreach (IdentityError err in result.Errors)
                    {
                        ModelState.AddModelError("",err.Description);
                    }

                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user=await _UserManager.FindByIdAsync(id);
            if(user!=null)
            {
                await _UserManager.DeleteAsync(user);
                
            }
            return RedirectToAction("Index");
        }
    }
}