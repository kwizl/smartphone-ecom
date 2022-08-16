using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			//Setup table and primary key

			builder.ToTable("Orders");

			builder.HasKey(t => t.ID)
				.HasName("PK_Orders_OrderID");

			//Setup required fields and sizes

			builder.Property(x => x.UserName)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.TotalPrice)
				.IsRequired();

			builder.Property(x => x.FirstName)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.LastName)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.AddressLine)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.Country)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.State)
				.IsRequired()
				.HasMaxLength(250);

			builder.Property(x => x.ZipCode)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.CardName)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.Expiration)
				.IsRequired()
				.HasMaxLength(4);

			builder.Property(x => x.CVV)
				.IsRequired()
				.HasMaxLength(3);

			builder.Property(x => x.PaymentMethod)
				.IsRequired();

			builder.Property(x => x.CreatedBy)
				.IsRequired();

			builder.Property(x => x.LastModifiedBy)
				.IsRequired();
		}
	}
}
