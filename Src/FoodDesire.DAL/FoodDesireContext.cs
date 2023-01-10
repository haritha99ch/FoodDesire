namespace FoodDesire.DAL;
public class FoodDesireContext: DbContext {
    public DbSet<User> User { get; set; }
    public DbSet<Account> Account { get; set; }
    public DbSet<Address> Address { get; set; }
    public DbSet<Admin> Admin { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Deliverer> Deliverer { get; set; }
    public DbSet<Chef> Chef { get; set; }
    public DbSet<Supplier> Supplier { get; set; }
    public DbSet<IngredientCategory> IngredientCategory { get; set; }
    public DbSet<Ingredient> Ingredient { get; set; }
    public DbSet<RecipeCategory> FoodCategory { get; set; }
    public DbSet<Recipe> Recipe { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredient { get; set; }
    public DbSet<FoodItem> FoodItem { get; set; }
    public DbSet<Image> Image { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Payment> Payment { get; set; }
    public DbSet<Delivery> Delivery { get; set; }
    public DbSet<Supply> Supply { get; set; }
    public FoodDesireContext(DbContextOptions<FoodDesireContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<FoodItem>()
            .OwnsMany(e => e.FoodItemIngredients, navigationBuilder => {
                navigationBuilder.ToJson();
            });
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
    }
}
