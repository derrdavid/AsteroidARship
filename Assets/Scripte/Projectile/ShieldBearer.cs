using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBearer : MonoBehaviour
{
    [SerializeField]
    private WaveManager wavManager;
    [SerializeField]
    private GameObject objectToSpawn;
    [SerializeField]
    private float averageSpawnTime;
    [SerializeField]
    private int firstApearance;
    [SerializeField]
    private int maxNumber;

    private List<GameObject> objects;
    [SerializeField]
    private int currentNumber;
    private float timer;
    private float actualSpawnTime;
    private int currentWave;
    // Start is called before the first frame update
    void Start()
    {
        objects = new List<GameObject>();
        currentNumber = 0;
        currentWave = 0;
        timer = 0f;
        actualSpawnTime = averageSpawnTime + Random.Range(-3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = objects.Count - 1; i >= 0; i--)
        {
            if (objects[i] == null)
            {
                objects.RemoveAt(i);
                currentNumber--;
            }
        }
        updateWave();
        if (!gameIsPaused())
        {
            timer += Time.deltaTime;
        }
        if (timer >= actualSpawnTime)
        {
            actualSpawnTime = averageSpawnTime + Random.Range(-3, 3);
            timer = 0f;
            if (currentWave >= firstApearance && currentNumber < maxNumber)
            {
                spawnShieldBearer();
                currentNumber++;
            }
        }
    }
    private void spawnShieldBearer()
    {
        objects.Add(Instantiate(objectToSpawn, randomSpawnLocation(), Quaternion.identity));
    }
    private Vector3 randomSpawnLocation()
    {
        int side = Random.Range(0, 2) * 2 - 1;
        float x = Random.Range(2, 4);
        x = x * side;
        float y = 2;
        float z = 20;
        return new Vector3(x, y, z);
    }
    private void updateWave()
    {
        int wave = wavManager.getWave();
        if (wave != currentWave)
        {
            currentWave = wave;
        }
    }
    private bool gameIsPaused()
    {
        return wavManager.getPaused();
    }
}
