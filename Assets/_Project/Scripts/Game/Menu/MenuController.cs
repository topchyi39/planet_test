using Game.Menu.UI;
using SceneManagement;
using UnityEngine;
using Zenject;

namespace Game.Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private MenuView view;

        private SceneLoader _loader;
        
        [Inject]
        private void Construct(SceneLoader loader)
        {
            _loader = loader;
            if (!view) return;

            view.StartPressed += LoadGamePlay;
        }

        private void LoadGamePlay()
        {
            _loader.LoadLevel(1);
        }
    }
}