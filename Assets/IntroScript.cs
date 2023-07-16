using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private string[] sentences;
    [SerializeField] private float[] displayTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(voiceActor());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator voiceActor()
    {
        for (int i = 0; i < sentences.Length; i++)
        {
            dialogueText.text = sentences[i];
            yield return new WaitForSeconds(displayTime[i]);
        }
    }
}
