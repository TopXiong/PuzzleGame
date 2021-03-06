﻿using System;
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
    public class Branch : ComplexInteraction
    {
        /// <summary>
        /// 选项的图片列表
        /// </summary>
        public List<string> m_PIC;
        /// <summary>
        /// 选择的名字列表
        /// </summary>
        public List<string> m_name;

        public override string ToString()
        {
            return m_PIC.ToArray() + "----" + m_name.ToArray();
        }
    }
}
