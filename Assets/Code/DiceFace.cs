using System;
using UnityEngine;

public abstract class DiceFace
{
    public DiceFace()
    {
        Level = 1;
        Face = FaceType.Play;

        TextColour = Color.black;
        DieColour = Color.white;

        Name = "DEFAULT";
    }

    public DiceFace(int levelRef, FaceType faceRef, Color textColourRef, Color dieColourRef, string nameRef, Sprite imageRef)
    {
        Level = levelRef;
        Face = faceRef;

        TextColour = textColourRef;
        DieColour = dieColourRef;

        Name = nameRef;
        Image = imageRef;
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

    public string Name
    {
        get;
        protected set;
    }

    public Sprite Image
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

    public PlayFace(int attackRef, int toughnessRef, int levelRef, FaceType faceRef, Color textColourRef, Color dieColourRef, string nameRef, Sprite imageRef) : 
        base(levelRef, faceRef, textColourRef, dieColourRef, nameRef, imageRef)
    {
        Attack = attackRef;
        Toughness = toughnessRef;
    }

    public PlayFace(FaceInfo infoRef)
    {
        Level = infoRef.Level;
        Face = infoRef.Face;
        TextColour = infoRef.TextColour;
        DieColour = infoRef.DieColour;
        Name = infoRef.Name;

        Attack = infoRef.Attack;
        Toughness = infoRef.Toughness;

        Image = SpriteLoader.Get(infoRef.SpriteName);
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

    public SpecialFace(BaseSpecial specialRef, int levelRef, FaceType faceRef, Color textColourRef, Color dieColourRef, string nameRef, Sprite imageRef) : 
        base(levelRef, faceRef, textColourRef, dieColourRef, nameRef, imageRef)
    { 
        Special = specialRef;
    }

    public SpecialFace(FaceInfo infoRef)
    {
        Level = infoRef.Level;
        Face = infoRef.Face;
        TextColour = infoRef.TextColour;
        DieColour = infoRef.DieColour;
        Name = infoRef.Name;

        //TODO
        Special = null;

        Image = SpriteLoader.Get(infoRef.SpriteName);
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

[Serializable]
public class FaceInfo
{
    public int Level;
    public FaceType Face;
    public Color TextColour;
    public Color DieColour;
    public string Name;

    public int Attack;
    public int Toughness;

    public string SpecialName;

    public string SpriteName;

    public FaceInfo(DiceFace faceRef)
    {
        if(faceRef.Face == FaceType.Play)
        {
            PlayFace newFace = (PlayFace)faceRef;

            Level = newFace.Level;
            Face = newFace.Face;
            TextColour = newFace.TextColour;
            DieColour = newFace.DieColour;
            Name = newFace.Name;

            Attack = newFace.Attack;
            Toughness = newFace.Toughness;

            SpecialName = "None";

            if (newFace.Image != null)
            {
                SpriteName = newFace.Image.name;
            }
            else
            {
                SpriteName = "None";
            }
        }
        else if(faceRef.Face == FaceType.Special)
        {
            SpecialFace newFace = (SpecialFace)faceRef;

            Level = newFace.Level;
            Face = newFace.Face;
            TextColour = newFace.TextColour;
            DieColour = newFace.DieColour;
            Name = newFace.Name;

            Attack = 0;
            Toughness = 0;

            SpecialName = newFace.Special.Name;

            if (newFace.Image != null)
            {
                SpriteName = newFace.Image.name;
            }
            else
            {
                SpriteName = "None";
            }
        }
    }
}