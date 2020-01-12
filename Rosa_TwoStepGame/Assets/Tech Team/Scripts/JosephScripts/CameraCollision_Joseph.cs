using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision_Joseph : MonoBehaviour
{
    #region Public
    public float MinDistance = 1.0f;
    public float MaxDistance = 4.0f;
    public float Smooth = 10.0f;
    public Vector3 DollyDirAdjusted;
    public float Distance;
    #endregion

    #region Private
    private Vector3 DollyDir;
    #endregion

    void Start()
    {
        DollyDir = transform.localPosition.normalized;
        Distance = transform.localPosition.magnitude;
    }

    void Update()
    {
        Vector3 DesiredCameraPos = transform.parent.TransformPoint(DollyDir * MaxDistance);
        RaycastHit Hit;

        if(Physics.Linecast(transform.parent.position, DesiredCameraPos, out Hit))
        {
            Distance = Mathf.Clamp((Hit.distance * 0.85f), MinDistance, MaxDistance);
        }
        else
        {
            Distance = MaxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, DollyDir * Distance, Time.deltaTime * Smooth);
    }
}
