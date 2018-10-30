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
using BookService.Models;

namespace BookService.Controllers
{
    public class AuthorsController : ApiController
    {
        private BookServiceContext db = new BookServiceContext();

        // GET: api/Authors
        /// <summary>
        /// Get all Authors
        /// </summary>
        /// <remarks>
        /// Get a list of all Authors
        /// </remarks>
        /// <returns></returns>
        public IQueryable<Author> GetAuthors()
        {
            return db.Authors;
        }

        // GET: api/Authors/5
        /// <summary>
        /// Get Author by ID
        /// </summary>
        /// <remarks>
        /// Get a list of a specific Author
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Author))]
        public async Task<IHttpActionResult> GetAuthor(int id)
        {
            Author author = await db.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // PUT: api/Authors/5
        /// <summary>
        /// Show an Author
        /// </summary>
        /// <remarks>
        /// Show a specified Author
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAuthor(int id, Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.Id)
            {
                return BadRequest();
            }

            db.Entry(author).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        /// <summary>
        /// Post a new Author into the Collection
        /// </summary>
        /// <remarks>
        /// Create a new Author and adds to the Collection
        /// </remarks>
        /// <param name="author"></param>
        /// <returns></returns>
        [ResponseType(typeof(Author))]
        public async Task<IHttpActionResult> PostAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Authors.Add(author);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        /// <summary>
        /// Delete an Author
        /// </summary>
        /// <remarks>
        /// Delete a specified Author
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Author))]
        public async Task<IHttpActionResult> DeleteAuthor(int id)
        {
            Author author = await db.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            db.Authors.Remove(author);
            await db.SaveChangesAsync();

            return Ok(author);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuthorExists(int id)
        {
            return db.Authors.Count(e => e.Id == id) > 0;
        }
    }
}