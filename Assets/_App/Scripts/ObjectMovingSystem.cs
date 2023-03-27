using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct ObjectMovingSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        MoveObjectJob job = new MoveObjectJob() { deltaTime = SystemAPI.Time.DeltaTime };
        job.ScheduleParallel();
    }
}

[WithNone(typeof(PlayerComponent))]
[BurstCompile]
public partial struct MoveObjectJob : IJobEntity
{
    public float deltaTime;

    [BurstCompile]
    private void Execute(ref LocalTransform transform, in SpeedComponent speed)
    {
        float3 forward = speed.Direction * speed.Value * deltaTime;
        forward.y = 0f;
        transform.Position += forward;
    }
}
