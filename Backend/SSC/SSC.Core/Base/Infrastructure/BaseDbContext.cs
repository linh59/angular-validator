using Microsoft.EntityFrameworkCore;
using SSC.Core.Arguments.Attributes;
using SSC.Core.Base.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSC.Core.Base.Infrastructure
{
    public abstract class BaseDbContext : DbContext
    {
        public BaseDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected BaseDbContext()
        {
        }

        public override int SaveChanges()
        {
            Stamp();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            Stamp();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            Stamp();
            return base.SaveChangesAsync(cancellationToken);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            Stamp();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void Stamp()
        {
            ChangeTracker.DetectChanges();
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is IDatetimeStampable stampable)
                {
                    if (entry.State == EntityState.Added)
                    {
                        stampable.CreatedAt = DateTime.Now;
                        stampable.LastModifiedAt = DateTime.Now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        var ignoreUpdate = entry.Entity.GetType().GetProperties().Where(x =>x.GetCustomAttributes(true).Any(y => y is IgnoreUpdateAttribute)).Select(x => x.Name);
                        foreach (var propertyName in ignoreUpdate)
                        {
                            entry.Property(propertyName).IsModified = false;
                        }
                        stampable.LastModifiedAt = DateTime.Now;
                    }
                }
                if (entry.Entity is ISoftDelete softDelete && entry.State == EntityState.Deleted)
                {
                    softDelete.IsDeleted = true;
                    softDelete.DeletedAt = DateTime.Now;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
