using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath : MonoBehaviour
{
	[SerializeField] Vector2Int startCoordinates;
	public Vector2Int StartCoordinates { get { return startCoordinates; } }
	[SerializeField] Vector2Int destinationCoordinates;
	public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }
	[SerializeField] Node currentSearchNode;

	Node startNode;
	Node destinationNode;

	Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

	GridManager gridManager;

	Queue<Node> frontier = new Queue<Node>();
	Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
	Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

	private void Awake()
	{
		gridManager = FindObjectOfType<GridManager>();
		if (object.ReferenceEquals(gridManager, null) == false)
		{
			grid = gridManager.Grid;
			startNode = grid[startCoordinates];
			destinationNode = grid[destinationCoordinates];

		}

	}

	private void Start()
	{
		GetNewPath();
	}

	public List<Node> GetNewPath(Vector2Int coordinates)
	{
		gridManager.ResetNode();
		BreadthFirstSearch(coordinates);
		return BuildPath();
	}

	public List<Node> GetNewPath()
	{
		return GetNewPath(startCoordinates);
	}

	void ExploreNeighbor()
	{
		List<Node> neighbors = new List<Node>();

		foreach (Vector2Int direction in directions)
		{
			Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

			if (object.ReferenceEquals(grid, null) == false && grid.ContainsKey(neighborCoords))
			{
				neighbors.Add(grid[neighborCoords]);

			}
		}

		foreach (Node neighbor in neighbors)
		{
			if (reached.ContainsKey(neighbor.coordinates) == false && neighbor.isWalkable == true)
			{
				reached.Add(neighbor.coordinates, neighbor);
				frontier.Enqueue(neighbor);
				neighbor.connectedTo = currentSearchNode;
			}
		}
	}

	void BreadthFirstSearch(Vector2Int coordinates)
	{
		startNode.isWalkable = true;
		destinationNode.isWalkable = true;

		frontier.Clear();
		reached.Clear();

		bool isRunning = true;

		frontier.Enqueue(grid[coordinates]);
		reached.Add(coordinates, grid[coordinates]);

		while (frontier.Count > 0 && isRunning == true)
		{
			currentSearchNode = frontier.Dequeue();
			currentSearchNode.isExplored = true;
			ExploreNeighbor();
			if (currentSearchNode.coordinates == destinationCoordinates)
			{
				isRunning = false;
			}
		}

	}

	List<Node> BuildPath()
	{
		Node currentNode = destinationNode;
		List<Node> path = new List<Node>();

		//* 끝 지점부터 시작 지점의 다음 지점까지 path로 지정한다.
		while (object.ReferenceEquals(currentNode.connectedTo, null) == false)
		{
			path.Add(currentNode);
			currentNode.isPath = true;
			currentNode = currentNode.connectedTo;
		}
		//* 시작 지점도 path로 지정한다. 시작 지점은 연결된 노드가 없기 때문에 따로 설정해줘야 한다.
		path.Add(currentNode);
		currentNode.isPath = true;

		path.Reverse();

		return path;
	}

	public bool WillBlockPath(Vector2Int coordinates)
	{
		if (grid.ContainsKey(coordinates))
		{
			bool previousState = grid[coordinates].isWalkable;

			grid[coordinates].isWalkable = false;
			List<Node> newPath = GetNewPath();
			grid[coordinates].isWalkable = previousState;

			//* BFS에서 목적지까지의 경로를 찾지 못하면 만들어진 경로가 없기 때문에 path의 크기가 1이하가 된다.
			if (newPath.Count <= 1)
			{
				GetNewPath();
				return true;
			}
		}

		return false;
	}

	public void NotifyReceivers()
	{
		BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
	}
}
