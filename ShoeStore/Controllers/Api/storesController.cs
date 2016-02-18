using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ShoeStore.Models;

namespace ShoeStore.Controllers
{
    public class storesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: services/stores
        public ResponseDTO GetStores()
        {
            if (!IsAuthenticated())
            {
                return new ErrorResponseDTO
                {
                    error_code = 401,
                    error_msg = "Not authorized"
                };
            }

            var stores = from s in db.Stores
                         select new StoreDTO
                         {
                             id = s.Id,
                             name = s.Name,
                             address = s.Address
                         };
            var amountOfStores = stores.Count();

            if (amountOfStores == 1)
            {
                return new SingleStoreResponseDTO
                {
                    store = stores.First(),
                    total_elements = amountOfStores
                };
            }

            return new PluralStoreResponseDTO
            {
                stores = stores,
                total_elements = amountOfStores
            };
        }

        private bool IsAuthenticated()
        {
            string[] parts = UTF8Encoding.UTF8.GetString(Convert.FromBase64String(Request.Headers.Authorization.Parameter)).Split(':');
            return parts.Length == 2 && parts[0] == "my_user" && parts[1] == "my_password";
        }
    }
}