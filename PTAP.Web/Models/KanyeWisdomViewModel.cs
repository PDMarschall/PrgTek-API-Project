using Microsoft.Build.Framework;
using PTAP.Core.Models;

namespace PTAP.Web.Models
{
    public class KanyeWisdomViewModel
    {
        [Required]
        public Quote WisdomText { get; set; }
        [Required]
        public KanyeImage WisdomImage { get; set; }

        public KanyeWisdomViewModel(Quote wisdomText, KanyeImage wisdomImage)
        {
            WisdomText = wisdomText;
            WisdomImage = wisdomImage;
        }
    }
}
