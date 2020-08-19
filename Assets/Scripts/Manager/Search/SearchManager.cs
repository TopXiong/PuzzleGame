using PuzzleGame.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PuzzleGame.Manager
{
    /// <summary>
    /// 未定
    /// </summary>
    public class SearchManager : BaseManager
    {
        /// <summary>
        /// 已结束
        /// </summary>
        private bool end = false;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void OnInit(Interactive interactive)
        {
            Search search = interactive as Search;
            transform.Find(search.m_Item).gameObject.SetActive(true);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (transform.childCount == 0 && !end)
            {
                Destroy(gameObject, 2f);
                GameManager.Instance.EndSuspend("");
                end = true;
            }
        }
    }
}
