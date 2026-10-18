using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject homePage;
    [SerializeField] private GameObject tasksPage;
    [SerializeField] private GameObject huntsPage;
    [SerializeField] private GameObject ranksPage;
    [SerializeField] private GameObject profilePage;

    public GameObject CurrentPage { get; set; }

    private void Awake()
    {
        CurrentPage = homePage;
        homePage.SetActive(true);
        tasksPage.SetActive(false);
        huntsPage.SetActive(false);
        ranksPage.SetActive(false);
        profilePage.SetActive(false);
    }

    public void OpenHomePage()
    {
        OpenNewPage(homePage);
    }
    
    public void OpenTasksPage()
    {
        OpenNewPage(tasksPage);
    }
    
    public void OpenHuntsPage()
    {
        OpenNewPage(huntsPage);
    }
    
    public void OpenRanksPage()
    {
        OpenNewPage(ranksPage);
    }
    
    public void OpenProfilePage()
    {
        OpenNewPage(profilePage);
    }

    private void OpenNewPage(GameObject newPage)
    {
        CurrentPage.SetActive(false);
        newPage.SetActive(true);
        CurrentPage = newPage;
    }
}
