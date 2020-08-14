using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// 可点击组件基础
/// </summary>
public abstract class BaseClickObject : MonoBehaviour
{
    public abstract void OnClick();
}

/// <summary>
/// 自定义组件需要基础
/// </summary>
public abstract class BaseManager : MonoBehaviour
{
    public abstract void OnInit(Interactive interactive);
}