// IStudentService.cs
using StudentManagerMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagerMVC.Services.StudentServ
{
    public interface IStudentService
    {
        Task<PaginatedList<Student>> GetStudentsAsync(string filterField, string filterCriteria, string filterValue, int pageNumber, int pageSize);
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
        Task<bool> StudentExistsAsync(int id);
        Task<IEnumerable<string>> GetDistinctPlacesOfBirthAsync();
    }
}
