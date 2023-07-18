using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class AtriumManager : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private BarController barController;
    [SerializeField] private Image interfaceImage;
    [SerializeField] private Button shootButton;
    [SerializeField] private TextMeshProUGUI[] placeTexts;
    [SerializeField] private Canvas scoreBoard;
    [SerializeField] private TextMeshProUGUI restartText;
    [SerializeField] private TextMeshProUGUI restartCountdown;

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
            scoreBoard.gameObject.SetActive(true);
            t += Time.deltaTime * lerpSpeed;
            interfaceImage.color = Color.Lerp(startColor, Color.red, t);
            shootButton.image.color = Color.Lerp(startColor, Color.black, t);
            scoreBoard.GetComponent<Image>().color = Color.Lerp(startColor, Color.white, t);

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
        StartCoroutine(Restart());
    }
    IEnumerator Restart()
    {
        restartText.gameObject.SetActive(true);
        int countdownSeconds = 10;

        while (countdownSeconds > 0)
        {
            restartCountdown.text = countdownSeconds.ToString();
            yield return new WaitForSeconds(1f);
            countdownSeconds--;
        }

        SceneManager.LoadScene("IntroScene"); // Hier den Namen der IntroScene einfügen
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
        myScore = waveManager.score;


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
        for (int i = 0; i < highscores.Count; i++)
        {
            if (i < highscores.Count && highscores[i] >= 0)
            {
                placeTexts[i].text = highscores[i].ToString();
            }
            else
            {
                placeTexts[i].text = "-";
            }
        }
    }
}
