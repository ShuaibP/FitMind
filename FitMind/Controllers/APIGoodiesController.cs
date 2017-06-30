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
using FitMind.Models;

namespace FitMind.Controllers
{
    public class APIGoodiesController : ApiController
    {
        private fitMindDbEntities db = new fitMindDbEntities();

        // GET: api/APIGoodies
        public IQueryable<Goodie> GetGoodies()
        {
            return db.Goodies;
        }

        // GET: api/APIGoodies/5
        [ResponseType(typeof(Goodie))]
        public async Task<IHttpActionResult> GetGoodie(int id)
        {
            Goodie goodie = await db.Goodies.FindAsync(id);
            if (goodie == null)
            {
                return NotFound();
            }

            return Ok(goodie);
        }

        // PUT: api/APIGoodies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGoodie(int id, Goodie goodie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goodie.goodieId)
            {
                return BadRequest();
            }

            db.Entry(goodie).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodieExists(id))
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

        // POST: api/APIGoodies
        [ResponseType(typeof(Goodie))]
        public async Task<IHttpActionResult> PostGoodie(Goodie goodie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Goodies.Add(goodie);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GoodieExists(goodie.goodieId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = goodie.goodieId }, goodie);
        }

        // DELETE: api/APIGoodies/5
        [ResponseType(typeof(Goodie))]
        public async Task<IHttpActionResult> DeleteGoodie(int id)
        {
            Goodie goodie = await db.Goodies.FindAsync(id);
            if (goodie == null)
            {
                return NotFound();
            }

            db.Goodies.Remove(goodie);
            await db.SaveChangesAsync();

            return Ok(goodie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GoodieExists(int id)
        {
            return db.Goodies.Count(e => e.goodieId == id) > 0;
        }
    }
}