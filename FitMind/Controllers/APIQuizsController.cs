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
    public class APIQuizsController : ApiController
    {
        private fitMindDbEntities1 db = new fitMindDbEntities1();

        // GET: api/APIQuizs
        public IQueryable<Quiz> GetQuizs()
        {
            return db.Quizs;
        }

        // GET: api/APIQuizs/5
        [ResponseType(typeof(Quiz))]
        public async Task<IHttpActionResult> GetQuiz(int id)
        {
            Quiz quiz = await db.Quizs.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz);
        }

        // PUT: api/APIQuizs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutQuiz(int id, Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quiz.quizId)
            {
                return BadRequest();
            }

            db.Entry(quiz).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
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

        // POST: api/APIQuizs
        [ResponseType(typeof(Quiz))]
        public async Task<IHttpActionResult> PostQuiz(Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Quizs.Add(quiz);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuizExists(quiz.quizId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = quiz.quizId }, quiz);
        }

        // DELETE: api/APIQuizs/5
        [ResponseType(typeof(Quiz))]
        public async Task<IHttpActionResult> DeleteQuiz(int id)
        {
            Quiz quiz = await db.Quizs.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            db.Quizs.Remove(quiz);
            await db.SaveChangesAsync();

            return Ok(quiz);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuizExists(int id)
        {
            return db.Quizs.Count(e => e.quizId == id) > 0;
        }
    }
}