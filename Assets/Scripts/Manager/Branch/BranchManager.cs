using PuzzleGame.Model;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace PuzzleGame.Manager
{
    /// <summary>
    /// 分支交互逻辑
    /// </summary>
    public class BranchManager : BaseManager
    {
        private Branch m_branch;
        /// <summary>
        /// 字物体
        /// </summary>
        public GameObject m_item;

        /// <summary>
        /// 初始化，导入图片，并根据图片大小计算布局
        /// </summary>
        /// <param name="interactive"></param>
        public override void OnInit(Interactive interactive)
        {
            float padding = 2f;
            float width = 0, height = 0;
            List<GameObject> list = new List<GameObject>();
            m_branch = interactive as Branch;
            for (int i = 0; i < m_branch.m_PIC.Count; i++)
            {
                GameObject gameObject = Instantiate(m_item);
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Image/" + m_branch.m_PIC[i], typeof(Sprite)) as Sprite;
                gameObject.transform.SetParent(transform);
                gameObject.GetComponent<BranchItem>().m_Name = m_branch.m_name[i];
                list.Add(gameObject);
                width += gameObject.GetComponent<SpriteRenderer>().sprite.rect.width;
                height += gameObject.GetComponent<SpriteRenderer>().sprite.rect.height;
            }
            //计算图片大小，布局
            float kWidth = Screen.width * 0.5f / width;
            float kHeight = Screen.height * 0.5f / height;
            for (int i = 0; i < list.Count; i++)
            {
                list[i].transform.localScale = new Vector3(kWidth, kHeight, 1);
                list[i].transform.position = new Vector3(-8 + padding * (i + 1) + width / 100 / list.Count * i, 2, 0);
            }

        }

        IEnumerator LoadSprite(string fileName)
        {
            var uri = new System.Uri(Path.Combine(Application.streamingAssetsPath, fileName));
            UnityWebRequest wr = UnityWebRequestTexture.GetTexture(uri.AbsoluteUri);
            yield return wr.SendWebRequest();
            while (!wr.isDone) { }
            Texture2D tex = DownloadHandlerTexture.GetContent(wr);
            Sprite s = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height),
                                     new Vector2(0.5f, 0.5f));
            //target.GetComponent<SpriteRenderer>().sprite = s;
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