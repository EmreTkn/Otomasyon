using NKUOtomayon.WebUI.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nkuotomasyon.data.Abstract;
using nkuotomasyon.data.Concrete.EfCore;

namespace NKUOtomayon.WebUI.Helpers
{
    public class CalculaterSemester
    {
      

        public void CalculateSemester(User user)
        {
            ISemesterRepository _semesterRepository = new EfCoreSemesterRepository();
            IStudentRepository _studentRepository=new EfCoreStudentRepository();

            var student = _studentRepository.GetStudentById(user.Id);
            var year = DateTime.Today.Year - student.RegistrationTime.Year;
            var month = Convert.ToInt32(DateTime.Today.Month);
            if (year == 0)
            {
                _semesterRepository.Update(student.Id, 1);
            }

            if (year == 1)
            {
                if (month > 2 && month < 9)
                {
                    _semesterRepository.Update(student.Id, 2);
                }
                else
                {
                    _semesterRepository.Update(student.Id, 3);
                }
            }

            if (year == 2)
            {
                if (month > 2 && month < 9)
                {
                    _semesterRepository.Update(student.Id, 4);
                }
                else
                {
                    _semesterRepository.Update(student.Id, 5);
                }
            }

            if (year == 3)
            {
                if (month > 2 && month < 9)
                {
                    _semesterRepository.Update(student.Id, 6);
                }
                else
                {
                    _semesterRepository.Update(student.Id, 7);
                }
            }

            if (year == 4)
            {
                _semesterRepository.Update(student.Id, 8);
            }
        }
    }
}
