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
            // DOTween�̳� Animator ��� ����
        }

        protected virtual void PlayCloseAnimation()
        {
            // DOTween�̳� Animator ��� ����
        }
    }
}