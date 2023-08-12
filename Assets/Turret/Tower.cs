using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	[SerializeField] int towerCost = 25;

	Bank bank;

	public bool CreateTower(Tower tower, Vector3 pos)
	{
		bank = FindObjectOfType<Bank>();
		if (object.ReferenceEquals(bank, null)) return false;

		if (bank.CurrentBalance < towerCost) return false;

		Instantiate(tower, pos, Quaternion.identity);

		bank.Withdraw(towerCost);

		return true;
	}
}
