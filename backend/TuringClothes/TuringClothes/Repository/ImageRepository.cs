using Microsoft.EntityFrameworkCore;
using TuringClothes.Database;

namespace TuringClothes.Repository
{
    public class ImageRepository : Repository<Image, int>
    {
        private readonly MyDatabase _myDatabase;

        public ImageRepository(MyDatabase myDatabase): base(myDatabase)
        {
            _myDatabase = myDatabase;
        }
    }
}
