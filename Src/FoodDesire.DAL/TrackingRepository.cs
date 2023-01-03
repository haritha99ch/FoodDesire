namespace FoodDesire.DAL;
public class TrackingRepository<T>: Repository<T>, ITrackingRepository<T> where T : TrackedEntity {

    public TrackingRepository(FoodDesireContext context) : base(context) { }

    public async Task<List<T>> GetAllTracked() {
        List<T>? entities = await _context.Set<T>().Where(e => !e.Deleted).ToListAsync();
        return entities;
    }
    public async Task<T> SoftDelete(int Id) {
        T? entity = await GetByID(Id);
        if(entity == null) return null!;
        entity.Deleted = true;
        await _context.SaveChangesAsync();
        return entity;
        throw new NotImplementedException();
    }
}
