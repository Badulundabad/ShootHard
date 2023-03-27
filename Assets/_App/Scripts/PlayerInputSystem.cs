using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(EarlySimulationSystemGroup))]
public partial class PlayerInputSystem : SystemBase
{
    private Camera _camera;
    private PlayerInputActions _input;

    protected override void OnCreate()
    {
        base.OnCreate();
        _camera = Camera.main;
        _input = new PlayerInputActions();
        _input.GamePlay.Enable();
    }

    protected override void OnUpdate()
    {
        Vector2 value = _input.GamePlay.Move.ReadValue<Vector2>() * SystemAPI.Time.DeltaTime;
        float3 moveDelta = new float3(value.x, 0, value.y);

        foreach (var (weapon, speed, localTransform, localToWorld) in SystemAPI.Query<RefRW<Weapon>, SpeedComponent, RefRW<LocalTransform>, LocalToWorld>().WithAll<PlayerComponent>())
        {
            localTransform.ValueRW.Position += moveDelta * speed.Value;

            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.value);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                float3 hitPoint = hit.point;
                hitPoint.y = localToWorld.Position.y;
                localTransform.ValueRW.Rotation = Helpers.LookAtRotation(localToWorld.Position, hitPoint, new float3(0, 1, 0));
            }

            if (_input.GamePlay.Attack.IsPressed())
            {
                weapon.ValueRW.Cooldown -= SystemAPI.Time.DeltaTime;
                if (weapon.ValueRO.Cooldown <= 0)
                {
                    weapon.ValueRW.Cooldown = weapon.ValueRO.FiringRate;
                    Attack(localTransform.ValueRO.Rotation);
                }
            }
        }
    }

    private void Attack(quaternion direction)
    {
        var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();

        new ShotJob
        {
            Direction = direction,
            LocalToWorldLookup = GetComponentLookup<LocalToWorld>(true),
            ECB = ecb.CreateCommandBuffer(EntityManager.WorldUnmanaged)
        }.Schedule();
    }
}
