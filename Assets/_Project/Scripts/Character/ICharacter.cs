using Player;

namespace Character
{
    public interface ICharacter
    {
        IPlayerView View { get; }
        bool LockOn { get; }
    }
}