using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoIcons : MonoBehaviour
{
    public string IconPath;

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.DrawIcon(transform.position, IconPath, true);
    }


}
