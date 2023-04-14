namespace FoodDesire.Web.Client.Contracts.Services;
public interface IComponentCommunicationService<T> {
    T Value { get; set; }
    event Action<T> OnChange;
    void NotifyStateChanged(T state);
}
