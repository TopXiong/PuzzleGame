using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGame.Manager
{
    /// <summary>
    /// 分支交互的选项
    /// </summary>
    public class BranchItem : BaseClickObject
    {
        /// <summary>
        /// 选项的名字
        /// </summary>
        public string Name;

        /// <summary>
        /// 点击事件
        /// </summary>
        public override void OnClick()
        {
            GameManager.Instance.EndSuspend(Name);
            Destroy(transform.parent.gameObject, 2f);
            transform.parent.gameObject.SetActive(false);
        }
    }

}