using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.Services;
using System.Collections.Generic;

namespace StudentManagement.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var students = _studentRepository.GetAllStudents();
            return View("List",students);
        }

        public IActionResult Males()
        {
            var students = _studentRepository.GetMaleStudents();
            return View("List", students);
        }

        public IActionResult OldestStudent()
        {
            var student = _studentRepository.GetOldestStudent();
            return View("SingleStudent", student);
        }

        public IActionResult FullNames()
        {
            var fullNames = _studentRepository.GetFullNames();
            return View(fullNames);
        }

        public IActionResult BornIn2000()
        {
            var students = _studentRepository.GetStudentsBornIn2000();
            return View("List", students);
        }

        public IActionResult BornAfter2000()
        {
            var students = _studentRepository.GetStudentsBornAfter2000();
            return View("List", students);
        }

        public IActionResult BornBefore2000()
        {
            var students = _studentRepository.GetStudentsBornBefore2000();
            return View("List", students);
        }

        public IActionResult FirstHanoian()
        {
            var student = _studentRepository.GetFirstStudentBornInHanoi();
            return View("SingleStudent", student);
        }
    }
}
