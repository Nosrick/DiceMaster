using System.Collections.Generic;

public abstract class BasePlayer
{
    protected List<DiceObject> m_Deck;
    protected List<DiceObject> m_Hand;
    protected List<DiceObject> m_Play;

    public BasePlayer(List<DiceObject> diceRef)
    {
        m_Deck = diceRef;
        m_Hand = new List<DiceObject>();
        m_Play = new List<DiceObject>();
    }

    public abstract bool Draw();
    public abstract bool Play();
    public abstract bool Special(DiceObject diceRef);

    public List<DiceObject> Deck
    {
        get
        {
            List<DiceObject> dice = new List<DiceObject>(m_Deck);
            return dice;
        }
    }

    public List<DiceObject> Hand
    {
        get
        {
            List<DiceObject> dice = new List<DiceObject>(m_Hand);
            return dice;
        }
    }

    public List<DiceObject> Played
    {
        get
        {
            List<DiceObject> dice = new List<DiceObject>(m_Play);
            return dice;
        }
    }
}