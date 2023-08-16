namespace quraan_castle_api.Models_BLL.Responses
{
    public class RatesResponse
    {
        public List<RateModel> models { get; set; }
    }

    public class RateModel
    {
        public string teacherId { get; set; }
        public int rate { get; set; }
        
    }
}
