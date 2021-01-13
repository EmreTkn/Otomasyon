using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using nkuotomasyon.data.Abstract;
using nkuotomasyon.entity;
using NKUOtomayon.WebUI.Helpers;
using NKUOtomayon.WebUI.Identity;
using NKUOtomayon.WebUI.Models;

namespace NKUOtomayon.WebUI.Controllers
{

    public class TeacherController : Controller
    {
        private UserManager<User> _userManager;
        private IStudentRepository _studentRepository;
        private ILessonRepository _lessonRepository;
        private IGradesRepository _gradesRepository;
        private ITeacherRepository _teacherRepository;
        private Cloudinary _cloudinary;
        private IOptions<CloudinarySettings> _cloudinaryConfig;
        private IPdfFileRepository _pdfFileRepository;


        public TeacherController(UserManager<User> userManager, IStudentRepository studentRepository, ILessonRepository lessonRepository, IGradesRepository gradesRepository, ITeacherRepository teacherRepository, IOptions<CloudinarySettings> cloudinaryConfig, IPdfFileRepository pdfFileRepository)
        {
            _userManager = userManager;
            _studentRepository = studentRepository;
            _lessonRepository = lessonRepository;
            _gradesRepository = gradesRepository;
            _teacherRepository = teacherRepository;
            _cloudinaryConfig = cloudinaryConfig;
            _pdfFileRepository = pdfFileRepository;
            Account account = new Account(_cloudinaryConfig.Value.CloudName, _cloudinaryConfig.Value.ApiKey, _cloudinaryConfig.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public IActionResult TeacherLessonList()
        {
            var id = _userManager.GetUserId(User);
            var teacher = _teacherRepository.GeTeacherById(id);
            var lessons = _teacherRepository.GetTeacherLessonList(id);
            return View(lessons);
        }
        public IActionResult StudentListByLesson(string lessonCode)
        {
            var studyLessons = _studentRepository.GetStudentsByLesson(lessonCode);
            StudentListByLessonModel students = new StudentListByLessonModel();
            foreach (var item in studyLessons)
            {
                students.Students.Add(_studentRepository.GetStudentById(item.StudentId));
            }

            students.LessonCode = lessonCode;
            return View(students);
        }

        public IActionResult EditStudentGrades(string lessonCode, string studentId)
        {
            var grade = _gradesRepository.GetGradesbyStudentAndLesson(lessonCode, studentId);
            var lesson = _lessonRepository.GetByIdLesson(lessonCode);
            var gradesModel = new EditGradesFromTeacherModel();
            if (grade != null)
            {
                gradesModel.LessonCode = lessonCode;
                gradesModel.LessonName = grade.Lesson.LessonName;
                gradesModel.StudentId = studentId;
                gradesModel.MakeUpExam = grade.MakeUpExam;
                gradesModel.FinalTerm = grade.FinalExam;
                gradesModel.Average = grade.Average;
                gradesModel.MakeUpExam = grade.MakeUpExam;
                gradesModel.FailedGrades = grade.FailedLowGrade;
                gradesModel.FailedAbsenteeism = grade.FailedAbsenteeism;
                gradesModel.MidTerm = grade.MidTerm;
                gradesModel.LetterNote = grade.GradeLetter;
            }
            else
            {
                gradesModel.LessonCode = lessonCode;
                gradesModel.StudentId = studentId;
                gradesModel.LessonName = lesson.LessonName;
            }

            return View(gradesModel);

        }
        [HttpPost]
        public IActionResult EditStudentGrades(EditGradesFromTeacherModel grade)
        {
            var gradebyStudent = _gradesRepository.GetGradesbyStudentAndLesson(grade.LessonCode, grade.StudentId);
            if (gradebyStudent == null)
            {

                var newGrade = new Grade();
                newGrade.MidTerm = grade.MidTerm;
                newGrade.Average = grade.Average;
                newGrade.FinalExam = grade.FinalTerm;
                newGrade.MakeUpExam = grade.MakeUpExam;
                newGrade.GradeLetter = grade.LetterNote;
                newGrade.FailedAbsenteeism = grade.FailedAbsenteeism;
                newGrade.FailedLowGrade = grade.FailedGrades;
                _gradesRepository.CreateGrade(newGrade, grade.StudentId, grade.LessonCode);


            }
            else
            {
                gradebyStudent.Average = grade.Average;
                gradebyStudent.FailedAbsenteeism = grade.FailedAbsenteeism;
                gradebyStudent.FailedLowGrade = grade.FailedGrades;
                gradebyStudent.FinalExam = grade.FinalTerm;
                gradebyStudent.GradeLetter = grade.LetterNote;
                gradebyStudent.MakeUpExam = grade.MakeUpExam;
                gradebyStudent.MidTerm = grade.MidTerm;
                _gradesRepository.Update(gradebyStudent);
            }

            return Redirect($"https://localhost:44380/Teacher/StudentListByLesson?lessonCode={grade.LessonCode}");
        }

        public IActionResult AddPdfForLesson(string lessonCode)
        {
            var pdfList = _pdfFileRepository.GetAllPdf(lessonCode);
            var addModel = new AddNewPdf();
            if (pdfList != null)
            {
                addModel.PdfFiles = pdfList;
                addModel.LessonCode = lessonCode;
                return View(addModel);
            }

            return View();
        }
        [HttpPost]
        public IActionResult AddPdfForLesson([FromForm] IFormFile file, AddNewPdf modelNewPdf)
        {
            if (file != null && modelNewPdf != null)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };
                    var uploadResult = _cloudinary.Upload(uploadParams);
                    var pdf = new PdfFile();
                    pdf.LessonCode = modelNewPdf.LessonCode;
                    pdf.PublicId = uploadResult.PublicId;
                    pdf.Name = modelNewPdf.Name;
                    pdf.Url = uploadResult.Url.ToString();
                    _pdfFileRepository.Create(pdf);
                }
            }
            var lessonPdf = _pdfFileRepository.GetAllPdf(modelNewPdf.LessonCode);
            var model = new AddNewPdf();
            model.PdfFiles = lessonPdf;
            return View(model);
        }

        public IActionResult DeletePdfToLesson(int pdfId)
        {
          var pdf=  _pdfFileRepository.GetById(pdfId);
          var lessonCode = pdf.LessonCode;
            _pdfFileRepository.Delete(pdf);
            _cloudinary.Destroy(new DeletionParams(pdf.PublicId));
            return Redirect($"https://localhost:44380/Teacher/AddPdfForLesson?lessonCode={lessonCode}");
        }
    }
}
