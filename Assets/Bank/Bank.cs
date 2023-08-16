using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI goldDisplay;

	[SerializeField] int startBalance = 300;

	[SerializeField] End end;

	int currentBalance;
	public int CurrentBalance
	{
		get { return currentBalance; }
		set { currentBalance = value; }
	}

	private void Awake()
	{
		currentBalance = startBalance;
		UpdateDisplay();
	}

	public void Withdraw(int amount)
	{
		currentBalance -= Mathf.Abs(amount);
		UpdateDisplay();
		if (currentBalance < 0)
		{
			end.OnEndPanel();
		}
	}
	public void Deposit(int amount)
	{
		currentBalance += Mathf.Abs(amount);
		UpdateDisplay();
	}

	void UpdateDisplay()
	{
		goldDisplay.text = "Gold: " + currentBalance;
	}
}
