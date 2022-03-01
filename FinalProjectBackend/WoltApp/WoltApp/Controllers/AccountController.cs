using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.DTOs.Account;
using WoltDataAccess.DAL;
using WoltEntity.Entities;
using WoltEntity.Utilities.Email;

namespace WoltApp.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        public AccountController(UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager,
                              RoleManager<IdentityRole> roleManager,
                              AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        //GET-Login
        public IActionResult Login()
        {
            return View();
        }
        //Post-Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO login, string ReturnUrl)
        {
            if (!ModelState.IsValid) return View(login);
            AppUser user = await _userManager.FindByEmailAsync(login.Email.ToString());
            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "Email or password wrong");
                return View(login);
            }
            if (!user.IsActivated)
            {
                ModelState.AddModelError(String.Empty, "Please, confirm your Email");
                return View(login);
            }
            var signInResult = await
           _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError(String.Empty, "Please, weit one minute");
                return View(login);
            }
            if (!signInResult.Succeeded)
            {
                //await _signInManager.SignInAsync(user, login.RememberMe);
                ModelState.AddModelError(String.Empty, "Email and Password is Wrong");
                return View(login);
            }
            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        //GET-Register
        public IActionResult Register()
        {
            return View();
        }
        //Post-Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            if (!ModelState.IsValid) return View(register);
            AppUser newUser = new AppUser()
            {
                FullName = register.Fullname,
                Email = register.Email,
                UserName = register.Username
            };
            bool isExistUsername = _userManager.Users.Any(us => us.UserName == newUser.UserName);
            if (isExistUsername)
            {
                ModelState.AddModelError(String.Empty, "This Username already exist. Please use another Username");
                return View();
            }
            bool isExistEmail = _userManager.Users.Any(us => us.Email == newUser.Email);
            if (isExistEmail)
            {
                ModelState.AddModelError(String.Empty, "This Email already exist. Please use another Email");
                return View();
            }
            IdentityResult identityResult = await _userManager.CreateAsync(newUser, register.Password);
            if (identityResult.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = register.Email }, Request.Scheme);
                EmailHelper emailHelper = new EmailHelper();
                bool emailResponse = emailHelper.SendEmail(register.Email, confirmationLink);
                await _signInManager.SignInAsync(newUser,false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(register);
            }
        }
        //Logout Account
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        //GET-ForgotPassword
        public IActionResult ForgotPassword()
        {
            return View();
        }
        //Post-ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgotPassword)
        {
            if (!ModelState.IsValid) return View(forgotPassword);
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User Is Not Found");
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
            EmailHelper emailHelper = new EmailHelper();
            emailHelper.SendEmail(user.Email, callback);
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }
        //GET-ForgotPasswordConfirmation
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        //GET-ResetPassword
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordDTO { Token = token, Email = email };
            return View(model);
        }
        //Post-ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPassword)
        {
            if (!ModelState.IsValid) return View(resetPassword);
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User Is Not Found");
                return View();
            }
            IdentityResult identityResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(resetPassword);
            }
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }
        //GET-ResetPasswordConfirmation
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        //GET-AccountSetting
        public IActionResult AccountSetting()
        {
            return View();
        }
        //Post-AccountSetting
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AccountSetting(string ReturnUrl)
        {
            if (ReturnUrl != null)
            {
                return LocalRedirect(ReturnUrl);
            }
            return View();
        }
        //public IActionResult ChangePassword()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ChangePassword(ChangePasswordDTO password)
        //{
        //    if (!ModelState.IsValid) return View(password);
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError(string.Empty, "User is Not Found");
        //        return View();
        //    }
        //    var checkPasword = await _userManager.CheckPasswordAsync(user, password.CurrentPassword);
        //    if (!checkPasword)
        //    {
        //        ModelState.AddModelError(string.Empty, "Incorrect Password");
        //        return View(password);
        //    }
        //    var result = await _userManager.ChangePasswordAsync(user, password.CurrentPassword,
        //                                                                password.NewPassword);
        //    if (!result.Succeeded)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //        return View(password);
        //    }
        //    await _signInManager.RefreshSignInAsync(user);
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
