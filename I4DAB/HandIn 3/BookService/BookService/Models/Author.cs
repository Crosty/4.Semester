using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookService.Models
{
    public class Author
    {
        /// <summary>
        /// Id of the Author
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the Author
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}