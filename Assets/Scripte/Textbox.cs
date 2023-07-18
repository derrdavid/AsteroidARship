using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class Textbox : MonoBehaviour
{
    private int powerupId = 0;
    public TextMeshProUGUI powerup;
    public TextMeshProUGUI description;
    public GameObject card;

    private float lerpTime;
    private Vector3 final_size;
    private Vector3 start_size;

    private void Update()
    {

        if (lerpTime < 1)
        {
            gameObject.GetComponent<RectTransform>().localScale = Vector3.Lerp(start_size, final_size, lerpTime);
            lerpTime += Time.deltaTime;
            resetText();
        }
        if (lerpTime > 1) { setText(); }



        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            card.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 0.0001f, 1);
        lerpTime = 0f;
        final_size = new Vector3(1, 1, 1);
        start_size = new Vector3(1, 0.0001f, 1);
    }

    private void OnDisable()
    {
        powerupId = 0;
    }

    public void setPowerup(int powerup)
    {
        powerupId = powerup;
        card.SetActive(true);
    }

    private void resetText()
    {
        powerup.text = "";
        description.text = "";
    }
    private void setText()
    {
        switch (powerupId)
        {
            case 0:
                powerup.text = "";
                description.text = "";

                break;
            case 1:
                powerup.text = "DOPPELTE <BR> XP";
                description.text = "IHR ERHALTET FUR JEDEN KILL DIE DOPPELTEN PUNKTE!";
                break;

            case 2:
                powerup.text = "RAPIDFIRE";
                description.text = "SCHNELLERES SCHIEßEN. WENIGER NACHDENKEN MEHR BALLERN!";
                break;

            case 3:
                powerup.text = "MULTISHOT";
                description.text = "DU SCHIEßT ZWEI SCHUSSE GLEICHZEITIG. DOPPELTER SCHADEN, DOPPELTER SPAß!";
                break;

        }
    }
}
