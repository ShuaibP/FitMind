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
    public class APIUserExamsController : ApiController
    {
        private fitMindDbEntities db = new fitMindDbEntities();

        // GET: api/APIUserExams
        public IQueryable<UserExam> GetUserExams()
        {
            return db.UserExams;
        }

        // GET: api/APIUserExams/5
        [ResponseType(typeof(UserExam))]
        public async Task<IHttpActionResult> GetUserExam(int id)
        {
            UserExam userExam = await db.UserExams.FindAsync(id);
            if (userExam == null)
            {
                return NotFound();
            }

            return Ok(userExam);
        }

        // PUT: api/APIUserExams/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserExam(int id, UserExam userExam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userExam.userExamId)
            {
                return BadRequest();
            }

            db.Entry(userExam).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExamExists(id))
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

        // POST: api/APIUserExams
        [ResponseType(typeof(UserExam))]
        public async Task<IHttpActionResult> PostUserExam(UserExam userExam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserExams.Add(userExam);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExamExists(userExam.userExamId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userExam.userExamId }, userExam);
        }

        // DELETE: api/APIUserExams/5
        [ResponseType(typeof(UserExam))]
        public async Task<IHttpActionResult> DeleteUserExam(int id)
        {
            UserExam userExam = await db.UserExams.FindAsync(id);
            if (userExam == null)
            {
                return NotFound();
            }

            db.UserExams.Remove(userExam);
            await db.SaveChangesAsync();

            return Ok(userExam);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExamExists(int id)
        {
            return db.UserExams.Count(e => e.userExamId == id) > 0;
        }
    }
}