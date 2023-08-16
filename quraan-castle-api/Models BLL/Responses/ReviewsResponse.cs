namespace quraan_castle_api.Models_BLL.Responses
{
    public class ReviewsResponse
    {
        public List<ReviewModel> models { get; set; }
    }
    public class ReviewModel
    {
        public int teacherId { get; set; }
        public string review { get; set; }

    }
}
