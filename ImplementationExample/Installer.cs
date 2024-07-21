using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindPoolGroup<Building, Building.Pool, Building.PoolGroup>("Prefabs/Buildings",
                                                                              poolConfig => poolConfig.WithInitialSize(4)
                                                                                                      .ExpandByOneAtATime());
    }


}