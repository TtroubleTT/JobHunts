using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerChoice : MonoBehaviour
{
    public void UpdateBannerChoice()
    {
        SaveManager.Instance.SaveValueByUserName(SaveManager.Banner, SaveManager.Instance.PlayerName, transform.GetSiblingIndex().ToString());
        ProfileManager.Instance.CloseChooseBanner();
        GameManager.Instance.PlayButtonClick();
    }
}
