using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipScript : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioSource airshipAudiosource;
    // Start is called before the first frame update
    public void playSound()
    {
        airshipAudiosource.Stop();
        soundManager.oneShotAirshipSound();
    }

}
