using System.Security.Cryptography;
using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<AppUser>_userManager;
        private readonly RoleManager<AppRole>_roleManager;
        private readonly SignInManager<AppUser>_singInManager;
        private IEmailSender _emailsender;
        public AccountController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> singInManager, IEmailSender IEmailsender)
        {
            _userManager=userManager;
            _roleManager= roleManager;
            _singInManager=singInManager;
            _emailsender=IEmailsender;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVİewModels model)
        {
            if(ModelState.IsValid)
            {
                var user=await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    await _singInManager.SignOutAsync();
                    if(!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError("", "Hesabınızı onaylayınız");
                        return View(model);
                    }
                    var result= await _singInManager.PasswordSignInAsync(user, model.Password,model.RememberMe,true);
                    if(result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user,null);
                        return RedirectToAction("Index","Home");
                    }
                    else if(result.IsLockedOut)
                    {
                        var lockoutDate=await _userManager.GetLockoutEndDateAsync(user);
                        var timeleft=lockoutDate.Value-DateTime.UtcNow;
                        ModelState.AddModelError("",$"Hesabınız kitlendi, Lütfen {timeleft.Minutes}dakika sonra deneyiniz" );
                    }else
                    {
                        ModelState.AddModelError("", "Parolanız hatalı");
                    }
                }
                else
                {
                    ModelState.AddModelError("","Bu email adresi ile hesap bulunamadı");
                }
            }
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModels model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var token=await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url=Url.Action("ConfirmEmail","Account",new{Id=user.Id,token=token});
                    
                    await _emailsender.SendEmailAsync(user.Email,"Hesap Onayı",$"Lüten email hesabınızı onaylamak için linke tıklayınız <a href='http://localhost:5034{url}'>tıklayınız</a>");

                    TempData["message"] = "Email hesabınızdaki onay mailine tıklayınız";
                    return RedirectToAction("Login","Account");
                }
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }
        public async Task<IActionResult>ConfirmEmail(string Id, string token)
        {
            if(Id==null || token==null)
            {
                TempData["message"]="Gecersiz token bilgisi";
                return View();
            }
            var user= await _userManager.FindByIdAsync(Id);
            if(user != null)
            {
                var result=await _userManager.ConfirmEmailAsync(user,token);
                if(result.Succeeded)
                {
                    TempData["message"] = "Hesabınız onaylandı";
                    return RedirectToAction("Login","Account");
                }
            }
            TempData["message"] = "Kullanıcı bulunamadı";
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _singInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public  IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if(string.IsNullOrEmpty(Email))
            {
                TempData["message"] = "Eposta adresinizi Giriniz";
                return View();
            }
            var user =await _userManager.FindByEmailAsync(Email);
            if(user==null)
            {
                TempData["message"] = "Eposta adresi ile eşlesen bir kayıt yok";
                return View();
            }
            var token=await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = Url.Action("ResetPassword", "Account", new { user.Id, token });

            await _emailsender.SendEmailAsync(Email,"Parola Sıfırlama",$"Parolanızı Yenilemek İçin linke tıklayınız <a href='http://localhost:5034{url}'>tıklayınız</a>");

            TempData["message"]="Eposta adresinize gönderilen link ile sifrenizi sıfırlayabilirsiniz";
            return View();
        }
        public IActionResult ResetPassword(string Id, string token)
        {
            if(Id==null || token==null)
            {
                return RedirectToAction("Login");
            }
            var model= new ResetPasswordModels{Token=token};
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModels model)
        {
            if(ModelState.IsValid)
            {
                var users=await _userManager.FindByEmailAsync(model.Email);
                if(users==null)
                {
                    TempData["message"] ="Bu Email adresi ile eşlesen kulanıcı yok";
                    return RedirectToAction("Login");
                }
                var result=await _userManager.ResetPasswordAsync(users, model.Token, model.Password);
                if(result.Succeeded)
                {
                    TempData["message"] ="Sifreniz değiştirildi";
                    return RedirectToAction("Login");
                }
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }
    }
}