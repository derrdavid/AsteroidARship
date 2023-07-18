using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminal : MonoBehaviour
{
    [SerializeField] private Roulette rouletteScript;
    [SerializeField] private WaveManager waveManager;
    public bool wasTriggerd = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!wasTriggerd)
        {
            if (waveManager.getWave() != 0)
            {
                rouletteScript.Rotate();
                wasTriggerd = true;
            }
        }


    }
}
