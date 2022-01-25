using UnityEngine;
using UnityEngine.UI;

namespace Assets.HeadStart.Features.ScreenData
{
    public interface IPointText : IPoolObject
    {
        void Init(int id, Vector2 totalPointsPos, Vector2Int actualScreenSize, PointTextParticle.OnPointsUpdated onPointsUpdated);
        void ChangeValue(int points, Color color);
        void ShowOnScreen(Vector3 ballsCenterPosition, Vector2 reflectDir);
    }
}