using quraan_castle_api.Models;
using quraan_castle_api.Services;
using Microsoft.EntityFrameworkCore;

namespace quraan_castle_api
{
    public static class InitializationData
    {
       
        private static quraancastledbContext _db = new quraancastledbContext();

        public static void Seed()
        {
            try
            {
                if (!_db.Users.Any())
                {
                    string password = "P@ssword@123";
                    _db.Users.Add(new User()
                    {
                        Name = "API",
                        Uuid = Guid.NewGuid().ToString(),
                        IsActive = true,
                        Email = "dev.moustafa.nada@gmail.com",
                        IsSubscriber = false,
                        CreatedAt = DateTime.UtcNow,
                        Password = password.ComputeSHA256Hash()
                        

                    }) ;

                    _db.SaveChanges();
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
