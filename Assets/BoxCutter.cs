using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCutter : MonoBehaviour
{
    public Vector3[] CutBoxes(GameObject upperBox, GameObject baseBox)
    {
        Vector3[] P = GetBoundaries(upperBox);
        Vector3[] Q = GetBoundaries(baseBox);

        Vector3[] A1 = MountBox(P[3], Q[1]);
        print(A1[0]);
        print(A1[1]);
        print(A1[2]);
        print(A1[3]);
        return A1;
    }

    private Vector3[] GetBoundaries(GameObject box)
    {
        Bounds bounds = box.GetComponent<BoxCollider>().bounds;
        Vector3 center = bounds.center;
        Vector3 P1 = center + bounds.extents.x * Vector3.right;
        Vector3 P2 = center + bounds.extents.z * Vector3.forward;
        Vector3 P3 = center - bounds.extents.x * Vector3.right;
        Vector3 P4 = center - bounds.extents.z * Vector3.forward;
        return new Vector3[] { P1, P2, P3, P4 };
    }

    private Vector3[] MountBox(Vector3 P, Vector3 Q)
    {
        Vector3 center = (P + Q) / 2;
        Vector3 diag = P - Q;
        Vector3 P1 = center + Quaternion.Euler(0, 0, 0) * diag / 2;
        Vector3 P2 = center + Quaternion.Euler(0, 90, 0) * diag / 2;
        Vector3 P3 = center + Quaternion.Euler(0, 180, 0) * diag / 2;
        Vector3 P4 = center + Quaternion.Euler(0, 270, 0) * diag / 2;
        return new Vector3[] { P1, P2, P3, P4 };
    }
}
