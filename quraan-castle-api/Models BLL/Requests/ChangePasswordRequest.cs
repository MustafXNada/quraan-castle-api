namespace quraan_castle_api.Models_BLL.Requests
{
    public class ChangePasswordRequest
    {
        public string userUuid { get; set; }
        public string userEmail { get; set; }
        public string userCurrentPassword { get; set; }
        public string userPassword { get; set; }
    }
}
