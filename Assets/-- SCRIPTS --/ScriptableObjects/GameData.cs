using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "GameData", order = 1)]
public class GameData : ScriptableObject
{

    [Range(0f, 1f)] public float perfectTolerance, niceTolerance, okTolerance;

    private void OnValidate()
    {
        perfectTolerance = Mathf.Clamp(perfectTolerance, niceTolerance, 1f);
        niceTolerance = Mathf.Clamp(niceTolerance, okTolerance, niceTolerance);
        okTolerance = Mathf.Clamp(okTolerance, 0f, niceTolerance);
    }
}
