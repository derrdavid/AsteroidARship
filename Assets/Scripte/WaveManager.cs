using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
    [SerializeField] private int _waveNumber;
    [SerializeField] private int _scoreGoal;
    [SerializeField] private ProjectileSpawner[] projectileSpawners;
    private bool _increased { get; set; }
    public int waveNumber
    {
        get { return _waveNumber; }
    }
    public int scoreGoal
    {
        get { return _scoreGoal; }
    }
    public bool increased
    {
        get { return _increased; }
        set { _increased = value; }
    }

    public void activate(bool active)
    {
        increased = true;
        foreach (ProjectileSpawner spawner in projectileSpawners)
        {
            spawner.isActive(active);
        }
    }
}
public class WaveManager : MonoBehaviour
{
    public int score;
    private int actualWave = 0;
    [SerializeField] private float waveCoolDown = 15f;
    private float remainingTime;
    private bool paused;
    [SerializeField] private Shootingscript shootingscript;
    [SerializeField] private terminal terminalscript;
    [SerializeField] private Wave[] waves;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(increaseWave());
    }

    // Update is called once per frame
    void Update()
    {
        if (actualWave >= 1)
        {
            score = shootingscript.kills;
            switch (score)
            {
                case var s when waves[actualWave - 1].scoreGoal == score
                && !waves[actualWave - 1].increased
                && waves.Length > actualWave
                && remainingTime == 0:
                    shootingscript.resetPowerUps();
                    terminalscript.wasTriggerd = false;
                    StartCoroutine(increaseWave());
                    break;

            }
        }
        if (remainingTime > 0)
        {
            paused = true;
        }
        else
        {
            paused = false;
        }
    }
    private IEnumerator increaseWave()
    {

        if (actualWave > 0)
        {
            waves[actualWave - 1].activate(false);
        }

        remainingTime = waveCoolDown;

        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        actualWave++;
        foreach (Wave wave in waves)
        {
            if (wave.waveNumber != actualWave)
            {
                wave.activate(false);
            }
            else
            {
                wave.activate(true);
            }
        }
    }
    public float getRemainingWaveCoolDown()
    {
        return remainingTime;
    }
    public float getWaveCoolDown()
    {
        return waveCoolDown;
    }
    public int getWave()
    {
        return actualWave;
    }
    public bool getPaused()
    {
        return paused;
    }
}