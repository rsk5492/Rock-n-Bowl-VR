using UnityEngine;
using System.Collections.Generic;
using System.Collections;
#if UNITY_5_3|| UNITY_5_4_OR_NEWER
using UnityEngine.SceneManagement;
#endif

public class UMW_Manager : MonoBehaviour
{
   /* [Header("Scene Manager")]
    public List<UMW_LevelInfo> Levels = new List<UMW_LevelInfo>();*/

    [Header("Windows Info")]
    public List<UMW_WindowInfo> Windows = new List<UMW_WindowInfo>();

    [Header("Settings")]
    public bool RequieredName = true;
    public bool useRainEffect = true;
    public string PlayerNameWindow = "PlayerName";

    [Header("References")]
    [SerializeField]private GameObject LevelPrefab;
    [SerializeField]private GameObject RainEffect;

    private UMW_Camera m_Camera;
    private UMW_UIReferences UIReference;
    public const string PERSIS_STATE_KEY = "uwm.p.state";
    private UMW_SceneManager m_SceneManager;


    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        UIReference = FindObjectOfType<UMW_UIReferences>();
        m_Camera = FindObjectOfType<UMW_Camera>();
        m_SceneManager = UMW_SceneManager.Instance;
        if (m_SceneManager) { m_SceneManager.Load(); }
        InstantiateLevels();

        if (RequieredName && string.IsNullOrEmpty(m_SceneManager.PlayerName) && !m_SceneManager.ToPlayMenu)
        {
            GoToWindow(PlayerNameWindow);
        }
        else if (!string.IsNullOrEmpty(m_SceneManager.PlayerName) && !m_SceneManager.ToPlayMenu)
        {
            GoToWindow("MainMenu");
        }
        else if(m_SceneManager.ToPlayMenu)
        {
            GoToWindow("SelectLevel");
            Invoke("RetartStart", 2);
        }
        else
        {
            GoToWindow("MainMenu");
        }

        if(RainEffect != null) { RainEffect.SetActive(useRainEffect); }
    }

    void RetartStart() { m_SceneManager.ToPlayMenu = false; }

    /// <summary>
    /// 
    /// </summary>
    void InstantiateLevels()
    {
        Transform panel = UIReference.LevelPanel;
        for(int i = 0; i < m_SceneManager.Levels.Count; i++)
        {
            GameObject l = Instantiate(LevelPrefab) as GameObject;
            l.GetComponent<UMW_LevelUI>().Init(m_SceneManager.Levels[i], i);
            l.transform.SetParent(panel, false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="windowName"></param>
    public void GoToWindow(string windowName)
    {
        if (GetWindow(windowName) != null)
        {
            m_Camera.SetPosition(GetWindow(windowName).Position);
        }
        else
        {
            Debug.Log("Window: " + windowName + " not exist!");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private UMW_WindowInfo GetWindow(string windowName)
    {
        for(int i= 0; i < Windows.Count; i++)
        {
            if(Windows[i].Name == windowName)
            {
                return Windows[i];
            }
        }
        Debug.LogWarning("Window with this name: " + windowName + " doesn't exist on the list.");
        return Windows[0];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    public void SetCameraTo(Vector3 p)
    {
        if(m_Camera == null)
        {
            m_Camera = FindObjectOfType<UMW_Camera>();
        }
        m_Camera.transform.position = p;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="level"></param>
    public void LoadLevel(string level)
    {
        StartCoroutine(LoadLevelIE(level));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    IEnumerator LoadLevelIE(string level)
    {
        CanvasGroup cg = UIReference.FadeAlpha;
        cg.alpha = 0;
        while (cg.alpha < 1)
        {
            cg.alpha += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
#if UNITY_5_3 || UNITY_5_4_OR_NEWER
        SceneManager.LoadScene(level);
#else
        Application.LoadLevelAsync(level);
#endif
    }

    /// <summary>
    /// 
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        if (Windows.Count > 1)
        {
            for (int i = 0; i < Windows.Count; i++)
            {
                for(int e = 0; e < Windows.Count; e++)
                {
                    if( e < Windows.Count - 1)
                    Gizmos.DrawLine(Windows[i].Position, Windows[e].Position);
                }
                Camera c = FindObjectOfType<UMW_Camera>().UICamera;
                if (c != null)
                {
                    Matrix4x4 temp = Gizmos.matrix;
                    Gizmos.matrix = Matrix4x4.TRS(Windows[i].Position, c.transform.rotation, Vector3.one);
                    Gizmos.DrawFrustum(Vector3.zero, c.fieldOfView, c.farClipPlane, c.nearClipPlane, c.aspect);
                    Gizmos.matrix = temp;
                }
            }
        }
    }

    public List<UMW_LevelInfo> Levels
    {
        get
        {
            return UMW_SceneManager.Instance.Levels;
        }
    }
}