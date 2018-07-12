using Ninesky.DAL;
using Ninesky.IBLL;
using Ninesky.Models;
using Ninesky.Models.ArticeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.BLL
{
    /// <summary>
    /// 栏目服务
    /// </summary>
    public class CategoryService :BaseService<Category>,interfaceCategoryService
    {
        public CategoryService():base(RepositoryFactory.CategoryRepository)
        {

        }

        public List<EasyuiTreeNodeViewModel> EasyuiTreeData(string model)
        {
            //栏目ID列表
            Dictionary<string, int> _categoryIDList = new Dictionary<string, int>();
            //查询栏目列表
            IQueryable<Category> _categoryList = CurrentRepository.Entities.OrderBy(c =>c.Order);
            if(!string.IsNullOrEmpty(model))
            {
                _categoryList = _categoryList.Where(c => c.Model == model);
            }
            //栏目parentParth
            var _partentParthList = _categoryList.Select(c => c.PartentPath).ToList();
            //遍历partentParth
            foreach(var _partentParth in _partentParthList)
            {
                //将partentParth分隔为ID字符串列表
                var _strCategoryIDList = _partentParth.Split(new char[] { ','},StringSplitOptions.RemoveEmptyEntries);
                //将CategoryID循环添加到栏目ID列表
                foreach(var _strCategoryID in _strCategoryIDList)
                {
                    if(!_categoryIDList.ContainsKey(_strCategoryID))
                    {
                        _categoryIDList.Add(_strCategoryID,int.Parse(_strCategoryID));
                    }
                }
            }
            //栏目树
            List<Models.EasyuiTreeNodeViewModel> _tree = new List<EasyuiTreeNodeViewModel>();
            //树栏目列表
            IQueryable<Category> _categoryTreeList = CurrentRepository.Entities.Where(c =>_categoryIDList.Values.Contains(c.ParentId)).OrderByDescending(c=>c.PartentPath).ThenBy(c=>c.Order);
            //遍历树栏目列表
            foreach(var _categoryThree in _categoryTreeList)
            {
                //树种节点父栏目为当前栏目
                if(_tree.Exists(n=>n.parentid == _categoryThree.CategoryID))
                {
                    var _children = _tree.Where(n=>n.parentid == _categoryThree.CategoryID).ToList();
                    _tree.RemoveAll(n=>n.parentid == _categoryThree.CategoryID);
                    _tree.Add(new EasyuiTreeNodeViewModel() { id = _categoryThree.CategoryID, parentid = _categoryThree.ParentId, text = _categoryThree.Name, children = _children });
                }
                else
                {
                    _tree.Add(new EasyuiTreeNodeViewModel() { id = _categoryThree.CategoryID,parentid = _categoryThree.ParentId,text = _categoryThree.Name});
                }
               
            }
            return _tree;
        }

        public List<Category> FindList(string model)
        {
            return CurrentRepository.Entities.Where(c => c.Model == model).ToList();
        }
    }
}
