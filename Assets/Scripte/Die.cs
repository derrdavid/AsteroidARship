using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Random = UnityEngine.Random;

public class Die : MonoBehaviour
{
    [SerializeField]
    private bool gravity = false;
    [SerializeField]
    private int explosionForce = 3;
    [SerializeField]
    private DEAD_ENEMY_TYPE type;
    private GameObject prefab;
    private void Start()
    {
        prefab = GameObject.Find("DeadEnemys");

        ArrayList slices = new ArrayList();
        slices = prefab.GetComponent<SlicesStart>().getDeadEnemy(type);
        foreach (GameObject e in slices)
        {
            float randomTime = Random.Range(2.0f, 4.0f);
            GameObject tmp = Instantiate(e, transform.position, transform.rotation);
            Destroy(tmp, randomTime);
            tmp.GetComponent<MeshRenderer>().enabled = true;
            tmp.AddComponent<Rigidbody>();
            if (!gravity)
            {
                tmp.GetComponent<Rigidbody>().useGravity = false;
            }
            tmp.GetComponent<Rigidbody>().AddForce(randomBackwardsDirection() * explosionForce, ForceMode.Impulse);
        }
        Destroy(this.gameObject);
    }
    private Vector3 randomDirection()
    {
        Vector3 direction = Random.insideUnitSphere.normalized;
        return direction;
    }
    private Vector3 randomUpwardsDirection()
    {
        Vector3 direction = (Random.insideUnitSphere + Vector3.up).normalized;
        return direction;
    }
    private Vector3 randomBackwardsDirection()
    {
        Vector3 direction = (Random.insideUnitSphere + new Vector3(0, 0, 1)).normalized;
        return direction;
    }
}
