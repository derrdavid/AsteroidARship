using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTweaking : Projectile
{
    // setzen der Ausgangsposition
    void Start()
    {
        setHealth(5f);
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
        if (health <= 0)
        {
            Instantiate(deathDouble, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
