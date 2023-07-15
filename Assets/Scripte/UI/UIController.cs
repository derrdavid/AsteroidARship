using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{


    [SerializeField] private WaveManager waveManager;
    [SerializeField] private TextMeshProUGUI waveCountText;
    [SerializeField] private TextMeshProUGUI waveCoolDownText;
    [SerializeField] private Shootingscript shootingscript;
    [SerializeField] private Button shootButton;
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI coordinatesText;
    [SerializeField] private TextMeshProUGUI coordinatesText2;
    private bool isShootButtonClicked = false;

    private void Start()
    {
        shootButton.onClick.AddListener(() => { shootButtonClicked(); });
    }
    private void Update()
    {
        coordinatesText.text = gameObject.transform.position.ToString();
        coordinatesText2.text = (Mathf.Round(Time.realtimeSinceStartup)).ToString();
        waveCountText.text = waveManager.getWave().ToString();
        killCountText.text = shootingscript.getKills().ToString();
        waveCoolDownText.text = waveManager.getRemainingWaveCoolDown().ToString();
        shootingscript.triggerShot(isShootButtonClicked);
        isShootButtonClicked = false;
    }
    private void shootButtonClicked()
    {
        isShootButtonClicked = true;
        GameObject.Find("Managers").GetComponent<SoundManager>().oneShotLaser();
    }
    public bool getShootButtonClicked()
    {
        return isShootButtonClicked;
    }
}
