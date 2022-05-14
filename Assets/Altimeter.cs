using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altimeter : MonoBehaviour
{
    public GameObject box;

    private float lastAltitude;
    private float boxHeigth;

    private void Awake()
    {
        boxHeigth = box.GetComponent<BoxCollider>().size.y;
        lastAltitude = 0f;
    }

    public float getNextAltitude()
    {
        lastAltitude += boxHeigth;
        return lastAltitude;
    }

    public float getLastAltitude()
    {
        return lastAltitude;
    }
}
