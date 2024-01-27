using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SkillManager : MonoBehaviour
{
    private GameController gameController;

    private Dictionary<int, Action> skillDictionary = new Dictionary<int, Action>();
    private float time;
    private Card actualCard;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        time = gameController.timeSettings.fullTime;
        gameController.playerSettings.speed = 5f;
        InicializarDicionarioDeHabilidades();
    }

    void InicializarDicionarioDeHabilidades()
    {
        skillDictionary.Add(1, () => DestrezaSkills.WoodLegs(gameController));
        skillDictionary.Add(2, () => DestrezaSkills.SteelLegs(gameController));
        skillDictionary.Add(3, () => DestrezaSkills.BulletHell(gameController));
        skillDictionary.Add(4, () => DestrezaSkills.HellFist(gameController));
        skillDictionary.Add(5, () => DestrezaSkills.ThunderStomp(gameController));


        skillDictionary.Add(6, () => MaestriaSkills.DoubleDash(gameController));
        skillDictionary.Add(7, () => MaestriaSkills.WarriorOfDarkness(gameController));
        skillDictionary.Add(8, () => MaestriaSkills.DemonicInsults(gameController));
        skillDictionary.Add(9, () => MaestriaSkills.YouVsYou(gameController));
        skillDictionary.Add(10, () => MaestriaSkills.SpongyBoots(gameController));

        skillDictionary.Add(11, () => VigorSkills.BurningBack(gameController));
        skillDictionary.Add(12, () => VigorSkills.HardHitter(gameController));
        skillDictionary.Add(13, () => VigorSkills.IndomitableSprit(gameController));
        skillDictionary.Add(14, () => VigorSkills.Superman(gameController));
        skillDictionary.Add(15, () => VigorSkills.FireMetal(gameController));

    }


    public void ActivateSkill(Card card)
    {

        int idSkill = card.id;
        actualCard = card;

        if (skillDictionary.ContainsKey(idSkill))
        {
            skillDictionary[idSkill]?.Invoke();
        }
        else
        {
            Debug.LogWarning("Unknown skill ID: " + idSkill);
        }


    }




    public static class DestrezaSkills
    {

        public static void WoodLegs(GameController gameController)
        {
            gameController.playerSettings.speed = 5f;
        }

        public static void SteelLegs(GameController gameController)
        {
            gameController.playerSettings.speed += gameController.playerSettings.speed * 0.30f;
        }

        public static void BulletHell(GameController gameController)
        {

        }

        public static void HellFist(GameController gameController)
        {

        }

        public static void ThunderStomp(GameController gameController)
        {

        }


    }

    public static class MaestriaSkills
    {

        public static void DoubleDash(GameController gameController)
        {

        }

        public static void WarriorOfDarkness(GameController gameController)
        {

        }

        public static void DemonicInsults(GameController gameController)
        {

        }

        public static void YouVsYou(GameController gameController)
        {

        }

        public static void SpongyBoots(GameController gameController)
        {

        }
    }

    public static class VigorSkills
    {

        public static void BurningBack(GameController gameController)
        {

        }

        public static void HardHitter(GameController gameController)
        {

        }

        public static void IndomitableSprit(GameController gameController)
        {

        }

        public static void Superman(GameController gameController)
        {

        }

        public static void FireMetal(GameController gameController)
        {

        }
    }



}
