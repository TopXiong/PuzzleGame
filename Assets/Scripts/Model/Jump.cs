using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PuzzleGame.Model
{
    /// <summary>
    /// 跳转至指定的Id
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class Jump : Interactive
    {
        /// <summary>
        /// 自身Id
        /// </summary>
        public string Id;

        /// <summary>
        /// 目标Id
        /// </summary>
        public string target;
        /// <summary>
        /// 判断重复
        /// </summary>
        public static Dictionary<string, Jump> JumpList = new Dictionary<string, Jump>();
    }
}
