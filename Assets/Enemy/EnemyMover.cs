using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
	[SerializeField] List<WayPoint> path = new List<WayPoint>();
	[SerializeField][Range(0, 5)] float rotateSpeed;
	[SerializeField][Range(0, 5)] float moveSpeed;

	Enemy enemy;

	private void Awake()
	{
		enemy = GetComponent<Enemy>();
	}

	private void OnEnable()
	{
		//* 경로 찾기
		FindPath();

		//* 경로의 처음에서 시작
		RestartPos();


		StartCoroutine("FollowPath");
	}

	private void RestartPos()
	{
		if (path.Count > 0)
			transform.position = path[0].transform.position;
	}

	void FindPath()
	{
		path.Clear();

		GameObject paths = GameObject.FindGameObjectWithTag("Path");

		foreach (Transform child in paths.transform)
		{
			WayPoint wayPoint = child.GetComponent<WayPoint>();
			if (object.ReferenceEquals(wayPoint, null) == false)
				path.Add(wayPoint);
		}

	}

	IEnumerator FollowPath()
	{
		foreach (WayPoint wayPoint in path)
		{
			Vector3 startPos = transform.position;
			Vector3 endPos = wayPoint.transform.position;
			float travelPercent = 0f;
			float rotatePercent = 0f;

			//* Rotate
			while (rotatePercent < 1f)
			{
				Vector3 dir = endPos - startPos;
				if (dir == Vector3.zero || transform.rotation == Quaternion.LookRotation(dir)) break;

				rotatePercent += Time.deltaTime * rotateSpeed;
				this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), rotatePercent);

				yield return new WaitForEndOfFrame();
			}

			//* Move
			while (travelPercent < 1f)
			{
				travelPercent += Time.deltaTime * moveSpeed;
				transform.position = Vector3.Lerp(startPos, endPos, travelPercent);

				yield return new WaitForEndOfFrame();
			}

		}

		ArriveEndPath();
	}

	void ArriveEndPath()
	{
		enemy.killEnemy(false);
	}
}
