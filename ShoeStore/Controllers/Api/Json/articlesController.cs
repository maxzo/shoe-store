using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ShoeStore.Models;

namespace ShoeStore.Controllers
{
    public class articlesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: services/articles
        public ResponseDTO GetArticles()
        {
            var articles = from a in db.Articles
                           select new ArticleDTO
                           {
                               id = a.Id,
                               name = a.Name,
                               description = a.Description,
                               price = a.Price,
                               total_in_shelf = a.TotalInShelf,
                               total_in_vault = a.TotalInVault,
                               store_name = a.Store.Name
                           };
            var amountOfArticles = articles.Count();

            if (amountOfArticles == 1)
            {
                return new SingleArticleResponseDTO
                {
                    article = articles.First(),
                    total_elements = amountOfArticles
                };
            }

            return new PluralArticleResponseDTO
            {
                articles = articles,
                total_elements = amountOfArticles
            };
        }

        [Route("services/articles/stores/{id}")]
        public ResponseDTO GetStoreArticles(int id)
        {
            var articles = from a in db.Articles
                           where a.StoreId == id
                           select new ArticleDTO
                           {
                               id = a.Id,
                               name = a.Name,
                               description = a.Description,
                               price = a.Price,
                               total_in_shelf = a.TotalInShelf,
                               total_in_vault = a.TotalInVault,
                               store_name = a.Store.Name
                           };
            var amountOfArticles = articles.Count();

            if (amountOfArticles == 1)
            {
                return new SingleArticleResponseDTO
                {
                    article = articles.First(),
                    total_elements = amountOfArticles
                };
            }

            return new PluralArticleResponseDTO
            {
                articles = articles,
                total_elements = amountOfArticles
            };
        }

        // GET: api/articles/5
        [ResponseType(typeof(Article))]
        public async Task<IHttpActionResult> GetArticle(int id)
        {
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        // PUT: api/articles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutArticle(int id, Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != article.Id)
            {
                return BadRequest();
            }

            db.Entry(article).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/articles
        [ResponseType(typeof(Article))]
        public async Task<IHttpActionResult> PostArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Articles.Add(article);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = article.Id }, article);
        }

        // DELETE: api/articles/5
        [ResponseType(typeof(Article))]
        public async Task<IHttpActionResult> DeleteArticle(int id)
        {
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            db.Articles.Remove(article);
            await db.SaveChangesAsync();

            return Ok(article);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticleExists(int id)
        {
            return db.Articles.Count(e => e.Id == id) > 0;
        }
    }
}