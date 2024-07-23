using System.Collections.Generic;
using System.Linq;
using StudentManagerMVC.Data;
using StudentManagerMVC.Models;
using Microsoft.EntityFrameworkCore;
using static StudentManagerMVC.Models.ViewModel;

namespace StudentManagerMVC.Repositories
{
    public class ScoresRepository : IScoresRepository
    {
        private readonly ApplicationDbContext _context;

        public ScoresRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Scores> GetAllScores()
        {
            return _context.Scores.ToList();
        }

        public Scores GetScoreById(int id)
        {
            return _context.Scores.Find(id);
        }

        public void AddScore(Scores score)
        {
            _context.Scores.Add(score);
        }

        public void UpdateScore(Scores score)
        {
            _context.Scores.Update(score);
        }

        public void DeleteScore(int id)
        {
            Scores score = _context.Scores.Find(id);
            if (score != null)
            {
                _context.Scores.Remove(score);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }


    }
}
