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

        public List<Student> GetAllStudents() => _students ?? new List<Student>();

        public List<Student> GetMaleStudents()
        {
            if (_students == null)
            {
                return new List<Student>();
            }

            return _students.Where(s => s.Gender?.Equals("Male", StringComparison.OrdinalIgnoreCase) ?? false).ToList();
        }

        public Student GetOldestStudent()
        {
            if (_students == null || !_students.Any())
            {
                return null;
            }

            return _students.OrderBy(s => s.DateOfBirth).FirstOrDefault();
        }

        public List<string> GetFullNames()
        {
            if (_students == null)
            {
                return new List<string>();
            }

            return _students.Select(s => $"{s.LastName} {s.FirstName}").Where(name => !string.IsNullOrWhiteSpace(name)).ToList();
        }

        public List<Student> GetStudentsBornIn2000()
        {
            if (_students == null)
            {
                return new List<Student>();
            }

            return _students.Where(s => s.DateOfBirth.Year == 2000).ToList();
        }

        public List<Student> GetStudentsBornAfter2000()
        {
            if (_students == null)
            {
                return new List<Student>();
            }

            return _students.Where(s => s.DateOfBirth.Year > 2000).ToList();
        }

        public List<Student> GetStudentsBornBefore2000()
        {
            if (_students == null)
            {
                return new List<Student>();
            }

            return _students.Where(s => s.DateOfBirth.Year < 2000).ToList();
        }

        public Student GetFirstStudentBornInHanoi()
        {
            if (_students == null)
            {
                return null;
            }

            return _students.FirstOrDefault(s => s.PlaceOfBirth?.Equals("Ha Noi", StringComparison.OrdinalIgnoreCase) ?? false);
        }

        public void SaveStudents()
        {
            if (_students == null)
            {
                return;
            }

            using (var writer = new StreamWriter(_filePath))
            {
                writer.WriteLine("ID,LastName,FirstName,DateOfBirth,Gender,PlaceOfBirth,Mobile,IsGraduated");
                foreach (var student in _students)
                {
                    writer.WriteLine($"{student.ID},{student.LastName},{student.FirstName},{student.DateOfBirth:dd/MM/yyyy},{student.Gender},{student.PlaceOfBirth},{student.Mobile},{student.IsGraduated}");
                }
            }
        }
    }
}
