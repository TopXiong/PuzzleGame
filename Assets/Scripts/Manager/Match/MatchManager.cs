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
    public class MatchManager : BaseManager
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void OnInit(Interactive interactive)
        {
            Match match = interactive as Match;
            transform.Find(match.m_Item).gameObject.SetActive(true);
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
