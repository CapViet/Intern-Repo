using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace StudentManagement.Services
{
    public class StudentRepository : IStudentRepository
    {
        private List<Student> _students;
        private readonly string _filePath;

        public StudentRepository(string filePath)
        {
            _filePath = filePath;
            _students = LoadStudents();
        }

        private List<Student> LoadStudents()
        {
            List<Student> students = new List<Student>();
            string[] dateFormats = { "dd/MM/yyyy" };

            if (File.Exists(_filePath))
            {
                var lines = File.ReadAllLines(_filePath);
                foreach (var line in lines.Skip(1))
                {
                    var values = line.Split(',');

                    try
                    {
                        students.Add(new Student
                        {
                            ID = int.Parse(values[0]),
                            LastName = values[1],
                            FirstName = values[2],
                            DateOfBirth = DateTime.ParseExact(values[3], dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None),
                            Gender = values[4],
                            PlaceOfBirth = values[5],
                            Mobile = values[6],
                            IsGraduated = bool.Parse(values[7])
                        });
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Error parsing student data: {line}. Error: {ex.Message}");
                    }
                }
            }

            return students;
        }

        public List<Student> GetAllStudents() => _students;

        public List<Student> GetMaleStudents() => _students.Where(s => s.Gender == "Male").ToList();

        public Student GetOldestStudent() => _students.OrderBy(s => s.DateOfBirth).FirstOrDefault();

        public List<string> GetFullNames() => _students.Select(s => $"{s.LastName} {s.FirstName}").ToList();

        public List<Student> GetStudentsBornIn2000() => _students.Where(s => s.DateOfBirth.Year == 2000).ToList();

        public List<Student> GetStudentsBornAfter2000() => _students.Where(s => s.DateOfBirth.Year > 2000).ToList();

        public List<Student> GetStudentsBornBefore2000() => _students.Where(s => s.DateOfBirth.Year < 2000).ToList();

        public Student GetFirstStudentBornInHanoi() => _students.FirstOrDefault(s => s.PlaceOfBirth == "Ha Noi");

    }
}
