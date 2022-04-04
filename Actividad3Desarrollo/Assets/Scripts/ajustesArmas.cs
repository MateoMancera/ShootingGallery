using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Armas/AjustesArmas")]
public class ajustesArmas : ScriptableObject
{
    public float fireRate { get { return _fireRate; } private set { _fireRate = value; } }
    [SerializeField] private float _fireRate = 5.0f;

    public float bulletSpeed { get { return _bulletSpeed; } private set { _bulletSpeed = value; } }
    [SerializeField] private float _bulletSpeed = 5.0f;

    public GameObject bulletPrefab { get { return _bulletPrefab; } private set { _bulletPrefab = value; } }
    [SerializeField] private GameObject _bulletPrefab = null;

    public float lifeSpan { get { return _lifeSpan; } private set { _lifeSpan = value; } }
    [SerializeField] private float _lifeSpan = 3f;

    public int ammo { get { return _ammo; } private set { _ammo = value; } }
    [SerializeField] public int _ammo = 40;

}
