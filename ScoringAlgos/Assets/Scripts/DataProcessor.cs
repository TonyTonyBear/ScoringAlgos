using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProcessor : MonoBehaviour {

	#region Properties

	public MatchData matchData; // Currently just an array of song names.

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
	private int finishSize; // How much data has been processed currently
	private bool isFinished; // Flag used for to determine what the View/Controller should be able to do.
	public bool IsFinished
	{
		get { return isFinished; }
	}

	#endregion

	#region Singleton Pattern

	public static DataProcessor instance;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
		{
			Debug.LogError("Data Processor already exists. Destroying prebuilt instance.");
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

	}

	#endregion

	#region Monobehavior

	private void Start()
	{
		Initialize();
		updateView();
	}

	#endregion

	#region Processing Functions

	private void Initialize()
	{
		int n = 0;
		int mid, i;

		songList.Add(new List<int>());

		for (i = 0; i < matchData.songNames.Count; i++)
		{
			songList[n].Add(i);
		}

		listParent.Add(-1);
		totalSize = 0;
		n++;


		//Create additional hierarchies lists in songList.
		for (i = 0; i < songList.Count; i++)
		{

			//Only create new lists if the current list can be split up.
			if (songList[i].Count >= 2)
			{
				mid = (int)Mathf.Ceil(songList[i].Count / 2);

				songList.Add(new List<int>());
				songList[n] = songList[i].GetRange(0, mid);

				totalSize += songList[n].Count;

				listParent.Add(i);

				n++;

				songList.Add(new List<int>());
				songList[n] = songList[i].GetRange(mid, (songList[i].Count) - mid);

				totalSize += songList[n].Count;

				listParent.Add(i);

				n++;
			}
		}

		for (i = 0; i < matchData.songNames.Count; i++)
		{
			recordedRanks.Add(0);
		}

		rankIdx = 0;

		comparer1 = songList.Count - 2;
		comparer2 = songList.Count - 1;

		head1 = 0;
		head2 = 0;

		questionNum = 1;
		finishSize = 0;
		totalSize = 0;
		isFinished = false;

	}

	public void SortList(int flag)
	{

		int i;

		//Add the song to the next index of recordedRanks based on input from the controller.
		if (flag < 0) // "Left Choice"
		{
			recordedRanks[rankIdx] = songList[comparer1][head1];
			head1++;
			rankIdx++;
			finishSize++;
		}

		else if (flag > 0) // "Right Choice"
		{

			recordedRanks[rankIdx] = songList[comparer2][head2];
			head2++;
			rankIdx++;
			finishSize++;

		}
		else
		{
			Debug.LogError("Bad response recieved in sortList. Invalid flag entered.");
			return;
		}

		//Process the remainder of one list if the other is now empty.
		if (head1 < songList[comparer1].Count && head2 == songList[comparer2].Count)
		{
			//Comparer2 copies
			while (head1 < songList[comparer1].Count)
			{
				recordedRanks[rankIdx] = songList[comparer1][head1];
				head1++;
				rankIdx++;
				finishSize++;
			}

		}
		else if (head1 == songList[comparer1].Count && head2 < songList[comparer2].Count)
		{
			//Comparer1 copies
			while (head2 < songList[comparer2].Count)
			{
				recordedRanks[rankIdx] = songList[comparer2][head2];
				head2++;
				rankIdx++;
				finishSize++;
			}

		}




		//Extra processing when both lists are empty
		if (head1 == songList[comparer1].Count && head2 == songList[comparer2].Count)
		{
			for (i = 0; i < songList[comparer1].Count + songList[comparer2].Count; i++)
			{
				songList[listParent[comparer1]][i] = recordedRanks[i];
			}

			songList.RemoveAt(songList.Count - 1);
			songList.RemoveAt(songList.Count - 1);

			comparer1 = comparer1 - 2;
			comparer2 = comparer2 - 2;

			head1 = 0;
			head2 = 0;


			//Reset recordedRanks to its initial value.
			if (head1 == 0 && head2 == 0)
			{
				for (i = 0; i < matchData.songNames.Count; i++)
				{
					recordedRanks[i] = 0;
				}

				rankIdx = 0;
			}
		}


		//If there's nothing left to compare, then the game's over.
		if (comparer1 < 0)
		{
			showResult();

			isFinished = true;
		}

		else
		{
			updateView();
		}

	}

	#endregion

	#region View Communication

	private void showResult()
	{

		int ranking = 1;
		int sameRank = 1;
		int i;

		for (i = 0; i < matchData.songNames.Count; i++)
		{
			if (i < matchData.songNames.Count - 1)
			{
				ranking += sameRank;
				sameRank = 1;
			}

		}
	}

	private void updateView()
	{
		Debug.Log(questionNum + ":  " + matchData.songNames[songList[comparer1][head1]] + " ||| " + matchData.songNames[songList[comparer2][head2]]);
		questionNum++;
	}

	#endregion
}
