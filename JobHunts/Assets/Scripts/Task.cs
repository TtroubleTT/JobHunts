using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    [SerializeField] private TMP_Text activityName;
    [SerializeField] private Image activityIcon;
    [SerializeField] private Image pointsIcon;

    public void SetUpTask(string chosenName, Sprite chosenActivityIcon, Sprite chosenPointsIcon)
    {
        activityName.text = chosenName;
        activityIcon.sprite = chosenActivityIcon;
        pointsIcon.sprite = chosenPointsIcon;
    }
}