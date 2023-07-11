using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFolow : MonoBehaviour
{
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(camera.transform);
        print("test");
        gameObject.transform.position = offset;
    }
}
