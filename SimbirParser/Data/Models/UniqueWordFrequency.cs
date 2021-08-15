using System;
using System.ComponentModel.DataAnnotations;

namespace SimbirParser.Data.Models
{
    /// <summary>
    /// Unique word frequency DB model
    /// </summary>
    public class UniqueWordFrequency
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Word
        /// </summary>
        [Required]
        public string Word { get; set; }

        /// <summary>
        /// Words count on page
        /// </summary>
        [Required]
        public int CountOnPage { get; set; }

        /// <summary>
        /// Page URL
        /// </summary>
        [Required]
        public string PageUrl { get; set; }

        /// <summary>
        /// Check date and time
        /// </summary>
        [Required]
        public DateTime CheckDateTime { get; set; }
    }
}
