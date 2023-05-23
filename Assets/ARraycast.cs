using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARraycast : MonoBehaviour
{
    public ARRaycastManager raycastManager;


    void Update()
    {
        // Überprüfen, ob der Bildschirm berührt wurde
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Führen Sie den Raycast aus
            Raycast();
        }
    }

    void Raycast()
    {
        // Erstellen Sie einen Raycast aus dem aktuellen Bildschirmpunkt
        Vector2 touchPosition = Input.GetTouch(0).position;
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(touchPosition, hits, TrackableType.AllTypes);

        // Überprüfen Sie, ob ein Treffer vorhanden ist
        if (hits.Count > 0)
        {
            // Holen Sie sich das getroffene GameObject und die hitPose
            Pose hitPose = hits[0].pose;

            // Geben Sie das GameObject im Debug.Log aus
            Debug.Log("Hit Pose: " + hitPose.position);
        }
    }
}