using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cam;
    public Altimeter altimeter;
    public Spawner spawner;

    public float speed;
    public int minimalAltitudeToFollowe = 6;

    public GameObject startBox;
    private Vector3 initialRelativePos;

    private void Start()
    {
        initialRelativePos = cam.transform.position - spawner.lastSpawnedBox.transform.position;
    }

    private void FixedUpdate()
    {
        if (altimeter.GetLastAltitude() >= minimalAltitudeToFollowe)
        {
            FollowLastBox();
        }
    }

    private void FollowLastBox()
    {
        Vector3 lastBoxPosition = spawner.lastSpawnedBox.transform.position;
        Vector3 actualRelativePosition = cam.transform.position - lastBoxPosition;
        //float correctionCossine =
        //    Vector3.Dot(actualRelativePosition, initialRelativePos) /
        //    (actualRelativePosition.magnitude * initialRelativePos.magnitude);
        Vector3 correctedRelativePosition = Vector3.Project(actualRelativePosition, initialRelativePos);

        Vector3 currentAltitude = startBox.transform.position + altimeter.GetLastAltitude() * Vector3.up;

        Vector3 desiredPosition = currentAltitude + correctedRelativePosition;

        cam.transform.position = Vector3.Lerp(
            cam.transform.position,
            desiredPosition,
            Time.fixedDeltaTime * speed
        );
    }
}
