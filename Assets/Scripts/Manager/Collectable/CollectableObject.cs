using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PuzzleGame.Manager
{
    /// <summary>
    /// 可收集物体为Sprite
    /// </summary>
    public class CollectableObject : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        /// <summary>
        /// 是否被收集
        /// </summary>
        public bool m_isCollectable = false;
        /// <summary>
        /// 可使用次数
        /// </summary>
        public int m_UseCount = 1;

        public delegate void handle();
        public event handle EndDragEvent;

        public enum CollectionType
        {
            Plate
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public CollectionType collectionType;

        public void Used()
        {
            m_UseCount--;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        public void OnDrag(PointerEventData eventData)
        {
            GetComponent<RectTransform>().anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDragEvent?.Invoke();
            if (m_UseCount > 0)
                GetComponent<Rigidbody2D>().gravityScale = 1;
            else
            {
                CollectableManager.Instance.RemoveCollection(this);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (m_isCollectable == false)
            {
                GetComponent<Rigidbody2D>().gravityScale = 1;
                m_isCollectable = true;
                CollectableManager.Instance.AddCollection(this);
            }
        }

        void Start()
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }

    }

}