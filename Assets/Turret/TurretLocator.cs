using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLocator : MonoBehaviour
{
	[SerializeField] Transform weapon;
	[SerializeField] Transform target;
	[SerializeField] ParticleSystem bullet;
	[SerializeField] float range = 15f;

	private void Awake()
	{
		bullet = GetComponentInChildren<ParticleSystem>();
	}

	private void Update()
	{
		FindTarget();
		AimWeapon();
	}

	void FindTarget()
	{
		Enemy[] targets = FindObjectsOfType<Enemy>();
		float closestDistance = Mathf.Infinity;

		for (int i = 0; i < targets.Length; i++)
		{
			float distance = Vector3.Distance(this.transform.position, targets[i].transform.position);
			if (distance < closestDistance)
			{
				target = targets[i].transform;
				closestDistance = distance;
			}
		}
	}

	private void AimWeapon()
	{
		weapon.LookAt(target);

		float distance = Vector3.Distance(this.transform.position, target.position);

		if (distance > range) Attack(false);
		else Attack(true);
	}

	void Attack(bool isActive)
	{
		var emissionModule = bullet.emission;
		if (isActive == true)
			emissionModule.enabled = true;
		else
			emissionModule.enabled = false;
	}
}
