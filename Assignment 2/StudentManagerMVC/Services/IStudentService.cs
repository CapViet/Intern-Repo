// IStudentService.cs
using StudentManagerMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagerMVC.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentsAsync(string filterField, string filterCriteria, string filterValue);
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
        Task<bool> StudentExistsAsync(int id);
        Task<IEnumerable<string>> GetDistinctPlacesOfBirthAsync();
    }
}
