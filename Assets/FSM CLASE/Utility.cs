﻿using UnityEngine;
using System.Collections.Generic;
using System;
public static class Utility {

	public static T Log<T>(T param, string message = "") {
		Debug.Log(message +  param.ToString());
		return param;
	}

	public static int Clampi(int v, int min, int max)
	{
		return v < min ? min : (v > max ? max : v);
	}

	public static IEnumerable<Src> Generate<Src>(Src seed, Func<Src, Src> generator)
	{
		while (true)
		{
			yield return seed;
			seed = generator(seed);
		}
	}
}
