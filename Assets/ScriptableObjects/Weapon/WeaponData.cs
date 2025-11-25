using UnityEngine;

public enum WeaponType { Ranged, Melee, Explosive }

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
	public string weaponName;
	public float fireRate; // shots per second
	public float damage;
	public float range; // meters
	public int maxClip;
	public int maxAmmo;
	public float reloadTime; // Time to reload in seconds
	public int bulletSpeed;
	public GameObject bulletPrefab; // Prefab for the bullet
	public WeaponType type;
}
