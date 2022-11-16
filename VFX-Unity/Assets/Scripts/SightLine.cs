/***
 * Author: Betzaida
 * Created: 11-16-2022
 * Modified:
 * Description: controller for navmeshagent
 ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightLine : MonoBehaviour
{
    public Transform EyePoint;
    public string TargetTag = "Player";
    public float FieldOfView = 45f;

    public bool IsTargetSightInLine {get; set;} = false;

    public Vector3 LastKnownSighting { get; set; } = Vector3.zero;

    private SphereCollider ThisCollider;

    private void Awake()
    {
        ThisCollider = GetComponent<SphereCollider>();
        LastKnownSighting =  transform.position;
    }

    private bool TargetInFOV(Transform target)
    {
        Vector3 DirectioToTarget = target.position - EyePoint.position;
        float angle = Vector3.Angle(EyePoint.forward, DirectioToTarget);

        if(angle <= FieldOfView)
        {
            return true;
        }
        return false;
    }

    private bool ClearLineOfSightTarget(Transform target)
    {
        RaycastHit Info;

        Vector3 DirectionToTarget = (target.position - EyePoint.position).normalized;

        if (Physics.Raycast(EyePoint.position, DirectionToTarget, out Info, ThisCollider.radius))
        {
            if (Info.transform.CompareTag(TargetTag))
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateSight(Transform target)
    {
        IsTargetSightInLine = ClearLineOfSightTarget(target) && TargetInFOV(target);
        if (IsTargetSightInLine)
        {
            LastKnownSighting = target.position;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TargetTag))
        {
            UpdateSight(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TargetTag))
        {
            IsTargetSightInLine = false;
        }
    }



}
