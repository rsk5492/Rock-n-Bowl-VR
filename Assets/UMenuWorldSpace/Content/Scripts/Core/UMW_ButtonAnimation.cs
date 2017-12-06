using UnityEngine;
using System.Collections;

public class UMW_ButtonAnimation : MonoBehaviour
{
    [SerializeField]private string Parameter = "state";

    private Animator m_anim = null;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        m_anim = GetComponent<Animator>();
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetNormal()
    {
        m_anim.SetInteger(Parameter, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetEnter()
    {
        m_anim.SetInteger(Parameter, 1);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetClick()
    {
        CancelInvoke();
        m_anim.SetInteger(Parameter, 2);
        Invoke("Reset", 0.5f);
    }

    /// <summary>
    /// 
    /// </summary>
    void Reset()
    {
        m_anim.SetInteger(Parameter, 0);
    }
}