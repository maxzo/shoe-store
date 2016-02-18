using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;
using ShoeStore.Models;

namespace ShoeStore.Controllers
{
    public class articlesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: services/articles
        public ResponseDTO GetArticles()
        {
            if (!IsAuthenticated())
            {
                return new ErrorResponseDTO
                {
                    error_code = 401,
                    error_msg = "Not authorized"
                };
            }

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

        // GET: services/articles/stores/5
        [Route("services/articles/stores/{id}")]
        public ResponseDTO GetStoreArticles(string id)
        {
            if (!IsAuthenticated())
            {
                return new ErrorResponseDTO
                {
                    error_code = 401,
                    error_msg = "Not authorized"
                };
            }

            int storeId;
            try
            {
                storeId = Convert.ToInt32(id);
            }
            catch (Exception ex)
            {
                return new ErrorResponseDTO
                {
                    error_code = 400,
                    error_msg = "Bad request"
                };
            }

            var stores = from s in db.Stores
                         where s.Id == storeId
                         select s;

            if (!stores.Any())
            {
                return new ErrorResponseDTO
                {
                    error_code = 404,
                    error_msg = "Record not found"
                };
            }

            var articles = from a in db.Articles
                           where a.StoreId == storeId
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

        private bool IsAuthenticated()
        {
            if (Request.Headers.Authorization == null) return false;
            string[] parts = UTF8Encoding.UTF8.GetString(Convert.FromBase64String(Request.Headers.Authorization.Parameter)).Split(':');
            return parts.Length == 2 && parts[0] == "my_user" && parts[1] == "my_password";
        }
    }
}