using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public static class BindingExtensions
{
    public static void BindPoolGroup<TItemContract, TPool, TPoolGroup>(this DiContainer container,
                                                                            string resourceFolder,
                                                                            Action<MemoryPoolInitialSizeMaxSizeBinder<TItemContract>> configurePoolGroup = null)
        where TItemContract : UnityEngine.Object
        where TPool : IMemoryPool
        where TPoolGroup : IMemoryPoolGroup
    {
        // Bind pool per enum type wth given ID
        foreach (BuildingType typeEnum in Enum.GetValues(typeof(BuildingType)))
        {
            var binder = container.BindMemoryPool<TItemContract, TPool>()
                                  .WithId(typeEnum);

            configurePoolGroup?.Invoke(binder);

            binder.FromComponentInNewPrefabResource($"{resourceFolder}/{typeEnum}");
        }

        container.Bind<TPoolGroup>().AsSingle();
    }
}
