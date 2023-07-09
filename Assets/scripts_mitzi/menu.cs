using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public GameObject[] SubMenus;
    public AudioMixer music;
    public AudioMixer sound;
    public GameObject CheckMarkFullScreen;
    public Slider sliderM;
    public Slider sliderS;
    public GameObject csScreen;
    public GameObject loadingSc;
    public bool PauseMenu;
    public bool SubMenu;
    private void Update()
    {
        if (PauseMenu && Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) Pause();
    }
    bool pause = false;
    public void Pause()
    {
        pause = !pause;
        transform.GetChild(0).gameObject.SetActive(pause);
        if (pause) Time.timeScale = 0;
        else if(!csScreen.activeSelf) Time.timeScale = 1;
    }
    public void Pause(bool on)
    {
        transform.GetChild(0).gameObject.SetActive(on);
        if (on) Time.timeScale = 0;
        else if (!csScreen.activeSelf) Time.timeScale = 1;
    }
    private void Start()
    {
        if (!SubMenu)
        {
            if (!PlayerPrefs.HasKey("fullScreen")) PlayerPrefs.SetInt("fullScreen", 1);
            if (PlayerPrefs.GetInt("fullScreen") == 0)
            {
                toggle = false;
                CheckMarkFullScreen.SetActive(false);
            }
            else
            {
                toggle = true;
                CheckMarkFullScreen.SetActive(true);
            }
            float x, y;
            music.GetFloat("volume", out y);
            sound.GetFloat("volume", out x);
            if (!PlayerPrefs.HasKey("music")) PlayerPrefs.SetFloat("music", y);
            else y = PlayerPrefs.GetFloat("music");

            if (!PlayerPrefs.HasKey("sound")) PlayerPrefs.SetFloat("sound", x);
            else x = PlayerPrefs.GetFloat("sound");
            sliderS.value = x;
            sliderM.value = y;
            music.SetFloat("volume", y);
            sound.SetFloat("volume", x);
        }
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        loadingSc.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }
    public void Restart()
    {
        Time.timeScale = 1;
        loadingSc.SetActive(true);
        Scene s = SceneManager.GetActiveScene();
        SceneManager.LoadScene(s.buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OpenMenu(int menu)
    {
        for (int i = 0; i < SubMenus.Length; i++)
        {
            SubMenus[i].SetActive(false);
        }
        SubMenus[menu].SetActive(true);
    }
    public void Music(float v)
    {
        float x;
        music.GetFloat("volume", out x);
        music.SetFloat("volume", x + v);
        if (x + v < -50) music.SetFloat("volume", -50);
        sliderM.value = x + v;
        PlayerPrefs.SetFloat("music", x);
        PlayerPrefs.Save();
    }
    public void Sound(float v)
    {
        float x;
        sound.GetFloat("volume", out x);
        sound.SetFloat("volume", x + v);
        if (x + v < -50) sound.SetFloat("volume", -50);
        sliderS.value = x + v;
        PlayerPrefs.SetFloat("sound", x);
        PlayerPrefs.Save();
    }
    bool toggle = true;
    public void ToggleFullScreen()
    {
        toggle = !toggle;
        CheckMarkFullScreen.SetActive(toggle);
        if (toggle) PlayerPrefs.SetInt("fullScreen", 1);
        else PlayerPrefs.SetInt("fullScreen", 0);
        Screen.fullScreen = toggle;
        PlayerPrefs.Save();
    }
    public int[,] resolutions = new int[,] { { 1920, 1080 }, { 1080, 720 }, { 720, 480 } };
    int i = 0;
    public void ChangeScreenSize()
    {
        i++;
        if (i >= resolutions.Length)
        {
            i = 0;
        }
        Screen.SetResolution(resolutions[i, 0], resolutions[i, 1], false);
        toggle = false;
        CheckMarkFullScreen.SetActive(false);
        PlayerPrefs.SetInt("fullScreen", 0);
        Screen.fullScreen = false;
        PlayerPrefs.Save();
    }
}
