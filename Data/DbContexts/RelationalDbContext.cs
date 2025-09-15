using BusinessEntities;
using Data.DbContexts.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.DbContexts;

public class RelationalDbContext : DbContext, iDbContext
{
    public DbSet<Order> Order { get; set; }
    public DbSet<Product> Product { get; set; }

    public const string DatabaseName = "LendingPad";

    public string ConnectionStringSqlLite = $"Data Source={DatabaseName}";
    public string ConnectionStringSqlServer=@$"Server=(localdb)\mssqllocaldb;Database={DatabaseName};Trusted_Connection=True;MultipleActiveResultSets=true";

    public RelationalDbContext() : base()
    {
       // this.Database.EnsureDeleted();
        this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
             optionsBuilder.UseSqlite(ConnectionStringSqlLite);
            // optionsBuilder.UseSqlServer(ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Order>()
           .Property(e => e.Identifier)
           .HasDefaultValueSql("NEWSEQUENTIALID()"); // Or "NEWID()" for random GUIDs


        //---Ignore-(Below)----//Used for storing and retieving string Array via EF Core for User
        string delimiter = ",";
        var converter = new ValueConverter<IEnumerable<string>, string>(
        v => string.Join(delimiter, v),
        v => v.Split(delimiter.ToCharArray()),
        new ConverterMappingHints(unicode: false));


        var comparer = new ValueComparer<IList<string>>(
          (c1, c2) => c1.SequenceEqual(c2), //An expression for checking equality
          c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), //An expression for generating a hash code

          c => (IList<string>)c.ToList()); //An expression to snapshot a value



      //  modelBuilder
      //.Entity<User>()
      //.Property(e => e.Tags)
      //.HasConversion(converter);

        //---Ignore-(Above)----


        Seed(modelBuilder);
    }

    public void Seed(ModelBuilder modelBuilder)
    {


        Guid FanucI90;
        Guid FanucI30;


        modelBuilder.Entity<Product>().HasData(
                  new Product
                  {
                      Identifier = FanucI90 = Guid.NewGuid(),
                      Name = "Fanuc i90",
                      Description = "Industrial Robotic Arm"
                  }
              );

        modelBuilder.Entity<Product>().HasData(
          new Product
          {
              Identifier = FanucI30 = Guid.NewGuid(),
              Name = "Fanuc i30",
              Description = "Industrial Robotic Arm"
          }
      );

        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                Identifier= Guid.NewGuid(),
                Customer = "Neville",
                ProductId = FanucI90
            }
        );

        modelBuilder.Entity<Order>().HasData(
           new Order
           {
               Identifier = Guid.NewGuid(),
               Customer = "Neville",
               ProductId = FanucI30
           }
       );
    }
}

