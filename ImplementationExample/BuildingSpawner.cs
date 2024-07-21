using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BuildingSpawner : MonoBehaviour
{
    private Building.PoolGroup _poolGroup;

    [Inject]
    void Construct(Building.PoolGroup poolGroup, BuildingManager buildingManager)
    {
        _poolGroup = poolGroup;
    }

    private void Spawn(BuildingData data, Vector3 pos)
    {
        var building = _poolGroup.Spawn(data.Type, new Building.PoolGroupParams()
        {
            Data = data,
            Position = pos
        });
    }
}
