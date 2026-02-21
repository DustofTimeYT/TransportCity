public interface IView<T>
{
    public void Bind(T presenter);

    public void Show();

    public void Hide();
}