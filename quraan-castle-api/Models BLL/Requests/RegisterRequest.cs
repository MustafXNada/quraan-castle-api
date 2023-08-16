namespace quraan_castle_api.Models_BLL.Requests
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        public bool Gender { get; set; }
    }
}
