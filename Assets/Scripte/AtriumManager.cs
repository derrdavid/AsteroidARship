using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtriumManager : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private BarController barController;
    [SerializeField] private Image interfaceImage;
    [SerializeField] private Button shootButton;

    [Header("colorize")]
    private float lerpSpeed = 0.5f;
    private bool deathScreen;
    private Color startColor;
    private float t = 0f;

    [Header("Highscore")]
    private List<int> highscores;
    private int myScore;

    private void Start()
    {
        barController.setValue(maxHealth);
        LoadHighscores();
    }

    private void Update()
    {
        if (barController.getCurrentHealth() <= 0 && !deathScreen)
        {
            t += Time.deltaTime * lerpSpeed;
            interfaceImage.color = Color.Lerp(startColor, Color.red, t);
            shootButton.image.color = Color.Lerp(startColor, Color.black, t);

            // Überprüfe, ob der Color-Lerp abgeschlossen ist
            if (t >= 1f)
            {
                StartCoroutine(DeathScreen());
            }
        }
    }

    IEnumerator DeathScreen()
    {
        deathScreen = true;
        shootButton.enabled = false;
        yield return new WaitForSeconds(4f);

        UpdateHighscores();
        DisplayHighscores();
    }

    public void takeDamage(int damage)
    {
        barController.decrease(damage);
    }

    public void heal(int heal)
    {
        barController.increase(heal);
    }

    public int getCurrentHealth()
    {
        return (int)barController.getCurrentHealth();
    }

    private void LoadHighscores()
    {
        highscores = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            int highscore = PlayerPrefs.GetInt("Highscore" + i, 0);
            highscores.Add(highscore);
        }
    }

    private void UpdateHighscores()
    {
        //myScore = waveManager.score;
        myScore = 19990;

        for (int i = 0; i < highscores.Count; i++)
        {
            if (myScore > highscores[i])
            {
                highscores.Insert(i, myScore);
                highscores.RemoveAt(highscores.Count - 1);
                break;
            }
        }

        for (int i = 0; i < highscores.Count; i++)
        {
            PlayerPrefs.SetInt("Highscore" + i, highscores[i]);
        }
    }

    private void DisplayHighscores()
    {
        Debug.Log(highscores);
        Debug.Log("Highscores:");
        for (int i = 0; i < highscores.Count; i++)
        {
            if (i < highscores.Count && highscores[i] > 0)
            {
                Debug.Log((i + 1) + ". " + highscores[i]);
            }
            else
            {
                Debug.Log((i + 1) + ". -");
            }
        }
    }
}
