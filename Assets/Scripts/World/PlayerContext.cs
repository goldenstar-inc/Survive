using System.Collections.Generic;
using UnityEngine;


public class PlayerContext : InitializableBehaviour
{
    [SerializeField] public List<InitializableBehaviour> initializables;

    public override void Initialize()
    {
        foreach (InitializableBehaviour initializable in initializables)
        {
            initializable.Initialize();
        }
    }
}