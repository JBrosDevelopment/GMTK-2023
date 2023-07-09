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
    public Slider sliderM;
    public Slider sliderS;
    public GameObject loadingSc;
    public bool PauseMenu;
    public bool SubMenu;
    private void Update()
    {
        if (PauseMenu && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))) Pause();
    }
    bool pause = false;
    public void Pause()
    {
        pause = !pause;
        transform.GetChild(0).gameObject.SetActive(pause);
        if (pause) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
    public void Pause(bool on)
    {
        transform.GetChild(0).gameObject.SetActive(on);
        if (on) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
    private void Start()
    {
        if (!SubMenu)
        {
            float x, y;
            music.GetFloat("volume", out y);
            sound.GetFloat("volume", out x);
            if (!PlayerPrefs.HasKey("music")) PlayerPrefs.SetFloat("music", y);
            else y = PlayerPrefs.GetFloat("music");

            if (!PlayerPrefs.HasKey("sound")) PlayerPrefs.SetFloat("sound", x);
            else x = PlayerPrefs.GetFloat("sound");
            sliderS.value = x;
            sliderM.value = y;
            Music();
            Sound();
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
    public void OpenMenu(int menu)
    {
        for (int i = 0; i < SubMenus.Length; i++)
        {
            SubMenus[i].SetActive(false);
        }
        SubMenus[menu].SetActive(true);
    }
    public void Music()
    {
        float x = sliderM.value;
        PlayerPrefs.SetFloat("music", x);
        x = Mathf.Log10(x) * 20;
        music.SetFloat("volume", x);
        if (x < -50) music.SetFloat("volume", -50);
        PlayerPrefs.Save();
    }
    public void Sound()
    {
        float x = sliderS.value;
        PlayerPrefs.SetFloat("sound", x);
        x = Mathf.Log10(x) * 20;
        sound.SetFloat("volume", x);
        if (x< -50) sound.SetFloat("volume", -50);
        PlayerPrefs.Save();
    }
}
