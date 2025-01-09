namespace TuringClothes.Database
{
    public class DataSeed
    {
        private readonly MyDatabase _context;
        public DataSeed(MyDatabase context)
        {
            _context = context;
        }

        public void Seed()
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("123456");
            var hashedPassword2 = BCrypt.Net.BCrypt.HashPassword("123456");
            var hashedPassword3 = BCrypt.Net.BCrypt.HashPassword("123456");
            var hashedPassword4 = BCrypt.Net.BCrypt.HashPassword("1234567");
            var hashedPassword5 = BCrypt.Net.BCrypt.HashPassword("123456");
            var hashedPassword6 = BCrypt.Net.BCrypt.HashPassword("123456");

            User[] users =
            {
                new User
            {
                Name = "Juanma",
                Surname = "López Navarro",
                Email = "juanmita@gmail.com",
                Password = hashedPassword,
                Address = "Calle La Fuente 3",
                Orders = new List<Order>(),
                Role = "admin"
            },
            new User
            {
                Name = "Francisco",
                Surname = "Ruiz Sánchez",
                Email = "franruiz@gmail.com",
                Password = hashedPassword2,
                Address = "Calle Hermes 4",
                Role = "user"
            },
            new User
            {
                Name = "Daniel",
                Surname = "Díaz Pérez",
                Email = "danidiazperez@gmail.com",
                Password = hashedPassword3,
                Address = "Calle Hermes 4",
                Role = "user"
            },
            new User
            {
                Name = "Nerea",
                Surname = "Muñoz Pérez",
                Email = "nereamuñoz@hotmail.com",
                Password = hashedPassword4,
                Address = "Calle Hermes 4",
                Role = "user"
            },
            new User
            {
                Name = "Julia",
                Surname = "Pérez Cabrera",
                Email = "julitaperez@gmail.com",
                Password = hashedPassword5,
                Address = "Calle Navarro Ledesma 13",
                Role = "user"
            },
            new User
            {
                Name = "Noe",
                Surname = "Frances",
                Email = "noe@gmail.com",
                Password = hashedPassword6,
                Address = "Riogordo n0 3",
                Role = "user"
            }
            };

            Product[] products ={
                new Product
                {
                    Name = "Sudadera blanca cuello redondo",
                    Description = "Lo mejor contra el frio",
                    Price = 2699,
                    Stock = 50,
                    Image = "images/sudadera-blanca.png"
                },
                new Product
                {
                    Name = "Camiseta blanca",
                    Description = "Una bonita camiseta blanca",
                    Price = 2050,
                    Stock = 100,
                    Image = "images/camiseta-blanca.png"
                },
                new Product
                {
                    Name = "Vestido largo",
                    Description = "Un vestido largo Turing",
                    Price = 2599,
                    Stock = 200,
                    Image = "images/Vestido-Largo.png"
                },
                new Product
                {
                    Name = "Cazadora marrón efecto ante",
                    Description = "Lo mejor contra el frio",
                    Price = 4000,
                    Stock = 50,
                    Image = "images/cazadoraAnte.png",

                },
                new Product
                {
                    Name = "Polo gris hombre",
                    Description = "Lo mejor contra el frio",
                    Price = 1999,
                    Stock = 0,
                    Image = "images/Polo-hombre.png"
                },
                new Product
                {
                    Name = "Camiseta negra con cuello redondo",
                    Description = "Lo mejor contra el frio",
                    Price = 2090,
                    Stock = 50,
                    Image = "images/Camiseta-cuello-redondo-black.png"
                },
                new Product
                {
                    Name = "Chaqueta de traje",
                    Description = "Lo mejor contra el frio",
                    Price = 4299,
                    Stock = 30,
                    Image = "images/chaqueta-traje.png"
                },
                new Product
                {
                    Name = "Traje beige",
                    Description = "Lo mejor contra el frio",
                    Price = 8000,
                    Stock = 30,
                    Image = "images/Traje-beige.png"
                },
                new Product
                {
                    Name = "Cazadora negra mujer",
                    Description = "Lo mejor contra el frio",
                    Price = 3495,
                    Stock = 50,
                    Image = "images/cazadora-negra-mujer.png"
                },
                new Product
                {
                    Name = "Blazer efecto piel",
                    Description = "Lo mejor contra el frio",
                    Price = 3500,
                    Stock = 50,
                    Image = "images/blazer-efecto-piel.png"
                },
                new Product
                {
                    Name = "Calcetines",
                    Description = "Lo mejor contra el frio",
                    Price = 599,
                    Stock = 40,
                    Image = "images/calcetines.png"
                },
                new Product
                {
                    Name = "Chaqueta vaquera",
                    Description = "Lo mejor contra el frio",
                    Price = 3599,
                    Stock = 40,
                    Image = "images/chaqueta-vaquera.png"
                },
                new Product
                {
                    Name = "Sudadera cremallera cuello",
                    Description = "Lo mejor contra el frio",
                    Price = 3500,
                    Stock = 60,
                    Image = "images/sudadera-cremallera-cuello.png"
                },
                new Product
                {
                    Name = "Jersey canalé blanco",
                    Description = "Lo mejor contra el frio",
                    Price = 3299,
                    Stock = 25,
                    Image = "images/jersey-canale-blanco.png"
                },
                new Product
                {
                    Name = "Jersey canalé oscuro",
                    Description = "Lo mejor contra el frio",
                    Price = 3299,
                    Stock = 25,
                    Image = "images/jersey-canale-oscuro.png"
                },
                new Product
                {
                    Name = "Pantalón wide fit oscuro",
                    Description = "Lo mejor contra el frio",
                    Price = 4000,
                    Stock = 28,
                    Image = "images/pantalon-wide-fit-negro.png"
                },
                new Product
                {
                    Name = "Pantalón vaquero hombre",
                    Description = "Lo mejor contra el frio",
                    Price = 4000,
                    Stock = 28,
                    Image = "images/pantalon-vaquero-hombre.png"
                },
                new Product
                {
                    Name = "Chaleco negro mujer",
                    Description = "Lo mejor contra el frio",
                    Price = 2890,
                    Stock = 25,
                    Image = "images/chaleco-negro-mujer.png"
                },
                new Product
                {
                    Name = "Pantalón recto soft",
                    Description = "Lo mejor contra el frio",
                    Price = 3600,
                    Stock = 25,
                    Image = "images/pantalon-recto-soft.png"
                },
                new Product
                {
                    Name = "Jersey punto cuello alto",
                    Description = "Lo mejor contra el frio",
                    Price = 2999,
                    Stock = 25,
                    Image = "images/jersey-punto-cuello-alto.png"
                },
                new Product
                {
                    Name = "Camisa gris mujer",
                    Description = "Lo mejor contra el frio",
                    Price = 1890,
                    Stock = 26,
                    Image = "images/Camisa-gris-mujer.png"
                },
                new Product
                {
                    Name = "Pantalón recto tiro medio",
                    Description = "Lo mejor contra el frio",
                    Price = 3295,
                    Stock = 30,
                    Image = "images/Pantalon-recto-tiro-medio.png"
                },
                new Product
                {
                    Name = "Vestido negro cinturón",
                    Description = "Lo mejor contra el frio",
                    Price = 4500,
                    Stock = 15,
                    Image = "images/vestido-negro-cinturon.png"
                },
                new Product
                {
                    Name = "Bomber negra acolchada",
                    Description = "Lo mejor contra el frio",
                    Price = 3850,
                    Stock = 15,
                    Image = "images/bomber-negra-acolchada.png"
                }
            };

            Review[] reviews = {
                new Review
                {
                    ProductId = 4,
                    UserId = 1,
                    Texto = "Me ha encantado, muy recomendada",
                    DateTime = DateTime.UtcNow,
                    Rating = 1
                },
                new Review
                {
                    ProductId = 4,
                    UserId = 3,
                    Texto = "Esta bien pero mejorable",
                    DateTime = DateTime.UtcNow,
                    Rating = 0
                },
                new Review
                {
                    ProductId = 6,
                    UserId = 2,
                    Texto = "Todo genial, muy cómodo",
                    DateTime = DateTime.UtcNow,
                    Rating = 1
                },
                new Review
                {
                    ProductId = 7,
                    UserId = 5,
                    Texto = "Lo odio, no me gusta",
                    DateTime = DateTime.UtcNow,
                    Rating = -1
                },

                new Review
                {

                    ProductId = 1,
                    UserId = 1,
                    Texto = "No me agrada",
                    DateTime = DateTime.UtcNow,
                    Rating = -1
                },
                new Review {

                    ProductId = 1,
                    UserId = 2,
                    Texto = "Me encanta",
                    DateTime = DateTime.UtcNow,
                    Rating = 1
                },
                new Review {
                    ProductId = 1,
                    UserId = 3,
                    Texto = "No está mal",
                    DateTime = DateTime.UtcNow,
                    Rating = 0
                },
                new Review {
                    ProductId = 1,
                    UserId = 4,
                    Texto = "Bomba",
                    DateTime = DateTime.UtcNow,
                    Rating = 1
                },
                new Review {
                    ProductId = 2,
                    UserId = 1,
                    Texto = "No me agrada",
                    DateTime = DateTime.UtcNow,
                    Rating = -1
                },
                new Review {
                    ProductId = 2,
                    UserId = 2,
                    Texto = "Basura",
                    DateTime = DateTime.UtcNow,
                    Rating = -1
                },
                new Review {
                    ProductId = 2,
                    UserId = 3,
                    Texto = "Alucinante",
                    DateTime = DateTime.UtcNow,
                    Rating = 1
                },
                new Review {
                    ProductId = 3,
                    UserId = 1,
                    Texto = "Malisimo",
                    DateTime = DateTime.UtcNow,
                    Rating = -1
                },
                new Review {
                    ProductId = 3,
                    UserId = 2,
                    Texto = "No me agrada",
                    DateTime = DateTime.UtcNow,
                    Rating = -1
                },
                new Review {
                    ProductId = 3,
                    UserId = 3,
                    Texto = "Me fascina",
                    DateTime = DateTime.UtcNow,
                    Rating = 1
                },
                new Review {
                    ProductId = 4,
                    UserId = 1,
                    Texto = "No me agrada",
                    DateTime = DateTime.UtcNow,
                    Rating = -1
                },
                new Review {
                    ProductId = 4,
                    UserId = 2,
                    Texto = "Lo mejor",
                    DateTime = DateTime.UtcNow,
                    Rating = 1
                },
                new Review {
                    ProductId = 4,
                    UserId = 3,
                    Texto = "No está mal",
                    DateTime = DateTime.UtcNow,
                    Rating = 0
                },
                new Review {
                    ProductId = 4,
                    UserId = 4,
                    Texto = "Mejor de lo esperado",
                    DateTime = DateTime.UtcNow,
                    Rating = 1
                },
                new Review {
                    ProductId = 4,
                    UserId = 5,
                    Texto = "No me agrada",
                    DateTime = DateTime.UtcNow,
                    Rating = -1
                }
            };



            _context.Users.AddRange(users);
            _context.SaveChanges();
            _context.Products.AddRange(products);
            _context.SaveChanges();
            _context.Reviews.AddRange(reviews);
            _context.SaveChanges();
            Console.WriteLine("datos insertados en la db");
        }
    }

}
