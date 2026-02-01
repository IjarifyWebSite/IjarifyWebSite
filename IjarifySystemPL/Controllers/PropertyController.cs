using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IjarifySystemBLL.Services.Interfaces;

namespace IjarifySystemPL.Controllers
{
    public class PropertyController(IPropertyService _propertyService) : Controller
    {
        // GET: PropertyController
        public async Task<ActionResult> Index(int page=1)
        {
            var (vmList, totalPages, currentPage) = await _propertyService.GetPagination(4, page);
            ViewBag.CurrentPage = currentPage-1;
            ViewBag.TotalPages = totalPages;


            return View(vmList);
        }

        // GET: PropertyController/Details/5
        public ActionResult Details(int id)
        {
            return View("Details");
        }

        // GET: PropertyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropertyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PropertyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PropertyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PropertyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PropertyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
