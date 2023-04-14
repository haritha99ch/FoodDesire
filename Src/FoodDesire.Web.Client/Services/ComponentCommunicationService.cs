namespace FoodDesire.Web.Client.Services;
public class ComponentCommunicationService<T> : IComponentCommunicationService<T> {
    private T? _value;
    public T Value {
        get => _value!;
        set => _value = value;
    }
    public event Action<T>? OnChange;

    public void NotifyStateChanged(T state) {
        Value = state;
        OnChange?.Invoke(Value);
    }
}
