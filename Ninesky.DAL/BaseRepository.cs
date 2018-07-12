using Ninesky.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.DAL
{
    public class BaseRepository<T> : InterfaceBaseRepository<T> where T : class
    {
        protected NineskyDbContext nContext = ContextFactory.GetCurrentContext();

        public IQueryable<T> Entities { get { return nContext.Set<T>(); } }

        public T Add(T entity,bool isSave = true)
        {
            nContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;
            nContext.Set<T>().Add(entity);
            nContext.SaveChanges();
            return entity;
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return nContext.Set<T>().Count(predicate);
        }

        public bool Delete(T entitiy,bool isSave = true)
        {
            nContext.Set<T>().Attach(entitiy);
            nContext.Entry<T>(entitiy).State = System.Data.Entity.EntityState.Deleted;
            return nContext.SaveChanges() > 0;

        }

        public bool Exist(Expression<Func<T, bool>> anyLambda)
        {
            return nContext.Set<T>().Any(anyLambda);
        }

        public T Find(Expression<Func<T, bool>> whereLambda)
        {
            T _entity = nContext.Set<T>().FirstOrDefault<T>(whereLambda);
            return _entity;
        }

        public T Find(int ID)
        {
            return nContext.Set<T>().Find(ID);
        }

        public IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        {
            var _list = nContext.Set<T>().Where<T>(whereLamdba);
            if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba);
            else _list = _list.OrderByDescending<T, S>(orderLamdba);
            return _list;
        }

        public IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, string orderName, bool isAsc)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        {
            var _list = nContext.Set<T>().Where<T>(whereLamdba);
            totalRecord = _list.Count();
            if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            else _list = _list.OrderByDescending<T, S>(orderLamdba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            return _list;
        }

        public IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, string orderName, bool isAsc)
        {
            var _list = nContext.Set<T>().Where<T>(whereLamdba);
            totalRecord = _list.Count();
            _list = OrderBy(_list, orderName, isAsc).Skip<T>((pageIndex-1)*pageSize).Take<T>(pageSize);
            return _list;
        }

        public int Save()
        {
            return nContext.SaveChanges();
        }

        public bool Update(T entity,bool isSave = true)
        {
            nContext.Set<T>().Attach(entity);
            nContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            return nContext.SaveChanges() > 0;
        }
        private IQueryable<T> OrderBy(IQueryable<T> source,string propertyName,bool isAsc)
        {
            if (source == null)
                throw new ArgumentNullException("source","不能为空");
            if (string.IsNullOrEmpty(propertyName))
                return source;
            var _parameter = Expression.Parameter(source.ElementType);
            var _property = Expression.Property(_parameter,propertyName);

            if (_property == null)
                throw new ArgumentNullException("propertyName","属性不存在");
            var _lambda = Expression.Lambda(_property,_parameter);
            var _methoName = isAsc ? "OrderBy" : "OrderByDescending";
            var _resultExpression = Expression.Call(typeof(Queryable),_methoName,new Type[] { source.ElementType,_property.Type},source.Expression,Expression.Quote(_lambda));
            return source.Provider.CreateQuery<T>(_resultExpression);
        }
    }
}
