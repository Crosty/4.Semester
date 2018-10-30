using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookService.Models
{
    public class Book
    {
        /// <summary>
        /// Id of the Book
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Title of the Book
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// Year of the Book
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Price of the Book
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Genre of the Book
        /// </summary>
        public string Genre { get; set; }

        // Foreign key
        /// <summary>
        /// The Authors id
        /// </summary>
        public int AuthorId { get; set; }

        // Virtual Navigation property
        /// <summary>
        /// The Authors name
        /// </summary>
        public virtual Author Author { get; set; }
    }
}