using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var villas = _db.Villas.ToList();

            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa villa)
        {
            if(villa.Name == villa.Description)
            {
                ModelState.AddModelError("Description", "The description cannot exactly match the name.");
            }

            if (ModelState.IsValid)
            { 
                _db.Villas.Add(villa);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(villa);
        }

        public IActionResult Update(int villaId)
        {
            Villa? villa = _db.Villas.FirstOrDefault(villa => villa.Id == villaId);
            if (villa == null)
            {
                return NotFound();
            }
            return View(villa);
        }
    }
}
