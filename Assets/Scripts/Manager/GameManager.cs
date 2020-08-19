using PuzzleGame.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace PuzzleGame.Manager
{
    /// <summary>
    /// 主逻辑
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private string m_PICName;
        private static GameManager m_instance;
        /// <summary>
        /// 交互列表
        /// </summary>
        public List<Interactive> m_interactives;
        /// <summary>
        /// 当前场景号
        /// </summary>
        public int m_SceneIndex = 0;
        /// <summary>
        /// 当前交互号
        /// </summary>
        public int InteractivesIndex
        {
            get { return m_InteractivesIndex; }
            set {
                m_InteractivesIndex = value;
                if(InteractivesIndex<m_interactives.Count)
                    Next();
                else
                {
                    m_SceneIndex++;
                    StartCoroutine(initXML(m_SceneIndex));
                }
            }
        }
        [SerializeField]
        private int m_InteractivesIndex = -1;
        /// <summary>
        /// 交互文字
        /// </summary>
        public Text m_text;
        /// <summary>
        /// 文字刷新间隔
        /// </summary>
        public float m_TextUpdateInterval = 0.1f;
        /// <summary>
        /// 背景图片，自定义交互的父物体
        /// </summary>
        public GameObject m_background;
        /// <summary>
        /// 当前刷新文字编号
        /// </summary>
        private int m_TextIndex = 0;
        /// <summary>
        /// 表示遇到特殊交互时暂停主逻辑
        /// </summary>
        public bool m_suspend = false;
        private IEnumerator m_TextUpdateHandle;
        public GameObject m_MainPanel;
        /// <summary>
        /// 单例模式
        /// </summary>
        public static GameManager Instance
        {
            get
            {
                if (m_instance == null)
                    //则创建一个
                    m_instance = GameObject.Find("Managers").GetComponent<GameManager>();
                //返回这个实例
                return m_instance;
            }
        }

        void Start()
        {
            StartCoroutine(initXML(m_SceneIndex));
            Debug.Log(Screen.width + "----" + Screen.height);
        }

        private IEnumerator initXML(int SceneIndex)
        {
            var request = UnityWebRequest.Get(Application.streamingAssetsPath + "/01.xml");
            yield return request.SendWebRequest();
            if (request.downloadHandler.text != string.Empty)
            {
                Debug.Log("StreamingAssets读取成功");
                m_interactives = XMlTools.GetInstance().GetInteractives(request.downloadHandler.text, SceneIndex, out m_PICName);
                m_TextUpdateHandle = TextUpdate("");
                StartCoroutine(m_TextUpdateHandle);
                StartCoroutine(MouseListen());
                InteractivesIndex++;
            }
        }

        private IEnumerator LoadMusic(string name) 
        {
            string path = Application.dataPath;
            int i = path.LastIndexOf("/");
            path = path.Substring(0, i);
            path += "/Music/" + name;
            var request = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV);
            yield return request.SendWebRequest();
            if (request.error == null && request.isDone)
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
                GetComponent<AudioSource>().clip = clip;
                GetComponent<AudioSource>().Play();
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }
        }
        

        private void Next()
        {
            //判断是不是基础的交互
            if (m_interactives[InteractivesIndex] is Session)
            {
                Session session = m_interactives[InteractivesIndex] as Session;
                m_MainPanel.SetActive(true);
                m_MainPanel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load("Image/portrait/" + session.m_PIC, typeof(Sprite)) as Sprite;
                UpdateTextString(session.m_value);
            }
            else if (m_interactives[InteractivesIndex] is If)
            {
                If @if = m_interactives[InteractivesIndex] as If;
                m_interactives.InsertRange(InteractivesIndex, @if.Interactives);
            }
            else if (m_interactives[InteractivesIndex] is Jump)
            {
                Jump jump = m_interactives[InteractivesIndex] as Jump;
                if (Jump.m_JumpList.ContainsKey(jump.m_target))
                {
                    InteractivesIndex = m_interactives.IndexOf(Jump.m_JumpList[jump.m_target]) + 1;
                }
                //interactives.InsertRange(InteractivesIndex, @if.Interactives);
            }
            else if (m_interactives[InteractivesIndex] is BackGround)
            {
                BackGround backGround = m_interactives[InteractivesIndex] as BackGround;
                m_background.GetComponent<Image>().sprite = Resources.Load("Image/BackGround/" + backGround.m_PIC, typeof(Sprite)) as Sprite;
                InteractivesIndex++;
            }
            else if (m_interactives[InteractivesIndex] is BGM)
            {
                BGM bgm = m_interactives[InteractivesIndex] as BGM;
                StartCoroutine(LoadMusic(bgm.m_Music));
                InteractivesIndex++;
            }else if (m_interactives[InteractivesIndex] is Disappear)
            {
                m_MainPanel.SetActive(false);
                InteractivesIndex++;
            }
            else
            {
                m_suspend = true;
                GameObject prefab = Instantiate(Resources.Load("Prefabs/" + m_interactives[InteractivesIndex].GetType().Name + "/" + m_interactives[InteractivesIndex].GetType().Name, typeof(GameObject)) as GameObject);
                prefab.transform.SetParent(m_background.transform);
                prefab.transform.localScale = new Vector3(1, 1, 1);
                Debug.Log(prefab.name);
                prefab.GetComponent<BaseManager>().OnInit(m_interactives[InteractivesIndex]);
                //特殊交互暂停主逻辑
            }
        }

        /// <summary>
        /// 鼠标监听与主体逻辑
        /// </summary>
        /// <returns></returns>
        private IEnumerator MouseListen()
        {
            float MouseInterval = 0.1f;
            float MouseTime = MouseInterval;
            while (true)
            {
                MouseTime -= Time.deltaTime;
                if (MouseTime <= 0 && !m_suspend)
                {
                    if (Input.GetMouseButton(0))
                    {
                        //重置
                        MouseTime = MouseInterval;
                        InteractivesIndex++;
                        m_text.text = "";
                        m_TextIndex = 0;                        
                    }
                }
                yield return null;
            }
        }

        private void UpdateTextString(string value)
        {
            StopCoroutine(m_TextUpdateHandle);
            m_TextUpdateHandle = TextUpdate(value);
            StartCoroutine(m_TextUpdateHandle);
        }

        /// <summary>
        /// 字体刷新
        /// </summary>
        /// <returns></returns>
        private IEnumerator TextUpdate(string value)
        {
            float TextTime = m_TextUpdateInterval;
            m_text.text = "";
            while (true)
            {
                TextTime -= Time.deltaTime;

                if (TextTime <= 0 && !m_suspend)
                {
                    TextTime = m_TextUpdateInterval;

                    if (m_TextIndex < value.Length)
                    {

                        m_text.text += value[m_TextIndex];
                        m_TextIndex++;
                    }

                }

                yield return null;
            }
        }

        /// <summary>
        /// 结束特殊交互,并对特殊交互的子交互进行处理
        /// </summary>
        public void EndSuspend(string Return)
        {
            ComplexInteraction complexInteraction = m_interactives[InteractivesIndex] as ComplexInteraction;
            if (complexInteraction == null)
            {
                m_suspend = false;
                //InteractivesIndex++;
                return;
            }
            foreach (Interactive interactive in complexInteraction.Interactives)
            {
                if (interactive is If)
                {
                    If @if = interactive as If;
                    if (@if.m_target.Equals(Return))
                    {
                        m_interactives.InsertRange(InteractivesIndex + 1, @if.Interactives);
                    }
                }
            }
            InteractivesIndex++;
            m_suspend = false;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}