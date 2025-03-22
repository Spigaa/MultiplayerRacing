using Fusion;
using UnityEngine;

public enum InputButton
{
    Brake
}

public struct NetInput : INetworkInput
{
    public NetworkButtons Buttons;
    public Vector2 Direction;
}