using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI goldDisplay;

	[SerializeField] int startBalance = 300;

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
			ReloadScene();
		}
	}
	public void Deposit(int amount)
	{
		currentBalance += Mathf.Abs(amount);
		UpdateDisplay();
	}

	void ReloadScene()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex);
	}

	void UpdateDisplay()
	{
		goldDisplay.text = "Gold: " + currentBalance;
	}
}
