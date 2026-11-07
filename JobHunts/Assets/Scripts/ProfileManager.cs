using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
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
        descriptionEntry.SetActive(false);
    }
}
