using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeCountDown : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 30f;
    Vector3 localScale;

    public Text CountdownText;
    public Image CountdownBar;

    void Start()
    {
        localScale = transform.localScale;
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        CountdownText.text = currentTime.ToString();
        localScale.x = currentTime / startingTime;
        transform.localScale = localScale;

        if (currentTime <= 0)
            currentTime = 0;
    }
}
