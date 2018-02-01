using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatchDataModel", menuName = "FanLabelRemix/MatchData")]
public class MatchData : ScriptableObject
{
	#region Properties

	public List<string> songNames;

	private List<List<int>> songList = new List<List<int>>();
	private List<int> listParent = new List<int>(); // Points to a prior list in songList that generated the songList with the same index as this.
	private List<int> recordedRanks = new List<int>(); // The rank of songs that are currently being evaluated.

	private int comparer1, comparer2; // The two lists in songList being compared.
	private int head1, head2; // The actual songID being compared. head1 goes with comparer1 and vice versa.
	private int rankIdx;

	private int questionNum; // Current question number. Used by MatchView.
	public int QuestionNum
	{
		get { return questionNum; }
	}

	private int totalSize; // Number of total comparisions. Used by MatchView.
	private bool isFinished; // Flag used for to determine what the View/Controller should be able to do.
	public bool IsFinished
	{
		get { return isFinished; }
	}

	#endregion


	#region Helper Functions

	private void Initialize()
	{

	}

	#endregion

}
