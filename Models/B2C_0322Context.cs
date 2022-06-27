using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace B2CAPI.Models
{
    public partial class B2C_0322Context : DbContext
    {
        public B2C_0322Context()
        {
        }

        public B2C_0322Context(DbContextOptions<B2C_0322Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Img> Imgs { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Param> Params { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<customer_service> customer_services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.CID);

                entity.ToTable("Cart");

                entity.Property(e => e.btime).HasColumnType("datetime");

                entity.Property(e => e.ctime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.DID);

                entity.ToTable("Discount");

                entity.Property(e => e.DID).HasComment("折扣ID");

                entity.Property(e => e.BeginTime)
                    .HasColumnType("datetime")
                    .HasComment("開始時間");

                entity.Property(e => e.Btime)
                    .HasColumnType("datetime")
                    .HasComment("建立時間");

                entity.Property(e => e.Buser).HasComment("建立者");

                entity.Property(e => e.Ctime)
                    .HasColumnType("datetime")
                    .HasComment("最後更改者");

                entity.Property(e => e.Cuser).HasComment("最後更改者");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasComment("結束時間");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("名稱");

                entity.Property(e => e.percentage)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("折數");
            });

            modelBuilder.Entity<Img>(entity =>
            {
                entity.HasKey(e => e.IID);

                entity.ToTable("Img");

                entity.Property(e => e.Btime)
                    .HasColumnType("datetime")
                    .HasComment("建立時間");

                entity.Property(e => e.Buser).HasComment("建立者");

                entity.Property(e => e.Ctime)
                    .HasColumnType("datetime")
                    .HasComment("最後更改時間");

                entity.Property(e => e.Cuser).HasComment("最後更改者");

                entity.Property(e => e.IID)
                    .ValueGeneratedOnAdd()
                    .HasComment("照片ID");

                entity.Property(e => e.Img_name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("照片名稱");

                entity.Property(e => e.Img_path)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("照片地址");

                entity.Property(e => e.PID).HasComment("商品ID");

                entity.Property(e => e.spare)
                    .HasMaxLength(50)
                    .HasComment("備用");

                entity.Property(e => e.status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("狀態");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.MID);

                entity.ToTable("Member");

                entity.Property(e => e.MID).HasComment("會員ID");

                entity.Property(e => e.auth)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("權限");

                entity.Property(e => e.birth)
                    .HasColumnType("date")
                    .HasComment("生日");

                entity.Property(e => e.btime)
                    .HasColumnType("datetime")
                    .HasComment("建立時間");

                entity.Property(e => e.buser).HasComment("建立者");

                entity.Property(e => e.changepswdate)
                    .HasColumnType("datetime")
                    .HasComment("最後更改密碼日期");

                entity.Property(e => e.ctime)
                    .HasColumnType("datetime")
                    .HasComment("最後更改時間");

                entity.Property(e => e.cuser).HasComment("最後更改者");

                entity.Property(e => e.email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("電子信箱");

                entity.Property(e => e.enddate)
                    .HasColumnType("datetime")
                    .HasComment("帳號終止日期");

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("密碼");

                entity.Property(e => e.phone)
                    .HasMaxLength(50)
                    .HasComment("電話號碼");

                entity.Property(e => e.sex)
                    .HasMaxLength(50)
                    .HasComment("性別");

                entity.Property(e => e.startdate)
                    .HasColumnType("datetime")
                    .HasComment("帳號起始日期");

                entity.Property(e => e.status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("是否啟用(Y:啟用,N停用)");

                entity.Property(e => e.username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("姓名");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OID);

                entity.ToTable("Order");

                entity.Property(e => e.OID).HasComment("訂單ID");

                entity.Property(e => e.MID).HasComment("會員ID");

                entity.Property(e => e.additional_fee).HasComment("附加費");

                entity.Property(e => e.btime)
                    .HasColumnType("datetime")
                    .HasComment("建立時間");

                entity.Property(e => e.buser).HasComment("建立使用者");

                entity.Property(e => e.ctime)
                    .HasColumnType("datetime")
                    .HasComment("最後更改時間");

                entity.Property(e => e.cuser).HasComment("最後更改人");

                entity.Property(e => e.customer_name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("顧客名稱");

                entity.Property(e => e.customer_phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("顧客連絡電話");

                entity.Property(e => e.deliver_address)
                    .HasMaxLength(50)
                    .HasComment("送貨地點");

                entity.Property(e => e.deliver_methods)
                    .HasMaxLength(50)
                    .HasComment("送貨方式");

                entity.Property(e => e.deliver_name)
                    .HasMaxLength(50)
                    .HasComment("收件人姓名");

                entity.Property(e => e.deliver_phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.deliver_status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("送貨狀態");

                entity.Property(e => e.discount).HasComment("優惠");

                entity.Property(e => e.freight).HasComment("運費");

                entity.Property(e => e.order_date)
                    .HasColumnType("date")
                    .HasComment("訂單日期");

                entity.Property(e => e.order_remark)
                    .HasMaxLength(50)
                    .HasComment("訂單備註");

                entity.Property(e => e.order_status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("訂單狀態");

                entity.Property(e => e.pay_method)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("付款方式");

                entity.Property(e => e.pay_status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("付款狀態");

                entity.Property(e => e.time)
                    .HasColumnType("date")
                    .HasComment("交易時間");

                entity.Property(e => e.total).HasComment("總金額");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.ODID);

                entity.Property(e => e.ODID).HasComment("訂單明細ID");

                entity.Property(e => e.OID).HasComment("訂單ID");

                entity.Property(e => e.PID).HasComment("商品ID");

                entity.Property(e => e.Prod_DID).HasComment("折扣ID");

                entity.Property(e => e.Prod_name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("商品名稱");

                entity.Property(e => e.Prod_price).HasComment("商品價錢");

                entity.Property(e => e.Prod_qty).HasComment("商品數量");

                entity.Property(e => e.Prod_sale).HasComment("商品優惠");

                entity.Property(e => e.btime)
                    .HasColumnType("datetime")
                    .HasComment("建立時間");

                entity.Property(e => e.buser).HasComment("建立者");

                entity.Property(e => e.ctime)
                    .HasColumnType("datetime")
                    .HasComment("最後更改時間");

                entity.Property(e => e.cuser).HasComment("最後更改者");
            });

            modelBuilder.Entity<Param>(entity =>
            {
                entity.HasKey(e => e.PID);

                entity.ToTable("Param");

                entity.Property(e => e.PID).HasComment("參數ID");

                entity.Property(e => e.Btime)
                    .HasColumnType("datetime")
                    .HasComment("建立時間");

                entity.Property(e => e.Buser).HasComment("建立者");

                entity.Property(e => e.Ctime)
                    .HasColumnType("datetime")
                    .HasComment("最後更改時間");

                entity.Property(e => e.Cuser).HasComment("最後更改者");

                entity.Property(e => e.category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("種類");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("名稱");

                entity.Property(e => e.priority).HasComment("優先順序");

                entity.Property(e => e.spare)
                    .HasMaxLength(50)
                    .HasComment("備用");

                entity.Property(e => e.status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("是否啟用(Y:啟用,N停用)");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.PID);

                entity.ToTable("Product");

                entity.Property(e => e.PID).HasComment("商品ID");

                entity.Property(e => e.DID).HasComment("折扣ID");

                entity.Property(e => e.btime)
                    .HasColumnType("datetime")
                    .HasComment("建立時間");

                entity.Property(e => e.buser).HasComment("建立者");

                entity.Property(e => e.category)
                    .HasMaxLength(50)
                    .HasComment("種類");

                entity.Property(e => e.ctime)
                    .HasColumnType("datetime")
                    .HasComment("最後更改時間");

                entity.Property(e => e.cuser).HasComment("最後更改者");

                entity.Property(e => e.description)
                    .HasMaxLength(50)
                    .HasComment("描述");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("名稱");

                entity.Property(e => e.price).HasComment("價錢");

                entity.Property(e => e.qty).HasComment("數量");

                entity.Property(e => e.sale).HasComment("優惠");

                entity.Property(e => e.spare)
                    .HasMaxLength(50)
                    .HasComment("備用");
            });

            modelBuilder.Entity<customer_service>(entity =>
            {
                entity.HasKey(e => e.CSID);

                entity.ToTable("customer_service");

                entity.Property(e => e.CSID).HasComment("ID");

                entity.Property(e => e.MID).HasComment("顧客ID");

                entity.Property(e => e.btime)
                    .HasColumnType("datetime")
                    .HasComment("建立時間");

                entity.Property(e => e.buser).HasComment("建立者");

                entity.Property(e => e.category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("種類");

                entity.Property(e => e.ctime)
                    .HasColumnType("datetime")
                    .HasComment("最後更改時間");

                entity.Property(e => e.cuser).HasComment("最後更改者");

                entity.Property(e => e.description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("描述");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
