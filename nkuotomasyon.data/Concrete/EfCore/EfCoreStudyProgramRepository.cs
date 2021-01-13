using System;
using System.Collections.Generic;
using System.Text;
using nkuotomasyon.data.Abstract;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Concrete.EfCore
{
    public class EfCoreStudyProgramRepository : EfCoreGenericRepository<StudyProgram, NkuContext>, IStudyProgramRepository
    {
    }
}
