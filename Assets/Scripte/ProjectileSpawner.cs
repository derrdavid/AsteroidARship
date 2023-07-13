using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using GD.MinMaxSlider;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField][MinMaxSlider(0.0f, 20.0f)] private Vector2 randomSpawnInterval;
    [SerializeField][MinMaxSlider(-5.0f, 5.0f)] private Vector2 randomYInterval;
    [SerializeField][Range(0, 20)] private int maxObjects;
    [SerializeField][Range(0, 20)] private float moveSpeed;
    [SerializeField] private float startSpeed = 5f;
    [SerializeField] private float targetZ = 1;
    [SerializeField][MinMaxSlider(-10, 10)] private Vector2 floatingIntensityX;
    [SerializeField][MinMaxSlider(-10, 10)] private Vector2 floatingIntensityY;
    private float timer = 0f;
    private List<GameObject> livingEnemies = new List<GameObject>();
    private bool active;
    private void Update()
    {
        if (active)
        {
            for (int i = 0; i < livingEnemies.Count; i++)
            {
                if (livingEnemies[i] == null)
                {
                    livingEnemies.Remove(livingEnemies[i]);
                }
            }
            float intervalValue = Random.Range(randomSpawnInterval.x, randomSpawnInterval.y);
            timer += Time.deltaTime;
            if (timer >= intervalValue)
            {
                timer = 0;
                if (livingEnemies.Count < maxObjects)
                {
                    float randomX = Random.Range(-5, 5);
                    float randomZ = Random.Range(40, 50);
                    float randomY = Random.Range(randomYInterval.x, randomYInterval.y);

                    Vector3 endPosition = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);
                    GameObject newObject = Instantiate(prefab, new Vector3(endPosition.x, endPosition.y + randomY, endPosition.z + randomZ), Quaternion.identity);

                    Projectile projectileMove = newObject.GetComponent<Projectile>();
                    livingEnemies.Add(newObject);

                    iTween.MoveTo(newObject, iTween.Hash(
                        "position", endPosition,
                        "speed", startSpeed,
                        "easetype", iTween.EaseType.easeInCirc,
                        "oncomplete", "OnMovementComplete",
                        "oncompletetarget", gameObject,
                        "oncompleteparams", newObject
                    ));

                    // Füge den "floating" Effekt hinzu
                    iTween.MoveBy(newObject, iTween.Hash(
                        "amount", new Vector3(Random.Range(floatingIntensityX.x, floatingIntensityX.y), Random.Range(floatingIntensityY.x, floatingIntensityY.y), 0f),
                        "time", moveSpeed,
                        "easetype", iTween.EaseType.easeInOutQuad,
                        "looptype", iTween.LoopType.pingPong
                    ));
                    projectileMove.set(Random.Range(moveSpeed, moveSpeed + 10), randomX, targetZ);
                }
            }
        }
    }

    private void OnMovementComplete(GameObject newObject)
    {
        if (newObject != null)
        {
            newObject.GetComponent<Projectile>().setInvincible(false);
        }
    }
    public void isActive(bool active)
    {
        this.active = active;
    }
}