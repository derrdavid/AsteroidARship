using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Screen = UnityEngine.Device.Screen;

public class Shootingscript : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    [SerializeField]
    private GameObject tracker;
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private LayerMask enemys;
    public float damage;
    public int kills;

    Ray ray;

    // Update is called once per frame
    private void Start()
    {
        ray = new Ray(camera.transform.position, camera.transform.forward);
    }
    void Update()
    {
        Debug.DrawRay(camera.transform.position, camera.transform.forward * 100, Color.red);
        // �berpr�fen, ob der Bildschirm ber�hrt wurde
        if (Input.GetMouseButtonDown(0))
        {
            Raycast();
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Raycast();
        }
    }
    void Raycast()
    {
        // Erstellen Sie einen Raycast aus dem aktuellen Bildschirmpunkt
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.AllTypes);

        // ueberpruefen Sie, ob ein Treffer vorhanden ist
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, 100, enemys, QueryTriggerInteraction.UseGlobal))
        {
            placeTracker(hit.point);

            if (hit.collider.tag == "Enemy")
            {
                if (hit.collider.GetComponent<Projectile>().getHealth() - damage <= 0 && !hit.collider.GetComponent<Projectile>().getInvincible())
                {
                    kills++;
                }
                hit.collider.GetComponent<Projectile>().getHit(hit.point, damage);
            }
        }
        /*else if (hits.Count > 0)
        {
            // Holen Sie sich das getroffene GameObject und die hitPose
            Pose hitPose = hits[0].pose;
            placeTracker(hitPose.position);
            // Geben Sie das GameObject im Debug.Log aus
            Debug.Log("Hit Pose: " + hitPose.position);
        }*/
    }
    void placeTracker(Vector3 pos)
    {
        tracker.transform.position = pos;
    }
}
