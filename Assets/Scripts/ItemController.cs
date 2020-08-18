using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PuzzleGame.Manager
{
    /// <summary>
    /// 此类对可收集物品进行移动、销毁操作
    /// </summary>
    public class ItemController : MonoBehaviour
    {
        public Transform currentPickedTf;
        public CollectableObject currentColObj;
        private static Dictionary<string, GameObject> itemDic;

        private void Start()
        {
            itemDic = new Dictionary<string, GameObject>();
            //Invoke("RemoveCurrentItem", 10);
        }

        /// <summary>
        /// 检测屏幕中物体是否被点击
        /// </summary>
        private void MousePick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(v.x, v.y), new Vector2(v.x, v.y), 1000f);
                if (Physics2D.Raycast(new Vector2(v.x, v.y), new Vector2(v.x, v.y), 1000f))
                {
                    Debug.Log(hit.transform.name);
                    currentPickedTf = hit.transform;
                    currentColObj = currentPickedTf.GetComponent<CollectableObject>();
                    if (currentColObj.isCollectable && itemDic.ContainsKey(hit.transform.name) != true)
                    {
                        AddItem();
                    }
                }
            }
        }

        /// <summary>
        /// 添加当前可收集物品进去Dictionary
        /// </summary>
        private void AddItem()
        {
            itemDic.Add(currentPickedTf.name, currentPickedTf.gameObject);
            currentPickedTf.SetParent(Camera.main.transform);
        }

        /// <summary>
        /// 当完成交互后物品被消耗时调用此方法
        /// </summary>
        public void RemoveCurrentItem()
        {
            Destroy(itemDic[currentPickedTf.name]);
            itemDic.Remove(currentPickedTf.name);
        }

        private bool isMouseDown = false;
        private Vector3 lastMousePosition = Vector3.zero;

        /// <summary>
        /// 控制物体的点击和拖拽
        /// </summary>
        void Update()
        {
            MousePick();

            if (Input.GetMouseButtonDown(0))
            {
                isMouseDown = true;
                currentPickedTf.GetComponent<Rigidbody2D>().gravityScale = 0;
                currentPickedTf.GetComponent<Rigidbody2D>().drag = 10;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isMouseDown = false;
                lastMousePosition = Vector3.zero;
                currentPickedTf.GetComponent<Rigidbody2D>().gravityScale = 1;
                currentPickedTf.GetComponent<Rigidbody2D>().drag = 0;
            }
            if (isMouseDown)
            {
                if (lastMousePosition != Vector3.zero)
                {
                    Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;
                    currentPickedTf.position += offset;
                }
                lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            }
        }


    }

}