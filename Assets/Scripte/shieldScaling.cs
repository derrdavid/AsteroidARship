using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shieldScaling : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;
    private float fraction = 0;
    private Vector3 targetScale;
    private Vector3 startScale;
    // Start is called before the first frame update
    void Start()
    {
        targetScale = new Vector3(0.1f, 7, (transform.position.z - 5) * 1.5f);
        startScale = new Vector3(1,1,1);
    }
    private void Update()
    {
        if (fraction < 1)
        {
            fraction += Time.deltaTime * speed;
            transform.localScale = Vector3.Lerp(startScale, targetScale, fraction);
        }
    }
}
