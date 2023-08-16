using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
	[SerializeField][Range(0, 5)] float rotateSpeed;
	[SerializeField][Range(0, 5)] float moveSpeed;

	List<Node> path = new List<Node>();
	Enemy enemy;
	GridManager gridManager;
	FindPath findPath;

	private void Awake()
	{
		enemy = GetComponent<Enemy>();
		gridManager = FindObjectOfType<GridManager>();
		findPath = FindObjectOfType<FindPath>();
	}

	private void OnEnable()
	{
		//* 경로의 처음에서 시작
		ReturnToStart();

		//* 경로 찾기
		RecalculatePath(true);
	}

	private void ReturnToStart()
	{
		transform.position = gridManager.GetPositionFromCoordinates(findPath.StartCoordinates);
	}

	void RecalculatePath(bool resetPath)
	{
		Vector2Int coordinates = new Vector2Int();

		//* resetPath가 true이면 처음 시작 노드부터 경로를 설정한다.
		if (resetPath)
		{
			coordinates = findPath.StartCoordinates;
		}
		//* resetPath가 false이면 현재 위치에서 경로를 재설정한다.
		else
		{
			coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
		}

		//* 경로를 재계산하고, 코루틴을 실행한다.
		StopAllCoroutines();
		path.Clear();
		path = findPath.GetNewPath(coordinates);
		StartCoroutine("FollowPath");
	}

	IEnumerator FollowPath()
	{
		for (int i = 1; i < path.Count; i++)
		{
			Vector3 startPos = transform.position;
			Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].coordinates);
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
