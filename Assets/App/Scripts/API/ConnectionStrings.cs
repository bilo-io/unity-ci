using System;
using UnityEngine;

using UnityCI.Core;

public class ConnectionStrings : SingletonBehaviour<ConnectionStrings>
{
    [SerializeField]
    public string url;
}
