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
    public class ApiUserEventsController : ApiController
    {
        private fitMindDbEntities1 db = new fitMindDbEntities1();

        // GET: api/ApiUserEvents
        public IQueryable<UserEvent> GetUserEvents()
        {
            return db.UserEvents;
        }

        // GET: api/ApiUserEvents/5
        [ResponseType(typeof(UserEvent))]
        public async Task<IHttpActionResult> GetUserEvent(int id)
        {
            UserEvent userEvent = await db.UserEvents.FindAsync(id);
            if (userEvent == null)
            {
                return NotFound();
            }

            return Ok(userEvent);
        }

        // PUT: api/ApiUserEvents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserEvent(int id, UserEvent userEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userEvent.userEventId)
            {
                return BadRequest();
            }

            db.Entry(userEvent).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEventExists(id))
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

        // POST: api/ApiUserEvents
        [ResponseType(typeof(UserEvent))]
        public async Task<IHttpActionResult> PostUserEvent(UserEvent userEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserEvents.Add(userEvent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserEventExists(userEvent.userEventId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userEvent.userEventId }, userEvent);
        }

        // DELETE: api/ApiUserEvents/5
        [ResponseType(typeof(UserEvent))]
        public async Task<IHttpActionResult> DeleteUserEvent(int id)
        {
            UserEvent userEvent = await db.UserEvents.FindAsync(id);
            if (userEvent == null)
            {
                return NotFound();
            }

            db.UserEvents.Remove(userEvent);
            await db.SaveChangesAsync();

            return Ok(userEvent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserEventExists(int id)
        {
            return db.UserEvents.Count(e => e.userEventId == id) > 0;
        }
    }
}