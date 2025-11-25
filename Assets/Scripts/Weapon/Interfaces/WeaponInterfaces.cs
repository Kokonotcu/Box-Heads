using UnityEngine;

namespace WeaponInterfaces 
{
	public interface IWeaponBase
	{
		string WeaponName { get; }
		float FireRate { get; }
		float Damage { get; }
		float Range { get; }
		int MaxClip { get; }
		int MaxAmmo { get; }
		float BulletSpeed { get; }
		float ReloadTime { get; }
		WeaponState State { get; set; }
	}
	public interface IRangedWeapon : IWeaponBase
	{
		WeaponType Type { get { return WeaponType.Ranged; } }
	}
	public interface IMeleeWeapon : IWeaponBase
	{
		WeaponType Type { get { return WeaponType.Melee; } }
	}
	public interface IExplosiveWeapon : IWeaponBase
	{
		WeaponType Type { get { return WeaponType.Explosive; } }
	}
}
public enum WeaponState
{
	Idle,
	Firing,
	OutOfAmmo
}

//void SetAmmo(int ammoCount);