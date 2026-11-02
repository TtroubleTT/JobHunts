using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskManager : MonoBehaviour
{
    [SerializeField] private GameObject taskPrefab;
    
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
    
    public List<TaskLayout> Entries { get; private set; } = new();

    public List<GameObject> CurrentEntries { get; private set; } = new();

    public class TaskLayout
    {
        public TaskLayout(HuntsSubmitManager.ActivityType activityType, Sprite activityIcon, Sprite pointsIcon)
        {
            ActivityType = activityType;
            ActivityIcon = activityIcon;
            PointsIcon = pointsIcon;
        }
        
        public HuntsSubmitManager.ActivityType ActivityType;

        public Sprite ActivityIcon;

        public Sprite PointsIcon;
    }

    public void Awake()
    {
        Entries.Add(new TaskLayout(HuntsSubmitManager.ActivityType.ApplyJob, applyToJobIcon, points100Icon));
        Entries.Add(new TaskLayout(HuntsSubmitManager.ActivityType.PortfolioUpdate, portfolioIcon, points50Icon));
        Entries.Add(new TaskLayout(HuntsSubmitManager.ActivityType.CoffeeChat, coffeeIcon, points75Icon));
        Entries.Add(new TaskLayout(HuntsSubmitManager.ActivityType.LinkedInConnection, linkedinIcon, points25Icon));

        foreach (TaskLayout layout in Entries)
        {
            GameObject gameObj = Instantiate(taskPrefab, transform);
            if (!gameObj.TryGetComponent(out Task task))
            {
                Debug.Log("couldnt get task script from game object");
                continue;
            }
            
            task.SetUpTask(HuntsSubmitManager.EnumToString(layout.ActivityType), layout.ActivityIcon, layout.PointsIcon);
            CurrentEntries.Add(gameObj);
        }
    }
}