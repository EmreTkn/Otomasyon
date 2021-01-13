using Microsoft.AspNetCore.Mvc;
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
    public class StudentController : Controller
    {
        private IStudentRepository _studentRepository;
        private IPhotoRepository _photoRepository;
        private IOptions<CloudinarySettings> _cloudinaryConfig;
        private UserManager<User> _userManager;
        private Cloudinary _cloudinary;
        public StudentController( IPhotoRepository photoRepository, IStudentRepository studentRepository, IOptions<CloudinarySettings> cloudinaryConfig, UserManager<User> userManager)
        {
            _photoRepository = photoRepository;
            _studentRepository = studentRepository;
            _cloudinaryConfig = cloudinaryConfig;
            _userManager = userManager;
            Account account=new Account(_cloudinaryConfig.Value.CloudName,_cloudinaryConfig.Value.ApiKey,_cloudinaryConfig.Value.ApiSecret);
           _cloudinary=new Cloudinary(account);
        }

        [HttpPost]
        public IActionResult AddPhotoForStudent(string studentId,[FromForm]IFormFile filetoCome)
        {
            var file = filetoCome;
            var uploadResult=new ImageUploadResult();
            if (file.Length>0)
            {
                using (var stream=file.OpenReadStream())
                {
                    var uploadParams=new ImageUploadParams
                    {
                        File = new FileDescription(file.Name,stream)
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            var photo=new Photo();
            photo.PublicId = uploadResult.PublicId;
            photo.Url = uploadResult.Url.ToString();
            photo.StudentId = studentId;
            _photoRepository.Create(photo);

            return RedirectToAction("StudentList", "Admin");
        }
    }
}
