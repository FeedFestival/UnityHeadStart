using System;

namespace DentedPixel
{
    public class GameScryptTween
    {
        public int? Id;
        private Action<float> _onUpdate;
        private Action _onComplete;

        public void Cancel()
        {
            if (Id.HasValue == false) { return; }
            LeanTween.cancel(Id.Value);
            Id = null;
        }

        public void Start(LTDescr lTDescr)
        {
            if (Id.HasValue) { Cancel(); }

            Id = lTDescr.id;
        }

        public void Start(LTDescr lTDescr, Action onComplete = null)
        {
            _onComplete = onComplete;

            Start(lTDescr);

            if (_onComplete != null)
            {
                LeanTween.descr(Id.Value).setOnComplete(_onComplete);
            }
        }

        public void Start(LTDescr lTDescr, Action<float> onUpdate = null, Action onComplete = null)
        {
            _onUpdate = onUpdate;
            _onComplete = onComplete;

            Start(lTDescr);

            if (_onUpdate != null)
            {
                LeanTween.descr(Id.Value).setOnUpdate(_onUpdate);
            }

            if (_onComplete != null)
            {
                LeanTween.descr(Id.Value).setOnComplete(_onComplete);
            }
        }
    }
}
