using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using nkuotomasyon.data.Abstract;
using nkuotomasyon.entity;
using NKUOtomayon.WebUI.EmailServices;
using NKUOtomayon.WebUI.Extensions;
using NKUOtomayon.WebUI.Helpers;
using NKUOtomayon.WebUI.Identity;
using NKUOtomayon.WebUI.Models;


namespace NKUOtomayon.WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
     
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        private IStudentRepository _studentRepository;
        private ITeacherRepository _teacherRepository;
        private IPhotoRepository _photoRepository;
        private ISemesterRepository _semesterRepository;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, IStudentRepository studentRepository, ITeacherRepository teacherRepository, IPhotoRepository photoRepository, ISemesterRepository semesterRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _photoRepository = photoRepository;
            _semesterRepository = semesterRepository;
        }

        public IActionResult Login(string ReturnUrl = null)
        {
            return View(new LoginModel
            {
                ReturnUrl = ReturnUrl,
            });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
           
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var user = await _userManager.FindByNameAsync(Convert.ToString(model.UserName));
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı adı bulunamadı!");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Kullanıcı onaylanmamış");
                return View(model);
            }

           
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                var photos = _photoRepository.GetPhotoByStudentId(user.Id);
                if (photos!=null)
                {
                    TempData.Put("Url",photos.Url);
                }

                if (User.IsInRole("öğrenci"))
                {
                    var cal = new CalculaterSemester();
                    cal.CalculateSemester(user);
                }
              
                

                return Redirect(model.ReturnUrl??"~/");
            }
            return View(model);
        }

       

        public IActionResult RegisterTeacher()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterTeacher(RegisterTeacherModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var teacher=new User()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            var result = await _userManager.CreateAsync(teacher, model.Password);
            var teacherToSql=new Teacher()
            {
                Id = teacher.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName
            };
            _teacherRepository.Create(teacherToSql);
            return View();
        }
        public IActionResult Register()
        {
          
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User()
            {
               
                UserName = model.UserName,
                TcNumber = model.TcNumber,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });


                //this part of code need the mail sign in value.From appsettings.
                //await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız.", $"Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:44380{url}'>tıklayınız</a>");
                var student=new Student()
                {
                    Id = user.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    SchoolNumber = model.UserName,
                    TcNumber = model.TcNumber,
                    PhoneNumber =model.PhoneNumber
                };
                _studentRepository.Create(student);
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Bilinmeyen bir hata oluştu");
            return View(model);
        }

        public IActionResult example()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData.Put("message", new AlertMessage()
            {
                Alert = "warning",
                Message = "Başarılı bir şekilde çıkış yapıldı",
                Title = "Çıkış Sağlandı"
            });
            
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Message = "Hatalı bir bilgi girdiniz. Lütfen yöneticiniz ile görüşün.",
                    Alert = "danger",
                    Title = "HATALI TOKEN"
                });
                return View();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Alert = "success",
                        Message = "Hesabınızın onay işlemi yapıldı.",
                        Title = "Hesabınız Onaylandı!"
                    });
                    return View();
                }
            }
            TempData.Put("message", new AlertMessage()
            {
                Title = "Hesabınız onaylanmadı",
                Alert = "danger",
                Message = "Email hesabınıza gelen mail ile hesabınızı onaylayın."
            });
            return View();
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return View();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = code
            });
            await _emailSender.SendEmailAsync(Email, "Şifre Sıfırlama", $"Parola yenilemek için linke <a href='https://localhost:44380{url}'>tıklayınız</a>");
            return View();
        }

        public IActionResult ResetPassword(string userId, string token)
        {
            if (User == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ResetPasswordModel
            {
                Token = token
            };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();

        }
    }
}
