using System.Collections;
using UnityEngine;
using WeaponInterfaces;
using static UnityEditor.Rendering.CameraUI;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
	[SerializeField] private ParticleSystem particles;
	ObjectPooler objectPooler;
	public void Initialize(GameObject bulletPrefab)
	{
		objectPooler = new ObjectPooler(bulletPrefab,new GameObject(bulletPrefab.name + "pool").transform);
	}

	public void LaunchBullet(Vector3 direction, float speed)
	{
		GameObject bullet = objectPooler.Spawn(shootPoint.position,shootPoint.forward);
		Bullet bulletScript = bullet.GetComponent<Bullet>();
		bullet.GetComponent<TrailRenderer>().Clear();
		bulletScript.speed = speed;
		bulletScript.parent = this;
		particles.Play();
	}

	public void ReturnBullet(Bullet bullet)
	{
		objectPooler.Release(bullet.gameObject);
	}
}
