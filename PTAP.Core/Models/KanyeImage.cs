using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTAP.Core.Models
{
    public class KanyeImage
    {
        public Bitmap Image { get; set; }

        public KanyeImage(Bitmap image)
        {
            Image = image;
        }

        public KanyeImage(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                Image = new Bitmap(memoryStream);
            }
        }
    }
}
