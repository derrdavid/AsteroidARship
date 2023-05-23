using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float maxObjects = 3f;
    public float targetZ = -17f;
    public float moveSpeed = 7f;
    private float timer = 0f;
    private float spawnInterval = 3f;
    private List<GameObject> objects = new List<GameObject>();

    private void Update()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] == null)
            {
                objects.Remove(objects[i]);
            }
        }
        spawnInterval = Random.Range(1, 3);
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0;
            if (objects.Count < maxObjects)
            {
                GameObject newObject = Instantiate(prefab, transform.position, Quaternion.identity);
                ProjectileMove projectileMove = newObject.GetComponent<ProjectileMove>();
                projectileMove.set(Random.Range(3, this.moveSpeed), Random.Range(-5, 5), this.targetZ);
                objects.Add(newObject);
            }
        }

    }
}
