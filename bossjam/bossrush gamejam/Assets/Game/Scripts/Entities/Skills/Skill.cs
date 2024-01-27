using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Data/Skill")]
public class Skill : ScriptableObject
{


    [Header("Skill Info")]
    public bool isUnlocked;
    public string skillName;
    public string skillDescription;
    public Sprite skillSprite;
    public float skillPrice;

    [Header("Skill Behavior")]
    public Skill nextSkill;
    public SkillBehavior skillBehavior;



    public void UnlockSkill()
    {
        isUnlocked = true;
    }


    // public void Update()
    // {
    //     if (isUnlocked)
    //     {
    //         skillBehavior.PerformSkillAction();
    //     }
    // }



}
