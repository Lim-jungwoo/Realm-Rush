using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
	TextMeshProUGUI scoreTxt;
	int score = 0;


	private void Awake()
	{
		scoreTxt = GetComponent<TextMeshProUGUI>();
		scoreTxt.text = "Start!!";
	}

	public int GetScore()
	{
		return score;
	}

	public void AddScore(int addScore)
	{
		score += addScore;
		UpdateScore();
	}

	void UpdateScore()
	{
		scoreTxt.text = "Score: " + score;
	}
}
