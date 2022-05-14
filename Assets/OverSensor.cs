using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverSensor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckForBottomBox());
    }

    private IEnumerator CheckForBottomBox()
    {
        Mover mover = gameObject.GetComponentInParent<Mover>();
        yield return new WaitUntil(() => mover.isMoving);
        yield return new WaitForSeconds(0.1f);

        float boxHeight = gameObject.GetComponent<BoxCollider>().size.y * transform.localScale.y;

        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            if (!mover.isMoving)
            {
                break;
            }

            float boxWidth = gameObject.GetComponent<BoxCollider>().size.z * transform.localScale.z;
            float boxLength = gameObject.GetComponent<BoxCollider>().size.x * transform.localScale.x;
            Vector3 sensorPosition = transform.position + transform.forward * boxWidth / 2 + transform.right * boxLength / 2;
            if (!Physics.Raycast(sensorPosition, -Vector3.up, boxHeight))
            {
                mover.StopMoving();
                break;
            }
        }
    }
}
