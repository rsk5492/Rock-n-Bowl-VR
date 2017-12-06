using UnityEngine;
using UnityEngine.UI;


public class UMW_LevelUI : MonoBehaviour
{
    [SerializeField]private Image ImagePreview;
    [SerializeField]private Text TitleText;
    [SerializeField]private Text DescriptionText;
    [SerializeField]private Animator Anim;
    [SerializeField] private GameObject LockUI;

    private UMW_LevelInfo cacheInfo;
    private UMW_UIReferences UIReference;
    private int cacheIndex = 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    public void Init(UMW_LevelInfo info, int index)
    {
        UIReference = FindObjectOfType<UMW_UIReferences>();
        cacheInfo = info;
        cacheIndex = index;
        ImagePreview.sprite = info.PreviewImage;
        TitleText.text = info.DisplayLevelName;
        DescriptionText.text = info.Description;
        if (UMW_SceneManager.Instance.useProgresiveLevel)
        {
            bool isLock = index > UMW_SceneManager.Instance.LastLevel;
            LockUI.SetActive(isLock);
        }
        else
        {
            LockUI.SetActive(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void LoadLevel()
    {
        UIReference.SetSelectLevel(cacheInfo, cacheIndex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enter"></param>
    public void OnMouseEvent(bool enter)
    {
        if (Anim == null)
            return;

        Anim.SetBool("show", enter);
    }

}