using UnityEngine;
using System.Collections;

public class UMW_Camera : MonoBehaviour {

    [Range(0.01f, 15)]public float Speed = 5;

    [Header("References")]
    public Camera UICamera;
    [SerializeField]private AudioClip MoveSound;

    private Vector3 nextPosition;
    private Vector3 currentPosition;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        currentPosition = transform.position;
        nextPosition = transform.position;
        StartCoroutine(OnUpdate());
    }

    /// <summary>
    /// 
    /// </summary>
    IEnumerator OnUpdate()
    {
        while (true)
        {
            float _speed = Speed / 10;
            currentPosition = Vector3.Lerp(currentPosition, nextPosition, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, _speed)));
            transform.position = currentPosition;
            yield return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos"></param>
    public void SetPosition(Vector3 pos)
    {
        nextPosition = pos;
        if(MoveSound != null)
        {
            GetComponent<AudioSource>().clip = MoveSound;
            GetComponent<AudioSource>().Play();
        }
    }

    public  Vector3 Step(Vector3 current, Vector3 target, float omega)
    {
        return Vector3.Lerp(target, current, Mathf.Exp(-omega * Time.deltaTime));
    }

    /// <summary>
    /// 
    /// </summary>
    void OnDrawGizmos()
    {
        if(UICamera != null)
        {
            Camera c = UICamera;
            Gizmos.color = Color.cyan;
            Matrix4x4 temp = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(c.transform.position, c.transform.rotation, Vector3.one);
            Gizmos.DrawFrustum(Vector3.zero, c.fieldOfView, c.farClipPlane, c.nearClipPlane, c.aspect);
            Gizmos.matrix = temp;
        }
    }
}