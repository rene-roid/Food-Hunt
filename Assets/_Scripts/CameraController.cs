using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera")]
    public Transform Target;
    [Range(0, 1)]public float Smoothing;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CameraFollow();
    }

    #region Camera Follow
    private void CameraFollow()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Target.position.x, Target.position.y, transform.position.z), Smoothing);
    }

    #endregion
}
