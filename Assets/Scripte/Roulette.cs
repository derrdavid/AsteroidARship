using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulette : MonoBehaviour
{

    public float rotatePower;
    public float stopPower;

    private Rigidbody2D rigidbody;
    private int inRotate;
    private float t;


    [SerializeField] private Shootingscript shootingscript;
    [SerializeField] private Textbox textboxscript;
    [SerializeField] private GameObject container;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody.angularVelocity > 0)
        {
            rigidbody.angularVelocity -= stopPower * Time.deltaTime;
            rigidbody.angularVelocity = Mathf.Clamp(rigidbody.angularVelocity, 0, 1440);
        }

        if (rigidbody.angularVelocity == 0 && inRotate == 1)
        {
            t += 1 * Time.deltaTime;
            if (t >= 0.5f)
            {
                GetReward();

                inRotate = 0;
                t = 0;
            }
        }
    }

    public void Rotate()
    {
        if(inRotate == 0)
        {
            rigidbody.AddTorque(rotatePower);
            inRotate = 1;
        }
    }

    public void GetReward()
    {
        float rotation = transform.eulerAngles.z;

        if (rotation > 0 && rotation <= 45)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 45-22);
            Win(1);
        }
        else if (rotation > 45 && rotation <= 90)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 90 - 22);
            Win(2);
        }
        else if (rotation > 90 && rotation <= 135)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 135 - 22);
            Win(1);
        }
        else if (rotation > 135 && rotation <= 180)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 180 - 22);
            Win(2);
        }
        else if (rotation > 180 && rotation <= 225)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 225 - 22);
            Win(3);
        }
        else if (rotation > 225 && rotation <= 270)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 270 - 22);
            Win(1);
        }
        else if (rotation > 270 && rotation <= 315)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 315 - 22);
            Win(2);
        }
        else if (rotation > 315 && rotation <= 360)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 360 - 22);
            Win(3); ;
        }
    }

    public void Win(int powerUp)
    {
        Debug.Log(powerUp);
        shootingscript.setPowerUp(powerUp);
        textboxscript.setPowerup(powerUp);
        container.SetActive(false);
    }
}
