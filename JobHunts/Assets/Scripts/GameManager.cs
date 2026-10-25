using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip changePage;
    [SerializeField] private AudioClip success;
    
    public static GameManager Instance { get; private set; }

    public int CurrentScore { get; set; } = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayButtonClick()
    {
        audioSource.clip = buttonClick;
        audioSource.Play();
    }

    public void PlayChangePage()
    {
        audioSource.clip = changePage;
        audioSource.Play();
    }

    public void PlaySuccess()
    {
        audioSource.clip = success;
        audioSource.Play();
    }
}
