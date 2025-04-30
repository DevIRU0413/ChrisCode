using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.Manager
{
    // ��ü���� �񵿱� ���ε� ��� ���
    // �׳� ���ε� ��� ��� ����
    public class SceneManagerEx : SimpleSingleton<SceneManagerEx>
    {
        // ���� �ε��� ������ ��, �� �̵��Ϸ��� ���� �ε� �� �Ǿ��� �� �����.
        // �ڽ��� ���ϴ� ���� �ε��� �Ǿ��� ��, �ʿ��ϴٰ� �����ϴ� �͵��� �־��ָ�ȴ�.
        // ���ٽ����� �޾Ƶ� �ǰ� ���
        // ����ϴٰ� �ϸ� Title���� �ΰ��� �Ѿ �� ����� ���ɼ� ����(�ε� UI �ݰų�, bgm Ʋ���ְų�..., ������ �ε� ����ص��ǰ�)
        public event Action<string> OnSceneLoaded;

        [Header("Fade Settings(���� ���)")]
        [SerializeField] private CanvasGroup _fadeCanvasGroup; // DontDestroy Object
        [SerializeField] private float _fadeDuration = 1f;

        [Header("Loading UI(���� ���)")]
        [SerializeField] private GameObject _loadingUI; // DontDestroy Object
        [SerializeField] private Slider _loadingBar;

        public string GetCurrentSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }

        // Scene
        public void LoadSceneAsync(string sceneName)
        {
            StartCoroutine(LoadSceneRoutine(sceneName));
        }
        public void LoadSceneWithFade(string sceneName)
        {
            StartCoroutine(LoadSceneWithFadeRoutine(sceneName));
        }
        public void LoadSceneAsyncWithLoading(string sceneName)
        {
            StartCoroutine(LoadSceneWithLoadingUI(sceneName));
        }
        public void UnloadSceneAsync(string sceneName)
        {
            // ���߿� ���� ������ ����ְ� ������ �� ����� ����(���� ������)
            StartCoroutine(UnloadSceneRoutine(sceneName));
        }

        // Coroutine
        private IEnumerator LoadSceneRoutine(string sceneName)
        {
            // �ε� ���� ���¿� �Ϸ� ���� ��ü(�񵿱�)
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName); // �񵿱� ���ε�
            yield return new WaitUntil(() => op.isDone); // �ε� �Ϸ� ���� Ȯ�α��� ��ٸ���
            OnSceneLoaded?.Invoke(sceneName); // 
        }
        private IEnumerator LoadSceneWithFadeRoutine(string sceneName)
        {
            yield return StartCoroutine(FadeOut());
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName); // �񵿱� ���ε�
            yield return new WaitUntil(() => op.isDone); // �ε� �Ϸ� ���� Ȯ�α��� ��ٸ���
            OnSceneLoaded?.Invoke(sceneName);
            yield return StartCoroutine(FadeIn());
        }
        private IEnumerator LoadSceneWithLoadingUI(string sceneName)
        {
            if (_loadingUI != null)
                _loadingUI.SetActive(true);

            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName); // �񵿱�
            op.allowSceneActivation = false; // �ڵ� �� ��ȯ �ȵǵ���

            // 90%���� ���
            while (op.progress < 0.9f)
            {
                if (_loadingBar != null)
                    _loadingBar.value = op.progress;
                yield return null;
            }

            if (_loadingBar != null)
                _loadingBar.value = 1f;

            yield return new WaitForSeconds(0.5f);
            op.allowSceneActivation = true; // ����ȯ ����

            yield return new WaitUntil(() => op.isDone); // �Ϸ�

            if (_loadingUI != null)
                _loadingUI.SetActive(false);

            OnSceneLoaded?.Invoke(sceneName);
        }
        private IEnumerator UnloadSceneRoutine(string sceneName)
        {
            AsyncOperation op = SceneManager.UnloadSceneAsync(sceneName);
            yield return new WaitUntil(() => op.isDone);
        }

        // ETC(���߿� �ű� ����)
        private IEnumerator FadeOut()
        {
            if (_fadeCanvasGroup == null) yield break;
            _fadeCanvasGroup.blocksRaycasts = true;
            float t = 0f;
            while (t < _fadeDuration)
            {
                t += Time.deltaTime;
                _fadeCanvasGroup.alpha = Mathf.Lerp(0, 1, t / _fadeDuration);
                yield return null;
            }
        }
        private IEnumerator FadeIn()
        {
            if (_fadeCanvasGroup == null) yield break;
            float t = 0f;
            while (t < _fadeDuration)
            {
                t += Time.deltaTime;
                _fadeCanvasGroup.alpha = Mathf.Lerp(1, 0, t / _fadeDuration);
                yield return null;
            }
            _fadeCanvasGroup.blocksRaycasts = false;
        }
    }
}
