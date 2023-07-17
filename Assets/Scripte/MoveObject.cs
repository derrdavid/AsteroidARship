using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour
{
    public Transform target; // Das Zielobjekt, zu dem das GameObject bewegt werden soll
    public float speedMin = 5f; // Minimale Geschwindigkeit
    public float speedMax = 20f; // Maximale Geschwindigkeit
    public bool rotateObject = false; // Steuert, ob das GameObject rotieren soll

    private Vector3 startPosition; // Startposition des GameObjects
    private float randomSpeed;

    void Start()
    {
        GenerateRandomStartPosition();
        MoveToTarget();
    }

    void GenerateRandomStartPosition()
    {
        // Generiere eine zufällige Startposition innerhalb eines bestimmten Bereichs
        float randomX = Random.Range(20f, 60f);
        float randomY = Random.Range(-20f, 40f);
        float randomZ = Random.Range(-10f, 10f);

        float startX = transform.position.x;
        float startZ = transform.position.z;

        startPosition = new Vector3(-startX + randomX, randomY, startZ + randomZ);
    }

    void MoveToTarget()
    {
        // Generiere eine zufällige Höhe für das Zielobjekt
        float randomHeight = Random.Range(-100f, 100f);
        float randomZ = Random.Range(-50f, 150f);// Beispielwert, anpassen an deine Bedürfnisse

        // Erzeuge eine zufällige Geschwindigkeit
        randomSpeed = Random.Range(speedMin, speedMax);

        // Definiere die Zielposition basierend auf der zufälligen Höhe und der vorgegebenen Richtung
        Vector3 targetPosition = new Vector3(target.position.x, randomHeight, target.position.z + randomZ);

        // Bewege das GameObject zum Ziel mit der zufälligen Geschwindigkeit
        iTween.MoveTo(gameObject, iTween.Hash(
            "position", targetPosition,
            "speed", randomSpeed,
            "easetype", iTween.EaseType.easeInCirc,
            "oncomplete", "MoveToStart",
            "oncompletetarget", gameObject
        ));

        // Starte die Rotation des GameObjects, wenn rotateObject true ist
        if (rotateObject)
        {
            StartCoroutine(RotateObject());
        }
    }

    IEnumerator RotateObject()
    {
        while (true)
        {
            transform.Rotate(new Vector3(1f, 1f, 0.5f), Time.deltaTime * 50f * randomSpeed); // Anpassen der Rotationsgeschwindigkeit nach Bedarf
            yield return null;
        }
    }

    void MoveToStart()
    {
        // Setze das GameObject zurück zur Startposition
        transform.position = startPosition;

        // Stoppe die Rotation des GameObjects
        StopAllCoroutines();

        // Starte die Bewegung zum nächsten Ziel
        MoveToTarget();
    }
}
