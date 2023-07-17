using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private AudioSource actorAudio;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Canvas titleScreen;
    [SerializeField] private Canvas actorScreen;
    [SerializeField] private string[] sentences;
    [SerializeField] private float[] displayTime;
    [SerializeField] private TextMeshProUGUI skipText;
    private bool skipEnabled;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(handleIntro());
        //StartCoroutine(voiceActor());
    }

    IEnumerator handleIntro()
    {
        yield return new WaitForSeconds(6f);
        titleScreen.gameObject.SetActive(false);
        actorScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        StartCoroutine(voiceActor());
        yield return new WaitForSeconds(5f);
        enableSkip();
    }

    private void enableSkip()
    {
        skipEnabled = true;
        skipText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        if (skipEnabled)
        {
            // Überprüfe, ob der linke Mausklick erfolgt ist
            if (Input.GetMouseButtonDown(0))
            {
                // Wechsele zur nächsten Szene
                StartCoroutine(ChangeScene());
            }
        }
    }
    IEnumerator voiceActor()
    {
        actorAudio.Play();
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 10; i++)
        {
            dialogueText.text = sentences[i];
            yield return new WaitForSeconds(displayTime[i]);
        }
        dialogueText.text = "";
        yield return new WaitForSeconds(7f);
        StartCoroutine(ChangeScene());
    }
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2f);
        // Rufe den Namen der nächsten Szene auf
        string nextSceneName = "SampleScene";

        // Lade die nächste Szene
        SceneManager.LoadScene(nextSceneName);
    }
}
