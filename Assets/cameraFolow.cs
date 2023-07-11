using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFolow : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(mainCamera.transform);
        gameObject.transform.position = offset;
    }
}
