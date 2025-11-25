using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
	public float lifeTime = 5f;
	private float timer = 0f;
	[HideInInspector] public BulletLauncher parent;

	private void FixedUpdate()
	{
		Vector3 length = (transform.forward * speed);
		//transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x , transform.rotation.eulerAngles.y , transform.rotation.eulerAngles.z + 2.0f);    Cool Animation
		timer += Time.fixedDeltaTime;

		if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, length.magnitude))
		{
			if (hit.collider != null)
			{
				//hit.collider.GetComponent<IDamageable>()?.TakeDamage(parent.weaponData.damage);
				Debug.Log($"Bullet hit: {hit.collider.name}");
			}
			transform.position += length;
			parent.ReturnBullet(this);
		}

		transform.position += length;

		if (timer >= lifeTime)
		{
			timer = 0f;
			parent.ReturnBullet(this);
		}
	}
}
