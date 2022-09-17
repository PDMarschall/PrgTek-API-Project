using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PTAP.Core.Models
{
    public class KanyeQuote
    {
        [DisplayName("#")]
        public int Id { get; set; }

        [Required, JsonPropertyName("quote"), DisplayName("Words of Wisdom")]
        public string QuoteText { get; set; }

        public override string ToString()
        {
            if (QuoteText.Last() == '.')
            {
                return QuoteText;
            }
            else
            {
                return QuoteText + ".";
            }
        }
    }
}