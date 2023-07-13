using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI coordinatesText;
    [SerializeField] private TextMeshProUGUI coordinatesText2;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private TextMeshProUGUI waveCountText;
    [SerializeField] private Shootingscript shootingscript;
    [SerializeField] private TextMeshProUGUI killCountText;

    private void Update()
    {
        coordinatesText.text = gameObject.transform.position.ToString();
        coordinatesText2.text = (Mathf.Round(Time.realtimeSinceStartup)).ToString();
        waveCountText.text = waveManager.getWave().ToString();
        killCountText.text = shootingscript.getKills().ToString();
    }
}
