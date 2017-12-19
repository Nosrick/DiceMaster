public abstract class BaseSpecial
{
    public BaseSpecial(TargetType targetRef, string nameRef)
    {
        Target = targetRef;
        Name = nameRef;
    }

    public abstract bool DoGlobal(GameManager managerRef);
    public abstract bool DoPlayer(BasePlayer playerRef);
    public abstract bool DoDie(DiceObject dieRef);

    public TargetType Target
    {
        get;
        protected set;
    }

    public string Name
    {
        get;
        protected set;
    }
}

public enum TargetType
{
    Global,
    Player,
    Die
}