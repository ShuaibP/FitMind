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
    public class APIUserSurveysController : ApiController
    {
        private fitMindDbEntities db = new fitMindDbEntities();

        // GET: api/APIUserSurveys
        public IQueryable<UserSurvey> GetUserSurveys()
        {
            return db.UserSurveys;
        }

        // GET: api/APIUserSurveys/5
        [ResponseType(typeof(UserSurvey))]
        public async Task<IHttpActionResult> GetUserSurvey(int id)
        {
            UserSurvey userSurvey = await db.UserSurveys.FindAsync(id);
            if (userSurvey == null)
            {
                return NotFound();
            }

            return Ok(userSurvey);
        }

        // PUT: api/APIUserSurveys/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserSurvey(int id, UserSurvey userSurvey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userSurvey.userSurveyId)
            {
                return BadRequest();
            }

            db.Entry(userSurvey).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSurveyExists(id))
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

        // POST: api/APIUserSurveys
        [ResponseType(typeof(UserSurvey))]
        public async Task<IHttpActionResult> PostUserSurvey(UserSurvey userSurvey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserSurveys.Add(userSurvey);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserSurveyExists(userSurvey.userSurveyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userSurvey.userSurveyId }, userSurvey);
        }

        // DELETE: api/APIUserSurveys/5
        [ResponseType(typeof(UserSurvey))]
        public async Task<IHttpActionResult> DeleteUserSurvey(int id)
        {
            UserSurvey userSurvey = await db.UserSurveys.FindAsync(id);
            if (userSurvey == null)
            {
                return NotFound();
            }

            db.UserSurveys.Remove(userSurvey);
            await db.SaveChangesAsync();

            return Ok(userSurvey);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserSurveyExists(int id)
        {
            return db.UserSurveys.Count(e => e.userSurveyId == id) > 0;
        }
    }
}