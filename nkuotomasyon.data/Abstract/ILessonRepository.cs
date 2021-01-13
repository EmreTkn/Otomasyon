using System;
using System.Collections.Generic;
using System.Text;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Abstract
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Lesson GetByIdLesson(string id);
        void DeleteFromStudent(string studentId, string lessonCode);
        void AddFromStudent(string studentId,string lessonCode);
        void AddNewLesson(Lesson lesson, string teacherId, int semesterId, int studyProgramId);
        void UpdateToAddingLesson(string teacherId, int semesterId, int studyProgramId, string lessonCode);
        List<Lesson> GetLessonsWithSemester();
        Lesson GetJustLesson(string lessonCode);
        List<Lesson> GetLessons();
    }
}
