using System.Collections.Generic;
using SkillGraph.Screens.Services;
using UnityEngine;

namespace SkillGraph.Views
{
    public class CloseScreenContainer : MonoBehaviour
    {
        [SerializeField] private List<CloseScreenProxy> _closeScreens;

        public void SetScreenService(IScreenService screenService)
        {
            _closeScreens.ForEach(a => a.SetScreenService(screenService));
        }
    }
}