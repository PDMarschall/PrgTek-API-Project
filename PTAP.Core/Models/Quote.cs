using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PTAP.Core.Models
{
    public class Quote
    {
        [DisplayName("#")]
        public int Id { get; set; }

        [Required, JsonPropertyName("quote"), DisplayName("Words of Wisdom")]
        public string QuoteText { get; set; }
    }
}