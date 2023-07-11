using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.InputSystem.HID;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static UnityEngine.GraphicsBuffer;
using Screen = UnityEngine.Device.Screen;

public class Shootingscript : MonoBehaviour
{
    [Header("targetSettings")]
    public ARRaycastManager raycastManager;
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private LayerMask enemys;
    public float damage;
    public int kills;
    [SerializeField]
    private float downtime = 1;
    [SerializeField]
    private float damage;
    private float timeSinceLastShot = 0;

    Ray ray;

    //fx Values
    [Header("fxSettings")]
    [SerializeField]
    private GameObject laserPrefap;
    [SerializeField]
    private GameObject firePoint;
    [SerializeField]
    private GameObject firePoint2;
    [SerializeField]
    private GameObject defaultTarget;

    private bool shotFired = false;
    private Vector3 shotTarget;
    private bool secondShot = false;
    private Vector3 currentFirePoint;
    private LineRenderer lineRenderer;
    private GameObject spawnedLaser;

    // Update is called once per frame
    private void Start()
    {
        ray = new Ray(camera.transform.position, camera.transform.forward);
        spawnedLaser = Instantiate(laserPrefap) as GameObject;
        lineRenderer = spawnedLaser.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
        spawnedLaser.transform.position = new Vector3(0, 0, 0);
        disableLaser();
    }
    void Update()
    {
        Debug.DrawRay(camera.transform.position, camera.transform.forward * 100, Color.red);
        // �berpr�fen, ob der Bildschirm ber�hrt wurde
        if (Input.GetMouseButtonDown(0) && !shotFired)
        {
            Raycast();
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !shotFired)
        {
            Raycast();
        }
        if (shotFired)
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot > downtime)
            {
                timeSinceLastShot = 0;
                shotFired = false;
                disableLaser();
            }
        }
    }
    void Raycast()
    {
        // Erstellen Sie einen Raycast aus dem aktuellen Bildschirmpunkt
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.AllTypes);
        if (secondShot)
        {
            secondShot = !secondShot;
            currentFirePoint = firePoint2.transform.position;
        }
        else
        {
            secondShot = !secondShot;
            currentFirePoint = firePoint.transform.position;
        }
        // ueberpruefen Sie, ob ein Treffer vorhanden ist
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, 100, enemys, QueryTriggerInteraction.UseGlobal))
        {
            shoot(hit.point);
            if (hit.collider.tag == "Enemy")
            {
                if (hit.collider.GetComponent<Projectile>().getHealth() - damage <= 0 && !hit.collider.GetComponent<Projectile>().getInvincible())
                {
                    kills++;
                }
                hit.collider.GetComponent<Projectile>().getHit(hit.point, damage);
            }
        }
        else
        {
            shoot(defaultTarget.transform.position);
        }
    }
    void shoot(Vector3 target)
    {
        shotTarget = target;
        enableLaser();
        shotFired = true;
    }
    void enableLaser()
    {
        spawnedLaser.SetActive(true);
        var pos = new Vector3[2];
        pos[0] = currentFirePoint;
        pos[1] = shotTarget;
        lineRenderer.SetPositions(pos);
    }
    void disableLaser()
    {
        spawnedLaser.SetActive(false);
    }
}
