public abstract class DiceFace
{
    public DiceFace(int levelRef, FaceType faceRef)
    {
        Level = levelRef;
        Face = faceRef;
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
}

public class PlayFace : DiceFace
{
    public PlayFace(int attackRef, int toughnessRef, int levelRef, FaceType faceRef) : base(levelRef, faceRef)
    {
        Attack = attackRef;
        Toughness = Toughness;
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
    public SpecialFace(BaseSpecial specialRef, int levelRef, FaceType faceRef) : base(levelRef, faceRef)
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