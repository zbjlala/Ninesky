using Ninesky.Models;
using Ninesky.Models.ArticeModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.DAL
{
    public class NineskyDbContext:DbContext
    {
        #region 内容
        public DbSet<Category> Categories { get; set; }
        public DbSet<CommonModel> CommonModels { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        #endregion

        #region 用户
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleRelation> UserRoleRelations { get; set; }
        public DbSet<UserConfig> UserConfig { get; set; }
        #endregion
        public NineskyDbContext():base("MyBlog")
        {

        }
    }
}
