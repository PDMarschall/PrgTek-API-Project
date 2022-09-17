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
        public string DisplayString { get; set; }

        public KanyeWisdomViewModel(Quote wisdomText, KanyeImage wisdomImage)
        {
            WisdomText = wisdomText;
            WisdomImage = wisdomImage;
            if (wisdomImage != null)
            {
                DisplayString = GetDisplayString(wisdomImage.ImageBytes);
            }
            else
            {
                DisplayString = string.Empty;
            }
        }

        private string GetDisplayString(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}