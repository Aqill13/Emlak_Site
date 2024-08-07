using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmlakUI.ViewComponents
{
    public class TypeGetList : ViewComponent
    {
        private readonly ITypeService _typeService;

        public TypeGetList(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var typeList = await _typeService.BL_ListAsync(x => x.Status == true);
            return View(typeList);
        }
    }
}
