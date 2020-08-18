
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 交互基类，所有的交互类型必须继承与此类或其子类
/// 所有的自定义属性必须是string，可以使用List不能用数组
/// </summary>
public abstract class Interactive:System.Attribute
{   
    /// <summary>
    /// XML中的值
    /// </summary>
    public string m_value;

    /// <summary>
    /// 输出value的值
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return m_value;
    }

    /// <summary>
    /// 被XML解析器赋值完成时调用，代替构造方法
    /// </summary>
    public virtual void Initialized() { }

}

/// <summary>
/// 复杂交互类，如果交互类型中有子交互，就继承此类
/// </summary>
public class ComplexInteraction : Interactive
{
    /// <summary>
    /// 此交互结束时会传出的值
    /// </summary>
    public string m_Return;
    /// <summary>
    /// 字交互
    /// </summary>
    public Interactive[] Interactives;
}