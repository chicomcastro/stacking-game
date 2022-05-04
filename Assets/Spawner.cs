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
        Instantiate(box, nextSpawn.Value.position + Vector3.up * altimeter.getNextAltitude(), nextSpawn.Value.rotation);
        nextSpawn = CircularLinkedList.NextOrFirst(nextSpawn);
        yield return null;
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

