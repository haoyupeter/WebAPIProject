using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
   
    public class QuotesController : ApiController
    {

        private WebApiProjectDataEntities db = new WebApiProjectDataEntities();

        // GET: api/Quotes
        //[Authorize]
        public IQueryable<Quote> GetQuote()
        {
            return db.Quote;
        }

        // GET: api/Quotes/5
        [ResponseType(typeof(Quote))]
        public IHttpActionResult GetQuote(int id)
        {
            Quote quote = db.Quote.Find(id);
            if (quote == null)
            {
                return Ok("The Quote ID can not find");
            }

            return Ok(quote);
        }

        // PUT: api/Quotes/5
        //[Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuote(int id, Quote quote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quote.QuoteID)
            {
                return BadRequest();
            }

            db.Entry(quote).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuoteExists(id))
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
        //[Authorize]
        // POST: api/Quotes
        [ResponseType(typeof(Quote))]
        public IHttpActionResult PostQuote(Quote quote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Quote.Add(quote);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = quote.QuoteID }, quote);
        }

        // DELETE: api/Quotes/5
        //[Authorize]
        [ResponseType(typeof(Quote))]
        public IHttpActionResult DeleteQuote(int id)
        {
            Quote quote = db.Quote.Find(id);
            if (quote == null)
            {
                return NotFound();
            }

            db.Quote.Remove(quote);
            db.SaveChanges();

            return Ok(quote);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuoteExists(int id)
        {
            return db.Quote.Count(e => e.QuoteID == id) > 0;
        }
    }
}