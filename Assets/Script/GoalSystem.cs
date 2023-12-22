using System;
using System.Collections.Generic;

public abstract class Objective
{
    #region Ctors
    public Objective() { }
    #endregion Ctors

    #region Properties
    public abstract string Description { get; }
    public virtual string Name { get; protected set; }
    public virtual int TargetValue { get; protected set; }
    public virtual int Id { get; protected set; }
    public bool IsComplete { get; protected set; }
    #endregion Properties

    #region Methods
    public virtual void CompleteGoal() => IsComplete = true;
    #endregion Methods
}
public class IncomeObjective : Objective
{
    #region Members
    private int _baseTarget = 10;

    #endregion Members

    #region Ctor
    public IncomeObjective(int targetModifier)
    { 
        TargetValue = targetModifier * _baseTarget;
        Name = "Gold";
        Id = 1;
    }
    #endregion Ctor

    #region Properties
    public override string Description => $"Reach {TargetValue} Gold";
    #endregion Properties
}

public class PlanObjective : Objective
{
    #region Members
    private int _baseTarget = 5;
    #endregion Members

    
    #region Ctor
    public PlanObjective(int targetModifier)
    {
        TargetValue = targetModifier * _baseTarget;
        Random random = new Random();
        int randomNumber = random.Next(1, LevelManager.currentLevel);
        switch (randomNumber)
        {
            case 1:
                Name = "Tomatoes";
                break;
            case 2:
                Name = "Corn";
                break;
            case 3:
                Name = "Rice";
                break;

        }
    }
    #endregion Ctor

    #region Properties
    public override string Description => $"Plan {TargetValue} {Name}";
    #endregion Properties
}

public class HusbandryObjective : Objective
{
    #region Members
    private int _baseTarget = 2;
    #endregion Members

    #region Ctor
    public HusbandryObjective(int targetModifier)
    {
        TargetValue = targetModifier * _baseTarget;
        Name = "Pigs";
        Id = 3;
    }
    #endregion Ctor

    #region Properties
    public override string Description => $"Sell {TargetValue} Pigs";
    #endregion Properties
}
