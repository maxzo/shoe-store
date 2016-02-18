using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ShoeStore.Models
{
    [KnownType(typeof(SingleStoreResponseDTO))]
    [KnownType(typeof(PluralStoreResponseDTO))]
    [KnownType(typeof(SingleArticleResponseDTO))]
    [KnownType(typeof(PluralArticleResponseDTO))]
    public class ResponseDTO
    {
        public bool success { get; set; }
    }
}