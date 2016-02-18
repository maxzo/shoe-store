using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStore.Models
{
    public class SingleArticleResponseDTO : OkResponseDTO
    {
        public ArticleDTO article { get; set; }
    }
}