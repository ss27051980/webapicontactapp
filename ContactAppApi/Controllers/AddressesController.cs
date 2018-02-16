using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ContactAppApi.BaseClasses;
using ContactAppApi.Edmx;

namespace ContactAppApi.Controllers
{
    public class AddressesController : ApiController
    {
        private IGenericContext Db { get; set; }

        public AddressesController()
        {
            Db = new BaseDbContext();
        }
        //private ContactEntities 

        public AddressesController(IGenericContext context)
        {
            this.Db = context;
        }

        // GET: api/Addresses
        public IQueryable<Address> GetAddresses()
        {
            return Db.Addresses;
        }

        // GET: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult GetAddress(int id)
        {
            Address address = Db.Addresses.Find(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/Addresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddress(int id, Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.AddressId)
            {
                return BadRequest();
            }

            Db.SetModifiedAddress(address);
            //db.Entry(address).State = EntityState.Modified;

            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        [ResponseType(typeof(Address))]
        public IHttpActionResult PostAddress(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Db.Addresses.Add(address);
            Db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = address.AddressId }, address);
        }

        // DELETE: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult DeleteAddress(int id)
        {
            Address address = Db.Addresses.Find(id);
            if (address == null)
            {
                return NotFound();
            }

            Db.Addresses.Remove(address);
            Db.SaveChanges();

            return Ok(address);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddressExists(int id)
        {
            return Db.Addresses.Count(e => e.AddressId == id) > 0;
        }
    }
}