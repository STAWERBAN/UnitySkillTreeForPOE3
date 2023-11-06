using System.Threading;
using Cysharp.Threading.Tasks;
using SkillGraph.Screens.Services;
using SkillGraph.Screens.Views;
using UnityEngine;
using UnityEngine.UI;

namespace SkillGraph.Views
{
    public class CloseScreenProxy : MonoBehaviour
    {
        [SerializeField] private ScreenViewBase _screenViewBase;
        [SerializeField] private Button _close;

        private IScreenService _screenService;

        private void Awake()
        {
            _close.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDestroy()
        {
            _close.onClick.RemoveListener(OnCloseButtonClicked);
        }

        public void SetScreenService(IScreenService screenService)
        {
            _screenService = screenService;
        }

        private void OnCloseButtonClicked()
        {
            _screenService.HideAsync(_screenViewBase, CancellationToken.None).Forget();
        }
    }
}