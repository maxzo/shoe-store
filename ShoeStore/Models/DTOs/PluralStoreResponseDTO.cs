﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStore.Models
{
    public class PluralStoreResponseDTO : OkResponseDTO
    {
        public IEnumerable<StoreDTO> stores { get; set; }
    }
}