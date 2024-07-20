// StudentService.cs
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentManagerMVC.Data;
using StudentManagerMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagerMVC.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync(string filterField, string filterCriteria, string filterValue)
        {
            var students = from s in _context.Students select s;

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
                        if (!string.IsNullOrEmpty(filterCriteria))
                        {
                            var names = filterCriteria.Split(' ');
                            if (names.Length == 2)
                            {
                                students = students.Where(s => s.FirstName == names[0] && s.LastName == names[1]);
                            }
                            else if (names.Length == 1)
                            {
                                students = students.Where(s => s.FirstName == names[0] || s.LastName == names[0]);
                            }
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


            return await students.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task AddStudentAsync(Student student)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> StudentExistsAsync(int id)
        {
            return await _context.Students.AnyAsync(e => e.ID == id);
        }

        public async Task<IEnumerable<string>> GetDistinctPlacesOfBirthAsync()
        {
            return await _context.Students.Select(s => s.PlaceOfBirth).Distinct().ToListAsync();
        }
    }
}
