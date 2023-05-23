using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private GameObject deathDouble;
    public void getHit()
    {
        Instantiate(deathDouble, transform.position, Quaternion.identity);
        
        Debug.Log("aua");
        Destroy(this.gameObject);
    }

}
