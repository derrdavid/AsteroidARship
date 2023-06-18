using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSettings : Projectile
{
    [SerializeField] private bool rotate;
    [SerializeField] private bool angle;
    public enum AllowedHitSides { Left, Right, Top, Bottom, Front, Back }
    public List<AllowedHitSides> allowedSides;
    [SerializeField] private int childAmount;
    [SerializeField] private GameObject child;
    private Quaternion localRotation;

    // setzen der Ausgangsposition
    void Start()
    {
        setHealth(10f);
        gameObject.transform.position = new Vector3(xPosition, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    // Bewege gameObject in z-Achse
    public override void Update()
    {
        Vector3 newPosition = new Vector3(xPosition, gameObject.transform.position.y, targetZ);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, newPosition, moveSpeed * Time.deltaTime);

        if (rotate)
        {
            iTween.RotateBy(gameObject, new Vector3(0, 100, 30), 600);
        }
        if (gameObject.transform.position.z <= targetZ)
        {
            GameObject.Find("AtriumManager").GetComponent<AtriumManager>().takeDamage(10);
            Destroy(gameObject);
        }
    }
    public override void getHit(Vector3 hitPos, float damage)
    {


        if (angle)
        {
            if (getHitSide(hitPos))
                health -= damage;
        }
        else
        {
            health -= damage;
        }

        if (childAmount > 0)
        {
            for (int i = 0; i < childAmount; i++)
            {
                float randomX = Random.Range(-2, 2);
                float randomY = Random.Range(-2, 2);
                Vector3 spawnPosition = new Vector3(randomX, 0f, randomY) + transform.position;
                Quaternion spawnRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                GameObject newObj = Instantiate(child, spawnPosition, spawnRotation);
                newObj.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
                newObj.GetComponent<Projectile>().set(2.0f, newObj.gameObject.transform.position.x, -17);
            }
        }

        if (health <= 0)
        {
            if (childAmount == 0)
            {
                Instantiate(deathDouble, transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }
    private bool getHitSide(Vector3 hitPos)
    {
        bool hit = false;
        // Berechne die lokale Position des Treffers relativ zum Prefab
        Vector3 localHitPos = transform.InverseTransformPoint(hitPos);

        // Erhalte den BoxCollider des Prefabs
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        // Berechne die normalisierte Trefferposition relativ zur Größe des BoxColliders
        Vector3 normalizedHitPos = new Vector3(
            Mathf.Abs(localHitPos.x / boxCollider.size.x),
            Mathf.Abs(localHitPos.y / boxCollider.size.y),
            Mathf.Abs(localHitPos.z / boxCollider.size.z)
        );

        // Identifiziere die Trefferseite basierend auf der normalisierten Trefferposition
        if (normalizedHitPos.x > normalizedHitPos.y && normalizedHitPos.x > normalizedHitPos.z)
        {
            if (localHitPos.x > 0 && allowedSides.Contains(AllowedHitSides.Right))
                hit = true;
            else if (allowedSides.Contains(AllowedHitSides.Left))
                hit = true;
        }
        else if (normalizedHitPos.y > normalizedHitPos.x && normalizedHitPos.y > normalizedHitPos.z)
        {
            if (localHitPos.y > 0 && allowedSides.Contains(AllowedHitSides.Top))
                hit = true;

            else if (allowedSides.Contains(AllowedHitSides.Bottom))
                hit = true;
        }
        else
        {
            if (localHitPos.z > 0 && allowedSides.Contains(AllowedHitSides.Front))
                hit = true;

            else if (allowedSides.Contains(AllowedHitSides.Back))
                hit = true;
        }
        return hit;
    }
}
