using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollController : MonoBehaviour
{
    [SerializeField] private int dollScore;

    public int GetDollScore()
    {
        return dollScore;
    }
}
