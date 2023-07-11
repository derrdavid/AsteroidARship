using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;

public class CanvasDelay : MonoBehaviour
{
    [SerializeField] private Vector2 amount;
    [SerializeField] private float lerp = .5f;
    private XROrigin xrOrigin;

    void Start()
    {
        xrOrigin = FindObjectOfType<XROrigin>();
    }

    void Update()
    {
        if (ARSession.state != ARSessionState.SessionTracking)
        {
            return;
        }

        Vector3 rotation = xrOrigin.transform.rotation.eulerAngles;
        float x = rotation.y;
        float y = rotation.x;

        transform.localEulerAngles = new Vector3(
            Mathf.LerpAngle(transform.localEulerAngles.x, y * amount.y, lerp),
            Mathf.LerpAngle(transform.localEulerAngles.y, x * amount.x, lerp),
            0);
    }
}