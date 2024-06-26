﻿using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>

{
    public DbSet<Event> Events { get; set; } = default!;
    public DbSet<Firm> Firms { get; set; } = default!;
    public DbSet<ParticipantEvent> ParticipantEvents { get; set; } = default!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = default!;
    public DbSet<Person> Persons { get; set; } = default!;
    
    public DbSet<AppRefreshToken> RefreshTokens { get; set; } = default!;

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entity in ChangeTracker.Entries().Where(e => e.State != EntityState.Deleted))
        {
            foreach (var prop in entity
                         .Properties
                         .Where(x => x.Metadata.ClrType == typeof(DateTime)))
            {
                prop.CurrentValue = ((DateTime) prop.CurrentValue).ToUniversalTime();
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}