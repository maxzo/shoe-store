using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ShoeStore.Models;

namespace ShoeStore.Controllers
{
    public class storesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: services/stores
        public ResponseDTO GetStores()
        {
            var stores = from s in db.Stores
                         select new StoreDTO
                         {
                             id = s.Id,
                             name = s.Name,
                             address = s.Address
                         };
            var amountOfStores = stores.Count();

            if (amountOfStores == 1)
            {
                return new SingleStoreResponseDTO
                {
                    store = stores.First(),
                    total_elements = amountOfStores
                };
            }

            return new PluralStoreResponseDTO
            {
                stores = stores,
                total_elements = amountOfStores
            };
        }

        // GET: api/StoreApi/5
        [ResponseType(typeof(Store))]
        public async Task<IHttpActionResult> GetStore(int id)
        {
            Store store = await db.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        // PUT: api/StoreApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStore(int id, Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != store.Id)
            {
                return BadRequest();
            }

            db.Entry(store).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
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

        // POST: api/StoreApi
        [ResponseType(typeof(Store))]
        public async Task<IHttpActionResult> PostStore(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stores.Add(store);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = store.Id }, store);
        }

        // DELETE: api/StoreApi/5
        [ResponseType(typeof(Store))]
        public async Task<IHttpActionResult> DeleteStore(int id)
        {
            Store store = await db.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            db.Stores.Remove(store);
            await db.SaveChangesAsync();

            return Ok(store);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StoreExists(int id)
        {
            return db.Stores.Count(e => e.Id == id) > 0;
        }
    }
}