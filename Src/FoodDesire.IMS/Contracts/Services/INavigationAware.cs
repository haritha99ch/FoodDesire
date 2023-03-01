namespace FoodDesire.IMS.Contracts.Services;
public interface INavigationAware {
    void OnNavigatedTo(object parameter);
    void OnNavigatedFrom();
}
