using Unity.Entities;
using UnityEngine;

public class SpeedAuthoring : MonoBehaviour
{
    public float Value;
}

public class SpeedBaker : Baker<SpeedAuthoring>
{
    public override void Bake(SpeedAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new SpeedComponent()
        {
            Value = authoring.Value
        });
    }
}
