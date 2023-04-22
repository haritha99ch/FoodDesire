namespace FoodDesire.Web.Client.Services;
public class ComponentCommunicationService<T> : IComponentCommunicationService<T> {
    public T? Value { get; set; }
    public event Action<T>? OnChange;

    public void NotifyStateChanged(T state) {
        Value = state;
        OnChange?.Invoke(Value);
    }
}
