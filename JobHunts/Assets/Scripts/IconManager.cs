using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    [Header("Icons")]
    [SerializeField] private List<Sprite> profileIcons;
    [SerializeField] private List<int> profilePointThreshold;

    [Header("Banners")] 
    [SerializeField] private List<Sprite> banners;
    [SerializeField] private List<int> bannerPointThreshold;
    
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

    public Sprite GetProfileIcon(string userName)
    {
        string iconNumber = SaveManager.Instance.GetValueByUserName(SaveManager.ProfileIcon, userName);
        if (iconNumber == string.Empty || !int.TryParse(iconNumber, out int result))
        {
            return profileIcons[0];
        }

        return profileIcons[Math.Clamp(result, 0, profileIcons.Count - 1)];
    }

    public List<Sprite> GetProfileIcons()
    {
        List<Sprite> icons = new();
        for (int i = 0; i < profileIcons.Count; i++)
        {
            if (GameManager.Instance.CurrentScore < profilePointThreshold[i])
            {
                continue;
            }
            
            icons.Add(profileIcons[i]);
        }

        return icons;
    }
    
    public Sprite GetBanner(string userName)
    {
        string bannerNumber = SaveManager.Instance.GetValueByUserName(SaveManager.Banner, userName);
        if (bannerNumber == string.Empty || !int.TryParse(bannerNumber, out int result))
        {
            return banners[0];
        }

        return banners[Math.Clamp(result, 0, profileIcons.Count - 1)];
    }

    public List<Sprite> GetBanners()
    {
        List<Sprite> filteredBanners = new();
        for (int i = 0; i < banners.Count; i++)
        {
            if (GameManager.Instance.CurrentScore < bannerPointThreshold[i])
            {
                continue;
            }
            
            filteredBanners.Add(banners[i]);
        }

        return filteredBanners;
    }
}
