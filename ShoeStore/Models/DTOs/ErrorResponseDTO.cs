using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStore.Models
{
    public class ErrorResponseDTO : ResponseDTO
    {
        public string error_msg { get; set; }
        public int error_code { get; set; }

        public ErrorResponseDTO()
        {
            this.success = false;
        }
    }
}