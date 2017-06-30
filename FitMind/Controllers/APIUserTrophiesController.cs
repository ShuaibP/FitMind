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
    public class APIUserTrophiesController : ApiController
    {
        private fitMindDbEntities db = new fitMindDbEntities();

        // GET: api/APIUserTrophies
        public IQueryable<UserTrophy> GetUserTrophies()
        {
            return db.UserTrophies;
        }

        // GET: api/APIUserTrophies/5
        [ResponseType(typeof(UserTrophy))]
        public async Task<IHttpActionResult> GetUserTrophy(int id)
        {
            UserTrophy userTrophy = await db.UserTrophies.FindAsync(id);
            if (userTrophy == null)
            {
                return NotFound();
            }

            return Ok(userTrophy);
        }

        // PUT: api/APIUserTrophies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserTrophy(int id, UserTrophy userTrophy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userTrophy.userTrophyId)
            {
                return BadRequest();
            }

            db.Entry(userTrophy).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTrophyExists(id))
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

        // POST: api/APIUserTrophies
        [ResponseType(typeof(UserTrophy))]
        public async Task<IHttpActionResult> PostUserTrophy(UserTrophy userTrophy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserTrophies.Add(userTrophy);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserTrophyExists(userTrophy.userTrophyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userTrophy.userTrophyId }, userTrophy);
        }

        // DELETE: api/APIUserTrophies/5
        [ResponseType(typeof(UserTrophy))]
        public async Task<IHttpActionResult> DeleteUserTrophy(int id)
        {
            UserTrophy userTrophy = await db.UserTrophies.FindAsync(id);
            if (userTrophy == null)
            {
                return NotFound();
            }

            db.UserTrophies.Remove(userTrophy);
            await db.SaveChangesAsync();

            return Ok(userTrophy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserTrophyExists(int id)
        {
            return db.UserTrophies.Count(e => e.userTrophyId == id) > 0;
        }
    }
}