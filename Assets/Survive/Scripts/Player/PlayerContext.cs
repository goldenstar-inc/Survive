using UnityEngine;
public class PlayerContext : MonoBehaviour, IPlayerDataProvider
{
    [SerializeField] AmmoHandler ammo;
    public AmmoHandler ammoHandler => ammo;
}
