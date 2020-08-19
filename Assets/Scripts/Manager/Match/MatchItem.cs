using PuzzleGame.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchItem : MonoBehaviour
{

    public CollectableObject.CollectionType m_CollectionType;
    //public GameObject target;

    public bool Find = false;
    public Sprite sprite;

    public void Check()
    {
        if (Find)
        {
            GetComponent<Image>().sprite = sprite;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("in");
        try
        {
            if(other.GetComponent<CollectableObject>().collectionType == m_CollectionType)
            {
                other.GetComponent<CollectableObject>().EndDragEvent += Check;
                Find = true;
            }

        }
        catch (Exception) { }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("out");
        try
        {
            if (other.GetComponent<CollectableObject>().collectionType == m_CollectionType)
            {
                Find = false;
            }

        }
        catch (Exception) { }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
