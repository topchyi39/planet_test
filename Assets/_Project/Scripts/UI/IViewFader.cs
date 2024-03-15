namespace UI
{
    public interface IViewFader
    {
        void FadeIn();
        void FadeOut(bool immediately = false);
    }
}