﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PTAP.Core.Models
{
    public class Quote
    {
        public int Id { get; set; }

        [Required, JsonPropertyName("quote")]
        public string QuoteText { get; set; }

        [JsonPropertyName("rating"), Range(1, 5)]
        public int Rating { get; set; }

    }
}
