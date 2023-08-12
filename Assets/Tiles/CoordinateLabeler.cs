using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
	[SerializeField] Color defaultColor = Color.white;
	[SerializeField] Color blockedColor = Color.grey;


	TextMeshPro label;
	Vector2Int coordinates = new Vector2Int();
	WayPoint wayPoint;

	private void Awake()
	{
		label = GetComponent<TextMeshPro>();
		wayPoint = GetComponentInParent<WayPoint>();

		label.enabled = false;

		DisplayCoordinates();
	}

	private void Update()
	{
		if (!Application.isPlaying)
		{
			label.enabled = true;
			DisplayCoordinates();
			UpdateObjectName();
		}

		ChangeColor();
		ToggleLabelByCKey();
	}

	private void ToggleLabelByCKey()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			label.enabled = !label.enabled;
		}
	}

	private void ChangeColor()
	{
		if (wayPoint.IsPlaceable)
			label.color = defaultColor;
		else
			label.color = blockedColor;
	}

	private void DisplayCoordinates()
	{
		coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
		coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
		label.text = coordinates.ToString();
	}

	void UpdateObjectName()
	{
		transform.parent.name = coordinates.ToString();
	}
}
