namespace quraan_castle_api.Models_BLL.Responses
{
    public class PlansResponse
    {
        public List<PlanModel> models { get; set; }
    }

    public class PlanModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public int order { get; set; }

    }
}
