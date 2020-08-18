using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PuzzleGame.Manager
{
    /// <summary>
    /// 开始游戏场景
    /// </summary>
    public class LoadManager : MonoBehaviour
    {
        /// <summary>
        /// 进度条物品
        /// </summary>
        public GameObject loadScreen;
        /// <summary>
        /// 进度条滑块
        /// </summary>
        public Slider slider;
        /// <summary>
        /// 进度条值
        /// </summary>
        public Text text;

        /// <summary>
        /// 加载下一关
        /// </summary>
        public void LoadNextLevel()
        {
            StartCoroutine(LoadLevel());
        }

        IEnumerator LoadLevel()
        {
            loadScreen.SetActive(true);
            AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                slider.value = operation.progress;
                text.text = operation.progress * 100 + "%";
                if (operation.progress >= 0.9f)
                {
                    slider.value = 1;

                    text.text = "按下任意键继续";
                    if (Input.anyKeyDown)
                    {
                        operation.allowSceneActivation = true;
                    }
                }
                yield return null;
            }
        }
    }

}