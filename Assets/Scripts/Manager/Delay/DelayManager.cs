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
    public class DelayManager : BaseManager
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void OnInit(Interactive interactive)
        {
            Delay delay = interactive as Delay;
            StartCoroutine(TimeDelay(float.Parse(delay.m_Time)));
        }

        private IEnumerator TimeDelay(float time)
        {
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
