using UnityEngine;

public class UMW_ApplySettings : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        Load();
    }

    /// <summary>
    /// 
    /// </summary>
    void Load()
    {
        QualitySettings.antiAliasing = PlayerPrefs.GetInt(UMW_Keys.AntiAliasign, 2);
        int at = PlayerPrefs.GetInt(UMW_Keys.AnisoTropic, 2);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt(UMW_Keys.Quality, 2));
        QualitySettings.masterTextureLimit = PlayerPrefs.GetInt(UMW_Keys.TextureResolution, 0);
        AudioListener.pause = (PlayerPrefs.GetInt(UMW_Keys.AudioEnable, 1) == 1) ? false : true;
        AudioListener.volume = PlayerPrefs.GetFloat(UMW_Keys.Volume, 1);

        switch (at)
        {
            case 0:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                break;
            case 1:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
                break;
            case 2:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                break;
        }
    }
}