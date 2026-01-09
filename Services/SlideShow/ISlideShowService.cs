namespace WebBanGiayTheThao.Services.SlideShow
{
    public interface ISlideShowService
    {
        Task<IEnumerable<WebBanGiayTheThao.Models.SlideShow>> LoadDSSlideShow();
        Task<WebBanGiayTheThao.Models.SlideShow?> GetSlideShowById(int id);
        Task UpdateSlideShow(WebBanGiayTheThao.Models.SlideShow slideShow);
        Task<bool> KiemTraLinkDaTonTai(string link, int idHienTai);
    }
}
