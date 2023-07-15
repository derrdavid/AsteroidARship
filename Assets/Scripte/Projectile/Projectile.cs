using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // standard Attributes
    [SerializeField] private float health = 10f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float xPosition;
    private float targetZ = 1;
    [SerializeField] private GameObject deathDouble;

    // advanced Attributes
    [SerializeField] private bool rotate;
    [SerializeField] private bool angle;
    [SerializeField] private enum AllowedHitSides { Left, Right, Top, Bottom, Front, Back }
    [SerializeField] private List<AllowedHitSides> allowedSides;
    [SerializeField] private int childAmount;
    [SerializeField] private GameObject child;
    [SerializeField] private ParticleSystem explosionParticle;
    private bool crashed;
    private bool invincible = true;

    // setzen der Ausgangsposition
    void Start()
    {
        setHealth(health);
        gameObject.transform.position = new Vector3(xPosition, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    // Bewege gameObject in z-Achse
    public void Update()
    {
        Vector3 newPosition = new Vector3(xPosition, gameObject.transform.position.y, targetZ);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, newPosition, moveSpeed * Time.deltaTime);

        if (rotate)
        {
            iTween.RotateBy(gameObject, new Vector3(0, 100, 30), 600);
        }
        if (gameObject.transform.position.z <= targetZ && crashed == false)
        {
            StartCoroutine(crash());
        }
        if (health <= 0)
            Destroy(gameObject);
    }

    public float getHealth()
    {
        return health;
    }
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
    public void setInvincible(bool invincible)
    {
        this.invincible = invincible;
    }
    public bool getInvincible()
    {
        return this.invincible;
    }
    public void getHit(Vector3 hitPos, float damage)
    {
        if (getInvincible() == false)
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
                    newObj.GetComponent<Projectile>().set(2.0f, newObj.gameObject.transform.position.x, targetZ);
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
    private IEnumerator crash()
    {
        crashed = true;

        GameObject.Find("Managers").GetComponent<AtriumManager>().takeDamage(10);
        explosionParticle.Play();
        Instantiate(deathDouble, transform.position, Quaternion.identity);
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
        /**
        if (gameObject.GetComponent<Renderer>() != null)
            gameObject.GetComponent<Renderer>().enabled = false;
            */
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
