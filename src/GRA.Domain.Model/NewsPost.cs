﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GRA.Domain.Model
{
    public class NewsPost : Abstract.BaseDomainEntity
    {
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime? PublishedAt { get; set; }
    }
}
