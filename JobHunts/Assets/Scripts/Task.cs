using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    [SerializeField] private TMP_Text activityName;
    [SerializeField] private Image activityIcon;
    [SerializeField] private Image pointsIcon;
    [SerializeField] private Image completedPointsIcon;
    [SerializeField] private GameObject button;
    
    public HuntsSubmitManager.ActivityType ActivityType { get; private set; }

    public void SetUpTask(HuntsSubmitManager.ActivityType activityType, Sprite chosenActivityIcon, Sprite chosenPointsIcon)
    {
        ActivityType = activityType;
        activityName.text = ActivityToString(activityType);
        activityIcon.sprite = chosenActivityIcon;
        pointsIcon.sprite = chosenPointsIcon;
        
        completedPointsIcon.gameObject.SetActive(false);
        pointsIcon.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
    }

    public void SetUpCompleted(HuntsSubmitManager.ActivityType activityType, Sprite completedIcon, Sprite chosenCompletedPoints)
    {
        ActivityType = activityType;
        activityName.text = ActivityToString(activityType);
        activityIcon.sprite = completedIcon;
        completedPointsIcon.sprite = chosenCompletedPoints;
        
        completedPointsIcon.gameObject.SetActive(true);
        pointsIcon.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
    }

    public void LogActivity()
    {
        GameManager.Instance.PlayChangePage();
        PageSwitcher.Instance.OpenHuntsPage();
        if (ActivityType == HuntsSubmitManager.ActivityType.ApplyJob)
        {
            HuntsSubmitManager.Instance.SubmitJob();
            return;
        }
        
        if (ActivityType == HuntsSubmitManager.ActivityType.PortfolioUpdate)
        {
            HuntsSubmitManager.Instance.SubmitPortfolio();
            return;
        }
        
        if (ActivityType == HuntsSubmitManager.ActivityType.CoffeeChat)
        {
            HuntsSubmitManager.Instance.SubmitCoffee();
            return;
        }
        
        if (ActivityType == HuntsSubmitManager.ActivityType.LinkedInConnection)
        {
            HuntsSubmitManager.Instance.SubmitLinkedIn();
        }
    }

    private string ActivityToString(HuntsSubmitManager.ActivityType activityType)
    {
        if (ActivityType == HuntsSubmitManager.ActivityType.ApplyJob)
        {
            return "Apply To A Job";
        }
        
        if (ActivityType == HuntsSubmitManager.ActivityType.PortfolioUpdate)
        {
            return "Update Portfolio";
        }
        
        if (ActivityType == HuntsSubmitManager.ActivityType.CoffeeChat)
        {
            return "Complete A Coffee Chat";
        }
        
        if (ActivityType == HuntsSubmitManager.ActivityType.LinkedInConnection)
        {
            return "Connect On LinkedIn";
        }

        return "Taylor Messed up";
    }
}