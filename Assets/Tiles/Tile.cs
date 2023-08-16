using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField] Tower balisterPrefab;
	[SerializeField] bool isPlaceable = false;
	public bool IsPlaceable
	{
		get { return isPlaceable; }
		set { isPlaceable = value; }
	}

	GridManager gridManager;
	FindPath findPath;
	Vector2Int coordinates = new Vector2Int();

	void Awake()
	{
		gridManager = FindObjectOfType<GridManager>();
		findPath = FindObjectOfType<FindPath>();
	}

	void Start()
	{
		if (object.ReferenceEquals(gridManager, null) == false)
		{
			//* 그리드 매니저로부터 위치를 통한 좌표를 얻어온다.
			coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

			//* 현재 타일이 배치가 불가능한 상태라면 그리드 매니저에서 현재 타일을 블럭처리한다.
			if (!isPlaceable)
			{
				gridManager.BlockNode(coordinates);
			}
		}
	}

	private void OnMouseDown()
	{
		if (gridManager.GetNode(coordinates).isWalkable && !findPath.WillBlockPath(coordinates))
		{
			bool isSuccessful = balisterPrefab.CreateTower(balisterPrefab, transform.position);
			if (isSuccessful)
			{
				gridManager.BlockNode(coordinates);
				findPath.NotifyReceivers();
			}
		}
	}
}
