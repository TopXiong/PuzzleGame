using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGame.Model
{
    /// <summary>
    /// 自定义分支类，用于多种选择时
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class Match : Interactive
    {
        /// <summary>
        /// 选择的名字列表
        /// </summary>
        public string m_Item;
    }
}
