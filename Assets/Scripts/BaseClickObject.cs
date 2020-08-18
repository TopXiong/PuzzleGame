using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace PuzzleGame.Manager
{
    /// <summary>
    /// 可点击组件继承
    /// </summary>
    public abstract class BaseClickObject : MonoBehaviour
    {
        /// <summary>
        /// 场景中的物体被点击时调用，需要有Box Collider2d组件才有效
        /// </summary>
        public abstract void OnClick();
    }

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