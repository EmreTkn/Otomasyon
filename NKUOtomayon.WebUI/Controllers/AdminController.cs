using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using nkuotomasyon.data.Abstract;
using nkuotomasyon.entity;
using NKUOtomayon.WebUI.Helpers;
using NKUOtomayon.WebUI.Identity;
using NKUOtomayon.WebUI.Models;

namespace NKUOtomayon.WebUI.Controllers
{

    public class AdminController : Controller
    {
        private UserManager<User> _userManager;
        private IStudentRepository _studentRepository;
        private ILessonRepository _lessonRepository;
        private RoleManager<IdentityRole> _roleManager;
        private ITeacherRepository _teacherRepository;
        private ISemesterRepository _semesterRepository;
        private IStudyProgramRepository _studyProgramRepository;
        private IPhotoRepository _photoRepository;
        private IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        public AdminController(IStudentRepository studentRepository, UserManager<User> userManager, ILessonRepository lessonRepository, RoleManager<IdentityRole> roleManager, ITeacherRepository teacherRepository, ISemesterRepository semesterRepository, IStudyProgramRepository studyProgramRepository, IPhotoRepository photoRepository, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _studentRepository = studentRepository;
            _userManager = userManager;
            _lessonRepository = lessonRepository;
            _roleManager = roleManager;
            _teacherRepository = teacherRepository;
            _semesterRepository = semesterRepository;
            _studyProgramRepository = studyProgramRepository;
            _photoRepository = photoRepository;
            _cloudinaryConfig = cloudinaryConfig;

            Account account = new Account(_cloudinaryConfig.Value.CloudName, _cloudinaryConfig.Value.ApiKey, _cloudinaryConfig.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }
        public IActionResult AddNewLesson()
        {
            var teacher = _teacherRepository.GetAll();
            var semesters = _semesterRepository.GetAll();
            var studyPrograms = _studyProgramRepository.GetAll();
            var selectList = new LessonAddModel()
            {
                Teachers = teacher,
                Semesters = semesters,
                StudyPrograms = studyPrograms
            };


            return View(selectList);
        }
        [HttpPost]
        public IActionResult AddNewLesson(LessonAddModel lesson)
        {
            var Id = lesson.Teacher.Id;
            var teacher = _teacherRepository.GeTeacherById(Id);
            // var semester = _semesterRepository.GetById(lesson.Semester.Id);
            var studyProgram = _studyProgramRepository.GetById(lesson.StudyProgram.Id);
            var newLesson = new Lesson()
            {
                LessonName = lesson.LessonName,
                Akts = lesson.Akts,
                ExamDate = lesson.ExamDate,
                LessonCode = lesson.LessonCode,
                LessonDay = lesson.LessonDay,
                LessonStartHour = lesson.LessonStartHour,
                LessonofNumber = lesson.LessonofNumber,
                PracticeTime = lesson.PracticeTime,
                TheoryTime = lesson.TheoryTime,
                StudyTime = lesson.StudyTime,
            };

            _lessonRepository.AddNewLesson(newLesson, teacher.Id, lesson.Semester.Id, lesson.StudyProgram.Id);
            //  _lessonRepository.UpdateToAddingLesson(lesson.Teacher.Id,lesson.Semester.Id,lesson.StudyProgram.Id,lesson.LessonCode);
            _lessonRepository.GetByIdLesson(lesson.LessonCode);
            return RedirectToAction("GetLessonsByStudent", "Home");
        }

        public async Task<IActionResult> UserEdit(string id)
        {


            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(i => i.Name);
                var photo = _photoRepository.GetPhotoByStudentId(id);
                string photoUrl;
                if (photo!=null)
                {
                 photoUrl = photo.Url;
                }
                else
                { 
                    photoUrl = "";
                }

                ViewBag.Roles = roles;
                return View(new UserDetailModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    TcNumber = user.TcNumber,
                    SelectedRoles = selectedRoles,
                    Url = photoUrl
                });
            }
            return Redirect("~/admin/user/list");
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailModel model, string[] selectedRoles, [FromForm] IFormFile filetoCome)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.TcNumber = model.TcNumber;
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;

                    var result = await _userManager.UpdateAsync(user);

                    var existPhoto = _photoRepository.GetPhotoByStudentId(user.Id);

                    if (filetoCome!=null)
                    {
                        if (existPhoto!=null)
                        {
                            _photoRepository.Delete(existPhoto);
                        }
                        var file = filetoCome;
                        var uploadResult = new ImageUploadResult();
                        if (file.Length > 0)
                        {
                            using (var stream = file.OpenReadStream())
                            {
                                var uploadParams = new ImageUploadParams
                                {
                                    File = new FileDescription(file.Name, stream)
                                };
                                uploadResult = _cloudinary.Upload(uploadParams);
                            }
                        }

                        var photo = new Photo();
                        photo.PublicId = uploadResult.PublicId;
                        photo.Url = uploadResult.Url.ToString();
                        photo.StudentId = model.UserId;
                        _photoRepository.Create(photo);
                    }
                   


                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles ??= new string[] { };
                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user,
                            userRoles.Except(selectedRoles).ToArray<string>());
                        if (selectedRoles.Any(a => a == "öğrenci"))
                        {
                            var student = new Student();
                            student.SchoolNumber = user.UserName;
                            student.Email = user.Email;
                            student.Id = user.Id;
                            student.FirstName = user.FirstName;
                            student.LastName = user.LastName;
                            student.PhoneNumber = user.PhoneNumber;
                            var existStudent = _studentRepository.GetStudentById(user.Id);
                            if (existStudent == null)
                            {
                                _studentRepository.Create(student);
                            }


                            //Will be redirect to student edit page.And will be security query made. for double adding and so on.
                        }

                        if (!selectedRoles.Any(a => a == "öğrenci"))
                        {
                            var deleteStudent = _studentRepository.GetStudentById(model.UserId);
                            if (deleteStudent != null)
                            {
                                _studentRepository.Delete(deleteStudent);
                            }
                        }

                        if (selectedRoles.Any(a => a == "teacher"))
                        {
                            var teacher = new Teacher();
                            teacher.FirstName = user.FirstName;
                            teacher.LastName = user.LastName;
                            teacher.Id = user.Id;
                            teacher.UserName = user.UserName;
                            var existTeacher = _teacherRepository.GeTeacherById(user.Id);
                            if (existTeacher == null)
                            {
                                _teacherRepository.Create(teacher);
                            }
                            //Will be redirect to teacher edit page.And will be security query made. for double adding and so on.
                        }

                        if (!selectedRoles.Any(a => a == "teacher"))
                        {
                            var deleteTeacher = _teacherRepository.GeTeacherById(model.UserId);
                            if (deleteTeacher != null)
                            {
                                _teacherRepository.Delete(deleteTeacher);
                            }

                            //also this too need security query.
                        }
                        return Redirect("/admin/user/list");
                    }


                   


                }

                return Redirect("/admin/user/list");
            }
            return View(model);
        }
        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }

        public IActionResult StudentList()
        {
            return View(_studentRepository.GetAll());
        }

        public IActionResult TeacherList()
        {
            return View(_teacherRepository.GetAll());
        }
        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var members = new List<User>();
            var nonmembers = new List<User>();

            foreach (var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonmembers;
                list.Add(user);

            }
            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }

                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }

            return Redirect("/admin/role/" + model.RoleId);
        }

        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (res.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                else
                {
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        public IActionResult LessonList()
        {
            var lessons = _lessonRepository.GetLessons();
            return View(lessons);
        }
        public IActionResult EditExamDate(string lessonCode)
        {
            var lesson = _lessonRepository.GetByIdLesson(lessonCode);
            var editExam = new ExamEditModel();
            editExam.LessonCode = lessonCode;
            editExam.LessonName = lesson.LessonName;
            if (lesson.ExamDate != null)
            {
                editExam.MidTermExamDate = lesson.ExamDate;
                //editExam.FinalExamDate = lesson.ExamDate;
                //editExam.MakeUpExamDate = lesson.ExamDate;
            }

            return View(editExam);
        }
        [HttpPost]
        public IActionResult EditExamDate(ExamEditModel model)//need to add exam location..
        {
            var lesson = _lessonRepository.GetByIdLesson(model.LessonCode);
            lesson.ExamDate = model.MidTermExamDate;
            _lessonRepository.Update(lesson);

            return RedirectToAction("LessonList");
        }
    }
}
