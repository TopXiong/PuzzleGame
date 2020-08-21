using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PuzzleGame.Manager
{
    /// <summary>
    /// 分支交互的选项
    /// </summary>
    public class BranchItem : MonoBehaviour, IPointerClickHandler
    {
        /// <summary>
        /// 选项的名字
        /// </summary>
        public string m_ItemName;

        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager.Instance.EndSuspend(m_ItemName);
            Destroy(transform.parent.gameObject, 2f);
            transform.parent.gameObject.SetActive(false);
        }
    }

}