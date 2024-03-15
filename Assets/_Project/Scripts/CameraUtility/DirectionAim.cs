using Cinemachine;
using UnityEngine;

namespace CameraUtility
{
    [AddComponentMenu("")] // Don't display in add component menu
    [SaveDuringPlay]
    public class DirectionAim : CinemachineComponentBase
    {
        public override CinemachineCore.Stage Stage => CinemachineCore.Stage.Aim;
        public override bool IsValid => enabled && LookAtTarget != null;

        public override void MutateCameraState(ref CameraState curState, float deltaTime)
        {
            if (!IsValid) return;
            var direction = (LookAtTargetPosition-curState.CorrectedPosition).normalized;
            
            curState.RawOrientation = Quaternion.LookRotation(direction);
        }

    }
}