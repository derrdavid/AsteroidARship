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
    private List<GameObject> DeadEnemyUfo;
    [SerializeField]
    private List<GameObject> DeadEnemyAsteroid;
    [SerializeField]
    private List<GameObject> DeadEnemyShield;
    [SerializeField]
    private int sliceIterations;

    private ArrayList DeadEnemyUfoSlices;
    private ArrayList DeadEnemyAsteroidSlices;
    private ArrayList DeadEnemyShieldSlices;
    // Start is called before the first frame update
    void Start()
    {
        if (DeadEnemyUfo != null)
            DeadEnemyUfoSlices = sliceObj(DeadEnemyUfo, sliceIterations);
        if (DeadEnemyAsteroid != null)
            DeadEnemyAsteroidSlices = sliceObj(DeadEnemyAsteroid, sliceIterations);
            print("asteroid geschnitten");
        if (DeadEnemyShield != null)
            DeadEnemyShieldSlices = sliceObj(DeadEnemyShield, sliceIterations);
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
                return DeadEnemyUfoSlices;
            case 1:
                return DeadEnemyAsteroidSlices;
            case 2:
                return DeadEnemyShieldSlices;
        }
        return null;
    }
    private bool checkPrefab(GameObject e)
    {
        String objName = e.scene.name;
        return objName == null;
    }
}
