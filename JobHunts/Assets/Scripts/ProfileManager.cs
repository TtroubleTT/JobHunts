using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    private const string DefaultDescription = "Press edit profile to change me!";
    
    [SerializeField] private LeaderboardManager leaderboardManager;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private GameObject descriptionEntry;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text userNameText;

    private void OnEnable()
    {
        descriptionEntry.SetActive(false);
        
        if (GameManager.Instance == null)
        {
            return;
        }

        userNameText.text = SaveManager.Instance.PlayerName;
        pointsText.text = $"{GameManager.Instance.CurrentScore}";
        rankText.text = $"Rank #{leaderboardManager.GetPlayerRank()}";

        string savedDescription = SaveManager.Instance.GetValueByUserName(SaveManager.ProfileDescription, SaveManager.Instance.PlayerName);
        if (savedDescription == string.Empty)
        {
            savedDescription = DefaultDescription;
        }

        description.text = savedDescription;
    }

    public void OpenDescriptionEntry()
    {
        descriptionEntry.SetActive(true);
        inputField.text = string.Empty;
    }

    public void CloseDescriptionEntry()
    {
        descriptionEntry.SetActive(false);
    }

    public void Submit()
    {
        description.text = inputField.text;
        SaveManager.Instance.SaveValueByUserName(SaveManager.ProfileDescription, SaveManager.Instance.PlayerName, inputField.text);
        descriptionEntry.SetActive(false);
    }
}
