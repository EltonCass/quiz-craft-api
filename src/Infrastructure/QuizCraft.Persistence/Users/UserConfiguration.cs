// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizCraft.Models.Entities;

namespace QuizCraft.Persistence.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Users_Id");

        builder.HasIndex(e => e.Email, "UQ_Users_Email")
            .IsUnique();

        builder.Property(e => e.Email)
            .HasMaxLength(200);
        builder.Property(e => e.FullName)
            .HasMaxLength(500);
    }
}
