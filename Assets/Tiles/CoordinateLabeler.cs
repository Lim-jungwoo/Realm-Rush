using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
	[SerializeField] Color defaultColor = Color.white;
	[SerializeField] Color blockedColor = Color.grey;
	[SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);
	[SerializeField] Color exploredColor = Color.yellow;


	TextMeshPro label;
	Vector2Int coordinates = new Vector2Int();

	GridManager gridManager;

	private void Awake()
	{
		label = GetComponent<TextMeshPro>();
		label.enabled = false;

		gridManager = FindObjectOfType<GridManager>();

		DisplayCoordinates();

	}

	private void Start()
	{
		// SetLabelColor();
	}

	private void Update()
	{
		if (!Application.isPlaying)
		{
			label.enabled = true;
			DisplayCoordinates();
			UpdateObjectName();
		}

		ToggleLabelByCKey();
		SetLabelColor();
	}

	private void ToggleLabelByCKey()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			label.enabled = !label.enabled;
		}
	}

	private void SetLabelColor()
	{
		if (object.ReferenceEquals(gridManager, null)) return;

		Node node = gridManager.GetNode(coordinates);

		if (node == null) return;

		if (node.isWalkable == false)
		{
			label.color = blockedColor;
		}
		else if (node.isPath == true)
		{
			label.color = pathColor;
		}
		else if (node.isExplored == true)
		{
			label.color = exploredColor;
		}
		else
		{
			label.color = defaultColor;
		}
	}

	private void DisplayCoordinates()
	{
		if (object.ReferenceEquals(gridManager, null)) return;

		coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
		coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
		label.text = coordinates.ToString();
	}

	void UpdateObjectName()
	{
		transform.parent.name = coordinates.ToString();
	}
}
