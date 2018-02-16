using ContactAppApi.Edmx;
using ContactAppApi.Models;
using System;
using System.Data.Entity;

namespace ContactAppApi.BaseClasses
{
    public interface IGenericContext : IDisposable
    {
        DbSet<Name> Names { get; }
        DbSet<Address> Addresses { get; }
        int SaveChanges();
        void SetModifiedName(Name item);
        void SetModifiedAddress(Address item);
    }
}