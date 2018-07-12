using Ninesky.BLL;
using Ninesky.IBLL;
using Ninesky.Models.ArticeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNinesky.Areas.Member.Controllers
{
    [Authorize]
    /// <summary>
    /// 文章控制器
    /// </summary>
    public class ArticleController : Controller
    {
        private interfaceArticleService articleService;
        private interfaceCommentModelService commentModelService;
        public ArticleController()
        {
            articleService = new ArticleService();
            commentModelService = new CommonModelService();
        }
        // GET: Member/Article
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(CommonModel commonModel)
        {
            if(ModelState.IsValid)
            {
                //设置固定值
                commonModel.Hits = 0;
                commonModel.Inputer = User.Identity.Name;
                commonModel.Model = "Article";
                commonModel.ReleaseDate = System.DateTime.Now;
                commonModel.Status = 29;
                commonModel = commentModelService.Add(commonModel);
                if(commonModel.Article.ArticleID > 0)
                {
                    //附件处理
                    interfaceAttachmentService _attachmentService = new AttachmentService();
                    //查询相关附件
                    var _attachments = _attachmentService.FindList(null,User.Identity.Name,string.Empty).ToList();
                    //遍历所有附件
                    foreach(var _att in _attachments)
                    {
                        var _filePath = Url.Content(_att.FileParth);
                        //文章首页图片或内容中使用了该附件则更改ModelID为文章保存后的ModelID
                        if((commonModel.DefaultPicUrl !=null && commonModel.DefaultPicUrl.IndexOf(_filePath)>=0)|| commonModel.Article.Content.IndexOf(_filePath)>0 )
                        {
                            _att.ModelID = commonModel.ModelID;
                            _attachmentService.Update(_att);
                        }
                        else
                        {
                            System.IO.File.Delete(Server.MapPath(_att.FileParth));
                            _attachmentService.Delete(_att);
                        }
                    }
                    return View("AddSuccess",commonModel);
                }
                
            }
            return View(commonModel);
        }
        //菜单Menu
        [ChildActionOnly]
        public ActionResult Menu()
        {
            return PartialView();
        }
        /// <summary>
        /// 全部文章页面
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
        /// <summary>
        /// 文章列表Json
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="input">录入者</param>
        /// <param name="category">栏目</param>
        /// <param name="fromDate">日期起</param>
        /// <param name="toDate">日期止</param>
        /// <param name="pageindex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        public ActionResult JsonList(string title,string input,Nullable<int> category,Nullable<DateTime> fromDate,Nullable<DateTime> toDate,int pageindex = 1,int pageSize = 20)
        {
            if(category == null)
            {
                category = 0;
            }
            int _total;
            var _rows = commentModelService.FindPageList(out _total, pageindex, pageSize, "Article", title, (int)category, input, fromDate, toDate, 0).Select(
                cm => new MyNinesky.Models.CommonModelViewModel()
                {
                    CategoryID = cm.CategoryID,
                    CategoryName = cm.Category.Name,
                    DefaultPicUrl = cm.DefaultPicUrl,
                    Hits = cm.Hits,
                    Inputer = cm.Inputer,
                    Model = cm.Model,
                    ModelID = cm.ModelID,
                    ReleaseDate = cm.ReleaseDate,
                    Status = cm.Status,
                    Title = cm.Title
                }
                );
            return Json(new { total = _total,rows =_rows.ToList()});
        }
        public ActionResult MyList()
        {
            return View();
        }
        /// <summary>
        /// 我的文章列表
        /// </summary>
        /// <param name="title"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult MyJsonList(string title,Nullable<DateTime> fromDate,Nullable<DateTime> toDate,
            int pageIndex = 1,int pageSize = 20)
        {
            int _total;
            var _rows = commentModelService.FindPageList(out _total,pageIndex,pageSize,"Article",title,0,string.Empty,
                fromDate,toDate,0).Select( cm => new MyNinesky.Models.CommonModelViewModel() {
                    CategoryID = cm.CategoryID,
                    CategoryName = cm.Category.Name,
                    DefaultPicUrl = cm.DefaultPicUrl,
                    Hits = cm.Hits,
                    Inputer = cm.Inputer,
                    Model = cm.Model,
                    ModelID = cm.ModelID,
                    ReleaseDate = cm.ReleaseDate,
                    Status = cm.Status,
                    Title = cm.Title
                });
            return Json(new { total = _total,rows = _rows.ToList()},JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <returns></returns>
        public JsonResult Delete(int id)
        {
            // 删除附件
            var _commonModel = commentModelService.Find(id);
            if(_commonModel == null)
            {
                return Json(false);
            }
            //删除附件
            foreach(var _attachment in _commonModel.Attachment)
            {
                System.IO.File.Delete(Server.MapPath(_attachment.FileParth));
            }
            if(commentModelService.Delete(_commonModel))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            return View(commentModelService.Find(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit()
        {
            int _id = int.Parse(ControllerContext.RouteData.GetRequiredString("id"));
            var _commonModel = commentModelService.Find(_id);
            TryUpdateModel(_commonModel,new string[] { "CategoryID","Title","DefaultPicUrl"});
            TryUpdateModel(_commonModel.Article,"Article",new string[] { "Author","Source","Intro","Content"});
            if(ModelState.IsValid)
            {
                if(commentModelService.Update(_commonModel))
                {
                    //附件处理
                    interfaceAttachmentService _attachmentService = new AttachmentService();
                    var _attachments = _attachmentService.FindList(_commonModel.ModelID,User.Identity.Name,string.Empty,true).ToList();
                    foreach(var _att in _attachments)
                    {
                        var _filePath = Url.Content(_att.FileParth);
                        if((_commonModel.DefaultPicUrl != null && _commonModel.DefaultPicUrl.IndexOf(_filePath) >= 0) || _commonModel.Article.Content.IndexOf(_filePath)>0)
                        {
                            _att.ModelID = _commonModel.ModelID;
                            _attachmentService.Update(_att);
                        }
                        else
                        {
                            System.IO.File.Delete(Server.MapPath(_att.FileParth));
                            _attachmentService.Delete(_att);
                        }
                    }
                    return View("EditSuccess",_commonModel);
                }
            }
            return View(_commonModel);
        }
       
    }
}