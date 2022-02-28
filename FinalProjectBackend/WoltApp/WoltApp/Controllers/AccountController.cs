using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.DTOs.Account;
using WoltDataAccess.DAL;
using WoltEntity.Entities;

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
        public IActionResult Login()
        {
            return View();
        }
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
        public IActionResult Register()
        {
            return View();
        }
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
                //var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                //var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                //EmailHelper emailHelper = new EmailHelper();
                //bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);

                //if (emailResponse)
                //    return RedirectToAction("Index");
                //else
                //{
                //    // log email failed 
                //}
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
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
