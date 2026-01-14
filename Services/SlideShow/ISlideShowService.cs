namespace WebBanGiayTheThao.Service
{
    public interface ISlideShowService
    {
        Task<List<WebBanGiayTheThao.Models.SlideShow>> LoadDSSlideShow();
        Task<WebBanGiayTheThao.Models.SlideShow?> GetSlideShowById(int id);
        Task UpdateSlideShow(WebBanGiayTheThao.Models.SlideShow slideShow);
    }
}
