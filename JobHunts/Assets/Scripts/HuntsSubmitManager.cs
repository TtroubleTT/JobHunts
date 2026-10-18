using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HuntsSubmitManager : MonoBehaviour
{
    [SerializeField] private GameObject huntsOptions;
    [SerializeField] private GameObject submitSection;
    [SerializeField] private TMP_Text activityType;
    [SerializeField] private TMP_Text pointsValue;
    
    public string HuntName { get; set; }
    
    public int PointValue { get; set; }

    private void OnEnable()
    {
        huntsOptions.SetActive(true);
        submitSection.SetActive(false);
    }

    public void OpenSubmitSection(string huntName)
    {
        HuntName = huntName;
        huntsOptions.SetActive(false);
        submitSection.SetActive(true);
        activityType.text = $"Activity Type - {huntName}";
    }

    public void UpdatePoints(int points)
    {
        PointValue = points;
        pointsValue.text = $"+{points}";
    }
}
