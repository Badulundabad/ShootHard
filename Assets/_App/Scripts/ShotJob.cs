using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
partial struct ShotJob : IJobEntity
{
    [ReadOnly] public ComponentLookup<LocalToWorld> LocalToWorldLookup;
    public quaternion Direction;
    public EntityCommandBuffer ECB;

    [BurstCompile]
    void Execute(in WeaponAspect weaponAspect)
    {
        Entity instance = ECB.Instantiate(weaponAspect.BulletPrefab);

        LocalToWorld localToWorld = LocalToWorldLookup[weaponAspect.SpawnPoint];
        LocalTransform localTransform = LocalTransform.FromPositionRotationScale(localToWorld.Position, Direction, 0.2f);

        ECB.AddComponent(instance, new SpeedComponent()
        { 
            Direction = localTransform.Forward(),
            Value = weaponAspect.Weapon.ValueRO.BulletSpeed
        });
        ECB.SetComponent(instance, localTransform);
    }
}