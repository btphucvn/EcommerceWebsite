using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EcommerceWebsite.Models
{
    public partial class dbecommerceContext : DbContext
    {
        public dbecommerceContext()
        {
        }

        public dbecommerceContext(DbContextOptions<dbecommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Advertise> Advertises { get; set; } = null!;
        public virtual DbSet<Arrival> Arrivals { get; set; } = null!;
        public virtual DbSet<Attribute> Attributes { get; set; } = null!;
        public virtual DbSet<Attributesprice> Attributesprices { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Orderdetail> Orderdetails { get; set; } = null!;
        public virtual DbSet<Page> Pages { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductSizeColor> ProductSizeColors { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Shipper> Shippers { get; set; } = null!;
        public virtual DbSet<Size> Sizes { get; set; } = null!;
        public virtual DbSet<Slide> Slides { get; set; } = null!;
        public virtual DbSet<Transactstatus> Transactstatuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=dbecommerce;port=3306;user id=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.27-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_vietnamese_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts");

                entity.HasIndex(e => e.RoleId, "fk_accounts_roles");

                entity.Property(e => e.AccountId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AccountID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(12);

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("RoleID");

                entity.Property(e => e.Salt).HasMaxLength(10);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("fk_accounts_roles");
            });

            modelBuilder.Entity<Advertise>(entity =>
            {
                entity.ToTable("advertises");

                entity.Property(e => e.AdvertiseId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AdvertiseID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ImageBg)
                    .HasMaxLength(250)
                    .HasColumnName("ImageBG");

                entity.Property(e => e.ImageProduct).HasMaxLength(250);

                entity.Property(e => e.SubTitle).HasMaxLength(150);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.UrlLink).HasMaxLength(250);
            });

            modelBuilder.Entity<Arrival>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PRIMARY");

                entity.ToTable("arrivals");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("ProductID");

                entity.Property(e => e.Sort).HasColumnType("int(11)");

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.Arrival)
                    .HasForeignKey<Arrival>(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Arrivals_Products");
            });

            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.ToTable("attributes");

                entity.Property(e => e.AttributeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AttributeID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Attributesprice>(entity =>
            {
                entity.ToTable("attributesprices");

                entity.HasIndex(e => e.AttributeId, "fk_AttributesPrices_Attributes");

                entity.HasIndex(e => e.ProductId, "fk_AttributesPrices_Products");

                entity.Property(e => e.AttributesPriceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AttributesPriceID");

                entity.Property(e => e.AttributeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AttributeID");

                entity.Property(e => e.Price).HasColumnType("int(11)");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductID");

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.Attributesprices)
                    .HasForeignKey(d => d.AttributeId)
                    .HasConstraintName("fk_AttributesPrices_Attributes");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Attributesprices)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fk_AttributesPrices_Products");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("PRIMARY");

                entity.ToTable("categories");

                entity.HasIndex(e => e.ParentId, "fk_Categories_Categories");

                entity.Property(e => e.CatId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CatID");

                entity.Property(e => e.Alias).HasMaxLength(255);

                entity.Property(e => e.CatName).HasMaxLength(50);

                entity.Property(e => e.Cover).HasMaxLength(255);

                entity.Property(e => e.Description).HasColumnType("mediumtext");

                entity.Property(e => e.Levels).HasColumnType("int(11)");

                entity.Property(e => e.MetaDesc).HasMaxLength(255);

                entity.Property(e => e.MetaKey).HasMaxLength(255);

                entity.Property(e => e.Ordering).HasColumnType("int(11)");

                entity.Property(e => e.ParentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ParentID");

                entity.Property(e => e.SchemaMarkup).HasColumnType("mediumtext");

                entity.Property(e => e.Thumb).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("fk_Categories_Categories");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("colors");

                entity.Property(e => e.ColorId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ColorID");

                entity.Property(e => e.ColorCode)
                    .HasMaxLength(7)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.HasIndex(e => e.LocationId, "fk_Customers_Location");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CustomerID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.District).HasColumnType("int(11)");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LocationId)
                    .HasColumnType("int(11)")
                    .HasColumnName("LocationID");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(12);

                entity.Property(e => e.Salt).HasMaxLength(8);

                entity.Property(e => e.Ward).HasColumnType("int(11)");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("fk_Customers_Location");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("locations");

                entity.Property(e => e.LocationId)
                    .HasColumnType("int(11)")
                    .HasColumnName("LocationID");

                entity.Property(e => e.Levels).HasColumnType("int(11)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NameWithType).HasMaxLength(100);

                entity.Property(e => e.Parent).HasColumnType("int(11)");

                entity.Property(e => e.Slug).HasMaxLength(100);

                entity.Property(e => e.Type).HasMaxLength(100);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.CustomerId, "fk_Orders_Customers");

                entity.HasIndex(e => e.TransactStatusId, "fk_Orders_TransactStatus");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("OrderID");

                entity.Property(e => e.Address).HasColumnType("mediumtext");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CustomerID");

                entity.Property(e => e.District).HasColumnType("int(11)");

                entity.Property(e => e.LocationId)
                    .HasColumnType("int(11)")
                    .HasColumnName("LocationID");

                entity.Property(e => e.Note).HasColumnType("mediumtext");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("PaymentID");

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.TotalMoney).HasColumnType("int(11)");

                entity.Property(e => e.TransactStatusId)
                    .HasColumnType("int(11)")
                    .HasColumnName("TransactStatusID");

                entity.Property(e => e.Ward).HasColumnType("int(11)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("fk_Orders_Customers");

                entity.HasOne(d => d.TransactStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TransactStatusId)
                    .HasConstraintName("fk_Orders_TransactStatus");
            });

            modelBuilder.Entity<Orderdetail>(entity =>
            {
                entity.ToTable("orderdetails");

                entity.HasIndex(e => e.OrderId, "fk_OrderDetails_Orders");

                entity.HasIndex(e => e.ProductId, "fk_OrderDetails_Products");

                entity.Property(e => e.OrderDetailId)
                    .HasColumnType("int(11)")
                    .HasColumnName("OrderDetailID");

                entity.Property(e => e.Amount).HasColumnType("int(11)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("int(11)");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("OrderID");

                entity.Property(e => e.OrderNumber).HasColumnType("int(11)");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductID");

                entity.Property(e => e.TotalMoney).HasColumnType("int(11)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("fk_OrderDetails_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fk_OrderDetails_Products");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("pages");

                entity.Property(e => e.PageId)
                    .HasColumnType("int(11)")
                    .HasColumnName("PageID");

                entity.Property(e => e.Alias).HasMaxLength(250);

                entity.Property(e => e.Contents).HasColumnType("mediumtext");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.MetaDesc).HasMaxLength(250);

                entity.Property(e => e.MetaKey).HasMaxLength(250);

                entity.Property(e => e.Ordering).HasColumnType("int(11)");

                entity.Property(e => e.PageName).HasMaxLength(250);

                entity.Property(e => e.Thumb).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("posts");

                entity.Property(e => e.PostId)
                    .HasColumnType("int(11)")
                    .HasColumnName("PostID");

                entity.Property(e => e.AccountId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AccountID");

                entity.Property(e => e.Alias).HasMaxLength(255);

                entity.Property(e => e.Author).HasMaxLength(255);

                entity.Property(e => e.CatId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CatID");

                entity.Property(e => e.Contents).HasColumnType("mediumtext");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.MetaDesc).HasMaxLength(255);

                entity.Property(e => e.MetaKey).HasMaxLength(255);

                entity.Property(e => e.Scontents)
                    .HasMaxLength(255)
                    .HasColumnName("SContents");

                entity.Property(e => e.Tags).HasMaxLength(255);

                entity.Property(e => e.Thumb).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Views).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.CatId, "fk_Products_Categories");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductID");

                entity.Property(e => e.Alias).HasMaxLength(255);

                entity.Property(e => e.CatId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CatID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("mediumtext");

                entity.Property(e => e.MetaDesc).HasMaxLength(255);

                entity.Property(e => e.MetaKey).HasMaxLength(255);

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.ShortDesc).HasMaxLength(50);

                entity.Property(e => e.Tags).HasColumnType("mediumtext");

                entity.Property(e => e.Thumb).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.UnitsInStock).HasColumnType("int(11)");

                entity.Property(e => e.Video).HasMaxLength(255);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("fk_Products_Categories");
            });

            modelBuilder.Entity<ProductSizeColor>(entity =>
            {
                entity.HasKey(e => new { e.SizeId, e.ColorId, e.ProductId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("product_size_color");

                entity.HasIndex(e => e.ColorId, "fk_Product_Size_Color_Colors");

                entity.HasIndex(e => e.ProductId, "fk_Product_Size_Color_Products");

                entity.Property(e => e.SizeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("SizeID");

                entity.Property(e => e.ColorId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ColorID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductID");

                entity.Property(e => e.Inventory).HasColumnType("int(11)");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.ProductSizeColors)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Size_Color_Colors");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSizeColors)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Size_Color_Products");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.ProductSizeColors)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Size_Color_Sizes");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("RoleID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("shippers");

                entity.Property(e => e.ShipperId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ShipperID");

                entity.Property(e => e.Company).HasMaxLength(150);

                entity.Property(e => e.Phone).HasMaxLength(12);

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.ShipperName).HasMaxLength(150);
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("sizes");

                entity.Property(e => e.SizeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("SizeID");

                entity.Property(e => e.SizeName)
                    .HasMaxLength(6)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Slide>(entity =>
            {
                entity.ToTable("slides");

                entity.Property(e => e.SlideId)
                    .HasColumnType("int(11)")
                    .HasColumnName("SlideID");

                entity.Property(e => e.Link).HasMaxLength(255);

                entity.Property(e => e.Sort).HasColumnType("int(11)");

                entity.Property(e => e.Thumb).HasMaxLength(255);
            });

            modelBuilder.Entity<Transactstatus>(entity =>
            {
                entity.ToTable("transactstatus");

                entity.Property(e => e.TransactStatusId)
                    .HasColumnType("int(11)")
                    .HasColumnName("TransactStatusID");

                entity.Property(e => e.Description).HasColumnType("mediumtext");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
