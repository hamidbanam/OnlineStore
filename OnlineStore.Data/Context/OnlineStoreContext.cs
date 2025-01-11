using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Model.CountactUs;
using OnlineStore.Domain.Model.Feature;
using OnlineStore.Domain.Model.Interests;
using OnlineStore.Domain.Model.Order;
using OnlineStore.Domain.Model.Product.Category;
using OnlineStore.Domain.Model.Product.Color;
using OnlineStore.Domain.Model.Product.Comment;
using OnlineStore.Domain.Model.Product.CommentReaction;
using OnlineStore.Domain.Model.Product.Feature;
using OnlineStore.Domain.Model.Product.Gallary;
using OnlineStore.Domain.Model.Product.Product;
using OnlineStore.Domain.Model.Slider.HomePageSlider;
using OnlineStore.Domain.Model.Ticket;
using OnlineStore.Domain.Model.User.Address;
using OnlineStore.Domain.Model.User.Permission;
using OnlineStore.Domain.Model.User.Role;
using OnlineStore.Domain.Model.User.User;
using OnlineStore.Domain.Model.Wallet;

namespace OnlineStore.Data.Context
{
    public class OnlineStoreContext : DbContext
    {
        #region Constructor
        public OnlineStoreContext(DbContextOptions<OnlineStoreContext> options) : base(options)
        {

        }
        #endregion

        #region Db Sets

        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        #endregion

        #region Product
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductColor> productColors { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductCommentReaction> productCommentReactions { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductGallery> ProductGalleries { get; set; }
        public DbSet<Interests> Interests { get; set; }
        #endregion

        #region Ticket
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }
        #endregion

        #region ContactUs
        public DbSet<ContactUs> ContactUs { get; set; }
        #endregion

        #region Feature
        public DbSet<Feature> Features { get; set; }
        #endregion

        #region Order
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        #endregion

        #region Wallet
        public DbSet<Wallet> Wallets { get; set; }
        #endregion

        #region Slider
        public DbSet<HomePageSlider> Sliders { get; set; }
        #endregion

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
