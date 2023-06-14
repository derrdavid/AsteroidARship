using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;
using Color = UnityEngine.Color;

public class Break : MonoBehaviour
{
    [SerializeField]
    private GameObject toDestroy;
    [SerializeField]
    private int cuts = 3;

    private Vector3 sliceDirection;
    private void Start()
    {
    }
    public void Update()
    {
        if (toDestroy != null)
        {
            DrawPlane(toDestroy.GetComponent<Renderer>().bounds.center, sliceDirection);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            ArrayList slice = new ArrayList();
            slice.Add(toDestroy);
            obliterateStep(slice, cuts);
        }
    }
    public ArrayList obliterate(GameObject toSlice)
    {
        Vector3 normal = randomDirection(); // make random
        Vector3 point = toSlice.GetComponent<Renderer>().bounds.center;

        Vector3 transformedNormal = ((Vector3)(toSlice.transform.localToWorldMatrix.transpose * normal)).normalized;
        Vector3 transformedStartingPoint = toSlice.transform.InverseTransformPoint(point);

        Plane plane = new Plane(transformedNormal, transformedStartingPoint);

        var direction = Vector3.Dot(Vector3.up, normal);

        //Flip the plane so that we always know which side the positive mesh is on
        if (direction < 0)
        {
            plane = plane.flipped;
        }

        ArrayList slices = new ArrayList();
        slices.AddRange(SliceHandler.Slice(plane, toSlice));

        Destroy(toSlice);
        return slices;
    }
    public void obliterateStep(ArrayList slices, int step)
    {
        if (step > 0)
        {
            foreach (GameObject e in slices)
            {
                ArrayList tmp = obliterate(e);
                obliterateStep(tmp, step - 1);
            }
        }
    }
    private Vector3 randomDirection()
    {
        Vector3 direction = Random.insideUnitSphere.normalized;
        return direction;
    }
    private void DrawPlane(Vector3 position, Vector3 normal)
    {

        Vector3 v3;

        if (normal.normalized != Vector3.forward)
            v3 = Vector3.Cross(normal, Vector3.forward).normalized * normal.magnitude;
        else
            v3 = Vector3.Cross(normal, Vector3.up).normalized * normal.magnitude; ;

        var corner0 = position + v3;
        var corner2 = position - v3;
        var q = Quaternion.AngleAxis(90.0f, normal);
        v3 = q * v3;
        var corner1 = position + v3;
        var corner3 = position - v3;

        Debug.DrawLine(corner0, corner2, Color.green);
        Debug.DrawLine(corner1, corner3, Color.green);
        Debug.DrawLine(corner0, corner1, Color.green);
        Debug.DrawLine(corner1, corner2, Color.green);
        Debug.DrawLine(corner2, corner3, Color.green);
        Debug.DrawLine(corner3, corner0, Color.green);
        Debug.DrawRay(position, normal, Color.red);
    }
}
