using PuzzleGame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public List<CollectableObject> m_collectables;
    public GameObject m_Collectable;
    private static CollectableManager m_instance;
    /// <summary>
    /// 单例
    /// </summary>
    public static CollectableManager Instance
    {
        get
        {
            if (m_instance == null)
                //则创建一个
                m_instance = GameObject.Find("Managers").GetComponent<CollectableManager>();
            //返回这个实例
            return m_instance;
        }
    }

    public void AddCollection(CollectableObject collectable)
    {
        m_collectables.Add(collectable);
        collectable.gameObject.transform.SetParent(m_Collectable.transform);
    }

    public void RemoveCollection(CollectableObject collectable)
    {
        m_collectables.Remove(collectable);
        collectable.gameObject.SetActive(false);
        Destroy(collectable.gameObject, 2f);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_collectables = new List<CollectableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
