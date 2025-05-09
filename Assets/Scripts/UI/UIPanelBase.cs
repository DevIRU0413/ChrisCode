using UnityEngine;

namespace Scripts.UI
{
    public abstract class UIPanelBase : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
            PlayOpenAnimation();
        }

        public virtual void Hide()
        {
            PlayCloseAnimation();
        }

        protected virtual void PlayOpenAnimation()
        {
            // DOTween이나 Animator 사용 가능
        }

        protected virtual void PlayCloseAnimation()
        {
            // DOTween이나 Animator 사용 가능
        }
    }
}