using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlicesStart : MonoBehaviour
{
    [SerializeField]
    private GameObject deadEnemy1;
    [SerializeField]
    private int sliceIterations;

    private ArrayList deadEnemy1Slices;
    // Start is called before the first frame update
    void Start()
    {
        deadEnemy1Slices = sliceObj(deadEnemy1, sliceIterations);
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

        return slices;
    }
    public ArrayList obliterateStep(ArrayList slices, int step)
    {
        ArrayList slicedObj = new ArrayList();
        if (step > 0)
        {
            foreach (GameObject e in slices)
            {
                ArrayList tmp = new ArrayList();
                tmp = obliterate(e);
                slicedObj.AddRange(obliterateStep(tmp, step - 1));
                if (slices.Count > 1)
                {
                    Destroy(e);
                }
            }
            return slicedObj;
        }
        if (step == 0)
        {
            return slices;
        }
        return null;
    }
    public ArrayList sliceObj(GameObject toSlice, int step)
    {
        ArrayList slices = new ArrayList();
        slices.Add(toSlice);
        return obliterateStep(slices, step);
    }
    private Vector3 randomDirection()
    {
        Vector3 direction = Random.insideUnitSphere.normalized;
        return direction;
    }
    public ArrayList getDeadEnemy1()
    {
        return deadEnemy1Slices;
    }
}
