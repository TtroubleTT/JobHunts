using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    [Header("Section Setups")] 
    [SerializeField] private List<Image> profileIcons;
    [SerializeField] private List<TMP_Text> names;
    [SerializeField] private List<TMP_Text> scores;

    [Header("Your Standing")] 
    [SerializeField] private TMP_Text playerScore;
    [SerializeField] private TMP_Text playerRank;
    [SerializeField] private TMP_Text userName;
    [SerializeField] private Image playerIcon;

    public class Entry
    {
        public Entry(string name, int score, Sprite icon)
        {
            Name = name;
            Score = score;
            Icon = icon;
        }

        public string Name;

        public Sprite Icon;

        public int Score;
    }

    public void OnEnable()
    {
        UpdateScoreBoard();
        UpdateYourStanding();
    }
    
    public int GetPlayerRank()
    {
        List<Entry> orderedEntries = GetOrderedEntries();
        for (int i = 0; i < orderedEntries.Count; i++)
        {
            Entry entry = orderedEntries[i];
            if (entry.Name == SaveManager.Instance.PlayerName)
            {
                return i + 1;
            }
        }

        return 0;
    }

    private void UpdateScoreBoard()
    {
        foreach (TMP_Text playerName in names)
        {
            playerName.transform.parent.gameObject.SetActive(false);
        }
        
        List<Entry> orderedEntries = GetOrderedEntries();
        for (int i = 0; i < orderedEntries.Count; i++)
        {
            if (names.Count - 1 < i)
            {
                break;
            }
            
            Entry entry = orderedEntries[i];

            TMP_Text playerName = names[i];
            playerName.text = entry.Name;

            TMP_Text score = scores[i];
            score.text = entry.Score.ToString();

            Image profileIcon = profileIcons[i];
            profileIcon.sprite = entry.Icon;
            
            playerName.transform.parent.gameObject.SetActive(true);
        }
    }

    private void UpdateYourStanding()
    {
        playerScore.text = GameManager.Instance.CurrentScore.ToString();
        playerRank.text = GetPlayerRank().ToString();
        userName.text = $"{SaveManager.Instance.PlayerName} - You";
        playerIcon.sprite = IconManager.Instance.GetProfileIcon(SaveManager.Instance.PlayerName);
    }

    private List<Entry> GetOrderedEntries()
    {
        List<Entry> entries = new();
        foreach (string username in SaveManager.Instance.GetAllUserNames())
        {
            string score = SaveManager.Instance.GetValueByUserName(SaveManager.Score, username);
            int scoreNumber = 0;
            if (score != string.Empty && int.TryParse(score, out int result))
            {
                scoreNumber = result;
            }

            Sprite icon = IconManager.Instance.GetProfileIcon(username);
            entries.Add(new Entry(username, scoreNumber, icon));
        }
        
        List<Entry> orderedEntries = entries.OrderByDescending(x => x.Score).ToList();
        return orderedEntries;
    }
}
