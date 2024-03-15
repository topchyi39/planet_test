using AI;

namespace Allies
{
    public class Ally : AIController
    {
        protected override void LateUpdateCallback()
        {
            SetTarget(Player.Position);
        }
    }
}