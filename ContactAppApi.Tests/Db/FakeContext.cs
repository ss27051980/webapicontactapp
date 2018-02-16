using ContactAppApi.BaseClasses;
using ContactAppApi.Edmx;
using System;
using System.Data.Entity;

namespace ContactAppApi.Tests.Db
{
    public class FakeContext : IGenericContext
    {
        public DbSet<Name> Names { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public FakeContext()
        {
            this.Names = new TestNamesDbSet();
            this.Addresses = new TestAddressDbSet();
            AddNames();
            AddAddress();
        }

        private void AddNames()
        {
            for (int i = 0; i < 50; i++)
            {
                var name = new Name() { SubjectId = i, FirstName = "FirstName"+i, LastName="LastName"+i, Mi="TestMI"+i, Suffix= i % 2 == 0 ? "Mr" : "Ms" };
                this.Names.Add(name);
            }
        }

        private void AddAddress()
        {
            for (int i = 0; i < 25; i++)
            {
                var name = new Address() { AddressId = i,  City = i % 2 == 0 ? "Miami" : "New York", State = i % 2 ==0 ? "Florida" : "Washington DC", Street = "Test Street", StreetType = "Fuck it", Zip = 12345 };
                this.Addresses.Add(name);
            }
        }

        public void Dispose()
        {
        }

        public int SaveChanges()
        {
            return 0;
        }

        public void SetModifiedAddress(Address item)
        {
        }

        public void SetModifiedName(Name item)
        {
        }
    }
}
