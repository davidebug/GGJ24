//Code inspired by "Level up your code with programming pattern" by unity

using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public interface IState 
{
    // code that runs when we first enter the state
    public abstract void Enter();

    // per - frame logic, include condition to transition to a new state
    public abstract void Update();

    public abstract void Exit();

}
