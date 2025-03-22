using Fusion;
using UnityEngine;

public class GameManager : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] private NetworkPrefabRef _player;
    [Networked, Capacity(6)] private NetworkDictionary<PlayerRef, Player_Core> allPlayers => default;

    public void PlayerJoined(PlayerRef player) {
        if (HasStateAuthority) {
            NetworkObject playerObject = Runner.Spawn(_player, Vector3.up, Quaternion.identity, player);
            allPlayers.Add(player, playerObject.GetComponent<Player_Core>());
        }
    }

    public void PlayerLeft(PlayerRef player) {
        if (HasStateAuthority && allPlayers.TryGet(player, out Player_Core p)) {
            allPlayers.Remove(player);
            Runner.Despawn(p.Object);
        }
    }
}
