using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private GameObject descriptionEntry;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text description;

    private void OnEnable()
    {
        descriptionEntry.SetActive(false);
        
        if (GameManager.Instance == null)
        {
            return;
        }

        pointsText.text = $"{GameManager.Instance.CurrentScore}";
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
