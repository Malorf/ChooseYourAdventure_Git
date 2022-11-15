using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using TMPro;

public class IntroEnd : MonoBehaviour
{
    public string lobbySceneName;
    public double time;
    public double currentTime;

    void Start()
    {

            time = GameObject.Find("Intro").GetComponent<VideoPlayer>().clip.length;
    }

    void Update()
    {
        currentTime = gameObject.GetComponent<VideoPlayer>().time;
        if (currentTime >= time-2)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(lobbySceneName);
        }
    }
}
