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

    public override bool Draw()
    {
        if(m_Hand.Count == 6)
        {
            return false;
        }

        while (m_Hand.Count < 6)
        {
            DiceObject next = m_Deck[0];
            next.Draw();
            m_Deck.RemoveAt(0);
            m_Hand.Add(next);
        }

        MoveHand();
        return true;
    }

    private void MoveHand()
    {
        for (int i = 0; i < m_Hand.Count; i++)
        {
            int j = i % 2;
            m_Hand[i].transform.position = new Vector3((i / 2 * 4), 4.0f, -(5.0f * (j + 1)));
        }
    }

    public override bool Play()
    {
        int levelsToSpend = 0;
        int levelsToPlay = 0;

        List<DiceObject> toPlay = new List<DiceObject>();
        foreach (DiceObject die in m_Hand)
        {
            if(die.Selected)
            {
                levelsToPlay += die.FaceUp.Level;
                toPlay.Add(die);
            }
            else
            {
                levelsToSpend += die.FaceUp.Level;
            }
        }

        if (levelsToSpend < levelsToPlay)
        {
            return false;
        }

        int levelsLeft = levelsToSpend;
        foreach(DiceObject die in m_Hand)
        {
            if(die.Selected == true)
            {
                continue;
            }

            levelsLeft -= die.FaceUp.Level;
            die.Spend();
            if(levelsLeft <= 0)
            {
                break;
            }
        }

        for(int i = 0; i < toPlay.Count; i++)
        {
            m_Hand.Remove(toPlay[i]);
            m_Play.Add(toPlay[i]);
            toPlay[i].Lower();
            toPlay[i].Play();
            toPlay[i].transform.position = toPlay[i].transform.position + new Vector3(0.0f, 0.0f, 10.0f);
        }

        return true;
    }

    public override bool Special(DiceObject diceRef)
    {
        return false;
    }
}