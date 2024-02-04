using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public static class ObjectiveFactory
{
    #region Members
    private static Random _rand = new Random();
    // Liet ke cac goal object
    private static IEnumerable<Type> _goals;
    private static List<int> Check;
    #endregion Member

    #region Ctor
    static ObjectiveFactory()
    {
        _goals = typeof(Objective).Assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(Objective)) );
    }
    #endregion Ctor

    #region Methods
    public static Objective CreateGoal(int targetModifier)
    {
        // goal.count = 3 randomnumber = 0 - 2
        int randomNumber = _rand.Next(_goals.Count());
        var ans = _goals.ElementAt(randomNumber);
        return (Objective)Activator.CreateInstance(ans, targetModifier);
    }
    #endregion Methods
}