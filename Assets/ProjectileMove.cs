using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float moveSpeed;
    public float xPosition;
    public float targetZ;

    // setzen der Ausgangsposition
    void Start()
    {
        gameObject.transform.position = new Vector3(xPosition, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    // Bewege gameObject in z-Achse
    void Update()
    {
        Vector3 newPosition = new Vector3(xPosition, gameObject.transform.position.y, targetZ);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, newPosition, moveSpeed * Time.deltaTime);
        if (gameObject.transform.position.z <= targetZ)
        {
            GameObject.Find("AtriumManager").GetComponent<AtriumManager>().takeDamage(10);
            Destroy(gameObject);
        }
    }
    public void set(float speed, float xPos, float tarZ)
    {
        this.moveSpeed = speed;
        this.xPosition = xPos;
        this.targetZ = tarZ;
    }
}
