using UnityEngine;

public abstract class DiceFace
{
    public DiceFace()
    {
        Level = 1;
        Face = FaceType.Play;
    }

    public DiceFace(int levelRef, FaceType faceRef, Color textColourRef, Color dieColourRef)
    {
        Level = levelRef;
        Face = faceRef;

        TextColour = textColourRef;
        DieColour = dieColourRef;
    }

    public int Level
    {
        get;
        protected set;
    }

    public FaceType Face
    {
        get;
        protected set;
    }

    public Color TextColour
    {
        get;
        protected set;
    }

    public Color DieColour
    {
        get;
        protected set;
    }
}

public class PlayFace : DiceFace
{
    public PlayFace() : base()
    {
        Attack = 1;
        Toughness = 1;
    }

    public PlayFace(int attackRef, int toughnessRef, int levelRef, FaceType faceRef, Color textColourRef, Color dieColourRef) : base(levelRef, faceRef, textColourRef, dieColourRef)
    {
        Attack = attackRef;
        Toughness = toughnessRef;
    }

    public int Attack
    {
        get;
        protected set;
    }

    public int Toughness
    {
        get;
        protected set;
    }
}

public class SpecialFace : DiceFace
{
    public SpecialFace() : base()
    {
        Special = null;
    }

    public SpecialFace(BaseSpecial specialRef, int levelRef, FaceType faceRef, Color textColourRef, Color dieColourRef) : base(levelRef, faceRef, textColourRef, dieColourRef)
    { 
        Special = specialRef;
    }

    public BaseSpecial Special
    {
        get;
        protected set;
    }
}

public enum FaceType
{
    Play,
    Special
}