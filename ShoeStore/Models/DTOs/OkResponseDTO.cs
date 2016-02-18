using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStore.Models
{
    public class OkResponseDTO : ResponseDTO
    {
        public int total_elements { get; set; }

        public OkResponseDTO()
        {
            this.success = true;
        }
    }
}