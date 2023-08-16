using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
	[SerializeField] int maxHP = 5;
	public int MaxHP { get { return maxHP; } }
	[SerializeField] int currentHP = 0;
	public int CurrentHP { get { return currentHP; } }
	[SerializeField] int difficultDamp = 1;

	Enemy enemy;

	private void Awake()
	{
		enemy = GetComponent<Enemy>();
	}

	private void OnEnable()
	{
		currentHP = maxHP;
	}

	private void OnParticleCollision(GameObject other)
	{
		currentHP--;
		if (currentHP < 0)
		{
			maxHP += difficultDamp;
			enemy.killEnemy(true);
		}
	}
}
