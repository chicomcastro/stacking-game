using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float startSpeed = 5f;

    void Start()
    {
        StartMoving();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StopMoving();
        }
    }

    private void StartMoving()
    {
        this.gameObject.GetComponent<Rigidbody>().velocity = this.transform.forward * startSpeed;
    }

    private void StopMoving()
    {
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
