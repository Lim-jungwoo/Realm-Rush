using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[SerializeField][Range(0, 50)] int poolSize = 5;
	[SerializeField] GameObject enemyPrefab;
	[SerializeField][Range(0.01f, 30f)] float spawnTimer = 1f;

	GameObject[] pool;

	private void Awake()
	{
		PopulatePool();

		StartCoroutine("SpawnEnemy");
	}

	void PopulatePool()
	{
		pool = new GameObject[poolSize];

		for (int i = 0; i < poolSize; i++)
		{
			pool[i] = Instantiate(enemyPrefab, transform);
			pool[i].SetActive(false);
		}
	}

	void EnableObjectInPool()
	{
		for (int i = 0; i < poolSize; i++)
		{
			if (pool[i].activeInHierarchy == false)
			{
				pool[i].SetActive(true);
				return;
			}
		}
	}

	IEnumerator SpawnEnemy()
	{
		while (true)
		{
			EnableObjectInPool();
			yield return new WaitForSeconds(spawnTimer);
		}
	}
}
