using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Date
{
	public int day = 0, month = 0;

	public int GetDay()
	{
		return day;
	}
	public int GetMonth()
	{
		return month;
	}

	public void SetDay(int newDay)
	{
		day = newDay;
	}

	public void SetMonth(int newMonth)
	{
		month = newMonth;
	}
}
