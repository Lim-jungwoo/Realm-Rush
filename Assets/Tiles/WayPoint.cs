using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
	[SerializeField] Tower balisterPrefab;
	[SerializeField] bool isPlaceable = false;
	public bool IsPlaceable
	{
		get { return isPlaceable; }
		set { isPlaceable = value; }
	}

	private void OnMouseDown()
	{
		if (isPlaceable == true)
		{
			bool isPlaced = balisterPrefab.CreateTower(balisterPrefab, transform.position);
			isPlaceable = !isPlaced;
		}
	}
}
