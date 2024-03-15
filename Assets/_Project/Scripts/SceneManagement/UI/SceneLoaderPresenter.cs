using UI;

namespace SceneManagement.UI
{
    public class SceneLoaderPresenter
    {
        private IViewFader _fader; 
        
        
        public SceneLoaderPresenter(IViewFader screenFader)
        {
            _fader = screenFader;
        }

        public void ShowLoadScreen()
        {
            _fader.FadeIn();
        }

        public void HideLoadScreen()
        {
            _fader.FadeOut();
        }

        public void HideLoadScreenImmediately()
        {
            _fader.FadeOut();
        }
    }
}