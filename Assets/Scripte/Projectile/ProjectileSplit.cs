using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSplit : Projectile
{
    public GameObject enemyStandard;
    public float childAmount;
    // setzen der Ausgangsposition
    void Start()
    {
        setHealth(10f);

        gameObject.transform.position = new Vector3(xPosition, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    // Bewege gameObject in z-Achse
    public override void Update()
    {
        Vector3 newPosition = new Vector3(xPosition, gameObject.transform.position.y, targetZ);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, newPosition, moveSpeed * Time.deltaTime);
        iTween.RotateBy(gameObject, new Vector3(0, 100, 30), 600);
        if (gameObject.transform.position.z <= targetZ)
        {
            GameObject.Find("AtriumManager").GetComponent<AtriumManager>().takeDamage(10);
            Destroy(gameObject);
        }
    }
    public override void getHit(float damage)
    {
        health -= damage;
        for (int i = 0; i < childAmount; i++)
        {
            float randomX = Random.Range(-2, 2);
            float randomY = Random.Range(-2, 2);
            Vector3 spawnPosition = new Vector3(randomX, 0f, randomY) + transform.position;
            Quaternion spawnRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            GameObject newObj = Instantiate(enemyStandard, spawnPosition, spawnRotation);
            newObj.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
            newObj.GetComponent<Projectile>().set(2.0f, newObj.gameObject.transform.position.x, -17);
        }
        if (health <= 0)
        {
            Instantiate(deathDouble, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}


