using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminal : MonoBehaviour
{
    public GameObject gl�cksrad;
    [SerializeField] private WaveManager waveManager;
    public bool wasTriggerd = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!wasTriggerd)
        {
            if (waveManager.getWave() != 0)
            {
                gl�cksrad.SetActive(true);
                wasTriggerd = true;
            }
        }


    }
}
