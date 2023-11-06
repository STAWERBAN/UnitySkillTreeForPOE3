using SkillGraph.Models;
using UnityEngine;
using UnityEngine.UI;

namespace SkillGraph.Views
{
    public class SkillPointIncreaseProxy : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private Wallet _skillWallet;

        private void Awake()
        {
            _button.onClick.AddListener(IncreaseBalance);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(IncreaseBalance);
        }

        private void IncreaseBalance()
        {
            _skillWallet.Put();
        }

        public void SetWallet(Wallet skillWallet)
        {
            _skillWallet = skillWallet;
        }
    }
}