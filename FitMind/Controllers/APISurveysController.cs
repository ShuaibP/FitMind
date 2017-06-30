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
    public class APISurveysController : ApiController
    {
        private fitMindDbEntities db = new fitMindDbEntities();

        // GET: api/APISurveys
        public IQueryable<Survey> GetSurveys()
        {
            return db.Surveys;
        }

        // GET: api/APISurveys/5
        [ResponseType(typeof(Survey))]
        public async Task<IHttpActionResult> GetSurvey(int id)
        {
            Survey survey = await db.Surveys.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }

            return Ok(survey);
        }

        // PUT: api/APISurveys/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSurvey(int id, Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != survey.surveyId)
            {
                return BadRequest();
            }

            db.Entry(survey).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyExists(id))
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

        // POST: api/APISurveys
        [ResponseType(typeof(Survey))]
        public async Task<IHttpActionResult> PostSurvey(Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Surveys.Add(survey);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SurveyExists(survey.surveyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = survey.surveyId }, survey);
        }

        // DELETE: api/APISurveys/5
        [ResponseType(typeof(Survey))]
        public async Task<IHttpActionResult> DeleteSurvey(int id)
        {
            Survey survey = await db.Surveys.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }

            db.Surveys.Remove(survey);
            await db.SaveChangesAsync();

            return Ok(survey);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SurveyExists(int id)
        {
            return db.Surveys.Count(e => e.surveyId == id) > 0;
        }
    }
}