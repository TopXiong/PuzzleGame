using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PuzzleGame.Model
{
    [AttributeUsage(AttributeTargets.All)]
    public class Session : Interactive
    {
        public string m_PIC;
        public string m_name;
    }

    [AttributeUsage(AttributeTargets.All)]
    public class If : ComplexInteraction
    {
        public string m_target;
    }

    [AttributeUsage(AttributeTargets.All)]
    public class BackGround : ComplexInteraction
    {
        public string m_PIC;
    }

    [AttributeUsage(AttributeTargets.All)]
    public class BGM : ComplexInteraction
    {
        public string m_Music;
    }

    /// <summary>
    /// 跳转至指定的Id
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class Jump : Interactive
    {
        /// <summary>
        /// 自身Id
        /// </summary>
        public string m_Id;

        /// <summary>
        /// 目标Id
        /// </summary>
        public string m_target;
        /// <summary>
        /// 判断重复
        /// </summary>
        public static Dictionary<string, Jump> m_JumpList = new Dictionary<string, Jump>();
    }
}
