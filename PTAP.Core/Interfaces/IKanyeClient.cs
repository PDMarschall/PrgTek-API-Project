using PTAP.Core.Models;

namespace PTAP.Core.Interfaces
{
    public interface IKanyeClient
    {
        Task GetWisdom();
        KanyeImage Image { get; set; }
        KanyeQuote Quote { get; set; }
    }
}
