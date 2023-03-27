using Unity.Entities;
using Unity.Mathematics;

public struct SpeedComponent : IComponentData
{
    public float Value;
    public float3 Direction;
}
