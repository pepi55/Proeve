using UnityEngine;
using System.Collections;

/// <summary>
/// needs a vector as argument
/// </summary>
/// <param name="position">Location where you clicked</param>
public delegate void ClickDelegate(Vector2 position);

/// <summary>
/// Generic Delegate that needs no arguments
/// </summary>
public delegate void VoidDelegate();