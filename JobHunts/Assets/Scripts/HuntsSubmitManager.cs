using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HuntsSubmitManager : MonoBehaviour
{
    public enum ActivityType
    {
        ApplyJob,
        PortfolioUpdate,
        CoffeeChat,
        LinkedInConnection,
    }
    
    [SerializeField] private GameObject huntsOptions;
    [SerializeField] private GameObject submitSection;
    [SerializeField] private GameObject completedSection;
    [SerializeField] private TMP_Text activityType;
    [SerializeField] private TMP_Text pointsValue;
    [SerializeField] private TMP_Text header;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text completedPoints;
    [SerializeField] private TMP_Text totalPoints;
    
    public int PointValue { get; set; }
    
    public ActivityType Activity { get; set; }
    
    public static HuntsSubmitManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        huntsOptions.SetActive(true);
        submitSection.SetActive(false);
        completedSection.SetActive(false);
    }

    public void SubmitJob()
    {
        OpenSubmitSection(ActivityType.ApplyJob);
        UpdatePoints(100);
        Activity = ActivityType.ApplyJob;
    }

    public void SubmitPortfolio()
    {
        OpenSubmitSection(ActivityType.PortfolioUpdate);
        UpdatePoints(50);
        Activity = ActivityType.PortfolioUpdate;
    }

    public void SubmitCoffee()
    {
        OpenSubmitSection(ActivityType.CoffeeChat);
        UpdatePoints(75);
        Activity = ActivityType.CoffeeChat;
    }

    public void SubmitLinkedIn()
    {
        OpenSubmitSection(ActivityType.LinkedInConnection);
        UpdatePoints(25);
        Activity = ActivityType.LinkedInConnection;
    }

    public void OpenSubmitSection(ActivityType activity)
    {
        string huntName = EnumToString(activity);
        inputField.text = string.Empty;
        huntsOptions.SetActive(false);
        submitSection.SetActive(true);
        completedSection.SetActive(false);
        activityType.text = $"Activity Type - {huntName}";
    }

    public void UpdatePoints(int points)
    {
        PointValue = points;
        pointsValue.text = $"+{points}";
    }

    public void OpenCompletedSection()
    {
        GameManager.Instance.CurrentScore += PointValue;
        completedPoints.text = $"+{PointValue}";
        totalPoints.text = $"{GameManager.Instance.CurrentScore}";
        header.text = "Well played";
        huntsOptions.SetActive(false);
        submitSection.SetActive(false);
        completedSection.SetActive(true);
        GameManager.Instance.PlaySuccess();
        DailyTaskManager.CompleteTask(Activity);
    }

    public static string EnumToString(ActivityType activity)
    {
        if (activity == ActivityType.ApplyJob)
        {
            return "Job Application";
        }

        if (activity == ActivityType.PortfolioUpdate)
        {
            return "Portfolio Update";
        }

        if (activity == ActivityType.CoffeeChat)
        {
            return "Coffee Chat";
        }

        if (activity == ActivityType.LinkedInConnection)
        {
            return "LinkedIn Connection";
        }

        return "Taylor Messed Up";
    }
}
