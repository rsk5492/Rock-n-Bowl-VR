using UnityEngine;
using System.Collections;

public class UMW_Example : MonoBehaviour
{

    public void SetAsComplete()
    {
        UMW_SceneManager.Instance.SetCompleteLevel();
    }

    public void ReturnMenu()
    {
        UMW_SceneManager.Instance.ReturnToMenuPlay();
    }
}