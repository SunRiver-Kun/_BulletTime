using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Timer))]
[RequireComponent(typeof(BulletTime))]
public class GLOBAL : GlobalComponent<GLOBAL> 
{
    public static bool isGamePause{get{ return Time.timeScale==0f; }}
    public bool isInUIMenu{ get{ return UI!=null && UI.activeSelf; } }
    [HideInInspector]
    public bool isInUIEditor;

    [SerializeField]
    private Image blackImage;
    [Min(0.01f),SerializeField,Tooltip("逐渐到黑屏的时间")]
    private float blackScreenTime = 3f;
    [Min(0.1f),SerializeField,Tooltip("黑屏变化频率")]
    private float blackScreenFrequency = 0.1f;

    [SerializeField]
    private Image cameraFilter;
    [Range(0f,1f),SerializeField]
    private float filterAlpha = 1f;
    public bool useFilter = true;

    [SerializeField]
    private GameObject UI;

    public void LoadScene(string name)
    {
        Scene scene =  SceneManager.GetSceneByName(name);
        int index = scene!=null?scene.buildIndex:0;
        LoadScene(index);
    }

    public void LoadScene(int index)
    {
        Timer.DoTaskInTime(blackScreenTime,(GameObject inst)=>{
            SceneManager.LoadScene(index);
        },gameObject);

        if(blackImage==null) { return; }
        Timer.DoPeriodTask(blackScreenTime,blackScreenFrequency,(GameObject inst)=>{
            blackImage.color = new Color(blackImage.color.r,blackImage.color.g,blackImage.color.b,blackImage.color.a+blackScreenFrequency/blackScreenTime);
        },gameObject);
    }

    public void LoadSceneImmediately(string name)
    {
        Scene scene =  SceneManager.GetSceneByName(name);
        int index = scene!=null?scene.buildIndex:0;
        LoadSceneImmediately(index);
    }

    public void LoadSceneImmediately(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void GameEnd()
    {
        var scene = SceneManager.GetActiveScene();
        LoadScene(scene.buildIndex);
    }

    public static void GameSaveAndQuit()
    {
        SaveSetting();
        Application.Quit();
    }

    public static void GamePause()
    {
        Time.timeScale = 0f;
    }

    public static void GameStopPause()
    {
        INPUT.ReSetValue();
        Time.timeScale = 1f;
    }

    //Data
    public static void SaveSetting()
    {
        INPUT.instance?.Save();
        AudioVolumeSetting.instance?.Save();
        SceneData.instance?.Save();
        print("Save Data");
    }

    //UI
    private void UI_EnterEscMenu()
    {
        if(UI==null) { return; }
        INPUT.isLocked = true;
        GamePause();
        UI.SetActive(true);
        int count = UI.transform.childCount;
        for (int i = 0; i < count; ++i)
        {
            var v = UI.transform.GetChild(i);
            if(v.tag.Equals(STRING.Tag.Menu))
                v.gameObject.SetActive(v.name.Equals("EscMenu"));
        }
    }

    public void UI_LeaveMenu()
    {
        if(UI==null) { return; }
        INPUT.isLocked = false;
        GameStopPause();
        UI.SetActive(false);
    }

    public void UI_GameSaveAndQuit()
    {
        GameSaveAndQuit();
    }

    public void UI_SetBGMVolume(float volume)
    {
        FindObjectOfType<AudioVolumeSetting>()?.SetBGMVolume(volume);
    }

    public void UI_SetSFXBolume(float volume)
    {
        FindObjectOfType<AudioVolumeSetting>()?.SetSFXVolume(volume);
    }

    private void AddCameraFilter()
    {
        if(cameraFilter==null || !useFilter) { return; }
        Color color = cameraFilter.color;
        color.a = filterAlpha;
        cameraFilter.color = color;
    }

    private void RemoveCameraFilter()
    {
        if(cameraFilter==null) { return; }
        Color color = cameraFilter.color;
        color.a = 0f;
        cameraFilter.color = color;
    }

    void Start() 
    {
        if(cameraFilter!=null)
        {
            BulletTime.EnterBulletTime += AddCameraFilter;
            BulletTime.LeaveBulletTime += RemoveCameraFilter;
        }
        if(UI==null) { Debug.LogWarning("No UI!"); }
    }

    void Update() 
    {
        if(INPUT.esc.value)
        {
            if(!isInUIMenu)
                UI_EnterEscMenu();
        }
    }

    public override void OnDestroy()
    {
        if(cameraFilter!=null) 
        {
            BulletTime.EnterBulletTime -= AddCameraFilter;
            BulletTime.LeaveBulletTime -= RemoveCameraFilter;
        }
        base.OnDestroy();
    }
}
