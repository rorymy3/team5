using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsHandler : MonoBehaviour
{
    [Header("References")]
    public GameObject menuUI;
    public GameObject bar;
    public AudioMixer mixer;
    public GameObject[] buttons;
    public GameObject logo;
    public GameObject settings;
    public GameObject butHolder;
    public GameObject setHolder;

    [Header("Variables")]
    public float tweenTime = 0.2f;
    public static float volumeLevel = 1.0f;

    //Private Extras
    private bool paused;
    private Slider sliderVolumeCtrl;
    private float[] butPositions;
    private float logoScale;
    private Vector3 startScale;

    //Happens at the start of the scene
    void Start()
    {
        butPositions = new float[3];
        for(int i = 0; i < buttons.Length; i++)
            butPositions[i] = buttons[i].transform.position.y + -150f;
        logoScale = logo.transform.localScale.z;
        Resume();
    }

    //Happens when the GameObject is toggled ON
    void Awake (){SetLevel(volumeLevel);}

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused)
                Resume();
            else
                Pause();
        }
    }

    public void SettingsOn()
    {
        LeanTween.scale(logo, Vector2.zero, tweenTime/3f)
        .setEase(LeanTweenType.easeOutQuad)
        .setIgnoreTimeScale(true);

        LeanTween.scale(settings, Vector2.one, tweenTime)
        .setEase(LeanTweenType.easeOutElastic).setDelay(tweenTime/3f)
        .setIgnoreTimeScale(true);

        LeanTween.scale(bar, new Vector3(11.5f,27.61f,1f), tweenTime)
        .setEase(LeanTweenType.easeOutElastic)
        .setIgnoreTimeScale(true);

        setHolder.SetActive(true);
        butHolder.SetActive(false);
    }

    void CloseSettings()
    {
        settings.transform.localScale = Vector3.zero;
        setHolder.SetActive(false);
        butHolder.SetActive(true);
    }

    public void SettingsOff()
    {
        LeanTween.scale(logo, new Vector2(logoScale, logoScale), tweenTime)
        .setEase(LeanTweenType.easeOutElastic).setDelay(tweenTime/3f)
        .setIgnoreTimeScale(true);

        LeanTween.scale(settings, Vector2.zero, tweenTime/3f)
        .setEase(LeanTweenType.easeInQuint)
        .setIgnoreTimeScale(true);

        LeanTween.scale(bar, new Vector3(10f,27.61f,1f), tweenTime)
        .setEase(LeanTweenType.easeOutElastic)
        .setIgnoreTimeScale(true);

        setHolder.SetActive(false);
        butHolder.SetActive(true);
    }

    public void Resume()
    {
        bar.transform.localScale = new Vector3(3f,27.61f,1f);
        for(int i = 0; i < buttons.Length; i++)
        {
            LeanTween.scale(buttons[i], Vector2.zero, tweenTime/10f)
            .setIgnoreTimeScale(true);
        }

        logo.transform.localScale = new Vector2(logoScale/2f,logoScale/2f);

        Time.timeScale = 1f;
        menuUI.SetActive(false);
        paused = false;
    }

    //Pauses the game
    public void Pause()
    {
        CloseSettings();
        LeanTween.scale(logo, new Vector2(logoScale, logoScale), tweenTime/2f).setEase(LeanTweenType.easeOutBounce)
        .setIgnoreTimeScale(true);

        for(int i = 0; i < buttons.Length; i++)
        {
            LeanTween.scale(buttons[i], new Vector2(2.5f, 2.5f), tweenTime)
            .setEase(LeanTweenType.easeOutElastic)
            .setDelay(0.075f*i)
            .setIgnoreTimeScale(true);
        }
        LeanTween.scale(bar, new Vector3(10f,27.61f,1f), tweenTime)
        .setEase(LeanTweenType.easeOutQuart)
        .setIgnoreTimeScale(true);

        Time.timeScale = 0f;
        menuUI.SetActive(true);
        paused = true;
    }

    //Quits the game (both in Unity and when exported)
    public void QuitGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void SetLevel (float sliderValue)
    {
        mixer.SetFloat("Volume", Mathf.Log10 (sliderValue) * 20);
        volumeLevel = sliderValue;
    }
}
