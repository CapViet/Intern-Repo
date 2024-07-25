using StudentManagerMVC.Models;
using StudentManagerMVC.Repositories.StudentRepo;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagerMVC.Services.StudentServ
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<PaginatedList<Student>> GetStudentsAsync(string filterField, string filterCriteria, string filterValue, int pageNumber, int pageSize)
        {
            var students = _studentRepository.GetAll();

            if (!string.IsNullOrEmpty(filterField))
            {
                switch (filterField)
                {
                    case "Gender":
                        if (!string.IsNullOrEmpty(filterCriteria))
                        {
                            students = students.Where(s => s.Gender == filterCriteria);
                        }
                        break;
                    case "Oldest":
                        students = students.OrderBy(s => s.DateOfBirth).Take(1);
                        break;
                    case "FullName":
                        if (!string.IsNullOrEmpty(filterValue))
                        {
                            string filterValueLower = filterValue.ToLower();
                            students = students.Where(s =>
                                (s.FirstName + " " + s.LastName).ToLower().Contains(filterValueLower) ||
                                (s.LastName + " " + s.FirstName).ToLower().Contains(filterValueLower));
                        }
                        break;
                    case "BirthYear":
                        if (int.TryParse(filterValue, out int year))
                        {
                            students = students.Where(s => s.DateOfBirth.Year == year);
                        }
                        break;
                    case "PlaceOfBirth":
                        if (!string.IsNullOrEmpty(filterCriteria))
                        {
                            students = students.Where(s => s.PlaceOfBirth == filterCriteria);
                        }
                        break;
                }
            }

            return await PaginatedList<Student>.CreateAsync(students, pageNumber, pageSize);
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _studentRepository.AddAsync(student);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _studentRepository.DeleteAsync(id);
        }

        public async Task<bool> StudentExistsAsync(int id)
        {
            return await _studentRepository.ExistsAsync(id);
        }

        public async Task<IEnumerable<string>> GetDistinctPlacesOfBirthAsync()
        {
            return await _studentRepository.GetDistinctPlacesOfBirthAsync();
        }
    }
}
