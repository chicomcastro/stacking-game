using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawns;
    public float spawnPeriod;
    public float startPause;

    public GameObject box;
    public Altimeter altimeter;

    private LinkedList<Transform> spawnList = new LinkedList<Transform>();
    private LinkedListNode<Transform> nextSpawn;

    public GameObject lastSpawnedBox;

    private void Start()
    {
        spawns.ToList().ForEach(spawn => spawnList.AddFirst(spawn));
        nextSpawn = spawnList.First;
        StartCoroutine(SpawnBox());
    }

    private void Update()
    {
        // TODO: check for game over
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(SpawnBox());
        }
    }

    private IEnumerator SpawnBox()
    {
        // Get next spawn position
        Vector3 altitudeCorrection = Vector3.up * altimeter.getNextAltitude();
        Vector3 spawnPosition = nextSpawn.Value.position + altitudeCorrection;

        // Spawn new box
        GameObject newSpawnedBox = Instantiate(box, spawnPosition, nextSpawn.Value.rotation);

        BoxCollider[] lastSubBoxes = lastSpawnedBox.GetComponentsInChildren<BoxCollider>();
        if (lastSubBoxes.Length == 2)
        {
            GameObject referenceBox = lastSubBoxes[1].gameObject;
            BoxCollider[] newSubBoxes = newSpawnedBox.GetComponentsInChildren<BoxCollider>();
            GameObject[] boxesToCorrect = newSubBoxes.Select(subBox => subBox.gameObject).ToArray();
            CorrectXScale(boxesToCorrect, referenceBox);

            // Correct x start position
            BoxCollider[] originalSubBoxes = box.GetComponentsInChildren<BoxCollider>();
            BoxCollider originalBox = originalSubBoxes[0];
            float originalXSize = originalBox.size.x * originalBox.transform.localScale.x;
            float actualXSize = newSubBoxes[0].size.x * newSubBoxes[0].transform.localScale.x;
            Vector3 latitudeCorrection = nextSpawn.Value.right * originalXSize / 2 - nextSpawn.Value.right * actualXSize / 2;
            Vector3 newSpawnPosition = spawnPosition + latitudeCorrection;
            newSpawnedBox.transform.position = newSpawnPosition;
        }


        // Set lastBox as newBox
        lastSpawnedBox = newSpawnedBox;
        nextSpawn = CircularLinkedList.NextOrFirst(nextSpawn);
        yield return null;
    }

    private void CorrectXScale(GameObject[] boxesToCorrect, GameObject referenceBox)
    {
        float newZScale = referenceBox.transform.localScale.z;
        boxesToCorrect.ToList().ForEach(newSubBox =>
        {
            Vector3 newScale = new Vector3(
                newZScale,
                newSubBox.transform.localScale.y,
                newSubBox.transform.localScale.z
            );
            newSubBox.transform.localScale = newScale;
        });
    }
}

static class CircularLinkedList
{
    public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current)
    {
        return current.Next ?? current.List.First;
    }

    public static LinkedListNode<T> PreviousOrLast<T>(this LinkedListNode<T> current)
    {
        return current.Previous ?? current.List.Last;
    }
}

