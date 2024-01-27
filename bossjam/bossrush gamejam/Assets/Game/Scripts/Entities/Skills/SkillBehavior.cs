using UnityEngine;


public class SkillBehavior : MonoBehaviour, ISkillBehavior

{
    public virtual void PerformSkillAction()
    {
      
    }
}

public interface ISkillBehavior
{
    void PerformSkillAction();
}