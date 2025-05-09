using Scripts.Interface;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.Manager
{
    public class SceneManagerEx : SimpleSingleton<SceneManagerEx>, IManager
    {
        #region PrivateVariables
        [Header("Fade Settings")]
        [SerializeField] private CanvasGroup m_fadeCanvasGroup;
        [SerializeField] private float m_fadeDuration = 1f;

        [Header("Loading UI")]
        [SerializeField] private GameObject m_loadingUI;
        [SerializeField] private Slider m_loadingBar;

        #endregion

        #region PublicVariables
        public event Action<string> OnSceneLoaded;
        public int Priority => (int)ManagerPriority.SceneManagerEx;
        #endregion

        #region PublicMethod
        public void Initialize() 
        { 
            if(m_fadeCanvasGroup != null)
            {
                m_fadeCanvasGroup.gameObject.SetActive(true);
                m_fadeCanvasGroup.GetComponent<Canvas>().sortingOrder = 100;
                m_fadeCanvasGroup.GetComponent<CanvasGroup>().alpha = 0f;
            }

            if (m_loadingUI != null && m_loadingBar != null)
            {
                m_loadingUI.GetComponent<Canvas>().sortingOrder = 100;
            }
        }

        public void Cleanup()
        {
            throw new NotImplementedException();
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        public string GetCurrentSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }

        public void LoadSceneAsync(string sceneName)
        {
            StartCoroutine(IE_LoadScene(sceneName));
        }

        public void LoadSceneWithFade(string sceneName)
        {
            StartCoroutine(IE_LoadSceneWithFade(sceneName));
        }

        public void LoadSceneAsyncWithLoading(string sceneName)
        {
            StartCoroutine(IE_LoadSceneWithLoadingUI(sceneName));
        }

        public void UnloadSceneAsync(string sceneName)
        {
            StartCoroutine(IE_UnloadScene(sceneName));
        }
        #endregion

        #region PrivateMethod
        private IEnumerator IE_LoadScene(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            yield return new WaitUntil(() => operation.isDone);
            OnSceneLoaded?.Invoke(sceneName);
        }

        private IEnumerator IE_LoadSceneWithFade(string sceneName)
        {
            yield return StartCoroutine(IE_FadeOut());

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            yield return new WaitUntil(() => operation.isDone);

            OnSceneLoaded?.Invoke(sceneName);
            yield return StartCoroutine(IE_FadeIn());
        }

        private IEnumerator IE_LoadSceneWithLoadingUI(string sceneName)
        {
            if (m_loadingUI != null)
                m_loadingUI.SetActive(true);

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            while (operation.progress < 0.9f)
            {
                if (m_loadingBar != null)
                    m_loadingBar.value = operation.progress;

                yield return null;
            }

            if (m_loadingBar != null)
                m_loadingBar.value = 1f;

            yield return new WaitForSeconds(0.5f);
            operation.allowSceneActivation = true;

            yield return new WaitUntil(() => operation.isDone);

            if (m_loadingUI != null)
                m_loadingUI.SetActive(false);

            OnSceneLoaded?.Invoke(sceneName);
        }

        private IEnumerator IE_UnloadScene(string sceneName)
        {
            AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneName);
            yield return new WaitUntil(() => operation.isDone);
        }

        private IEnumerator IE_FadeOut()
        {
            if (m_fadeCanvasGroup == null) yield break;

            m_fadeCanvasGroup.blocksRaycasts = true;
            float time = 0f;

            while (time < m_fadeDuration)
            {
                time += Time.deltaTime;
                m_fadeCanvasGroup.alpha = Mathf.Lerp(0, 1, time / m_fadeDuration);
                yield return null;
            }
        }

        private IEnumerator IE_FadeIn()
        {
            if (m_fadeCanvasGroup == null) yield break;

            float time = 0f;

            while (time < m_fadeDuration)
            {
                time += Time.deltaTime;
                m_fadeCanvasGroup.alpha = Mathf.Lerp(1, 0, time / m_fadeDuration);
                yield return null;
            }

            m_fadeCanvasGroup.blocksRaycasts = false;
        }
        #endregion
    }
}
