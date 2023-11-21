using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCameraFlowLayer : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransfrom;    // �Ǐ]�Ώۂ̃J����
    [SerializeField] private float _followFactor;   // �J�����ɒǏ]������x(1: �J�����Ɠ����ړ��� 0: �ړ����Ȃ�)

    private Vector2 _previousCameraPos;

    private void Update()
    {
        Vector2 currentPos = _cameraTransfrom.position;
        var deltaPos = currentPos - _previousCameraPos;
        _previousCameraPos = currentPos;
        var calcedPos = deltaPos * _followFactor;
        transform.AddLocalPos((Vector2)calcedPos);
    }
}

// ���[�e�B���e�B
public static class ParallaxCameraFlowLayerExtensions
{
    public static void AddLocalPos(this Transform self, in Vector2 pos)
    {
        Vector2 vec = self.localPosition;
        vec.x += pos.x;
        vec.y += pos.y;
        //vec.x = Mathf.Round(vec.x) / 10;
        self.localPosition = vec;
    }
}