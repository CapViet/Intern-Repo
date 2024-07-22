using Microsoft.EntityFrameworkCore;
using StudentManagerMVC.Data;
using StudentManagerMVC.Models;

namespace StudentManagerMVC.Repositories
{
    public class StudentRepository: IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task AddAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Students.AnyAsync(e => e.ID == id);
        }

        public async Task<IEnumerable<string>> GetDistinctPlacesOfBirthAsync()
        {
            return await _context.Students.Select(s => s.PlaceOfBirth).Distinct().ToListAsync();
        }
    }
}
