using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class Enemy : MonoBehaviour
{
	[SerializeField] int goldReward = 20;
	[SerializeField] int goldPenalty = 20;
	int scoreNum = 0;

	Bank bank;
	Score score;
	EnemyHealth enemyHealth;

	private void Awake()
	{
		bank = FindObjectOfType<Bank>();
		score = FindObjectOfType<Score>();
		enemyHealth = GetComponent<EnemyHealth>();
	}

	public void killEnemy(bool isKilled)
	{
		gameObject.SetActive(false);
		scoreNum = enemyHealth.MaxHP;

		//* bank가 없을 경우 그냥 종료
		if (object.ReferenceEquals(bank, null)) return;

		if (isKilled)
		{
			bank.Deposit(goldReward);
			score.AddScore(scoreNum);
		}
		else
			bank.Withdraw(goldPenalty);
	}
}
