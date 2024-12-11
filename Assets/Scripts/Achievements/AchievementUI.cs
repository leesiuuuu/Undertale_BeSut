using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }
}
public class AchiList
{
    Sprite Icon;
    string Title;
    string context;
    string ID;
    public AchiList(Sprite Icon, string Title, string context, string ID)
    {
        this.Icon = Icon;
        this.Title = Title;
        this.context = context;
        this.ID = ID;
    }
}
