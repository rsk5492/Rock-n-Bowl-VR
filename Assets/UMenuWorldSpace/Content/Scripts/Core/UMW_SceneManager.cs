using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_5_3|| UNITY_5_4_OR_NEWER
using UnityEngine.SceneManagement;
#endif

public class UMW_SceneManager : ScriptableObject
{
    [Header("Scene Manager")]
    public List<UMW_LevelInfo> Levels = new List<UMW_LevelInfo>();
    public bool useProgresiveLevel = true;

    private int UnlockLevel = 0;
    private bool playingLastLevel = false;
    private bool isInitializated = false;

    [HideInInspector] public bool ToPlayMenu = false;
    [HideInInspector] public string PlayerName = string.Empty;

    private static UMW_SceneManager _instance;
    public static UMW_SceneManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = Resources.Load("UMWSceneManager", typeof(UMW_SceneManager)) as UMW_SceneManager;
            }
            return _instance;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void Load()
    {
        UnlockLevel = PlayerPrefs.GetInt(UniqueKey, 0);
        if (!isInitializated)
        {
            isInitializated = true;
            PlayerName = string.Empty;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetCompleteLevel()
    {
        if (playingLastLevel)
        {
           if(UnlockLevel <= Levels.Count - 1)
            {
                UnlockLevel++;
                PlayerPrefs.SetInt(UniqueKey, LastLevel);
                Debug.Log("Completed level: " + (LastLevel + 1));
            }
            else
            {
                Debug.Log("Complete all levels!");
            }
        }
    }

    public void RegisterLoadLevel(int index)
    {
        playingLastLevel = (index == LastLevel);
    }

    /// <summary>
    /// Load next level in list
    /// </summary>
    public void LoadNextLevel()
    {
        if (LastLevel <= Levels.Count - 1)
        {
            LoadLevelUtil(Levels[LastLevel].SceneName);
        }
        else
        {
            Debug.Log("All levels has been complete");
        }
    }

   /*public bool isLastLevel(string sceneName)
    {
        for(int i = 0; Levels.Count; i++)
        {

        }
    }*/

    public void ReturnToMenuPlay()
    {
        ToPlayMenu = true;
        LoadLevelUtil("UMenu");
    }

    private string UniqueKey
    {
        get
        {
            return string.Format("{0}.{1}.{2}", Application.platform.ToString(), Application.companyName, Application.productName);
        }
    }

    public int LastLevel { get { return UnlockLevel; } }

    public static void LoadLevelUtil(string scene)
    {
#if UNITY_5_3 || UNITY_5_4_OR_NEWER
  SceneManager.LoadScene(scene);
#else
        Application.LoadLevel(scene);
#endif
    }
}