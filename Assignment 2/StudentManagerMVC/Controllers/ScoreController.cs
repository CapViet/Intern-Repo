using Microsoft.AspNetCore.Mvc;
using static StudentManagerMVC.Models.ViewModel;
using StudentManagerMVC.Data;
using StudentManagerMVC.Models;

namespace StudentManagerMVC.Controllers
{
    public class ScoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string sortOrder)
        {
            ViewData["IDSortParm"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["MathSortParm"] = sortOrder == "MathScore" ? "math_desc" : "MathScore";
            ViewData["ChemSortParm"] = sortOrder == "ChemScore" ? "chem_desc" : "ChemScore";
            ViewData["PhysSortParm"] = sortOrder == "PhysScore" ? "phys_desc" : "PhysScore";

            var scores = from s in _context.Scores
                         select new ScoresViewModel
                         {
                             ID = s.ID,
                             MathScore = s.MathScore,
                             ChemScore = s.ChemScore,
                             PhysScore = s.PhysScore,
                         };

            switch (sortOrder)
            {
                case "id_desc":
                    scores = scores.OrderByDescending(s => s.ID);
                    break;
                case "MathScore":
                    scores = scores.OrderBy(s => s.MathScore);
                    break;
                case "math_desc":
                    scores = scores.OrderByDescending(s => s.MathScore);
                    break;
                case "ChemScore":
                    scores = scores.OrderBy(s => s.ChemScore);
                    break;
                case "chem_desc":
                    scores = scores.OrderByDescending(s => s.ChemScore);
                    break;
                case "PhysScore":
                    scores = scores.OrderBy(s => s.PhysScore);
                    break;
                case "phys_desc":
                    scores = scores.OrderByDescending(s => s.PhysScore);
                    break;
                default:
                    scores = scores.OrderBy(s => s.ID);
                    break;
            }

            return View(scores.ToList());
        }
    }
}
