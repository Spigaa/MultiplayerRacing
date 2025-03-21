using Fusion;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private NetworkPrefabRef prefab;
    [Networked, Capacity(6)] private NetworkDictionary<PlayerRef, Player_Core> Players => default;
}
