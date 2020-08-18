using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using PuzzleGame.Model;
using System.Linq;
using System.Xml;

/// <summary>
/// XML解析工具
/// </summary>
public class XMlTools
{
    private Type[] types;
    private XmlDocument xmlDocument;

    /// <summary>
    /// 获取XMl解析器
    /// </summary>
    /// <returns></returns>
    public static XMlTools GetInstance()
    {
        return new XMlTools();
    }
    /// <summary>
    /// 初始化解析器
    /// </summary>
    private XMlTools()
    {
        Assembly asm = Assembly.GetExecutingAssembly();
        types = asm.GetTypes().Where(t => String.Equals(t.Namespace, "PuzzleGame.Model", StringComparison.Ordinal)).ToArray();
        xmlDocument = new XmlDocument();
    }
    /// <summary>
    /// 从value中解析第index个场景，场景图片名为PicName
    /// </summary>
    /// <param name="value">配置文件</param>
    /// <param name="index">场景数</param>
    /// <param name="PicName">图片名称</param>
    /// <returns>当前场景的交互列表</returns>
    public List<Interactive> GetInteractives(string value,int index,out string PicName)
    {
        List<Interactive> interactives = new List<Interactive>();
        xmlDocument.LoadXml(value);
        XmlNode node = xmlDocument.SelectSingleNode("Scenes");
        XmlNode scene = node.ChildNodes[index];
        //解析场景的背景图片名称
        PicName = scene.Attributes["PIC"].Value;
        foreach(XmlNode interactive in scene)
        {
            //try
            //{
            Interactive obj = CreateInteractiveByXmlNode(interactive);
            //Debug.Log(interactive.Name + "---" + interactive.ChildNodes.Count);


            interactives.Add(obj);
            //}
            //catch (Exception e)
            //{
            //    Debug.LogError(interactive.InnerXml +"  Error " + e);
            //}
        }
        return interactives;
    }

    private Interactive CreateInteractiveByXmlNode(XmlNode interactive)
    {
        Type type = Type.GetType("PuzzleGame.Model." + interactive.Name + "," + Assembly.GetExecutingAssembly().FullName);
        Interactive obj = Activator.CreateInstance(type, true) as Interactive;
        //解析属性
        foreach (XmlAttribute attribute in interactive.Attributes)
        {
            //含有-表示当前属性是列表
            if (attribute.Name.Contains("-"))
            {
                //为null则创建
                //Debug.Log(attribute.Name.Split('-')[0] + "---" + obj.GetType().Name);
                if (type.GetField("m_" + attribute.Name.Split('-')[0]).GetValue(obj) == null)
                {
                    object list = Activator.CreateInstance(typeof(List<string>));
                    type.GetField("m_" + attribute.Name.Split('-')[0]).SetValue(obj, list);
                }
                //为列表添加值
                (type.GetField("m_" + attribute.Name.Split('-')[0]).GetValue(obj) as List<string>).Add(attribute.Value);

            }
            else
            {                
                type.GetField("m_" + attribute.Name).SetValue(obj, attribute.Value);
                type.GetField("m_" + "value").SetValue(obj, interactive.InnerText);
            }            
        }
        obj.Initialized();
        //对Jump的特殊解析
        if (obj is Jump)
        {
            Jump jump = obj as Jump;
            if (Jump.m_JumpList.ContainsKey(jump.m_Id))
            {
                throw new Exception("JumpId : " + jump.m_Id + "repeated");
            }
            Jump.m_JumpList.Add(jump.m_Id, jump);
        }
        if(interactive.ChildNodes[0] == null)
        {
            return obj;
        }
        //dfs解析value
        if (interactive.ChildNodes[0].ChildNodes.Count > 0)
        {
            obj = dfs(interactive, obj);
        }
        return obj;
    }


    /// <summary>
    /// dfs生成交互类型，并将子类型全部实例化
    /// </summary>
    /// <param name="xmlNode">当前类型的节点</param>
    /// <param name="obj">当前类型实体</param>
    /// <returns>当前类型实体</returns>
    public Interactive dfs(XmlNode xmlNode, Interactive obj)
    {
        ComplexInteraction complexInteraction = obj as ComplexInteraction;
        //转型失败
        if(complexInteraction == null)
        {
            return obj;
        }
        complexInteraction.Interactives = new Interactive[xmlNode.ChildNodes.Count];
        for(int i = 0; i < xmlNode.ChildNodes.Count; i++)
        {
            complexInteraction.Interactives[i] = CreateInteractiveByXmlNode(xmlNode.ChildNodes[i]);
        }
        return complexInteraction;
    }

}
