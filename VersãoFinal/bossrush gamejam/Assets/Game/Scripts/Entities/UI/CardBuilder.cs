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

        cards.Add(new Card { title = "Wood Legs", description = "Habilita você a correr", type = "destreza", timeRequired = 0.5f, isMechanical = false, isCardUsed = false, id = 0, art = sprites[0] });

        cards.Add(new Card { title = "Steel Legs", description = "Faz com que você corra 50% mais rápido", type = "destreza", timeRequired = 0.5f, isMechanical = false, isCardUsed = false, id = 0, art = sprites[1] });

        cards.Add(new Card { title = "Bullet Hell", description = "Cooldown do Dash Reduzido", type = "destreza", timeRequired = 0.5f, isMechanical = false, isCardUsed = false, id = 0, art = sprites[2] });

        cards.Add(new Card { title = "Hell Fist", description = "40% de velocidade de ataque da arte", type = "destreza", timeRequired = 0.5f, isMechanical = false, isCardUsed = false, id = 0, art = sprites[3] });

        cards.Add(new Card { title = "Tjunder stomp", description = "Dash mais rápido", type = "destreza", timeRequired = 0.5f, isMechanical = false, isCardUsed = false, id = 0, art = sprites[4] });

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

        cards.Add(new Card { title = "Double Dash", description = "Permite que você dê dois dashs simultâneos", type = "maestria", timeRequired = 0.5f, isMechanical = false, isCardUsed = false, id = 0, art = sprites[0] });

        cards.Add(new Card { title = "Warrior of Darkness", description = "Se limite apenas a ataques curtos.", type = "maestria", timeRequired = 0.5f, isMechanical = false, isCardUsed = false, id = 0, art = sprites[1] });

        cards.Add(new Card { title = "Demonic Insults", description = "+30% Damage", type = "maestria", timeRequired = 0.5f, isMechanical = false, isCardUsed = false, id = 0, art = sprites[2] });

        cards.Add(new Card { title = "You vs You", description = "+1 club head", type = "maestria", timeRequired = 0.5f, isMechanical = false, isCardUsed = false, id = 0, art = sprites[3] });

        cards.Add(new Card { title = "SpongyBoots", description = "?", type = "maestria", timeRequired = 0.5f, isMechanical = false, isCardUsed = false, id = 0, art = sprites[4] });

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

        cards.Add(new Card { title = "Burning Back", description = "+1 HP", type = "vigor", timeRequired = 0.5f, isMechanical = false, isCardUsed = false, id = 0, art = sprites[0] });

        cards.Add(new Card { title = "Hard Hitter", description = "Seu ataque faz sangrar.", type = "vigor", timeRequired = 0.5f, isMechanical = false, isCardUsed = false, id = 0, art = sprites[1] });

        cards.Add(new Card { title = "Superman", description = "+1 ?", type = "vigor", timeRequired = 0.5f, isMechanical = false, isCardUsed = false, id = 0, art = sprites[2] });

        cards.Add(new Card { title = "Indomitable Sprit ", description = "Recupera +1 Coração a cada minuto.", type = "vigor", timeRequired = 0.5f, isMechanical = false, isCardUsed = false, id = 0, art = sprites[3] });

        cards.Add(new Card { title = "Fire Metal", description = "Cada Hit faz o Boss queimar", type = "vigor", timeRequired = 0.5f, isMechanical = false, isCardUsed = false, id = 0, art = sprites[4] });

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