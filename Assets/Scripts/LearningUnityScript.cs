using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LearningUnityScript : MonoBehaviour
{
    public int countDown = 0;

    [SerializeField]
    TextMeshProUGUI countDownText;
    private bool hasCountDownStarted;
    private void Awake()
    {
        hasCountDownStarted = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartCountDown()
    {
        if (countDownText == null) return;
        if (hasCountDownStarted == true) return;

        hasCountDownStarted = true;
        StartCoroutine(CountDown(countDown));
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }


    IEnumerator CountDown(int times)
    {
        for (int i = times; i >= 0; i--)
        {
            Debug.LogFormat("{0}....", i);
            countDownText.text = i.ToString();
            if (i < 2)
            {
                countDownText.color = Color.red;

            }
            yield return new WaitForSeconds(1);
        }

        hasCountDownStarted = false;
        countDownText.color = Color.white;

        Debug.Log("CountDown Complete");
    }
}
