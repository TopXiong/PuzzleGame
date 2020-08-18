using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGame.Model
{ 
    /// <summary>
    /// 未定
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class Select : Interactive
    {
        /// <summary>
        /// 未定
        /// </summary>
        public List<string> item;
    }
}
