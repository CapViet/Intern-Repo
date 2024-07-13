using StudentManagement.Models;
using System.Collections.Generic;

namespace StudentManagement.Services
{
    public interface IStudentRepository
    {
        List<Student> GetAllStudents();
        List<Student> GetMaleStudents();
        Student GetOldestStudent();
        List<string> GetFullNames();
        List<Student> GetStudentsBornIn2000();
        List<Student> GetStudentsBornAfter2000();
        List<Student> GetStudentsBornBefore2000();
        Student GetFirstStudentBornInHanoi();
        void SaveStudents();
    }
}
