using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBearer : MonoBehaviour
{
    [SerializeField]
    GameObject wavManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private bool gameIsPaused()
    {
        return wavManager.GetComponent<WaveManager>.get
    }
}
