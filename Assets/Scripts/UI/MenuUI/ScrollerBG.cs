using UnityEngine;
using UnityEngine.UI;

public class ScrollerBG : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _scrollSpeedX, _scrollSpeedY;
    [SerializeField] private float _rotationSpeedZ;

    void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_scrollSpeedX, _scrollSpeedY) * Time.deltaTime, _img.uvRect.size);

        Quaternion newRotation = Quaternion.Euler(0f, 0f, _rotationSpeedZ * Time.deltaTime);
        _img.rectTransform.localRotation *= newRotation;
    }
}


