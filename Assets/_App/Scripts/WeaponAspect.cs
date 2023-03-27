using Unity.Entities;

readonly partial struct WeaponAspect : IAspect
{
    public readonly RefRO<Weapon> Weapon;

    public Entity SpawnPoint => Weapon.ValueRO.SpawnPoint;
    public Entity BulletPrefab => Weapon.ValueRO.BulletPrefab;
}