using Microsoft.AspNetCore.Mvc;

namespace Demo.AspNetCore.JqGrid.Controllers
{
    public class HelperController : Controller
    {
        #region Actions
        public IActionResult Basics()
        {
            return GetJqGridView(nameof(Basics));
        }

        public IActionResult DynamicScrolling()
        {
            return GetJqGridView(nameof(DynamicScrolling));
        }

        public IActionResult HeaderGrouping()
        {
            return GetJqGridView(nameof(HeaderGrouping));
        }

        public IActionResult Grouping()
        {
            return GetJqGridView(nameof(Grouping));
        }

        public IActionResult SingleSearching()
        {
            return GetJqGridView(nameof(SingleSearching));
        }

        public IActionResult AdvancedSearching()
        {
            return GetJqGridView(nameof(AdvancedSearching));
        }

        public IActionResult ToolbarSearching()
        {
            return GetJqGridView(nameof(ToolbarSearching));
        }

        public IActionResult CellEditing()
        {
            return GetJqGridView(nameof(CellEditing));
        }

        public IActionResult InlineEditing()
        {
            return GetJqGridView(nameof(InlineEditing));
        }

        public IActionResult FormEditing()
        {
            return GetJqGridView(nameof(FormEditing));
        }

        public IActionResult Subgrid()
        {
            return GetJqGridView(nameof(Subgrid));
        }

        public IActionResult SubgridAsGrid()
        {
            return GetJqGridView(nameof(SubgridAsGrid));
        }

        public IActionResult TreeGrid()
        {
            return GetJqGridView(nameof(TreeGrid));
        }
        #endregion

        #region Methods
        private IActionResult GetJqGridView(string viewName)
        {
            //ViewBag.JqGrid = "free-jqgrid";

            return View(viewName);
        }
        #endregion
    }
}
