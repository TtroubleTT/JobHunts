using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

public class DailyTaskManager : MonoBehaviour
{
    [SerializeField] private GameObject taskPrefab;
    [SerializeField] private Sprite completedActivityIcon;
    [SerializeField] private GameObject completedLine;
    
    [Header("Activity Icons")] 
    [SerializeField] private Sprite applyToJobIcon;
    [SerializeField] private Sprite portfolioIcon;
    [SerializeField] private Sprite coffeeIcon;
    [SerializeField] private Sprite linkedinIcon;

    [Header("Points Icon")] 
    [SerializeField] private Sprite points100Icon;
    [SerializeField] private Sprite points75Icon;
    [SerializeField] private Sprite points50Icon;
    [SerializeField] private Sprite points25Icon;

    [Header("Completed Points Icon")] 
    [SerializeField] private Sprite completedPoints100;
    [SerializeField] private Sprite completedPoints75;
    [SerializeField] private Sprite completedPoints50;
    [SerializeField] private Sprite completedPoints25;
    
    public static List<TaskLayout> Entries { get; private set; } = new();

    public static List<GameObject> CurrentEntries { get; private set; } = new();

    public class TaskLayout
    {
        public TaskLayout(HuntsSubmitManager.ActivityType activityType, Sprite activityIcon, Sprite pointsIcon, Sprite completedPointsIcon)
        {
            ActivityType = activityType;
            ActivityIcon = activityIcon;
            PointsIcon = pointsIcon;
            CompletedPointsIcon = completedPointsIcon;
        }
        
        public HuntsSubmitManager.ActivityType ActivityType;

        public Sprite ActivityIcon;

        public Sprite PointsIcon;

        public Sprite CompletedPointsIcon;

        public bool IsCompleted;
    }

    public static void CompleteTask(HuntsSubmitManager.ActivityType completedActivity)
    {
        foreach (TaskLayout task in Entries)
        {
            if (task.ActivityType != completedActivity)
            {
                continue;
            }

            task.IsCompleted = true;
            break;
        }
    }

    public void Awake()
    {
        Entries.Add(new TaskLayout(HuntsSubmitManager.ActivityType.ApplyJob, applyToJobIcon, points100Icon, completedPoints100));
        Entries.Add(new TaskLayout(HuntsSubmitManager.ActivityType.PortfolioUpdate, portfolioIcon, points50Icon, completedPoints50));
        Entries.Add(new TaskLayout(HuntsSubmitManager.ActivityType.CoffeeChat, coffeeIcon, points75Icon, completedPoints75));
        Entries.Add(new TaskLayout(HuntsSubmitManager.ActivityType.LinkedInConnection, linkedinIcon, points25Icon, completedPoints25));
    }

    public void OnEnable()
    {
        SetUpEntries();
    }

    public void SetUpEntries()
    {
        foreach (GameObject obj in CurrentEntries)
        {
            Destroy(obj);
        }
        
        CurrentEntries.Clear();
        completedLine.SetActive(false);
        bool setUpCompleted = false;

        Entries = Entries.OrderBy(x => x.IsCompleted).ToList();
        foreach (TaskLayout layout in Entries)
        {
            GameObject gameObj = Instantiate(taskPrefab, transform);
            if (!gameObj.TryGetComponent(out Task task))
            {
                Debug.Log("couldn't get task script from game object");
                continue;
            }

            if (!layout.IsCompleted)
            {
                task.SetUpTask(layout.ActivityType, layout.ActivityIcon, layout.PointsIcon);
            }
            else
            {
                task.SetUpCompleted(layout.ActivityType, completedActivityIcon, layout.CompletedPointsIcon);
                if (!setUpCompleted)
                {
                    setUpCompleted = true;
                    completedLine.SetActive(true);
                    completedLine.transform.SetSiblingIndex(gameObj.transform.GetSiblingIndex() - 1);
                }
            }

            CurrentEntries.Add(gameObj);
        }
    }
}