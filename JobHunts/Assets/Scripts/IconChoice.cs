using UnityEngine;

public class IconChoice : MonoBehaviour
{
    public void UpdateIconChoice()
    {
        SaveManager.Instance.SaveValueByUserName(SaveManager.ProfileIcon, SaveManager.Instance.PlayerName, transform.GetSiblingIndex().ToString());
        ProfileManager.Instance.CloseChooseIcon();
        GameManager.Instance.PlayButtonClick();
    }
}
