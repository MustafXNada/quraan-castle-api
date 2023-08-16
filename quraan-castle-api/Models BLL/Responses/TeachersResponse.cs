namespace quraan_castle_api.Models_BLL.Responses
{
    public class TeachersResponse
    {
        public List<TeacherModel> models { get; set; }
    }

    public class TeacherModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public bool gender { get; set; }
        public string bio { get; set; }
        public int order { get; set; }
    }
}

