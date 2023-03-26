using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour { }

public class PlayerBaker : Baker<PlayerAuthoring>
{
    public override void Bake(PlayerAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new PlayerComponent());
    }
}
