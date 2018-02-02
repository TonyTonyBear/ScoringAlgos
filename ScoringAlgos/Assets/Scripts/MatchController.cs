using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DataProcessor))]
public class MatchController : MonoBehaviour
{
	#region Properties

	public KeyCode leftSelection, rightSelection;

	#endregion

	#region Monobehavior

	private void Update()
	{
		if (!DataProcessor.instance.IsFinished)
		{
			if (Input.GetKeyDown(leftSelection))
			{
				SelectLeft();
			}
			if (Input.GetKeyDown(rightSelection))
			{
				SelectRight();
			}
		}
	}

	#endregion

	#region Model Communication

	private void SelectLeft()
	{
		DataProcessor.instance.SortList(-1);
	}

	private void SelectRight()
	{
		DataProcessor.instance.SortList(1);
	}

	#endregion
}
