using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shieldEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject shieldPrefab;
    [SerializeField]
    private GameObject deathDouble;
    [SerializeField]
    private float lives = 10;
    private GameObject shield;
    private float leftBound;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 270, 0);
        if (transform.position.x < 0)
        {
            leftBound = 0.3f;
            transform.localScale = new Vector3(1, 1, -1);
        }
        else
        {
            leftBound = -0.3f;
        }
        shield = Instantiate(shieldPrefab,transform.position + new Vector3(leftBound, 0, 0), Quaternion.identity);
    }
    public void getDamadge(float damadge)
    {
        lives -= damadge;
        if (lives <= 0)
        {
            die();
        }
    }
    void die()
    {
        Instantiate(deathDouble, transform.position, Quaternion.identity);
        Destroy(shield);
        Destroy(this.gameObject);
    }
}
