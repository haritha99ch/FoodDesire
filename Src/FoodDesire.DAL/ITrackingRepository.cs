using FoodDesire.Models;

namespace FoodDesire.DAL;
public interface ITrackingRepository<T> where T : TrackedEntity {
    Task<T> SoftDelete(int Id);
}
