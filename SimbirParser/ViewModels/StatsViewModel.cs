using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimbirParser.ViewModels
{
    /// <summary>
    /// Words statistic view model
    /// </summary>
    public class StatsViewModel
    {
        /// <summary>
        /// Exploring page url
        /// </summary>
        [DisplayName("URL")]
        [Required]
        [StringLength(500)]
        public string Url { get; set; }

        /// <summary>
        /// Exploring page url
        /// </summary>
        [DisplayName("Unique words count")]
        [Required]
        public Dictionary<string, int> UniqueWordsCount { get; set; }
    }
}
