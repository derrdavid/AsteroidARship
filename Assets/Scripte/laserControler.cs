using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class laserControler : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPrefap;
    [SerializeField]
    private GameObject firePoint;
    [SerializeField]
    private GameObject target;

    private LineRenderer lineRenderer;
    private GameObject shotBloom;
    private GameObject spawnedLaser;
    // Start is called before the first frame update
    void Start()
    {
        spawnedLaser = Instantiate(laserPrefap, firePoint.transform) as GameObject;
        lineRenderer = spawnedLaser.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
        shotBloom = spawnedLaser.transform.GetChild(1).gameObject;
        spawnedLaser.transform.position = new Vector3(0, 0, 0);
        disableLaser();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            enableLaser();
        }
        if (Input.GetMouseButton(0))
        {
            UpdateLaser();
        }
        if (Input.GetMouseButtonUp(0))
        {
            disableLaser();
        }
    }
    void enableLaser()
    {
        spawnedLaser.SetActive(true);
    }
    void UpdateLaser()
    {
        if (firePoint != null)
        {
            shotBloom.transform.position = firePoint.transform.position;
            var pos = new Vector3[2];
            pos[0] = firePoint.transform.position;
            pos[1] = target.transform.position;
            lineRenderer.SetPositions(pos);
        }
    }
    void disableLaser() { 
        spawnedLaser.SetActive(false);
    }
}
