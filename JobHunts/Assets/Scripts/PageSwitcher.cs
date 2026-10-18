using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PageSwitcher : MonoBehaviour
{
    [SerializeField] private TMP_Text txt;
    
    [Header("Pages")]
    [SerializeField] private GameObject homePage;
    [SerializeField] private GameObject tasksPage;
    [SerializeField] private GameObject huntsPage;
    [SerializeField] private GameObject ranksPage;
    [SerializeField] private GameObject profilePage;

    public GameObject CurrentPage { get; set; }

    private void Awake()
    {
        CurrentPage = homePage;
        txt.text = "Job Hunts";
        homePage.SetActive(true);
        tasksPage.SetActive(false);
        huntsPage.SetActive(false);
        ranksPage.SetActive(false);
        profilePage.SetActive(false);
    }

    public void OpenHomePage()
    {
        OpenNewPage(homePage, "Job Hunts");
    }
    
    public void OpenTasksPage()
    {
        OpenNewPage(tasksPage, "Daily Quests");
    }
    
    public void OpenHuntsPage()
    {
        OpenNewPage(huntsPage, "Log your activity");
    }
    
    public void OpenRanksPage()
    {
        OpenNewPage(ranksPage, "Leaderboard");
    }
    
    public void OpenProfilePage()
    {
        OpenNewPage(profilePage, "Your profile");
    }

    private void OpenNewPage(GameObject newPage, string headerText)
    {
        CurrentPage.SetActive(false);
        newPage.SetActive(true);
        CurrentPage = newPage;
        txt.text = headerText;
    }
}
