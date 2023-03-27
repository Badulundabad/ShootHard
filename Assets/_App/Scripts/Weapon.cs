using Unity.Entities;

struct Weapon : IComponentData
{
    public float Cooldown;
    public float FiringRate;
    public float BulletSpeed;
    public Entity SpawnPoint;
    public Entity BulletPrefab;
}