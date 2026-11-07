using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    [Header("Icons")]
    [SerializeField] private List<Sprite> profileIcons;
    
    public static IconManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public Sprite GetProfileIcon()
    {
        string iconNumber = SaveManager.Instance.GetValueByUserName(SaveManager.ProfileIcon, SaveManager.Instance.PlayerName);
        if (iconNumber == string.Empty || !int.TryParse(iconNumber, out int result))
        {
            return profileIcons[0];
        }

        return profileIcons[Math.Clamp(result, 0, profileIcons.Count - 1)];
    }

    public List<Sprite> GetProfileIcons()
    {
        return profileIcons;
    }
}
