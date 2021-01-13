using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nkuotomasyon.data.Abstract;
using nkuotomasyon.entity;
using NKUOtomayon.WebUI.Identity;
using NKUOtomayon.WebUI.Models;

namespace NKUOtomayon.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<User> _userManager;
        private IStudentRepository _studentRepository;
        private IPdfFileRepository _pdfFileRepository;
        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IStudentRepository studentRepository, IPdfFileRepository pdfFileRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _studentRepository = studentRepository;
            _pdfFileRepository = pdfFileRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public  IActionResult GetLessonsByStudent()
        {
            
           var id= _userManager.GetUserId(User);
           var student = _studentRepository.GetStudentById(id);

           


            var lesson = _studentRepository.GetStudentLessons(id);
           student.Grades = _studentRepository.GetStudentGrades(id);

            for (int i = 0; i < lesson.Count; i++) // this can also be done from api
            {
                lesson[i].Teacher = _studentRepository.GetTeacherForLesson(lesson[i].LessonCode);
            }
            var lessonlistModel=new LessonListModel()
            {
                Lesson = lesson,
                Grades = student.Grades
            };

            return View(lessonlistModel);
        }

        public IActionResult Grades()
        {
            var id = _userManager.GetUserId(User);
            var student = _studentRepository.GetStudentById(id);
            var lesson = _studentRepository.GetStudentLessons(id);
            student.Grades = _studentRepository.GetStudentGrades(id);
            var gradeListModel=new LessonListModel()
            {
                Lesson = lesson,
                Grades = student.Grades
            };
            return View(gradeListModel);
        }

        public IActionResult ExamDate()
        {
            var id = _userManager.GetUserId(User);
            var lesson = _studentRepository.GetStudentLessons(id);
            return View(lesson);
        }

        public IActionResult Syllabus()
        {
            var id = _userManager.GetUserId(User);
            var lesson = _studentRepository.GetStudentLessons(id);
            return View(lesson);
        }

        public IActionResult PdfByLesson(string lessonCode)
        {
           var pdf= _pdfFileRepository.GetAllPdf(lessonCode);
            return View(pdf);
        }
    }
}
