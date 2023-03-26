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

        foreach (var (speed, localTransform, localToWorld) in SystemAPI.Query<SpeedComponent, RefRW<LocalTransform>, LocalToWorld>())
        {
            localTransform.ValueRW.Position += moveDelta * speed.Value;

            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.value);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                localTransform.ValueRW.Rotation = Helpers.LookAtRotation(localToWorld.Position, hit.point, new float3(0, 1, 0));
            }
        }
    }
}
