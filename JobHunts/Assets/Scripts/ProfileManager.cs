using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    private const string DefaultDescription = "Press edit profile to change me!";
    
    [SerializeField] private LeaderboardManager leaderboardManager;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private GameObject descriptionEntry;
    [SerializeField] private GameObject iconChoosing;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text userNameText;
    [SerializeField] private Image profileIcon;
    [SerializeField] private GameObject iconChoicePrefab;
    [SerializeField] private Transform iconLayoutGroup;
    
    public static ProfileManager Instance { get; private set; }

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
        descriptionEntry.SetActive(false);
        iconChoosing.SetActive(false);
        
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
        profileIcon.sprite = IconManager.Instance.GetProfileIcon();
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

    public void OpenChooseIcon()
    {
        iconChoosing.SetActive(true);

        foreach (Sprite icon in IconManager.Instance.GetProfileIcons())
        {
            GameObject gameObj = Instantiate(iconChoicePrefab, iconLayoutGroup);
            if (!gameObj.TryGetComponent(out Image image))
            {
                Debug.Log("couldn't get image script from game object");
                continue;
            }

            image.sprite = icon;
        }
    }

    public void CloseChooseIcon()
    {
        iconChoosing.SetActive(false);
        profileIcon.sprite = IconManager.Instance.GetProfileIcon();

        foreach (Transform child in iconLayoutGroup)
        {
            Destroy(child.gameObject);
        }
    }

    public void Submit()
    {
        description.text = inputField.text;
        SaveManager.Instance.SaveValueByUserName(SaveManager.ProfileDescription, SaveManager.Instance.PlayerName, inputField.text);
        descriptionEntry.SetActive(false);
    }
}
