using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmlakUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class TypeController : Controller
    {
        private readonly ITypeService _typeService;

        public TypeController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public async Task<IActionResult> Index()
        {
            var typeList = await _typeService.BL_ListAsync();
            return View(typeList);
        }

        //Delete 
        public async Task<IActionResult> Delete(int id)
        {
            var deleteRow = await _typeService.BL_ReadAsync(id);
            await _typeService.BL_DeleteAsync(deleteRow);
            return RedirectToAction(nameof(Index), "Type");
        }

        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EntityLayer.Entities.Type data)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                ViewData["Errors"] = errors;
                return View(data);
            }
            data.Status = true;
            await _typeService.BL_CreateAsync(data);
            TempData["newTypeSuccess"] = "New Type Added";
            return RedirectToAction(nameof(Index), "Type");
        }

        //Update 

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var upRow = await _typeService.BL_ReadAsync(id);
            return View(upRow);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EntityLayer.Entities.Type upData)
        {
            if (ModelState.IsValid)
            {
                await _typeService.BL_UpdateAsync(upData);
                TempData["updateType"] = "Type Updated";
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(x => x.Errors);
            ViewData["Errors"] = errors;
            return View(upData);
        }
    }
}
