using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using nkuotomasyon.data.Abstract;
using nkuotomasyon.entity;
using NKUOtomayon.WebUI.Identity;
using NKUOtomayon.WebUI.Models;

namespace NKUOtomayon.WebUI.Controllers
{
    public class LessonController : Controller
    {
        private UserManager<User> _userManager;
        private IStudentRepository _studentRepository;
        private ILessonRepository _lessonRepository;
        private IGradesRepository _gradesRepository;
        private ISemesterRepository _semesterRepository;


        public LessonController(IStudentRepository studentRepository, UserManager<User> userManager, ILessonRepository lessonRepository, IGradesRepository gradesRepository, ISemesterRepository semesterRepository)
        {
            _studentRepository = studentRepository;
            _userManager = userManager;
            _lessonRepository = lessonRepository;
            _gradesRepository = gradesRepository;
            _semesterRepository = semesterRepository;
        }


        public IActionResult AddAndDeleteLessonToStudent()
        {
            //DateTime bugun = Convert.ToDateTime("12/12/2020");  //This will be used for the student's midterm lectures.
            //DateTime yarın = Convert.ToDateTime("13 / 12 / 2020");
            //TimeSpan hesap = yarın - bugun;
            //var sonuc = hesap.TotalDays;


            var lesson1 = new AddDeleteLessonModel();
            var studentId = _userManager.GetUserId(User);
            var student = _studentRepository.GetStudentById(studentId);
            var studentLesson = _studentRepository.GetStudentLessons(studentId);
            var lesson = _lessonRepository.GetLessonsWithSemester();
            var grades = _studentRepository.GetStudentGrades(studentId);
            lesson1.Student = student;


            foreach (var item in lesson)
            {
                if (studentLesson.Exists(i => i.LessonName == item.LessonName))
                {
                    if (item.Semester.Id == student.Semester.Id)
                    {
                        lesson1.SelectedLesson.Add(item);
                    }

                }
                else
                {
                    if (item.Semester.Id == student.Semester.Id)
                    {
                        lesson1.NonSelectedLesson.Add(item);
                    }
                }
            }
            
            foreach (var item in grades)
            {
                int a = student.Semester.Id;
                int b = item.Lesson.Semester.Id;

                if (a == b || a == b + 2 || a == b + 4 || a == b + 6)
                {
                    if (item.FailedLowGrade)
                    {
                        var failedLesson = _gradesRepository.GetLessonByGradesId(item.Id);
                        lesson1.GradesFailedLessons.Add(failedLesson.Lesson);

                        var lessonCode = failedLesson.Lesson.LessonCode;
                        _lessonRepository.AddFromStudent(lessonCode, studentId);
                    }

                }
            }


            return View(lesson1);
        }
        [HttpPost]
        public IActionResult AddAndDeleteLessonToStudent(string lessonDeleteCode, string lessonAddCode)
        {


            var studentId = _userManager.GetUserId(User);
            if (lessonDeleteCode != null)
            {
                _lessonRepository.DeleteFromStudent(studentId, lessonDeleteCode);
            }

            if (lessonAddCode != null)
            {
                _lessonRepository.AddFromStudent(lessonAddCode, studentId);
            }
            var lesson1 = new AddDeleteLessonModel();
            var studentLesson = _studentRepository.GetStudentLessons(studentId);
            var lesson = _lessonRepository.GetLessonsWithSemester();
            var student = _studentRepository.GetStudentById(studentId);
            var grades = _studentRepository.GetStudentGrades(studentId);

            lesson1.Student = student;
            foreach (var item in lesson)
            {
                if (studentLesson.Exists(i => i.LessonName == item.LessonName))
                {
                    if (item.Semester.Id == student.Semester.Id)
                    {
                        lesson1.SelectedLesson.Add(item);
                    }

                }
                else
                {
                    if (item.Semester.Id == student.Semester.Id)
                    {
                        lesson1.NonSelectedLesson.Add(item);
                    }
                }
            }
            foreach (var item in grades)
            {
                if (item.FailedLowGrade)
                {
                    var failedLesson = _gradesRepository.GetLessonByGradesId(item.Id);
                    lesson1.GradesFailedLessons.Add(failedLesson.Lesson);
                }
            }
            return View(lesson1);
        }
    }
}
