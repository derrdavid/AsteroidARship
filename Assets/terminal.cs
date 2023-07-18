using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminal : MonoBehaviour
{
    public GameObject glücksrad;
    [SerializeField] private WaveManager waveManager;
    public bool wasTriggerd = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!wasTriggerd)
        {
            if (waveManager.getWave() != 0)
            {
                glücksrad.SetActive(true);
                wasTriggerd = true;
            }
        }


    }
}
