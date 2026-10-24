using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageSwitcher : MonoBehaviour
{
    [SerializeField] private TMP_Text txt;
    
    [Header("Pages")]
    [SerializeField] private GameObject homePage;
    [SerializeField] private GameObject tasksPage;
    [SerializeField] private GameObject huntsPage;
    [SerializeField] private GameObject ranksPage;
    [SerializeField] private GameObject profilePage;

    [Header("Normal Icons")]
    [SerializeField] private Sprite homePageIcon;
    [SerializeField] private Sprite tasksPageIcon;
    [SerializeField] private Sprite huntsPageIcon;
    [SerializeField] private Sprite ranksPageIcon;
    [SerializeField] private Sprite profilePageIcon;
    
    [Header("Highlighted Icons")]
    [SerializeField] private Sprite homePageHighlightedIcon;
    [SerializeField] private Sprite tasksPageHighlightedIcon;
    [SerializeField] private Sprite huntsPageHighlightedIcon;
    [SerializeField] private Sprite ranksPageHighlightedIcon;
    [SerializeField] private Sprite profilePageHighlightedIcon;
    
    [Header("Images")]
    [SerializeField] private Image homePageImage;
    [SerializeField] private Image tasksPageImage;
    [SerializeField] private Image huntsPageImage;
    [SerializeField] private Image ranksPageImage;
    [SerializeField] private Image profilePageImage;

    public GameObject CurrentPage { get; set; }
    
    public Sprite NonHighlightedCurrentIcon { get; set; }
    
    public Image CurrentImage { get; set; }

    private void Awake()
    {
        CurrentPage = homePage;
        NonHighlightedCurrentIcon = homePageIcon;
        CurrentImage = homePageImage;
        txt.text = "Job Hunts";
        SetActiveHomePageOnly();
        SetActiveHomePageImageOnly();
    }

    public void OpenHomePage()
    {
        OpenNewPage(homePage, "Job Hunts");
        SetNewHighlightedIcon(homePageImage, homePageHighlightedIcon, homePageIcon);
    }
    
    public void OpenTasksPage()
    {
        OpenNewPage(tasksPage, "Daily Quests");
        SetNewHighlightedIcon(tasksPageImage, tasksPageHighlightedIcon, tasksPageIcon);
    }
    
    public void OpenHuntsPage()
    {
        OpenNewPage(huntsPage, "Log your activity");
        SetNewHighlightedIcon(huntsPageImage, huntsPageHighlightedIcon, huntsPageIcon);
    }
    
    public void OpenRanksPage()
    {
        OpenNewPage(ranksPage, "Overall ranks");
        SetNewHighlightedIcon(ranksPageImage, ranksPageHighlightedIcon, ranksPageIcon);
    }
    
    public void OpenProfilePage()
    {
        OpenNewPage(profilePage, "Your profile");
        SetNewHighlightedIcon(profilePageImage, profilePageHighlightedIcon, profilePageIcon);
    }

    private void OpenNewPage(GameObject newPage, string headerText)
    {
        CurrentPage.SetActive(false);
        newPage.SetActive(true);
        CurrentPage = newPage;
        txt.text = headerText;
    }

    private void SetNewHighlightedIcon(Image newImage, Sprite highlightedIcon, Sprite normalIcon)
    {
        CurrentImage.sprite = NonHighlightedCurrentIcon;
        newImage.sprite = highlightedIcon;
        CurrentImage = newImage;
        NonHighlightedCurrentIcon = normalIcon;
    }

    private void SetActiveHomePageOnly()
    {
        homePage.SetActive(true);
        tasksPage.SetActive(false);
        huntsPage.SetActive(false);
        ranksPage.SetActive(false);
        profilePage.SetActive(false);
    }

    private void SetActiveHomePageImageOnly()
    {
        homePageImage.sprite = homePageHighlightedIcon;
        tasksPageImage.sprite = tasksPageIcon;
        huntsPageImage.sprite = huntsPageIcon;
        ranksPageImage.sprite = ranksPageIcon;
        profilePageImage.sprite = profilePageIcon;
    }
}
