using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string PasswordKey = "Password";
    
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_Text invalid;
    
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

    private void OnEnable()
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
            PageSwitcher.Instance.SetActiveHomePageOnly();
            PageSwitcher.Instance.SetActiveHomePageImageOnly();
            PlayerName = usernameInput.text;
            GameManager.Instance.PlayChangePage();
            return;
        }

        if (properPassword != passwordInput.text)
        {
            invalid.gameObject.SetActive(true);
            invalid.text = "Wrong password for associated username!";
            GameManager.Instance.PlayButtonClick();
            return;
        }
        
        PageSwitcher.Instance.SetActiveHomePageOnly();
        PageSwitcher.Instance.SetActiveHomePageImageOnly();
        PlayerName = usernameInput.text;
        GameManager.Instance.PlayChangePage();
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
        string[] entries = Array.Empty<string>();
        if (everything != string.Empty)
        {
            entries = everything.Split("), ");
        }

        bool found = false;

        for (int i = 0; i < entries.Length; i++)
        {
            string cleaned = entries[i].Replace("(", "").Replace(")", "");
            string[] parts = cleaned.Split(", ");

            if (parts[0] == userName)
            {
                entries[i] = $"({userName}, {value})";
                found = true;
                break;
            }
        }

        if (!found)
        {
            entries = entries.Append($"({userName}, {value})").ToArray();
        }

        PlayerPrefs.SetString(key, string.Join(", ", entries));
        PlayerPrefs.Save();
    }
}