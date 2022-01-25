using UnityEngine;

namespace Assets.HeadStart.Features.ScreenData
{
    public class PointIconParticle : PointTextParticle, IPoolObject, IPointText
    {
        void IPointText.ChangeValue(int points, Color color)
        {
            _pointsValue = points;
            _textComponent.text = _pointsValue.ToString();
            _currentColor = color;
            _textComponent.color = color;
        }
    }
}
