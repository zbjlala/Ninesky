using Ninesky.BLL;
using Ninesky.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNinesky.Areas.Member.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private interfaceCategoryService categoryService;
        public CategoryController()
        {
            categoryService = new CategoryService();
        }
        /// <summary>
        /// 获取json格式栏目树
        /// </summary>
        /// <param name="model">模型名</param>
        /// <returns></returns>
        public ActionResult JsonTree(string model)
        {
            return Json(categoryService.EasyuiTreeData(model));
        }
        public ActionResult JsonComboBox(string model)
        {
            return Json(categoryService.FindList(model));
        }

    }
}