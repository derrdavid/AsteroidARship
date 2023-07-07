using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [Range(0.0f, 50.0f)] public float spawnIntervalMin;
    [Range(0.0f, 50.0f)] public float spawnIntervalMax;
    [SerializeField] private float maxObjects = 3f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameObject deadEnemyLists;
    private float timer = 0f;
    private float spawnTime;
    private List<GameObject> livingEnemies = new List<GameObject>();

    private void Update()
    {
        for (int i = 0; i < livingEnemies.Count; i++)
        {
            if (livingEnemies[i] == null)
            {
                livingEnemies.Remove(livingEnemies[i]);
            }
        }
        float intervalValue = Random.Range(spawnIntervalMin, spawnIntervalMax);
        timer += Time.deltaTime;
        if (timer >= intervalValue)
        {
            timer = 0;
            if (livingEnemies.Count < maxObjects)
            {
                float randomX = Random.Range(-5, 5);
                float randomZ = Random.Range(20, 30);
                float randomY = Random.Range(3, 6);
                Vector3 endPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
                GameObject newObject = Instantiate(prefab, new Vector3(endPosition.x, endPosition.y + randomY, endPosition.z + randomZ), Quaternion.identity);
                Projectile projectileMove = newObject.GetComponent<Projectile>();
                livingEnemies.Add(newObject);

                iTween.MoveTo(newObject, iTween.Hash(
                    "position", endPosition,
                    "speed", 15f,
                    "easetype", iTween.EaseType.easeInOutCirc,
                    "oncomplete", "OnMovementComplete",
                    "oncompletetarget", gameObject,
                    "oncompleteparams", newObject
                ));
                projectileMove.set(Random.Range(this.moveSpeed - 3, this.moveSpeed), randomX, -17);
            }
        }

    }
    private void OnMovementComplete(GameObject newObject)
    {
        if (newObject != null)
        {
            newObject.GetComponent<ProjectileSettings>().setInvincible(false);
        }
    }
}
