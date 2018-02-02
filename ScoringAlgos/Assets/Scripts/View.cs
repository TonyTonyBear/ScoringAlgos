using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour {

	#region Properties

	public RectTransform matchPanel, resultsPanel;
	public Text questionNumber, leftButtonText, rightButtonText, resultsText;

	#endregion

	#region Monobehavior

	#endregion

	#region Processor Communication

	public void UpdateMatchView(int questionNumber, string leftText, string rightText)
	{
		// Sanity check on panels. Results should never be open and Matches should be open when this function is running.
		if (resultsPanel.gameObject.activeSelf)
			resultsPanel.gameObject.SetActive(false);
		if (!matchPanel.gameObject.activeSelf)
			matchPanel.gameObject.SetActive(true);

		//Populate buttons with text.
		leftButtonText.text = leftText;
		rightButtonText.text = rightText;
		this.questionNumber.text = "Question: " + questionNumber;
	}

	public void ShowResults(List<string> results)
	{
		//Disable match panel and enable results panel.
		matchPanel.gameObject.SetActive(false);
		resultsPanel.gameObject.SetActive(true);

		//Format List
		string resultsOutput = "";

		for(int i = 0; i < results.Count; i++)
		{
			resultsOutput += (i + 1) + ".  " + results[i] + "\n"; 
		}

		resultsText.text = resultsOutput;
	}

	#endregion
}
