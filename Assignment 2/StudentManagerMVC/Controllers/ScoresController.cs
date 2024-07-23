using Microsoft.AspNetCore.Mvc;
using static StudentManagerMVC.Models.ViewModel;
using StudentManagerMVC.Data;
using StudentManagerMVC.Models;
using StudentManagerMVC.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace StudentManagerMVC.Controllers
{
    public class ScoresController : Controller
    {
        private readonly IScoresService _scoresService;
        private readonly IStudentService _studentService;

        public ScoresController(IScoresService scoresService, IStudentService studentService)
        {
            _scoresService = scoresService;
            _studentService = studentService;
        }

        public IActionResult Index(string sortOrder)
        {
            ViewData["IDSortParm"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["MathSortParm"] = sortOrder == "MathScore" ? "math_desc" : "MathScore";
            ViewData["ChemSortParm"] = sortOrder == "ChemScore" ? "chem_desc" : "ChemScore";
            ViewData["PhysSortParm"] = sortOrder == "PhysScore" ? "phys_desc" : "PhysScore";

            var scores = _scoresService.GetAllScores(sortOrder).Select(s => new ScoresViewModel
            {
                ID = s.ID,
                MathScore = s.MathScore,
                ChemScore = s.ChemScore,
                PhysScore = s.PhysScore,
            });

            return View(scores.ToList());
        }
    }
}
