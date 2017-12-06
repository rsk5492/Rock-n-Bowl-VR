using UnityEngine;

[System.Serializable]
public class UMW_LevelInfo
{
    public string DisplayLevelName;
    public string SceneName;
    [TextArea(2,5)]
    public string Description;
    public Sprite PreviewImage;
}