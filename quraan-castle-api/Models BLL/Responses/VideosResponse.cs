namespace quraan_castle_api.Models_BLL.Responses
{
    public class VideosResponse
    {
        public List<VideoModel> models { get; set; }
    }
    public class VideoModel
    {

        public string title { get; set; }
        public string bio { get; set; }
        public string path { get; set; }
        public int order { get; set; }
    }
}
