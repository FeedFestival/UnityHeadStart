using UnityEngine;

namespace GameScrypt.GSCamera
{
    /// <summary>
    /// <seealso>v.3.0.0</seealso>
    /// </summary>
    public class GSLogo : MonoBehaviour
    {
        public Transform LogoT;
        protected Vector3 _toLogoSize;
        private GSLeanTween enlargeLogoLT;
        private GSLeanTween fadeAlphaLT;
        protected readonly float HIDE_LOGO_TIME = 0.77f;
        protected readonly Vector3 TO_LOGO_SIZE = new Vector3(44, 44, 44);

        public void SetDefaultScale()
        {
            LogoT.localScale = new Vector3(4, 4, 4);
        }

        public void EnlargeLogo()
        {
            _toLogoSize = (_toLogoSize + (TO_LOGO_SIZE * 2));

            if (enlargeLogoLT == null) { enlargeLogoLT = new GSLeanTween(); }

            enlargeLogoLT.Start(
                LeanTween.scale(LogoT.gameObject, _toLogoSize, GSCamera.CAMERA_SETUP_TIME)
            );
        }

        public void RevealLogo()
        {
            if (fadeAlphaLT == null) { fadeAlphaLT = new GSLeanTween(); }

            fadeAlphaLT.Start(
                LeanTween.alpha(LogoT.gameObject, 1f, GSCamera.CAMERA_SETUP_TIME)
            );
        }

        public void HideLogo()
        {
            if (LogoT == null) { return; }

            if (fadeAlphaLT == null) { fadeAlphaLT = new GSLeanTween(); }

            fadeAlphaLT.Start
            (
                LeanTween.alpha(LogoT.gameObject, 0f, HIDE_LOGO_TIME),
                onComplete: () =>
                {
                    this.destroyLogo();
                }
            );
        }

        public void CancelAnimation()
        {
            enlargeLogoLT.Cancel();
        }

        private void destroyLogo()
        {
            fadeAlphaLT = null;
            Destroy(this.gameObject);
        }
    }
}
