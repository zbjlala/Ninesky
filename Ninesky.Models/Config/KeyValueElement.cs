using System.Configuration;

namespace Ninesky.Models.Config
{
    /// <summary>
    /// 键值元素
    /// <remarks>
    /// 创建：2014.03.09
    /// </remarks>
    /// </summary>
    public class KeyValueElement:ConfigurationElement
    {
        /// <summary>
        /// 键
        /// </summary>
        [ConfigurationProperty("key",IsRequired=true)]
        public string Key {
            get { return this["key"].ToString(); }
            set { this["key"] = value; }
        }
        /// <summary>
        /// 值
        /// </summary>
        [ConfigurationProperty("value")]
        public string Value
        {
            get { return this["value"].ToString(); }
            set { this["value"] = value; }
        }
    }
}
