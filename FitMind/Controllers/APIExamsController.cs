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
    public class APIExamsController : ApiController
    {
        private fitMindDbEntities db = new fitMindDbEntities();

        // GET: api/APIExams
        public IQueryable<Exam> GetExams()
        {
            return db.Exams;
        }

        // GET: api/APIExams/5
        [ResponseType(typeof(Exam))]
        public async Task<IHttpActionResult> GetExam(int id)
        {
            Exam exam = await db.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            return Ok(exam);
        }

        // PUT: api/APIExams/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutExam(int id, Exam exam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exam.examId)
            {
                return BadRequest();
            }

            db.Entry(exam).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(id))
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

        // POST: api/APIExams
        [ResponseType(typeof(Exam))]
        public async Task<IHttpActionResult> PostExam(Exam exam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Exams.Add(exam);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExamExists(exam.examId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = exam.examId }, exam);
        }

        // DELETE: api/APIExams/5
        [ResponseType(typeof(Exam))]
        public async Task<IHttpActionResult> DeleteExam(int id)
        {
            Exam exam = await db.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            db.Exams.Remove(exam);
            await db.SaveChangesAsync();

            return Ok(exam);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExamExists(int id)
        {
            return db.Exams.Count(e => e.examId == id) > 0;
        }
    }
}