using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using WeaponInterfaces;

public class RangedWeapon : MonoBehaviour ,IRangedWeapon
{
	[SerializeField] private WeaponData weaponData;

	private int currentClip;
	private int currentAmmo;

	private Timer.TimerData timerData;
	private BulletLauncher bulletLauncher;

	public string WeaponName { get { return weaponData.name; } }
	public float FireRate { get { return weaponData.fireRate; } }
	public float Damage { get { return weaponData.damage; } }
	public float Range { get { return weaponData.range; } }
	public int MaxClip { get { return weaponData.maxClip; } }
	public int MaxAmmo { get { return weaponData.maxAmmo; } }
	public float BulletSpeed { get { return weaponData.bulletSpeed; } }
	public float ReloadTime { get { return weaponData.reloadTime; } }

	public WeaponState State { get; set; } = WeaponState.Idle;
    private void OnEnable()
    {
        InputControl.Instance.SubscribeHeld(Fire, "Attack");
        InputControl.Instance.SubscribeCancelled(Idle, "Attack");
    }

    private void OnDisable()
    {
        InputControl.Instance.UnsubsribeAll(Fire, "Attack");
        InputControl.Instance.UnsubsribeAll(Idle, "Attack");
    }
    private void Awake()
    {
        bulletLauncher = GetComponent<BulletLauncher>();

        if (bulletLauncher == null)
            return;

        bulletLauncher.Initialize(weaponData.bulletPrefab);
        currentClip = MaxClip;
        currentAmmo = MaxAmmo;

        timerData = Timer.Instance.RequestTimer(FireRate);
    }

    public void Fire(InputAction.CallbackContext ctx)
	{
		if (timerData.HasTriggered && State != WeaponState.OutOfAmmo)
		{
			if (currentClip > 0)
			{
				State = WeaponState.Firing;
				bulletLauncher.LaunchBullet(transform.forward, BulletSpeed);
				currentClip--;
				Debug.Log($"Firing {WeaponName} towards {transform.forward}");
				Debug.Log($"Current clip: {currentClip}, Current ammo: {currentAmmo}");
			}
			else
			{
				State = WeaponState.OutOfAmmo;
				Debug.Log("Out of ammo!");
				DelayedEvent.Instance.Wait(Reload, ReloadTime);
			}
		}
	}

	public void Reload()
	{
		Debug.Log($"Reloading {WeaponName}");
		if (currentAmmo > 0)
		{
			State = WeaponState.Idle;
			currentClip = MaxClip;
			currentAmmo -= MaxClip;
		}
	}

	public void Idle(InputAction.CallbackContext ctx) 
	{
		if (State == WeaponState.Firing)
		{
			State = WeaponState.Idle;
			Debug.Log($"{WeaponName} is now idle.");
		}
	}
}
