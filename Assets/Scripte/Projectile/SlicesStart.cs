using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public enum DEAD_ENEMY_TYPE
{
    UFO,
    ASTEROID,
    SHIELDBEARER,
}
public class SlicesStart : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> deadEnemy1;
    [SerializeField]
    private List<GameObject> deadEnemy2;
    [SerializeField]
    private List<GameObject> deadEnemy3;
    [SerializeField]
    private int sliceIterations;

    private ArrayList deadEnemy1Slices;
    private ArrayList deadEnemy2Slices;
    private ArrayList deadEnemy3Slices;
    // Start is called before the first frame update
    void Start()
    {
        if (deadEnemy1 != null)
            deadEnemy1Slices = sliceObj(deadEnemy1, sliceIterations);
        if (deadEnemy2 != null)
            deadEnemy2Slices = sliceObj(deadEnemy2, sliceIterations);
        if (deadEnemy3 != null)
            deadEnemy3Slices = sliceObj(deadEnemy3, sliceIterations);
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
        foreach (GameObject e in slices)
        {
            e.GetComponent<MeshCollider>().enabled = false;
        }
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
                if (!checkPrefab(e))
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
    public ArrayList sliceObj(List<GameObject> toSlice, int step)
    {
        ArrayList slices = new ArrayList();
        foreach (GameObject e in toSlice)
        {
            slices.Add(e);
        }
        return obliterateStep(slices, step);
    }
    private Vector3 randomDirection()
    {
        Vector3 direction = Random.insideUnitSphere.normalized;
        return direction;
    }
    public ArrayList getDeadEnemy(DEAD_ENEMY_TYPE type)
    {
        switch ((int)type){
            case 0:
                return deadEnemy1Slices;
            case 1:
                return deadEnemy2Slices;
            case 2:
                return deadEnemy3Slices;
        }
        return null;
    }
    private bool checkPrefab(GameObject e)
    {
        String objName = e.scene.name;
        return objName == null;
    }
}
