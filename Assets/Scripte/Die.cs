using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    [SerializeField]
    private int cuts = 3;
    [SerializeField]
    private int explosionForce = 3;
    private void Start()
    {
        ArrayList list = new ArrayList();
        list.Add(this.gameObject);
        obliterateStep(list, cuts);
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
        if (step == 0)
        {
            foreach (GameObject e in slices)
            {
                e.GetComponent<Rigidbody>().AddForce(randomUpwardsDirection() * explosionForce, ForceMode.Impulse);
                float randomTime = Random.Range(2.0f, 4.0f);
                Destroy(e, randomTime);
            }
        }
    }
    private Vector3 randomDirection()
    {
        Vector3 direction = Random.insideUnitSphere.normalized;
        return direction;
    }
    private Vector3 randomUpwardsDirection()
    {
        Vector3 direction =  (Random.insideUnitSphere + Vector3.up).normalized;
        return direction;
    }
}
