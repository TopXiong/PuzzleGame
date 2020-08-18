using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GameObject OptionMenu;
    public AudioMixer AudioMixer;
    public GameObject MakerMenu;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void OptionGame()
    {
        OptionMenu.SetActive(true);
    }
    public void OptionBack()
    {
        OptionMenu.SetActive(false);
    }
    public void Vocie(float value)
    {
        AudioMixer.SetFloat("MainVoice",value);
    }
    public void Maker()
    {
        MakerMenu.SetActive(true);
    }
    public void MakerBack()
    {
        MakerMenu.SetActive(false);
    }
}
