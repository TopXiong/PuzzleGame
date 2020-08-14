
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Interactive:System.Attribute
{   
    public enum InteractiveType
    {
        Session
    }

    /// <summary>
    /// 交互类型,暂时没用
    /// </summary>
    public InteractiveType interactiveType;
    /// <summary>
    /// XML中的值
    /// </summary>
    public string value;
    /// <summary>
    /// XML中的值
    /// </summary>
    public string Return;

    public override string ToString()
    {
        return value;
    }
}

public class ComplexInteraction : Interactive
{
    public Interactive[] Interactives;
}