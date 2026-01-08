namespace WebBanGiayTheThao.Services.SlideShow
{
    public interface ISlideShowService
    {
        Task<IEnumerable<WebBanGiayTheThao.Models.SlideShow>> LoadDSSlideShow();
    }
}
