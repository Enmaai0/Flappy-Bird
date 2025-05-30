using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnXPos = 3f;
    public float speed = 2f;
    public float minY = -1f;
    public float maxY = 1f;
    public float gapsize = 2.5f;
    public float spawnInterval = 2f;
    public List<GameObject> pipes = new List<GameObject>();

    private Coroutine spawnRoutine = null;
    public bool isSpawning = false;

    public void StartSpawn()
    {
        isSpawning = true;
        spawnRoutine = StartCoroutine(SpawnPipes());
    }

    IEnumerator SpawnPipes()
    {
        while (isSpawning)
        {
            SpawnPipe();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnPipe()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(spawnXPos, randomY);

        GameObject pipe = Instantiate(pipePrefab, spawnPos, Quaternion.identity);
        pipe.GetComponent<Pipes>().Initialize(speed);

        pipes.Add(pipe);
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopCoroutine(spawnRoutine);
        spawnRoutine = null;
    }

    public void ClearPipes()
    {
        foreach (GameObject pipe in pipes)
        {
            Destroy(pipe);
        }
        pipes.Clear();
    }

    public void RemovePipe(GameObject pipe)
    {
        if (pipes.Contains(pipe))
        {
            pipes.Remove(pipe);
        }
    }
}
