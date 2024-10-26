using UnityEngine;
namespace Manager
{
    public class PlayerManager : SingletonMono<PlayerManager>
    {
        [SerializeField] private Player player;

        public Player Player => player;
    }
}
