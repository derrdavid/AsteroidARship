using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float health;
    public float moveSpeed;
    public float xPosition;
    public float targetZ = -17;
    public GameObject deathDouble;

    // setzen der Ausgangsposition
    void Start()
    {
        gameObject.transform.position = new Vector3(xPosition, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    // Bewege gameObject in z-Achse
    public abstract void Update();
    public abstract void getHit(Vector3 hitPos, float damage);

    public void setHealth(float health)
    {
        this.health = health;
    }
    public void set(float speed, float xPos, float tarZ)
    {
        this.moveSpeed = speed;
        this.xPosition = xPos;
        this.targetZ = tarZ;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(projectileCrash(other));
        }
    }
    private IEnumerator projectileCrash(Collision other)
    {
        Vector3 forceDirection = (other.rigidbody.transform.position - transform.position).normalized;
        other.rigidbody.AddForce(forceDirection * 0.5f, ForceMode.Impulse);
        gameObject.GetComponent<Rigidbody>().
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 0.02f, ForceMode.Impulse);
        yield return new WaitForSeconds(2f);
    }
}
