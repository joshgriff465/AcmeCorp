using AcmeCorp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorp.Data
{
    public static class SampleData
    {

        public static void InitializeSeedDataWithUserManager(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();


                db.Database.Migrate();
                InsertSampleData(db);
            }
        }

        public static void InsertSampleData(ApplicationDbContext db)
        {

            List<News> newsItems = new List<News>()
            {
                new News()
                {
                    Title = "News Item 1",
                    Seo = "news-item-1",
                    ShortDescription = "nulla aliquet enim tortor at auctor urna nunc id cursus metus aliquam eleifend mi in nulla posuere sollicitudin aliquam ultrices sagittis orci a scelerisque purus"
                },
                new News()
                {
                    Title = "News Item 2",
                    Seo = "news-item-1",
                    ShortDescription = "nulla aliquet enim tortor at auctor urna nunc id cursus metus aliquam eleifend mi in nulla posuere sollicitudin aliquam ultrices sagittis orci a scelerisque purus"
                },
                new News()
                {
                    Title = "News Item 3",
                    Seo = "news-item-1",
                    ShortDescription = "nulla aliquet enim tortor at auctor urna nunc id cursus metus aliquam eleifend mi in nulla posuere sollicitudin aliquam ultrices sagittis orci a scelerisque purus"
                },
            };

            if (db.News.Count() == 0)
            {
                db.News.AddRange(newsItems);
                db.SaveChanges();
            }





        }


    }
}
