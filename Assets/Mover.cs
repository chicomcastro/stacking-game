using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float startSpeed = 5f;

    public GameObject frontBox;
    public GameObject backBox;

    Vector3 originalSize;

    void Start()
    {
        originalSize = backBox.transform.localScale;
        StartCoroutine("StartMoving");
    }

    private void Update()
    {
        bool reachedEnd = ReachEnd();
        if (Input.GetKeyDown(KeyCode.Mouse0) || reachedEnd)
        {
            StopMoving();
        }
        if (reachedEnd && backBox != null)
        {
            Destroy(backBox);
            // TODO: game over
        }
    }

    private bool ReachEnd()
    {
        if (backBox == null)
        {
            return true;
        }
        return backBox.transform.localScale.z <= 0.01f;
    }

    private void Translade(GameObject box)
    {
        box.transform.position += this.transform.forward * startSpeed * Time.deltaTime;
    }

    private IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            Grow(frontBox);
            Shrink(backBox);
            Translade(frontBox);
            Translade(backBox);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void Grow(GameObject box)
    {
        box.transform.localScale = new Vector3(
            box.transform.localScale.x,
            box.transform.localScale.y,
            Mathf.Min(box.transform.localScale.z + 2 * startSpeed * Time.deltaTime, originalSize.z)
        );
    }

    private void Shrink(GameObject box)
    {
        box.transform.localScale = new Vector3(
            box.transform.localScale.x,
            box.transform.localScale.y,
            Mathf.Max(box.transform.localScale.z - 2 * startSpeed * Time.deltaTime, 0.01f)
        );
    }

    private void StopMoving()
    {
        StopCoroutine("StartMoving");
        if (backBox != null)
        {
            frontBox.GetComponent<Rigidbody>().isKinematic = true;
            Rigidbody rigidbody = backBox.GetComponent<Rigidbody>();
            rigidbody.useGravity = true;
            rigidbody.constraints = RigidbodyConstraints.None;
            Destroy(backBox, 2.0f);
        }
    }
}
