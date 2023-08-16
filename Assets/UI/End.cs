using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI endScoreTxt;
	[SerializeField] GameObject startBtn;

	Score score;

	int endScore = 0;

	public void OnEndPanel()
	{
		score = FindObjectOfType<Score>();
		endScore = score.GetScore();
		DisplayEndScore();
		gameObject.SetActive(true);
	}

	void DisplayEndScore()
	{
		endScoreTxt.text = "Score: " + endScore;
	}

	public void OnClickStartBtn()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex);
	}
}
