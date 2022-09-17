namespace PTAP.Core.Models
{
    public class KanyeImage
    {
        public byte[] ImageBytes { get; }

        public KanyeImage(byte[] bytes)
        {
            ImageBytes = bytes;
        }
    }
}
