using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ContactAppApi.Models
{
    public class NamesModel
    {

        public int SubjectId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mi { get; set; }

        public string Suffix { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public int Zip { get; set; }
    }
}