using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject prefab;
    [Range(0.0f, 50.0f)] public float spawnIntervalMin;
    [Range(0.0f, 50.0f)] public float spawnIntervalMax;
    public float maxObjects = 3f;
    public float moveSpeed = 7f;
    private float timer = 0f;
    private List<GameObject> objects = new List<GameObject>();
    [SerializeField]
    private GameObject deadEnemyLists;

    private void Update()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] == null)
            {
                objects.Remove(objects[i]);
            }
        }
        float intervalValue = Random.Range(spawnIntervalMin, spawnIntervalMax);
        timer += Time.deltaTime;
        if (timer >= intervalValue)
        {
            timer = 0;
            if (objects.Count < maxObjects)
            {
                GameObject newObject = Instantiate(prefab, transform.position, Quaternion.identity);
                Projectile projectileMove = newObject.GetComponent<Projectile>();
                projectileMove.set(Random.Range(this.moveSpeed - 3, this.moveSpeed), Random.Range(-5, 5), -17);
                objects.Add(newObject);
            }
        }

    }
}
