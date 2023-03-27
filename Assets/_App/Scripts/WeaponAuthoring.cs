using Unity.Entities;
using UnityEngine;

class WeaponAuthoring : MonoBehaviour
{
    [SerializeField] private float _firingRate;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;

    public float FiringRate => _firingRate;
    public float BulletSpeed => _bulletSpeed;
    public GameObject BulletPrefab => _bulletPrefab;
    public Transform BulletSpawnPoint => _bulletSpawnPoint;
}

class WeaponBaker : Baker<WeaponAuthoring>
{
    public override void Bake(WeaponAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Weapon()
        {
            FiringRate = authoring.FiringRate,
            BulletSpeed = authoring.BulletSpeed,
            SpawnPoint = GetEntity(authoring.BulletSpawnPoint, TransformUsageFlags.None),
            BulletPrefab = GetEntity(authoring.BulletPrefab, TransformUsageFlags.None)
        });
    }
}
