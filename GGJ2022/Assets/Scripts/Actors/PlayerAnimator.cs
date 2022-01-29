using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : ActorAnimator
{
    [SerializeField] protected Transform _leftEyeWhite;
    [SerializeField] protected Transform _rightEyeWhite;

    [SerializeField] protected Transform _rightEyeBlack;
    [SerializeField] protected Transform _leftEyeBlack;

    [SerializeField] private float whiteOffsetX = 0.1f;
    [SerializeField] private float whiteLerp = 0.05f;
    [SerializeField] private float blackffsetX = 0.1f;

    public override void UpdateAnimator()
    {
        base.UpdateAnimator();
        UpdateEyeWhitePosition(
                (_actorRuntime.WalkRight ? 1 : -1) * whiteOffsetX);
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMouse = CameraManager.Instance.MainCamera.ScreenToWorldPoint(mousePosition);

        Vector3 direction = worldMouse - transform.position;
        float angle = Vector3.SignedAngle(Vector3.up, direction,
             Vector3.forward);
        _rightEyeBlack.localPosition = direction.normalized* blackffsetX;
        _leftEyeBlack.localPosition = direction.normalized * blackffsetX;
    }

    private void UpdateEyeWhitePosition(float offsetX)
    {
        Vector3 leftEyeWhitePos = _leftEyeWhite.transform.localPosition;
        _leftEyeWhite.transform.localPosition =
            Vector3.Lerp(leftEyeWhitePos,
             new Vector3(offsetX, 0,0)
                , whiteLerp);
        Vector3 rightEyeWhitePos = _rightEyeWhite.transform.localPosition;

        _rightEyeWhite.transform.localPosition =
            Vector3.Lerp(rightEyeWhitePos,
             new Vector3(offsetX, 0, 0)
             //rightEyeWhitePos + offsetX * Vector3.right
                , whiteLerp);
    }
}
