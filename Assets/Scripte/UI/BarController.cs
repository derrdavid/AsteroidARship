using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
public class BarController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI barTextField;
    [SerializeField] Image forgroundBar;
    [SerializeField] String barText;
    

    float value, maxValue = 100;
    float lerpSpeed;
    void Start()
    {
        value = maxValue;
    }

    void Update()
    {
        barTextField.text = barText + ": " + value + "%";
        if (value > maxValue)
        {
            value = maxValue;
        }

        lerpSpeed = 3f * Time.deltaTime;
        BarFiller();
    }

    void BarFiller()
    {
        forgroundBar.fillAmount = Mathf.Lerp(forgroundBar.fillAmount, value / maxValue, lerpSpeed);
    }

    public void decrease(float damagePoints)
    {
        if (value > 0)
        {
            value -= damagePoints;
        }
    }

    public void increase(float healingPoints)
    {
        if (value < maxValue)
        {
            value += healingPoints;
        }
    }
    public void setValue(int newValue)
    {
        value = newValue;
    }

    public double getCurrentHealth()
    {
        return value;
    }

}
