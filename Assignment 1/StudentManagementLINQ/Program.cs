using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace StudentManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "D:/Study/University/Internship/Assignment 1/student.csv";
            List<Student> students = LoadStudents(filePath);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Student Management System");
                Console.WriteLine("1. Display Students");
                Console.WriteLine("2. List of male students");
                Console.WriteLine("3. Oldest Student");
                Console.WriteLine("4. Display Full Names");
                Console.WriteLine("5. Display Students born in 2000");
                Console.WriteLine("6. Display Students born after 2000");
                Console.WriteLine("7. Display Students born before 2000");
                Console.WriteLine("8. First Student born in Ha Noi");
                Console.WriteLine("9. Save and Exit");
                Console.Write("Choose an option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        DisplayStudents(students);
                        break;
                    case 2:
                        Console.WriteLine("Male Students: ");
                        DisplayStudents(students.Where(s => s.Gender == "Male"));
                        break;
                    case 3:
                        Console.WriteLine("Oldest Student: ");
                        DisplayStudent(students.OrderBy(s => s.DateOfBirth).FirstOrDefault());
                        break;
                    case 4:
                        var fullNames = students.Select(s => $"{s.LastName} {s.FirstName}");
                        Console.WriteLine("Full Names:");
                        foreach (var name in fullNames)
                        {
                            Console.WriteLine(name);
                        }
                        break;
                    case 5:
                        Console.WriteLine("Students born in year 2000: ");
                        DisplayStudents(students.Where(s => s.DateOfBirth.Year == 2000));
                        break;
                    case 6:
                        Console.WriteLine("Students born after 2000: ");
                        DisplayStudents(students.Where(s => s.DateOfBirth.Year > 2000));
                        break;
                    case 7:
                        Console.WriteLine("Students born before 2000: ");
                        DisplayStudents(students.Where(s => s.DateOfBirth.Year < 2000));
                        break;
                    case 8:
                        Console.WriteLine("First student born in Ha Noi: ");
                        DisplayStudent(students.FirstOrDefault(s => s.PlaceOfBirth == "Ha Noi"));
                        break;
                    case 9:
                        SaveStudents(filePath, students);
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static List<Student> LoadStudents(string filePath)
        {
            var dateFormats = new[] { "dd/MM/yyyy" };
            return File.Exists(filePath)
                ? File.ReadAllLines(filePath)
                      .Skip(1)
                      .Select(line =>
                      {
                          var values = line.Split(',');
                          return new Student
                          {
                              ID = int.Parse(values[0]),
                              LastName = values[1],
                              FirstName = values[2],
                              DateOfBirth = DateTime.ParseExact(values[3], dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None),
                              Gender = values[4],
                              PlaceOfBirth = values[5],
                              Mobile = values[6],
                              IsGraduated = bool.Parse(values[7])
                          };
                      }).ToList()
                : new List<Student>();
        }

        static void DisplayStudents(IEnumerable<Student> students)
        {
            Console.WriteLine("{0,-8} {1,-20} {2,-12} {3,-15} {4,-8} {5,-18} {6,-15} {7,-12} {8,-5}",
                              "ID", "Last Name", "First Name", "Date of Birth", "Gender", "Place of Birth", "Mobile", "Graduated", "Age");
            foreach (var student in students)
            {
                Console.WriteLine("{0,-8} {1,-20} {2,-12} {3,-15:dd/MM/yyyy} {4,-8} {5,-18} {6,-15} {7,-12} {8,-5}",
                                  student.ID, student.LastName, student.FirstName, student.DateOfBirth, student.Gender,
                                  student.PlaceOfBirth, student.Mobile, student.IsGraduated ? "Yes" : "No", student.Age);
            }
        }

        static void DisplayStudent(Student student)
        {
            if (student == null)
            {
                Console.WriteLine("No students fit such requirements");
            }
            else
            {
                Console.WriteLine("{0,-8} {1,-20} {2,-12} {3,-15} {4,-8} {5,-18} {6,-15} {7,-12} {8,-5}",
                                  "ID", "Last Name", "First Name", "Date of Birth", "Gender", "Place of Birth", "Mobile", "Graduated", "Age");

                Console.WriteLine("{0,-8} {1,-20} {2,-12} {3,-15:dd/MM/yyyy} {4,-8} {5,-18} {6,-15} {7,-12} {8,-5}",
                                  student.ID, student.LastName, student.FirstName, student.DateOfBirth, student.Gender,
                                  student.PlaceOfBirth, student.Mobile, student.IsGraduated ? "Yes" : "No", student.Age);
            }
        }

        static void SaveStudents(string filePath, List<Student> students)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("ID,LastName,FirstName,DateOfBirth,Gender,PlaceOfBirth,Mobile,IsGraduated");
                students.ForEach(student =>
                {
                    writer.WriteLine($"{student.ID},{student.LastName},{student.FirstName},{student.DateOfBirth:dd/MM/yyyy},{student.Gender},{student.PlaceOfBirth},{student.Mobile},{student.IsGraduated}");
                });
            }
        }
    }

    class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Mobile { get; set; }
        public bool IsGraduated { get; set; }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
    }
}
