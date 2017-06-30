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
    public class APITrophiesController : ApiController
    {
        private fitMindDbEntities1 db = new fitMindDbEntities1();

        // GET: api/APITrophies
        public IQueryable<Trophy> GetTrophies()
        {
            return db.Trophies;
        }

        // GET: api/APITrophies/5
        [ResponseType(typeof(Trophy))]
        public async Task<IHttpActionResult> GetTrophy(int id)
        {
            Trophy trophy = await db.Trophies.FindAsync(id);
            if (trophy == null)
            {
                return NotFound();
            }

            return Ok(trophy);
        }

        // PUT: api/APITrophies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTrophy(int id, Trophy trophy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trophy.trophyId)
            {
                return BadRequest();
            }

            db.Entry(trophy).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrophyExists(id))
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

        // POST: api/APITrophies
        [ResponseType(typeof(Trophy))]
        public async Task<IHttpActionResult> PostTrophy(Trophy trophy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trophies.Add(trophy);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TrophyExists(trophy.trophyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = trophy.trophyId }, trophy);
        }

        // DELETE: api/APITrophies/5
        [ResponseType(typeof(Trophy))]
        public async Task<IHttpActionResult> DeleteTrophy(int id)
        {
            Trophy trophy = await db.Trophies.FindAsync(id);
            if (trophy == null)
            {
                return NotFound();
            }

            db.Trophies.Remove(trophy);
            await db.SaveChangesAsync();

            return Ok(trophy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrophyExists(int id)
        {
            return db.Trophies.Count(e => e.trophyId == id) > 0;
        }
    }
}