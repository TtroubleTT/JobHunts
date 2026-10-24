using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    private const string PlayerName = "Malak";

    [SerializeField] private List<TMP_Text> letters;
    [SerializeField] private List<TMP_Text> names;
    [SerializeField] private List<TMP_Text> scores;

    public class Entry
    {
        public Entry(string name, int score)
        {
            Name = name;
            Letter = name[0].ToString();
            Score = score;
        }
        
        public string Name;

        public string Letter;

        public int Score;
    }

    public List<Entry> PlaceholderEntries { get; private set; } = new()
    {
        new Entry("Taylor", 325),
        new Entry("Lauren", 175),
        new Entry("Amelia", 100),
    };

    public void OnEnable()
    {
        int currentScore = 0;
        if (GameManager.Instance != null)
        {
            currentScore = GameManager.Instance.CurrentScore;
        }
        
        UpdateScoreBoard(currentScore);
    }

    private void UpdateScoreBoard(int currentScore)
    {
        List<Entry> entries = new();
        entries.AddRange(PlaceholderEntries);
        entries.Add(new Entry(PlayerName, currentScore));
        List<Entry> orderedEntries = entries.OrderByDescending(x => x.Score).ToList();

        for (int i = 0; i < orderedEntries.Count; i++)
        {
            Entry entry = orderedEntries[i];
            
            TMP_Text letter = letters[i];
            letter.text = entry.Letter;

            TMP_Text playerName = names[i];
            playerName.text = entry.Name;

            TMP_Text score = scores[i];
            score.text = entry.Score.ToString();
        }
    }
}
