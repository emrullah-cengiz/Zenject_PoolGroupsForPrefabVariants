using UnityEngine;
using Zenject;
using static Building;

public class Building : MonoBehaviour, IPoolable<PoolGroupParams>
{
    [SerializeField] private SpriteRenderer _sprite;

    public void OnSpawned(PoolGroupParams attrs)
    {
        _sprite.sprite = attrs.Data.Sprite;

        transform.position = attrs.Position;
    }

    public void OnDespawned()
    {
    }

    public class Pool : MonoPoolableMemoryPool<PoolGroupParams, Building>, IMemoryPool
    {
    }

    public class PoolGroup : MemoryPoolGroup<PoolGroupParams, Building, BuildingType, Building.Pool>, IMemoryPoolGroup
    {
    }

    public struct PoolGroupParams : IMemoryPoolGroupParams
    {
        public BuildingData Data { get; set; }
        public Vector3 Position { get; set; }
    }
}
