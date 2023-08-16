using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	[SerializeField] int towerCost = 75;
	[SerializeField] float buildDelayTime = 1f;

	Bank bank;

	void Start()
	{
		StartCoroutine(Build());
	}

	public bool CreateTower(Tower tower, Vector3 pos)
	{
		bank = FindObjectOfType<Bank>();
		if (object.ReferenceEquals(bank, null)) return false;

		if (bank.CurrentBalance < towerCost) return false;

		Instantiate(tower, pos, Quaternion.identity);

		bank.Withdraw(towerCost);

		return true;
	}

	IEnumerator Build()
	{
		//* 모든 오브젝트를 끈다.
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(false);
			foreach (Transform grandChild in child)
			{
				grandChild.gameObject.SetActive(false);
			}
		}

		//* 오브젝트를 순서대로 킨다.
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(true);
			yield return new WaitForSeconds(buildDelayTime);
			foreach (Transform grandChild in child)
			{
				grandChild.gameObject.SetActive(true);
			}
		}
	}
}
