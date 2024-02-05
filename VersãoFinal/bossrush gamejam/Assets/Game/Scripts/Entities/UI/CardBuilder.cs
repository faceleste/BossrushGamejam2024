using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;


public class CardBuilder : MonoBehaviour
{


    public Sprite[] destreza;
    public Sprite[] maestria;
    public Sprite[] vigor;


    public List<Card> listagemDestreza(Sprite[] sprites)
    {
        List<Card> cards = new List<Card>();



        cards.Add(new Card
        {
            title = "Steel Legs",
            description = "Significantly improves your running speed by 50%. The sturdy steel construction enhances both speed and durability.",
            type = "destreza",
            timeRequired = 5,
            isMechanical = false,
            isCardUsed = false,
            id = 0,
            art = sprites[1]
        });

        cards.Add(new Card
        {
            title = "Bullet Hell",
            description = "Dramatically reduces Dash cooldown by 60%, making you more agile and responsive in combat. Effectiveness has been fine-tuned for optimal performance.",
            type = "destreza",
            timeRequired = 8,
            isMechanical = false,
            isCardUsed = false,
            id = 0,
            art = sprites[2]
        });

        cards.Add(new Card
        {
            title = "Hell Fist",
            description = "Boosts your weapon's attack speed by 40% and increases weapon damage by 20%. Unleash a flurry of attacks with this empowering card.",
            type = "destreza",
            timeRequired = 5,
            isMechanical = false,
            isCardUsed = false,
            id = 0,
            art = sprites[3]
        });

        cards.Add(new Card
        {
            title = "Wood Legs",
            description = "Enhances your mobility by allowing you to run. These wooden legs provide a basic level of dexterity.",
            type = "destreza",
            timeRequired = 30,
            isMechanical = false,
            isCardUsed = false,
            id = 0,
            art = sprites[0]
        });
        
        cards.Add(new Card
        {
            title = "Thunder Stomp",
            description = "Provides a faster Dash, allowing you to traverse the battlefield with lightning speed. Perfect for quick repositioning and evading enemy attacks.",
            type = "destreza",
            timeRequired = 35,
            isMechanical = false,
            isCardUsed = false,
            id = 0,
            art = sprites[4]
        });

        if (ValidateCards(cards, sprites))
        {
            return cards;
        }
        else
        {
            Debug.LogError("Erro ao validar cartas, Sprites: " + sprites.Count() + " Cards: " + cards.Count());
        }
        return cards;
    }

    public List<Card> listagemMaestria(Sprite[] sprites)
    {
        List<Card> cards = new List<Card>();

        cards.Add(new Card
        {
            title = "Double Dash",
            description = "Master the art of rapid movement by performing two simultaneous dashes. Enhance your agility and surprise your opponents.",
            type = "maestria",
            timeRequired = 8,
            isMechanical = false,
            isCardUsed = false,
            id = 0,
            art = sprites[0]
        });

        cards.Add(new Card
        {
            title = "Warrior of Darkness",
            description = "Embrace the shadows and limit yourself to short-range attacks. Navigate the battlefield with precision and strike fear into your enemies.",
            type = "maestria",
            timeRequired = 5,
            isMechanical = false,
            isCardUsed = false,
            id = 0,
            art = sprites[1]
        });

        cards.Add(new Card
        {
            title = "Demonic Insults",
            description = "Unleash the power within by infusing your weapon with demonic energy. Enjoy a significant +30% damage boost to your attacks.",
            type = "maestria",
            timeRequired = 5,
            isMechanical = false,
            isCardUsed = false,
            id = 0,
            art = sprites[2]
        });

        cards.Add(new Card
        {
            title = "You vs You",
            description = "Challenge yourself and gain an extra club head..",
            type = "maestria",
            timeRequired = 10,
            isMechanical = false,
            isCardUsed = false,
            id = 0,
            art = sprites[3]
        });

        cards.Add(new Card
        {
            title = "SpongyBoots",
            description = "Mystery surrounds this card. Equip the SpongyBoots and uncover their enigmatic effects. A short time investment may reveal surprising benefits.",
            type = "maestria",
            timeRequired = 0.5f,
            isMechanical = false,
            isCardUsed = false,
            id = 0,
            art = sprites[4]
        });

        if (ValidateCards(cards, sprites))
        {
            return cards;
        }
        else
        {
            Debug.LogError("Erro ao validar cartas, Sprites: " + sprites.Count() + " Cards: " + cards.Count());
        }
        return cards;

    }

    public List<Card> listagemVigor(Sprite[] sprites)
    {

        List<Card> cards = new List<Card>();

        cards.Add(new Card
        {
            title = "Burning Back",
            description = "Boost your vitality with the Burning Back card, gaining an additional +1 HP. Strengthen your endurance and stay in the fight longer.",
            type = "vigor",
            timeRequired = 8,
            isMechanical = false,
            isCardUsed = false,
            id = 0,
            art = sprites[0]
        });

        cards.Add(new Card
        {
            title = "Hard Hitter",
            description = "Infuse your attacks with a potent bleeding effect. Make your enemies suffer from continuous damage with the Hard Hitter card.",
            type = "vigor",
            timeRequired = 8,
            isMechanical = false,
            isCardUsed = false,
            id = 0,
            art = sprites[1]
        });

        cards.Add(new Card
        {
            title = "Superman",
            description = "Feel invigorated with the Superman card, granting you an additional +1 HP. Unleash your inner strength and endure the challenges that lie ahead.",
            type = "vigor",
            timeRequired = 8,
            isMechanical = false,
            isCardUsed = false,
            id = 0,
            art = sprites[2]
        });

        cards.Add(new Card
        {
            title = "Indomitable Spirit",
            description = "Nurture your indomitable spirit with this card, recovering +1 Heart every 2 minutes. Maintain your resilience and face adversity head-on.",
            type = "vigor",
            timeRequired = 5,
            isMechanical = false,
            isCardUsed = false,
            id = 0,
            art = sprites[3]
        });

        cards.Add(new Card
        {
            title = "Fire Metal",
            description = "Inflict the Boss with a burning sensation on each hit using the Fire Metal card. Turn up the heat and gain a strategic advantage in battle.",
            type = "vigor",
            timeRequired = 8,
            isMechanical = false,
            isCardUsed = false,
            id = 0,
            art = sprites[4]
        });
        if (ValidateCards(cards, sprites))
        {
            return cards;
        }
        else
        {
            Debug.LogError("Erro ao validar cartas, Sprites: " + sprites.Count() + " Cards: " + cards.Count());
        }
        return cards;
    }

    private bool ValidateCards(List<Card> cards, Sprite[] sprites)
    {
        if (cards.Count() == sprites.Count())
        {
            return true;
        }
        return false;
    }

}