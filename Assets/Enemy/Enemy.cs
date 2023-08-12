using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] int goldReward = 20;
	[SerializeField] int goldPenalty = 20;

	Bank bank;

	private void Awake()
	{
		bank = FindAnyObjectByType<Bank>();
	}

	public void killEnemy(bool isKilled)
	{
		gameObject.SetActive(false);

		//* bank가 없을 경우 그냥 종료
		if (object.ReferenceEquals(bank, null)) return;

		if (isKilled)
			bank.Deposit(goldReward);
		else
			bank.Withdraw(goldPenalty);
	}
}
