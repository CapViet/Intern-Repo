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
                        DisplayStudents(Males(students));
                        break;
                    case 3:
                        Console.WriteLine("Oldest Student: ");
                        DisplayStudent(OldestStudent(students));
                        break;
                    case 4:
                        DisplayFullNames(students);
                        break;
                    case 5:
                        Console.WriteLine("Students born in year 2000: ");
                        DisplayStudents(Year2000(students));
                        break;
                    case 6:
                        Console.WriteLine("Students born after 2000: ");
                        DisplayStudents(YearGreater2000(students));
                        break;
                    case 7:
                        Console.WriteLine("Students born before 2000: ");
                        DisplayStudents(YearLesser2000(students));
                        break;
                    case 8:
                        Console.WriteLine("First student born in Ha Noi: ");
                        DisplayStudent(FirstHanoian(students));
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
            List<Student> students = new List<Student>();
            string[] dateFormats = { "dd/MM/yyyy" };

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
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

        static void DisplayStudents(List<Student> students)
        {
            Console.WriteLine("{0,-8} {1,-20} {2, -12} {3,-15} {4,-8} {5,-18} {6,-15} {7,-12} {8,-5}", 
                              "ID", "Last Name", "First Name","Date of Birth", "Gender", "Place of Birth", "Mobile", "Graduated", "Age");
            foreach (var student in students)
            {
                Console.WriteLine("{0,-8} {1,-20} {2, -12} {3,-15:dd/MM/yyyy} {4,-8} {5,-18} {6,-15} {7,-12} {8,-5}", 
                                  student.ID, student.LastName, student.FirstName, student.DateOfBirth, student.Gender, 
                                  student.PlaceOfBirth, student.Mobile, student.IsGraduated ? "Yes" : "No", student.Age);
            }
        }

        static void DisplayStudent(Student student)
        {
            if(student == null){
                Console.WriteLine("No students fit such requirements");
            }
            else{
                Console.WriteLine("{0,-8} {1,-20} {2, -12} {3,-15} {4,-8} {5,-18} {6,-15} {7,-12} {8,-5}", 
                                "ID", "Last Name", "First Name","Date of Birth", "Gender", "Place of Birth", "Mobile", "Graduated", "Age");
                
                Console.WriteLine("{0,-8} {1,-20} {2, -12} {3,-15:dd/MM/yyyy} {4,-8} {5,-18} {6,-15} {7,-12} {8,-5}", 
                                    student.ID, student.LastName, student.FirstName, student.DateOfBirth, student.Gender, 
                                    student.PlaceOfBirth, student.Mobile, student.IsGraduated ? "Yes" : "No", student.Age);
            }
        
        }

        static List<Student> Males(List<Student> students)
        {
            List<Student> maleStudents = new List<Student>();
            foreach (var student in students)
            {
                if (student.Gender == "Male")
                {
                    maleStudents.Add(student);
                }
            }
            return maleStudents;
        }

        static List<Student> Year2000(List<Student> students)
        {
            List<Student> studentsIn2000 = new List<Student>();
            foreach (var student in students)
            {
                if (student.DateOfBirth.Year == 2000)
                {
                    studentsIn2000.Add(student);
                }
            }
            return studentsIn2000;
        }

        static List<Student> YearGreater2000(List<Student> students)
        {
            List<Student> studentsAfter2000 = new List<Student>();
            foreach (var student in students)
            {
                if (student.DateOfBirth.Year > 2000)
                {
                    studentsAfter2000.Add(student);
                }
            }
            return studentsAfter2000;
        }

        static List<Student> YearLesser2000(List<Student> students)
        {
            List<Student> studentsAfter2000 = new List<Student>();
            foreach (var student in students)
            {
                if (student.DateOfBirth.Year < 2000)
                {
                    studentsAfter2000.Add(student);
                }
            }
            return studentsAfter2000;
        }

        static Student FirstHanoian(List<Student> students)
        {
            int i = 0;
            while(i < students.Count())
            {
                if (students[i].PlaceOfBirth == "Ha Noi"){
                    return students[i];
                }
                i++;
            }
            return null;
        }
        static Student OldestStudent(List<Student> students)
        {
            if (students.Count == 0)
            {
                return null;
            }

            Student oldestStudent = students[0];
            foreach (var student in students)
            {
                if (student.DateOfBirth < oldestStudent.DateOfBirth)
                {
                    oldestStudent = student;
                }
            }
            return oldestStudent;
        }

        static List<string> GetFullNames(List<Student> students)
        {
            List<string> fullNames = new List<string>();
            foreach (var student in students)
            {
                fullNames.Add($"{student.LastName} {student.FirstName}");
            }
            return fullNames;
        }

        static void DisplayFullNames(List<Student> students)
        {
            var fullNames = GetFullNames(students);
            Console.WriteLine("Full Names:");
            foreach (var name in fullNames)
            {
                Console.WriteLine(name);
            }
        }

        static void SaveStudents(string filePath, List<Student> students)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("ID,LastName,FirstName,DateOfBirth,Gender,PlaceOfBirth,Mobile,IsGraduated");
                foreach (var student in students)
                {
                    writer.WriteLine($"{student.ID},{student.LastName},{student.FirstName},{student.DateOfBirth:dd/MM/yyyy},{student.Gender},{student.PlaceOfBirth},{student.Mobile},{student.IsGraduated}");
                }
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
