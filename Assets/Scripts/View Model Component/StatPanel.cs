using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class StatPanel : MonoBehaviour
{
    public Panel panel;
    public Sprite allyBackground;
    public Sprite enemyBackground;
    public Image background;
    public Image avatar;
    public Text nameLabel;
    public Text hpLabel;
    public Text lvLabel;

    public void Display(GameObject obj)
    {
        Alliance alliance = obj.GetComponent<Alliance>();
        background.sprite = alliance.type == Alliances.Enemy ? enemyBackground : allyBackground;
        nameLabel.text = obj.name;
        Stats stats = obj.GetComponent<Stats>();
        if(stats)
        {
            hpLabel.text = string.Format("HP {0} / {1}", stats[StatTypes.HP], stats[StatTypes.MHP]);
            lvLabel.text = string.Format("LV. {0}", stats[StatTypes.LVL]);
        }
    }
}
