using ContactAppApi.Edmx;
using System.Linq;

namespace ContactAppApi.Tests.Db
{
    public class TestNamesDbSet : FakeDbMock<Name>
    {
        public override Name Find(params object[] keyValues)
        {
            return this.SingleOrDefault(user => user.SubjectId == (int)keyValues.Single());
        }
    }

    public class TestAddressDbSet : FakeDbMock<Address>
    {
        public override Address Find(params object[] keyValues)
        {
            return this.SingleOrDefault(user => user.AddressId == (int)keyValues.Single());
        }
    }
}
