using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public const string PasswordKey = "Password";
    public const string ProfileDescription = "ProfileDescription";
    public const string Score = "Score";
    public const string ProfileIcon = "ProfileIcon";
    
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_Text invalid;
    [SerializeField] private DailyTaskManager dailyTaskManager;
    
    public static SaveManager Instance { get; private set; }
    
    public string PlayerName { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ClearSignInInfo()
    {
        usernameInput.text = string.Empty;
        passwordInput.text = string.Empty;
        invalid.gameObject.SetActive(false);
    }

    public void SignIn()
    {
        if (usernameInput.text == string.Empty)
        {
            invalid.gameObject.SetActive(true);
            invalid.text = "Username not inputted - Please input Username!";
            GameManager.Instance.PlayButtonClick();
            return;
        }
        
        if (passwordInput.text == string.Empty)
        {
            invalid.gameObject.SetActive(true);
            invalid.text = "Password not inputted - Please input Password!";
            GameManager.Instance.PlayButtonClick();
            return;
        }

        string properPassword = GetValueByUserName(PasswordKey, usernameInput.text);
        if (properPassword == string.Empty)
        {
            SaveValueByUserName(PasswordKey, usernameInput.text, passwordInput.text);
            GoToHomePage();
            UpdateStats(usernameInput.text);
            return;
        }

        if (properPassword != passwordInput.text)
        {
            invalid.gameObject.SetActive(true);
            invalid.text = "Wrong password for associated username!";
            GameManager.Instance.PlayButtonClick();
            return;
        }

        GoToHomePage();
        UpdateStats(usernameInput.text);
    }

    public List<string> GetAllUserNames()
    {
        string everything = PlayerPrefs.GetString(PasswordKey, string.Empty);
        string[] entries = everything.Split("), ");
        List<string> userNames = new();
        
        foreach (string entry in entries)
        {
            string cleaned = entry.Replace("(", "").Replace(")", "");
            string[] parts = cleaned.Split(", ");
            userNames.Add(parts[0]);
        }

        return userNames;
    }

    public string GetValueByUserName(string key, string userName)
    {
        string everything = PlayerPrefs.GetString(key, string.Empty);
        string[] entries = everything.Split("), ");

        foreach (string entry in entries)
        {
            string cleaned = entry.Replace("(", "").Replace(")", "");
            string[] parts = cleaned.Split(", ");

            if (parts[0] == userName)
            {
                return parts[1];
            }
        }

        return string.Empty;
    }

    public void SaveValueByUserName(string key, string userName, string value)
    {
        string everything = PlayerPrefs.GetString(key, string.Empty);
        string[] entries = everything == string.Empty
            ? Array.Empty<string>()
            : everything.Split("), ");


        bool found = false;

        for (int i = 0; i < entries.Length; i++)
        {
            string cleaned = entries[i].Trim('(', ')');
            string[] parts = cleaned.Split(", ");

            if (parts[0] == userName)
            {
                entries[i] = $"{userName}, {value}";
                found = true;
                break;
            }
            
            entries[i] = cleaned;
        }

        if (!found)
        {
            entries = entries.Append($"{userName}, {value}").ToArray();
        }
        
        string result = string.Join("), (", entries.Select(e => e.Trim('(', ')')));
        result = $"({result})";

        PlayerPrefs.SetString(key, result);
        PlayerPrefs.Save();
    }

    private void GoToHomePage()
    {
        PageSwitcher.Instance.SetActiveHomePageOnly();
        PageSwitcher.Instance.SetActiveHomePageImageOnly();
        PlayerName = usernameInput.text;
        GameManager.Instance.PlayChangePage();
    }

    private void UpdateStats(string userName)
    {
        string score = GetValueByUserName(Score, userName);
        if (score != string.Empty && int.TryParse(score, out int scoreNumber))
        {
            GameManager.Instance.CurrentScore = scoreNumber;
        }
        else
        {
            GameManager.Instance.CurrentScore = 0;
        }
        
        dailyTaskManager.ResetTasks();
    }
}