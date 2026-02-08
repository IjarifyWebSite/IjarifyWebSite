using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.PropertyViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.Services.Classes;

namespace IjarifySystemPL.Controllers
{
    public class PropertyController(IPropertyService _propertyService) : Controller
    {
        // GET: PropertyController
        public async Task<ActionResult> Index(PropertyFilterViewModel filter, int page = 1)
        {
            var pageViewModel = await _propertyService.GetPagination(4, page, filter);
            ViewBag.CurrentPage = pageViewModel.CurrentPage;
            ViewBag.TotalPages = pageViewModel.TotalPages;
            return View(pageViewModel);
        }

        // GET: PropertyController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            int fakeUserId = 3;
            var vmModel = await _propertyService.GetPropertyDetails(id , fakeUserId);

            return View("Details",vmModel);
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
