using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    [Header("Profile Icons")]
    [SerializeField] private Sprite playerIcon;
    [SerializeField] private Sprite taylorIcon;
    [SerializeField] private Sprite laurenIcon;
    [SerializeField] private Sprite ameliaIcon;
    [SerializeField] private Sprite kyleIcon;

    [Header("Section Setups")]
    [SerializeField] private List<Image> profileIcons;
    [SerializeField] private List<TMP_Text> names;
    [SerializeField] private List<TMP_Text> scores;

    [Header("Your Standing")] 
    [SerializeField] private TMP_Text playerScore;
    [SerializeField] private TMP_Text playerRank;
    [SerializeField] private TMP_Text userName;

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

    public List<Entry> PlaceholderEntries { get; private set; } = new();

    public void Awake()
    {
        PlaceholderEntries.Add(new Entry("Taylor", 325, taylorIcon));
        PlaceholderEntries.Add(new Entry("Lauren", 175, laurenIcon));
        PlaceholderEntries.Add(new Entry("Amelia", 100, ameliaIcon));
        PlaceholderEntries.Add(new Entry("Kyle", 50, kyleIcon));
    }

    public void OnEnable()
    {
        int currentScore = 0;
        if (GameManager.Instance != null)
        {
            currentScore = GameManager.Instance.CurrentScore;
        }
        
        UpdateScoreBoard(currentScore);
        UpdateYourStanding();
    }
    
    public int GetPlayerRank()
    {
        List<Entry> entries = new();
        entries.AddRange(PlaceholderEntries);
        entries.Add(new Entry(SaveManager.Instance.PlayerName, GameManager.Instance.CurrentScore, playerIcon));
        List<Entry> orderedEntries = entries.OrderByDescending(x => x.Score).ToList();

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

    private void UpdateScoreBoard(int currentScore)
    {
        List<Entry> entries = new();
        entries.AddRange(PlaceholderEntries);
        entries.Add(new Entry(SaveManager.Instance.PlayerName, currentScore, playerIcon));
        List<Entry> orderedEntries = entries.OrderByDescending(x => x.Score).ToList();

        for (int i = 0; i < orderedEntries.Count; i++)
        {
            Entry entry = orderedEntries[i];

            TMP_Text playerName = names[i];
            playerName.text = entry.Name;

            TMP_Text score = scores[i];
            score.text = entry.Score.ToString();

            Image profileIcon = profileIcons[i];
            profileIcon.sprite = entry.Icon;
        }
    }

    private void UpdateYourStanding()
    {
        playerScore.text = GameManager.Instance.CurrentScore.ToString();
        playerRank.text = GetPlayerRank().ToString();
        userName.text = $"{SaveManager.Instance.PlayerName} - You";
    }
}
