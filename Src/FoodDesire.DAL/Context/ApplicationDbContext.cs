using FoodDesire.DAL.Helpers;
using Newtonsoft.Json;

namespace FoodDesire.DAL.Context;
public class ApplicationDbContext : DbContext {
    public DbSet<User>? User { get; set; }
    public DbSet<Account>? Account { get; set; }
    public DbSet<Address>? Address { get; set; }
    public DbSet<Admin>? Admin { get; set; }
    public DbSet<Customer>? Customer { get; set; }
    public DbSet<Employee>? Employee { get; set; }
    public DbSet<Deliverer>? Deliverer { get; set; }
    public DbSet<Chef>? Chef { get; set; }
    public DbSet<Supplier>? Supplier { get; set; }
    public DbSet<IngredientCategory>? IngredientCategory { get; set; }
    public DbSet<Ingredient>? Ingredient { get; set; }
    public DbSet<RecipeCategory>? FoodCategory { get; set; }
    public DbSet<Recipe>? Recipe { get; set; }
    public DbSet<FoodItem>? FoodItem { get; set; }
    public DbSet<Order>? Order { get; set; }
    public DbSet<Payment>? Payment { get; set; }
    public DbSet<Delivery>? Delivery { get; set; }
    public DbSet<Supply>? Supply { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Account>()
            .HasIndex(a => a.Email)
            .IsUnique();
        modelBuilder.Entity<FoodItem>()
            .Property(r => r.FoodItemIngredients)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<FoodItemIngredient>>(v)!);
        modelBuilder.Entity<Delivery>()
            .HasOne(e => e.Order)
            .WithOne()
            .OnDelete(DeleteBehavior.ClientSetNull);
        modelBuilder.Entity<FoodItem>()
            .HasOne(e => e.Recipe)
            .WithOne()
            .OnDelete(DeleteBehavior.ClientSetNull);
        modelBuilder.Entity<Account>()
            .HasIndex(e => e.Email)
            .IsUnique();
        modelBuilder.Entity<Recipe>()
            .Property(e => e.RecipeIngredients)
            .HasConversion(
                e => JsonConvert.SerializeObject(e),
                e => JsonConvert.DeserializeObject<List<RecipeIngredient>>(e)!);
        modelBuilder.Entity<Recipe>()
            .Property(e => e.Images)
            .HasConversion(
                e => JsonConvert.SerializeObject(e, new ByteArrayConverter()),
                e => JsonConvert.DeserializeObject<List<Image>>(e, new ByteArrayConverter())!);
        modelBuilder.Entity<Delivery>()
            .Property(e => e.Address)
            .HasConversion(
                e => JsonConvert.SerializeObject(e),
                e => JsonConvert.DeserializeObject<Address>(e));
        modelBuilder.Entity<FoodItem>()
            .HasOne(fi => fi.Recipe)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
