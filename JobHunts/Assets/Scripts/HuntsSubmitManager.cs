using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HuntsSubmitManager : MonoBehaviour
{
    [SerializeField] private GameObject huntsOptions;
    [SerializeField] private GameObject submitSection;
    [SerializeField] private GameObject completedSection;
    [SerializeField] private TMP_Text activityType;
    [SerializeField] private TMP_Text pointsValue;
    [SerializeField] private TMP_Text header;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text completedPoints;
    [SerializeField] private TMP_Text totalPoints;
    
    public string HuntName { get; set; }
    
    public int PointValue { get; set; }

    private void OnEnable()
    {
        huntsOptions.SetActive(true);
        submitSection.SetActive(false);
        completedSection.SetActive(false);
    }

    public void OpenSubmitSection(string huntName)
    {
        inputField.text = string.Empty;
        HuntName = huntName;
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
    }
}
