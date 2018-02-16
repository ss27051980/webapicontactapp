using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using ContactAppApi.BaseClasses;
using ContactAppApi.Edmx;
using ContactAppApi.Models;

namespace ContactAppApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NamesController : ApiController
    {
        //private ContactEntities Db = new ContactEntities();
        private IGenericContext Db;

        // default initialization
        public NamesController()
        {
            this.Db = new BaseDbContext();
        }

        // for test initialization.
        public NamesController(IGenericContext context)
        {
            Db = context;
        }

        // GET: api/Names
        public IQueryable<NamesModel> GetNames()
        {
            return Db.Names.Select(x => new NamesModel() { FirstName = x.FirstName, LastName = x.LastName, Mi = x.Mi, SubjectId = x.SubjectId, Suffix = x.Suffix }).AsQueryable();
        }

        // GET: api/Names/5
        [ResponseType(typeof(Name))]
        public IHttpActionResult GetName(int id)
        {
            Name name = Db.Names.Find(id);
            if (name == null)
            {
                return NotFound();
            }

            return Ok(name);
        }

        private Name Convert(NamesModel name)
        {
            return new Name() { FirstName = name.FirstName, LastName = name.LastName, Suffix = name.Suffix, Mi = name.Mi, SubjectId = name.SubjectId };
        }

        // PUT: api/Names/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutName(int id, NamesModel name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != name.SubjectId)
            {
                return BadRequest();
            }

            Db.SetModifiedName(Convert(name));

            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private Address GetAddress(NamesModel model)
        {
            var add = new Address() { City = model.City, State = model.State, Zip = model.Zip };
            var regex = new Regex("\\d+");
            add.StreeNumber = regex.Match(model.Address).Value;
            add.StreetType = Regex.Match(model.Address, "(Avenue|Lane|Road|Boulevard|Drive|Street|Ave|Dr|Rd|Blvd|Ln|St)").Value;
            add.Street = model.Address.Split(' ')[1];
            return add;
        }

        // POST: api/Names
        [ResponseType(typeof(Name))]
        public IHttpActionResult PostName(NamesModel name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addMdl = GetAddress(name);
            var nameMdl = Convert(name);
            nameMdl.Addresses.Add(addMdl);
            Db.Names.Add(nameMdl);
            Db.SaveChanges();
            return Json(name);
        }

        // DELETE: api/Names/5
        [ResponseType(typeof(Name))]
        public IHttpActionResult DeleteName(int id)
        {
            Name name = Db.Names.Find(id);
            if (name == null)
            {
                return NotFound();
            }

            Db.Names.Remove(name);
            Db.SaveChanges();

            return Ok(name);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NameExists(int id)
        {
            return Db.Names.Count(e => e.SubjectId == id) > 0;
        }
    }
}