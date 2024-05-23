using IIETA.Data;
using IIETA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace IIETA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;


        public HomeController( ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var dataArray = new Cause[]
    {
        new Cause { Id = 1, Name = "HUNGER AND POVERTY", Description = "Hunger poverty afflict countless lives globally, underscoring the urgency of collective action ...", Img = "../assets/images/causes/cause-hunger.jpg" },
        new Cause { Id = 2, Name = "EDUCATION", Description = "Education in impoverished nations stands as a beacon of hope...", Img = "../assets/images/causes/cause-education.jpg" },
        new Cause { Id = 3, Name = "HUMAN RIGHTS", Description = "Human rights empower individuals, promoting equality, justice, and dignity...", Img = "../assets/images/causes/cause-rights.jpg" },
        new Cause { Id = 4, Name = "ARTS AND CULTURE", Description = "Arts and culture are the vibrant threads weaving the tapestry of human experience...", Img = "../assets/images/causes/cause-culture.jpg" }
    };
            // Call the function to get the count
            int rowCount = CountAllRowsInCatigorie();
            int argentRowCount = CountArgentRowsInCatigorie();
            int equalCostRowCount = CountRowsWithEqualCost();

            // Pass the count to the view
            ViewBag.CatigorieRowCount = rowCount;
            ViewBag.ArgentRowCount = argentRowCount;
            ViewBag.EqualCostRowCount = equalCostRowCount;

            return View(dataArray);
        }

        private int CountAllRowsInCatigorie()
        {
            // Use the _context to query the database and count the rows
            int rowCount = _context.Catigorie.Count();

            return rowCount;
        }

        private int CountArgentRowsInCatigorie()
        {
            // Use the _context to query the database and count rows where is_argent is true
            int rowCount = _context.Catigorie.Count(c => c.Is_agent);

            return rowCount;
        }

        private int CountRowsWithEqualCost()
        {
            // Use the _context to query the database and count rows where act_cost is equal to total_cost
            int rowCount = _context.Catigorie.Count(c => c.Act_cost == c.Total_cost);

            return rowCount;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}