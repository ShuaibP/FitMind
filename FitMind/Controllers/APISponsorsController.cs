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
    public class APISponsorsController : ApiController
    {
        private fitMindDbEntities1 db = new fitMindDbEntities1();

        // GET: api/APISponsors
        public IQueryable<Sponsor> GetSponsors()
        {
            return db.Sponsors;
        }

        // GET: api/APISponsors/5
        [ResponseType(typeof(Sponsor))]
        public async Task<IHttpActionResult> GetSponsor(int id)
        {
            Sponsor sponsor = await db.Sponsors.FindAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }

            return Ok(sponsor);
        }

        // PUT: api/APISponsors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSponsor(int id, Sponsor sponsor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sponsor.sponsorId)
            {
                return BadRequest();
            }

            db.Entry(sponsor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SponsorExists(id))
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

        // POST: api/APISponsors
        [ResponseType(typeof(Sponsor))]
        public async Task<IHttpActionResult> PostSponsor(Sponsor sponsor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sponsors.Add(sponsor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SponsorExists(sponsor.sponsorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sponsor.sponsorId }, sponsor);
        }

        // DELETE: api/APISponsors/5
        [ResponseType(typeof(Sponsor))]
        public async Task<IHttpActionResult> DeleteSponsor(int id)
        {
            Sponsor sponsor = await db.Sponsors.FindAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }

            db.Sponsors.Remove(sponsor);
            await db.SaveChangesAsync();

            return Ok(sponsor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SponsorExists(int id)
        {
            return db.Sponsors.Count(e => e.sponsorId == id) > 0;
        }
    }
}