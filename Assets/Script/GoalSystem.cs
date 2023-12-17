public abstract class Objective
{
    #region Ctors
    public Objective() { }
    #endregion Ctors

    #region Properties
    public abstract string Description { get; }
    public virtual float TargetValue { get; protected set; }
    public bool IsComplete { get; protected set; }
    #endregion Properties

    #region Methods
    public virtual void CompleteGoal() => IsComplete = true;
    #endregion Methods
}
public class IncomeObjective : Objective
{
    #region Members
    private float _baseTarget = 10f;
    #endregion Members

    #region Ctor
    public IncomeObjective(float targetModifier) => TargetValue = targetModifier * _baseTarget;
    #endregion Ctor

    #region Properties
    public override string Description => $"Reach {TargetValue} income per second";
    #endregion Properties
}

public class GooberObjective : Objective
{
    #region Members
    private float _baseTarget = 25f;
    #endregion Members

    #region Ctor
    public GooberObjective(float targetModifier) => TargetValue = targetModifier * _baseTarget;
    #endregion Ctor

    #region Properties
    public override string Description => $"Reach {TargetValue} goobers";
    #endregion Properties
}

public class WidgetObjective : Objective
{
    #region Members
    private float _baseTarget = 50f;
    #endregion Members

    #region Ctor
    public WidgetObjective(float targetModifier) => TargetValue = targetModifier * _baseTarget;
    #endregion Ctor

    #region Properties
    public override string Description => $"Build {TargetValue} widgets";
    #endregion Properties
}
