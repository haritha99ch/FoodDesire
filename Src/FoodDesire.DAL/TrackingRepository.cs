namespace FoodDesire.DAL;
public class TrackingRepository<T>: Repository<T>, ITrackingRepository<T> where T : TrackedEntity {

    public TrackingRepository(FoodDesireContext context) : base(context) { }

    public async Task<List<T>> GetAllTracked() {
        List<T>? entities = await _context.Set<T>().AsNoTracking().Where(e => !e.Deleted).ToListAsync();
        return entities;
    }
    public async Task<bool> SoftDelete(int Id) {
        T? entity = await GetByID(Id);

        if(entity == null) return false;
        entity.Deleted = true;

        T? updatedEntity = await Update(entity);
        await _context.SaveChangesAsync();

        return updatedEntity.Deleted;
    }
}
