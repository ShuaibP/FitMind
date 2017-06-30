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
    public class APIUserQuizsController : ApiController
    {
        private fitMindDbEntities db = new fitMindDbEntities();

        // GET: api/APIUserQuizs
        public IQueryable<UserQuiz> GetUserQuizs()
        {
            return db.UserQuizs;
        }

        // GET: api/APIUserQuizs/5
        [ResponseType(typeof(UserQuiz))]
        public async Task<IHttpActionResult> GetUserQuiz(int id)
        {
            UserQuiz userQuiz = await db.UserQuizs.FindAsync(id);
            if (userQuiz == null)
            {
                return NotFound();
            }

            return Ok(userQuiz);
        }

        // PUT: api/APIUserQuizs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserQuiz(int id, UserQuiz userQuiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userQuiz.userQuizId)
            {
                return BadRequest();
            }

            db.Entry(userQuiz).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserQuizExists(id))
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

        // POST: api/APIUserQuizs
        [ResponseType(typeof(UserQuiz))]
        public async Task<IHttpActionResult> PostUserQuiz(UserQuiz userQuiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserQuizs.Add(userQuiz);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserQuizExists(userQuiz.userQuizId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userQuiz.userQuizId }, userQuiz);
        }

        // DELETE: api/APIUserQuizs/5
        [ResponseType(typeof(UserQuiz))]
        public async Task<IHttpActionResult> DeleteUserQuiz(int id)
        {
            UserQuiz userQuiz = await db.UserQuizs.FindAsync(id);
            if (userQuiz == null)
            {
                return NotFound();
            }

            db.UserQuizs.Remove(userQuiz);
            await db.SaveChangesAsync();

            return Ok(userQuiz);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserQuizExists(int id)
        {
            return db.UserQuizs.Count(e => e.userQuizId == id) > 0;
        }
    }
}