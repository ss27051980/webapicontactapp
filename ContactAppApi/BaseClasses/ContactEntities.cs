using ContactAppApi.Edmx;

namespace ContactAppApi.BaseClasses
{
    public class BaseDbContext : ContactEntities, IGenericContext
    {
        public void SetModifiedAddress(Address item)
        {
            Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public void SetModifiedName(Name item)
        {
            Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}