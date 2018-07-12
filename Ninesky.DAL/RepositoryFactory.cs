using Ninesky.IDAL;

namespace Ninesky.DAL
{
    /// <summary>
    /// 简单工厂
    /// </summary>
    public static class RepositoryFactory
    {
        /// <summary>
        /// 文章仓库
        /// </summary>
        public static InterfaceArticleRepository ArticleRepository { get { return new ArticleRepository(); } }
        /// <summary>
        /// 附件仓库
        /// </summary>
        public static InterfaceAttachmentRepository AttachmentRepository { get { return new AttachmentRepository(); } }
        /// <summary>
        /// 栏目仓库
        /// </summary>
        public static InterfaceCategoryRepository CategoryRepository { get { return new CategoryRepository(); } }
        /// <summary>
        /// 咨询仓库
        /// </summary>
        public static InterfaceConsultationRepository ConsultationRepository { get { return new ConsultationRepository(); } }
        /// <summary>
        /// 公共模型仓库
        /// </summary>
        public static InterfaceCommonModelRepository CommonModelRepository { get { return new CommonModelRepository(); } }
        /// <summary>
     /// 用户仓储
     /// </summary>
        public static InterfaceUserRepository UserRepository { get { return new UserRepository(); } }

    }
}
