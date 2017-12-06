using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UMW_UIReferences : MonoBehaviour
{
    public Text QualityText;
    public Text AntiAliasignText;
    public Text AnisoText;
    public Text TextureResText;
    public Text AudioEnableText;
    public Text PlayerNameText;
    public Text ErrorPlayerNameText;
    [SerializeField]private Text TitleLevelText;
    [SerializeField]private Text DescriptionLevelText;
    [SerializeField]private Image LevelPreviewImage;

    public Slider VolumeSlider;
    public InputField PlayerNameInput;
    public CanvasGroup FadeAlpha;
    public RectTransform LevelPanel;
    [SerializeField]private GameObject PlayButton;

    private int CurrentQuality = 0;
    private int CurrentAS = 0;
    private int CurrentAA = 0;
    private string[] AAOptions = new string[] { "X0", "X2", "X4", "X8" };
    private string[] TROptions = new string[] { "Full Res", "Half Res", "Quarter Res", "Eighth Res",};
    private int CurrentTR = 3;
    private bool isAudioEnable = true;
    private UMW_Manager Manager;
    private UMW_LevelInfo cacheLevelInfo;
    private int cacheIndex = 0;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        Manager = FindObjectOfType<UMW_Manager>();
        LoadSettings();
        StartCoroutine(Fade(true));
        PlayButton.SetActive(false);
        if(Manager.Levels.Count > 0) { SetSelectLevel(Manager.Levels[0], 0); }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        if (!string.IsNullOrEmpty(UMW_SceneManager.Instance.PlayerName))
        {
            Manager.GoToWindow("MainMenu");//changes
            PlayerNameInput.text = UMW_SceneManager.Instance.PlayerName;
            SetPlayerName();
        }

    }

    /// <summary>
    /// 
    /// </summary>
    void LoadSettings()
    {
        ErrorPlayerNameText.canvasRenderer.SetAlpha(0);
        CurrentAA = PlayerPrefs.GetInt(UMW_Keys.AntiAliasign, 2);
        CurrentAS = PlayerPrefs.GetInt(UMW_Keys.AnisoTropic, 2);
        CurrentQuality = PlayerPrefs.GetInt(UMW_Keys.Quality, 2);
        CurrentTR = PlayerPrefs.GetInt(UMW_Keys.TextureResolution, 0);
        isAudioEnable = (PlayerPrefs.GetInt(UMW_Keys.AudioEnable, 1) == 1) ? true : false;
        VolumeSlider.value = PlayerPrefs.GetFloat(UMW_Keys.Volume, 1);

        QualityText.text = QualitySettings.names[CurrentQuality];
        AntiAliasignText.text = AAOptions[CurrentAA];
        TextureResText.text = TROptions[CurrentTR];
        AudioEnableText.text = (isAudioEnable) ? "Enable" : "Disable";

        switch (CurrentAS)
        {
            case 0:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                AnisoText.text = AnisotropicFiltering.Disable.ToString();
                break;
            case 1:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
                AnisoText.text = AnisotropicFiltering.Enable.ToString();
                break;
            case 2:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                AnisoText.text = AnisotropicFiltering.ForceEnable.ToString();
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetSelectLevel(UMW_LevelInfo info, int ndx)
    {
        cacheLevelInfo = info;
        cacheIndex = ndx;
        LevelPreviewImage.sprite = info.PreviewImage;
        TitleLevelText.text = info.DisplayLevelName;
        DescriptionLevelText.text = info.Description;
        PlayButton.SetActive(true);
        PlayButton.GetComponent<Animator>().Play("select", 0, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="v"></param>
    public void Volume(float v)
    {
        AudioListener.volume = v;
        PlayerPrefs.SetFloat(UMW_Keys.Volume, v);
    }

    /// <summary>
    /// 
    /// </summary>
    public void LoadLevel()
    {
        UMW_SceneManager.Instance.RegisterLoadLevel(cacheIndex);
        Manager.LoadLevel(cacheLevelInfo.SceneName);
    }

    /// <summary>
    /// 
    /// </summary>
    public void ChangeTextureRes(bool b)
    {
        if (b)
        {
            CurrentTR = (CurrentTR + 1) % 4;
        }
        else
        {
            if (CurrentTR != 0)
            {
                CurrentTR = (CurrentTR - 1) % 3;
            }
            else
            {
                CurrentTR = 3;
            }
        }
        QualitySettings.masterTextureLimit = CurrentTR;
        PlayerPrefs.SetInt(UMW_Keys.TextureResolution, CurrentTR);
        TextureResText.text = TROptions[CurrentTR];
    }

    /// <summary>
    /// 
    /// </summary>
    public void ChangeQuality(bool mas)
    {
        if (mas)
        {
            CurrentQuality = (CurrentQuality + 1) % QualitySettings.names.Length;
        }
        else
        {
            if (CurrentQuality != 0)
            {
                CurrentQuality = (CurrentQuality - 1) % QualitySettings.names.Length;
            }
            else
            {
                CurrentQuality = (QualitySettings.names.Length - 1);
            }
        }
        QualityText.text = QualitySettings.names[CurrentQuality];
        QualitySettings.SetQualityLevel(CurrentQuality,true);
        PlayerPrefs.SetInt(UMW_Keys.Quality, CurrentQuality);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    public void ChangeAnisotropic(bool b)
    {
        if (b) { CurrentAS = (CurrentAS + 1) % 3; }
        else
        {
            if (CurrentAS != 0) { CurrentAS = (CurrentAS - 1) % 3; }
            else { CurrentAS = 2; }
        }

        switch (CurrentAS)
        {
            case 0:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                AnisoText.text = AnisotropicFiltering.Disable.ToString();
                break;
            case 1:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
                AnisoText.text = AnisotropicFiltering.Enable.ToString();
                break;
            case 2:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                AnisoText.text = AnisotropicFiltering.ForceEnable.ToString();
                break;
        }
        PlayerPrefs.SetInt(UMW_Keys.AnisoTropic, CurrentAS);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    public void ChangeAntiAliasing(bool b)
    {
        CurrentAA = (b) ? (CurrentAA + 1) % 4 : (CurrentAA != 0) ? (CurrentAA - 1) % 4 : 3;
        AntiAliasignText.text = AAOptions[CurrentAA];
        switch (CurrentAA)
        {
            case 0:
                QualitySettings.antiAliasing = 0;
                break;
            case 1:
                QualitySettings.antiAliasing = 2;
                break;
            case 2:
                QualitySettings.antiAliasing = 4;
                break;
            case 3:
                QualitySettings.antiAliasing = 8;
                break;
        }
        PlayerPrefs.SetInt(UMW_Keys.AntiAliasign, CurrentAA);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    public void AudioEnable(bool b)
    {
        isAudioEnable = b;
        AudioListener.pause = !b;
        AudioEnableText.text = (isAudioEnable) ? "Enable" : "Disable";
        int ian = (isAudioEnable) ? 1 : 0;
        PlayerPrefs.SetInt(UMW_Keys.AudioEnable, ian);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="field"></param>
    public void SetPlayerName()
    {
        if (string.IsNullOrEmpty(PlayerNameInput.text))
        {
            ErrorPlayerNameText.CrossFadeAlpha(1, 0.7f, true);
            return;
        }

        ErrorPlayerNameText.CrossFadeAlpha(0, 0.5f, true);
        PlayerPrefs.SetString(UMW_Keys.PlayerName, PlayerNameInput.text);
        UMW_SceneManager.Instance.PlayerName = PlayerNameInput.text;
#if UNITY_5_2 || UNITY_5_3 || UNITY_5_4_OR_NEWER
        int nameSize = 10;
#else
        int nameSize = 40;
#endif
        PlayerNameText.text = string.Format("PLAYER NAME: <size=" + nameSize + "><color=#00ffd8>{0}</color></size>", PlayerNameInput.text);
        if (!UMW_SceneManager.Instance.ToPlayMenu)
        {
            Manager.GoToWindow("MainMenu");
        }
        else
        {
            Manager.GoToWindow("SelectLevel");
        }
        PlayerPrefs.SetInt(UMW_Manager.PERSIS_STATE_KEY, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR
     UnityEditor.EditorApplication.isPlaying = false;
#else
     Application.Quit();
#endif
    }

    /// <summary>
    /// 
    /// </summary>
    IEnumerator Fade(bool FadeOut)
    {
        if (FadeAlpha == null)
            yield break;

        if (FadeOut)
        {
            FadeAlpha.alpha = 1;
            while(FadeAlpha.alpha > 0)
            {
                FadeAlpha.alpha -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            FadeAlpha.alpha = 0;
            while (FadeAlpha.alpha < 1)
            {
                FadeAlpha.alpha += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }


}