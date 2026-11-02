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
    
    public HuntsSubmitManager.ActivityType ActivityType { get; private set; }

    public void SetUpTask(HuntsSubmitManager.ActivityType activityType, Sprite chosenActivityIcon, Sprite chosenPointsIcon)
    {
        ActivityType = activityType;
        activityName.text = HuntsSubmitManager.EnumToString(activityType);
        activityIcon.sprite = chosenActivityIcon;
        pointsIcon.sprite = chosenPointsIcon;
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
}