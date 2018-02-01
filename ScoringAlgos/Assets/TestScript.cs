using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
	private void Start()
	{
		initList();

		Debug.Log(namMember[lstMember[cmp1][head1]]);
		Debug.Log(namMember[lstMember[cmp2][head2]]);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			sortList(-1);
		if (Input.GetKeyDown(KeyCode.RightArrow))
			sortList(1);
	}

	string[] namMember = new string[]
	{

	"You Are In Love",
	"Wonderland",
	"New Romantics",
	"Welcome To New York",
	"Blank Space",
	"Style",
	"Out Of The Woods",
	"All You Had To Do Was Stay",
	"Shake It Off",
	"I Wish You Would",
	"Bad Blood",
	"Wildest Dreams",
	"How You Get The Girl",
	"This Love",
	"I Know Places",
	"Clean"
	};

	//*********************************************************



	List<List<int>> lstMember = new List<List<int>>();
	List<int> parent = new List<int>();
	List<int> equal = new List<int>();
	List<int> rec = new List<int>();

	int cmp1, cmp2;
	int head1, head2;
	int nrec;

	int numQuestion;
	int totalSize;
	int finishSize;
	int finishFlag;


	//The initialization of the variable+++++++++++++++++++++++++++++++++++++++++++++

	public void initList()
	{

		int n = 0;
		int mid;
		int i;

		//The sequence that you should sort

		lstMember.Add(new List<int>()); //1.

		for (i = 0; i < namMember.Length; i++) //2.
		{

			lstMember[n].Add(i);

		}

		parent.Add(-1); // 3.

		totalSize = 0; // 3.

		n++; // 3.



		for (i = 0; i < lstMember.Count; i++) // 4. - 
		{
			//And element divides it in two/more than two

			//Increase divided sequence of last in first member

			if (lstMember[i].Count >= 2) // 5.
			{

				mid = (int)Mathf.Ceil(lstMember[i].Count / 2); // 6.

				lstMember.Add(new List<int>()); // 7.

				lstMember[n] = lstMember[i].GetRange(0, mid); // 8.

				totalSize += lstMember[n].Count; // 9.

				parent.Add(i); // 10.

				n++; // 10.

				lstMember.Add(new List<int>()); // 11.

				lstMember[n] = lstMember[i].GetRange(mid, (lstMember[i].Count) - mid); // 11.

				totalSize += lstMember[n].Count; // 12.

				parent.Add(i); // 13.

				n++; // 13.

			}

		}



		//Preserve this sequence

		for (i = 0; i < namMember.Length; i++)
		{

			rec.Add(0);

		}

		nrec = 0;



		//List that keeps your results

		//Value of link initial
		for (i = 0; i <= namMember.Length; i++)
		{

			equal.Add(-1);

		}



		cmp1 = lstMember.Count - 2;

		cmp2 = lstMember.Count - 1;

		head1 = 0;

		head2 = 0;

		numQuestion = 1;

		finishSize = 0;

		finishFlag = 0;

	}



	//&#12522;&#12473;&#12488;&#12398;&#12477;&#12540;&#12488;+++++++++++++++++++++++++++++++++++++++++++

	//flag&#65306;Don't know characters

	// -1&#65306;Chose the left

	// 0&#65306;Tie

	// 1&#65306;Chose the right

	public void sortList(int flag)
	{

		int i;

		string str;



		//rec preservation

		if (flag < 0) // "Left Choice"
		{

			rec[nrec] = lstMember[cmp1][head1];

			head1++;

			nrec++;

			finishSize++;

			while (equal[rec[nrec - 1]] != -1)
			{

				rec[nrec] = lstMember[cmp1][head1];

				head1++;

				nrec++;

				finishSize++;

			}

		}

		else if (flag > 0) // "Right Choice"
		{

			rec[nrec] = lstMember[cmp2][head2];

			head2++;

			nrec++;

			finishSize++;

			while (equal[rec[nrec - 1]] != -1)
			{

				rec[nrec] = lstMember[cmp2][head2];

				head2++;

				nrec++;

				finishSize++;

			}

		}

		else // "Center Choice - Can't Decide"
		{

			rec[nrec] = lstMember[cmp1][head1];

			head1++;

			nrec++;

			finishSize++;

			while (equal[rec[nrec - 1]] != -1)
			{

				rec[nrec] = lstMember[cmp1][head1];

				head1++;

				nrec++;

				finishSize++;

			}

			equal[rec[nrec - 1]] = lstMember[cmp2][head2];

			rec[nrec] = lstMember[cmp2][head2];

			head2++;

			nrec++;

			finishSize++;

			while (equal[rec[nrec - 1]] != -1)
			{

				rec[nrec] = lstMember[cmp2][head2];

				head2++;

				nrec++;

				finishSize++;

			}

		}



		//Processing after finishing with one list

		if (head1 < lstMember[cmp1].Count && head2 == lstMember[cmp2].Count)
		{

			//List the remainder of cmp2 copies, list cmp1 copies when finished scanning

			while (head1 < lstMember[cmp1].Count)
			{

				rec[nrec] = lstMember[cmp1][head1];

				head1++;

				nrec++;

				finishSize++;

			}

		}

		else if (head1 == lstMember[cmp1].Count && head2 < lstMember[cmp2].Count)
		{

			//List the remainder of cmp1 copies, list cmp2 copies when finished scanning

			while (head2 < lstMember[cmp2].Count)
			{

				rec[nrec] = lstMember[cmp2][head2];

				head2++;

				nrec++;

				finishSize++;

			}

		}



		//When it arrives at the end of both lists

		//Update a pro list

		if (head1 == lstMember[cmp1].Count && head2 == lstMember[cmp2].Count)
		{

			for (i = 0; i < lstMember[cmp1].Count + lstMember[cmp2].Count; i++)
			{

				lstMember[parent[cmp1]][i] = rec[i];

			}

			lstMember.RemoveAt(lstMember.Count - 1);
			lstMember.RemoveAt(lstMember.Count - 1);

			cmp1 = cmp1 - 2;

			cmp2 = cmp2 - 2;

			head1 = 0;

			head2 = 0;



			//Initialize the rec before performing the new comparison

			if (head1 == 0 && head2 == 0)
			{

				for (i = 0; i < namMember.Length; i++)
				{

					rec[i] = 0;

				}

				nrec = 0;

			}

		}



		if (cmp1 < 0)
		{

			str = "Battle #" + (numQuestion - 1) + "<br>" + Mathf.Floor(finishSize * 100 / totalSize) + "% sorted.";

			//document.getElementById("battleNumber").innerHTML = str;



			showResult();

			finishFlag = 1;

		}

		else
		{

			//showImage();

		}

	}



	//The results+++++++++++++++++++++++++++++++++++++++++++++++

	//&#38918;&#20301;=Rank/Grade/Position/Standing/Status

	//&#21517;&#21069;=Identification term

	public void showResult()
	{

		int ranking = 1;

		int sameRank = 1;

		string str = "";

		int i;



		str += "<table style=\"width:200px; font-size:18px; line-height:120%; margin-left:auto; margin-right:auto; border:1px solid #000; border-collapse:collapse\" align=\"center\">";

		//str += "<tr><td style=\"color:#ffffff; background-color:#000; text-align:center;\">Rank<\/td><td style=\"color:#ffffff; background-color:#000; text-align:center;\">Song<\/td><\/tr>";



		for (i = 0; i < namMember.Length; i++)
		{

			//str += "<tr><td style=\"border:1px solid #000; text-align:center; padding-right:5px;\">" + ranking + "<\/td><td style=\"border:1px solid #000; padding-left:5px;\">" + namMember[lstMember[0][i]] + "<\/td><\/tr>";

			if (i < namMember.Length - 1)
			{

				if (equal[lstMember[0][i]] == lstMember[0][i + 1])
				{

					sameRank++;

				}
				else
				{

					ranking += sameRank;

					sameRank = 1;

				}

			}

		}

		//str += "<\/table>";



		//document.getElementById("resultField").innerHTML = str;

	}


}
