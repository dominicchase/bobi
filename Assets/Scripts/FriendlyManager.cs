using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyManager : MonoBehaviour
{
    #region singleton

    public static FriendlyManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject friends;
}
