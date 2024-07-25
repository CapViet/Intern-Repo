using StudentManagerMVC.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StudentManagerMVC.Repositories.StudentRepo
{
    public interface IStudentRepository
    {
        IQueryable<Student> GetAll(); // Note: Changed return type
        Task<Student> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<string>> GetDistinctPlacesOfBirthAsync();
    }
}
