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
    public class SelectManager : BaseManager
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void OnInit(Interactive interactive)
        {
            Select select = interactive as Select;
            Debug.Log(string.Join(",", select.m_item.ToArray()));
            StartCoroutine(TimeDelay());
        }

        private IEnumerator TimeDelay()
        {
            float time = 3f;
            while (true)
            {
                time -= Time.deltaTime;
                if (time <= 0)
                {
                    GameManager.Instance.EndSuspend("");
                }
                yield return null;
            }
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
}
