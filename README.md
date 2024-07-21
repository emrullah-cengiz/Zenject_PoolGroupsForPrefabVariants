# PoolGroup for Pooling Prefab Variants

## Description
A small tool I prepared to manage various variants of the same prefab in different pools while using Zenject Pools, allowing us to use this process with minimal code whenever needed.

## PoolGroup Definition

A PoolGroup is defined just like a Pool for an object that will be pooled separately for each variant based on an Enum value. You specify the model containing the parameters to be provided during spawning, the type of object to be pooled, the Enum for variants, and the Pool class of the object.

```csharp
public class Building : MonoBehaviour, IPoolable<PoolGroupParams>
{
    ...

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
```

## Example Binding
We set up the Resource path containing the variants and the common configurations to be applied for each pool using the BindPoolGroup extension method on our **Installer.cs**.

```csharp
Container.BindPoolGroup<Building, Building.Pool, Building.PoolGroup>("Prefabs/Buildings",
                                                                      poolConfig => poolConfig.WithInitialSize(4)
                                                                                              .ExpandByOneAtATime());
```

## Example Implementation

```csharp
private Building.PoolGroup _poolGroup;

[Inject]
void Construct(Building.PoolGroup poolGroup)
{
    _poolGroup = poolGroup;
}

private void Spawn(BuildingData data, Vector3 pos)
{
    var building = _poolGroup.Spawn(data.Type,
                                    new Building.PoolGroupParams()
                                    {
                                        Data = data,
                                        Position = pos
                                    });
}
```
