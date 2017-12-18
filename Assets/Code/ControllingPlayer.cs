using System;
using System.Collections.Generic;
using UnityEngine;

public class ControllingPlayer : BasePlayer
{
    public ControllingPlayer(List<DiceObject> diceRef) : base(diceRef)
    {
    }

    public void Shuffle()
    {
        int i = m_Deck.Count;
        while (i > 1)
        {
            i -= 1;
            int j = UnityEngine.Random.Range(0, i);
            DiceObject first = m_Deck[i];
            DiceObject second = m_Deck[j];
            m_Deck[i] = second;
            m_Deck[j] = first;
        }
    }

    public override void Draw()
    {
        if(m_Hand.Count == 6)
        {
            return;
        }

        while (m_Hand.Count < 6)
        {
            DiceObject next = m_Deck[0];
            next.Draw();
            m_Deck.RemoveAt(0);
            m_Hand.Add(next);
        }

        MoveHand();
    }

    private void MoveHand()
    {
        for(int i = 0; i < m_Hand.Count; i++)
        {
            m_Hand[i].transform.position = new Vector3((i * 4) - 8, 4.0f, -5.0f);
        }
    }

    public override void Play(DiceObject diceRef)
    {
        throw new NotImplementedException();
    }

    public override void Special(DiceObject diceRef)
    {
        throw new NotImplementedException();
    }
}