using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCutter : MonoBehaviour
{
    public GameObject boxPrefab;
    public GameObject upperBox;
    public GameObject baseBox;

    private void Start()
    {
        CutBoxes(upperBox, baseBox);
    }

    public Vector3[] CutBoxes(GameObject upperBox, GameObject baseBox)
    {
        Vector3[] P = GetBoundaries(baseBox);
        Vector3[] Q = GetBoundaries(upperBox);

        Vector3[] R = MountBox(P[0], P[1], Q[1], Q[2]);
        InstantiateBox(R, upperBox);

        print(R[0]);
        print(R[1]);
        print(R[2]);
        print(R[3]);

        return R;
    }

    private Vector3[] GetBoundaries(GameObject box)
    {
        Bounds bounds = box.GetComponent<BoxCollider>().bounds;
        Vector3 center = bounds.center;

        Vector3 x = bounds.extents.x * transform.right;
        Vector3 z = bounds.extents.z * transform.forward;

        Vector3 diag1 = z + x;
        Vector3 diag2 = z - x;

        Vector3 P1 = center + diag1;
        Vector3 P2 = center + diag2;
        Vector3 P3 = center - diag1;
        Vector3 P4 = center - diag2;

        return new Vector3[] { P1, P2, P3, P4 };
    }

    private Vector3[] MountBox(Vector3 P1, Vector3 P2, Vector3 P3, Vector3 P4)
    {
        Vector3 center = (P1 + P3) / 2;
        Vector3 diag1 = (P1 - P3) / 2;
        Vector3 diag2 = (P2 - P4) / 2;

        Vector3 Q1 = center + diag1;
        Vector3 Q2 = center + diag2;
        Vector3 Q3 = center - diag1;
        Vector3 Q4 = center - diag2;

        return new Vector3[] { Q1, Q2, Q3, Q4 };
    }

    private void InstantiateBox(Vector3[] box, GameObject refBox)
    {
        print(box[0]);
        Vector3 boxCenter = (box[0] + box[2]) / 2;
        GameObject gamo = Instantiate(boxPrefab, boxCenter, refBox.transform.rotation);

        Vector3 desiredSize = new Vector3((box[0] - box[1]).magnitude, 1, (box[0] - box[3]).magnitude);
        gamo.transform.localScale = new Vector3(
            gamo.GetComponent<BoxCollider>().size.x / desiredSize.x * gamo.transform.localScale.x,
            gamo.GetComponent<BoxCollider>().size.y / desiredSize.y * gamo.transform.localScale.y,
            gamo.GetComponent<BoxCollider>().size.z / desiredSize.z * gamo.transform.localScale.z
        );
    }
}
