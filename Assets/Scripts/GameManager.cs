using PuzzleGame.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace PuzzleGame.Manager
{
    /// <summary>
    /// 主逻辑
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private string PICName;
        private static GameManager _instance;
        /// <summary>
        /// 交互列表
        /// </summary>
        public List<Interactive> interactives;
        /// <summary>
        /// 当前场景号
        /// </summary>
        public int SceneIndex = 0;
        /// <summary>
        /// 当前交互号
        /// </summary>
        public int InteractivesIndex
        {
            get { return interactivesIndex; }
            set { interactivesIndex = value;
                UpdateTextString(interactives[InteractivesIndex].value);
            }
        }
        private int interactivesIndex = 0;
        /// <summary>
        /// 交互文字
        /// </summary>
        public Text text;
        /// <summary>
        /// 文字刷新间隔
        /// </summary>
        public float TextUpdateInterval = 0.2f;
        /// <summary>
        /// 背景图片，自定义交互的父物体
        /// </summary>
        public GameObject background;
        /// <summary>
        /// 当前刷新文字编号
        /// </summary>
        private int TextIndex = 0;
        /// <summary>
        /// 表示遇到特殊交互时暂停主逻辑
        /// </summary>
        public bool suspend = false;
        private IEnumerator TextUpdateHandle;

        /// <summary>
        /// 单例模式
        /// </summary>
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    //则创建一个
                    _instance = GameObject.Find("GameManager").GetComponent<GameManager>();
                //返回这个实例
                return _instance;
            }
        }

        void Start()
        {
            StartCoroutine(initXML());
            Debug.Log(Screen.width + "----" + Screen.height);
        }

        private IEnumerator initXML()
        {
            var request = UnityWebRequest.Get(Application.streamingAssetsPath + "/01.xml");
            yield return request.SendWebRequest();
            if (request.downloadHandler.text != string.Empty)
            {
                Debug.Log("StreamingAssets读取成功");
                interactives = XMlTools.GetInstance().GetInteractives(request.downloadHandler.text, SceneIndex, out PICName);
                TextUpdateHandle = TextUpdate(interactives[InteractivesIndex].value);
                StartCoroutine(TextUpdateHandle);
                StartCoroutine(MouseListen());
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
                if (MouseTime <= 0 && !suspend)
                {
                    if (Input.GetMouseButton(0))
                    {
                        //重置
                        MouseTime = MouseInterval;
                        InteractivesIndex++;
                        text.text = "";
                        TextIndex = 0;

                        //判断是不是特殊的固定的交互
                        if (interactives[InteractivesIndex] is Session)
                        {
                            UpdateTextString(interactives[InteractivesIndex].value);
                        }
                        else if (interactives[InteractivesIndex] is If)
                        {
                            If @if = interactives[InteractivesIndex] as If;
                            interactives.InsertRange(InteractivesIndex, @if.Interactives);
                        }
                        else if (interactives[InteractivesIndex] is Jump)
                        {
                            Jump jump = interactives[InteractivesIndex] as Jump;
                            if (Jump.JumpList.ContainsKey(jump.target))
                            {
                                InteractivesIndex = interactives.IndexOf(Jump.JumpList[jump.target]) + 1;                               
                            }
                            //interactives.InsertRange(InteractivesIndex, @if.Interactives);
                        }
                        else
                        {
                            suspend = true;
                            Debug.Log("Prefabs/" + interactives[InteractivesIndex].GetType().Name + "/" + interactives[InteractivesIndex].GetType().Name);
                            GameObject prefab = Instantiate(Resources.Load("Prefabs/" + interactives[InteractivesIndex].GetType().Name + "/" + interactives[InteractivesIndex].GetType().Name, typeof(GameObject)) as GameObject);
                            prefab.transform.SetParent(background.transform);
                            prefab.GetComponent<BaseManager>().OnInit(interactives[InteractivesIndex]);
                            //特殊交互暂停主逻辑
                        }

                    }
                }
                yield return null;
            }
        }

        private void UpdateTextString(string value)
        {
            StopCoroutine(TextUpdateHandle);
            TextUpdateHandle = TextUpdate(value);
            StartCoroutine(TextUpdateHandle);
        }

        /// <summary>
        /// 字体刷新
        /// </summary>
        /// <returns></returns>
        private IEnumerator TextUpdate(string value)
        {
            float TextTime = TextUpdateInterval;
            text.text = "";
            while (true)
            {
                TextTime -= Time.deltaTime;

                if (TextTime <= 0 && !suspend)
                {
                    TextTime = TextUpdateInterval;

                    if (TextIndex < value.Length)
                    {

                        text.text += value[TextIndex];
                        TextIndex++;
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
            ComplexInteraction complexInteraction = interactives[InteractivesIndex] as ComplexInteraction;
            if (complexInteraction == null)
            {
                suspend = false;
                return;
            }
            foreach (Interactive interactive in complexInteraction.Interactives)
            {
                if (interactive is If)
                {
                    If @if = interactive as If;
                    if (@if.target.Equals(Return))
                    {
                        interactives.InsertRange(InteractivesIndex + 1, @if.Interactives);
                    }
                }
            }
            InteractivesIndex++;            
            suspend = false;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}