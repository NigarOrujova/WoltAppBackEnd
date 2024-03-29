﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.DTOs.Account;
using WoltDataAccess.DAL;
using WoltEntity.Entities;
using WoltEntity.Utilities.Email;
using static WoltEntity.Utilities.File.Helper;

namespace WoltApp.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager,
                              RoleManager<IdentityRole> roleManager,
                              AppDbContext context,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
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
                EmailHelper emailHelper = new EmailHelper(_configuration.GetSection("EmailConfirmation:fromEmail").Value, _configuration.GetSection("EmailConfirmation:fromPassword").Value);
                bool emailResponse = emailHelper.SendEmail(register.Email, confirmationLink);
                await _userManager.AddToRoleAsync(newUser, UserRoles.Member.ToString());
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
            EmailHelper emailHelper = new EmailHelper(_configuration.GetSection("EmailConfirmation:Email").Value, _configuration.GetSection("EmailConfirmation:Password").Value);
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
        //GET-AccountProfil
        public async Task<IActionResult> AccountProfil()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            RegisterDTO profile = new RegisterDTO
            {
                    Email = user.Email,
                    Fullname = user.FullName,
                    Username = user.UserName
            };
            return View(profile);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AccountProfil(RegisterDTO profil)
        //{

        //}
        //GET-AccountSetting
        //public async Task<IActionResult> AccountSetting()
        //{
        //    AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
        //    RegisterDTO profile = new RegisterDTO
        //    {
        //        Email = user.Email,
        //        Fullname = user.FullName
        //    };
        //    return View(profile);
        //}
        //Post-AccountSetting
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AccountProfil(string ReturnUrl)
        {
            if (ReturnUrl != null)
            {
                return LocalRedirect(ReturnUrl);
            }
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO password)
        {
            if (!ModelState.IsValid) return View(password);
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User is Not Found");
                return View();
            }
            var checkPasword = await _userManager.CheckPasswordAsync(user, password.CurrentPassword);
            if (!checkPasword)
            {
                ModelState.AddModelError(string.Empty, "Incorrect Password");
                return View(password);
            }
            var result = await _userManager.ChangePasswordAsync(user, password.CurrentPassword,
                                                                        password.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(password);
            }
            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ChangeUsername()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUsername(ChangeUserNameDTO username)
        {
            if (!ModelState.IsValid) return View(username);
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User is Not Found");
                return View(username);
            }
            var checkPasword = await _userManager.CheckPasswordAsync(user, username.Password);
            if (!checkPasword)
            {
                ModelState.AddModelError(string.Empty, "Incorrect Password");
                return View(username);
            }
            var result = await _userManager.SetUserNameAsync(user, username.NewUsername);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(username);
            }
            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction("Index", "Home");
        }
        //public IActionResult ChangeEmail()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ChangeEmail(ChangeEmailDTO email)
        //{
        //    if (!ModelState.IsValid) return View(email);
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError(string.Empty, "User is Not Found");
        //        return View(email);
        //    }
        //    var checkPasword = await _userManager.CheckPasswordAsync(user, email.Password);
        //    if (!checkPasword)
        //    {
        //        ModelState.AddModelError(string.Empty, "Incorrect Password");
        //        return View(email);
        //    }
        //    var result = await _userManager.SetUserNameAsync(user, email.NewEmail);
        //    if (!result.Succeeded)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //        return View(email);
        //    }
        //    await _signInManager.RefreshSignInAsync(user);
        //    return RedirectToAction("Index", "Home");
        //}


        #region CreateRole
        //public async Task CreateRole()
        //{
        //    foreach (var role in Enum.GetValues(typeof(UserRoles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }
        //    }
        //}
        #endregion
    }
}
