using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace PuzzleGame.Manager
{

    /// <summary>
    /// 自定义组件需要继承
    /// </summary>
    public abstract class BaseManager : MonoBehaviour
    {
        /// <summary>
        /// 自定义组件初始化时调用，代替Start函数，会将配置中定义的参数传进来
        /// </summary>
        /// <param name="interactive"></param>
        public abstract void OnInit(Interactive interactive);
    }
}