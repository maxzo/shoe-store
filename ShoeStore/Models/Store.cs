using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoeStore.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Article> Articles { get; set; }

        /*public Store()
        {
            this.Articles = new HashSet<Article>();
        }*/
    }
}