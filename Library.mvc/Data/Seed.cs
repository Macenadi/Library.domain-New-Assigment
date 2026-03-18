using Library.domain.Models;
using Library.mvc.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.MVC.Data
{
    public static class Seed
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            if (await context.Books.AnyAsync() || await context.Members.AnyAsync() || await context.Loans.AnyAsync())
                return;

            var books = new[]
            {
                new Book { Title = "C# Basics", Author = "John Smith", Isbn = "111", Category = "Programming", IsAvailable = true },
                new Book { Title = "ASP.NET Core", Author = "Jane Doe", Isbn = "112", Category = "Programming", IsAvailable = true },
                new Book { Title = "Database Design", Author = "Mark Lee", Isbn = "113", Category = "IT", IsAvailable = true },
                new Book { Title = "Clean Code", Author = "Robert Martin", Isbn = "114", Category = "Programming", IsAvailable = true },
                new Book { Title = "Design Patterns", Author = "Gamma", Isbn = "115", Category = "IT", IsAvailable = true },
                new Book { Title = "AI Basics", Author = "Andrew Ng", Isbn = "116", Category = "AI", IsAvailable = true },
                new Book { Title = "Machine Learning", Author = "Tom Mitchell", Isbn = "117", Category = "AI", IsAvailable = true },
                new Book { Title = "Deep Learning", Author = "Ian Goodfellow", Isbn = "118", Category = "AI", IsAvailable = true },
                new Book { Title = "Networking Fundamentals", Author = "Cisco", Isbn = "119", Category = "Networking", IsAvailable = true },
                new Book { Title = "Cyber Security", Author = "Kevin Mitnick", Isbn = "120", Category = "Security", IsAvailable = true },
                new Book { Title = "Linux Essentials", Author = "Linus Torvalds", Isbn = "121", Category = "IT", IsAvailable = true },
                new Book { Title = "Python Programming", Author = "Guido", Isbn = "122", Category = "Programming", IsAvailable = true },
                new Book { Title = "Java Fundamentals", Author = "James Gosling", Isbn = "123", Category = "Programming", IsAvailable = true },
                new Book { Title = "Web Development", Author = "Brad Traversy", Isbn = "124", Category = "Web", IsAvailable = true },
                new Book { Title = "HTML & CSS", Author = "Jon Duckett", Isbn = "125", Category = "Web", IsAvailable = true },
                new Book { Title = "JavaScript Guide", Author = "Kyle Simpson", Isbn = "126", Category = "Web", IsAvailable = true },
                new Book { Title = "React Basics", Author = "Dan Abramov", Isbn = "127", Category = "Web", IsAvailable = true },
                new Book { Title = "Angular Guide", Author = "Google", Isbn = "128", Category = "Web", IsAvailable = true },
                new Book { Title = "Cloud Computing", Author = "AWS", Isbn = "129", Category = "Cloud", IsAvailable = true },
                new Book { Title = "DevOps Handbook", Author = "Gene Kim", Isbn = "130", Category = "DevOps", IsAvailable = true }
            };

            await context.Books.AddRangeAsync(books);
            await context.SaveChangesAsync();

            var members = new[]
            {
                new Member { FullName = "Alice Johnson", Email = "alice@email.com", Phone = "1111" },
                new Member { FullName = "Bob Smith", Email = "bob@email.com", Phone = "2222" },
                new Member { FullName = "Charlie Brown", Email = "charlie@email.com", Phone = "3333" },
                new Member { FullName = "David Wilson", Email = "david@email.com", Phone = "4444" },
                new Member { FullName = "Emma Davis", Email = "emma@email.com", Phone = "5555" },
                new Member { FullName = "Frank Moore", Email = "frank@email.com", Phone = "6666" },
                new Member { FullName = "Grace Taylor", Email = "grace@email.com", Phone = "7777" },
                new Member { FullName = "Henry Anderson", Email = "henry@email.com", Phone = "8888" },
                new Member { FullName = "Isabella Thomas", Email = "isabella@email.com", Phone = "9999" },
                new Member { FullName = "Jack White", Email = "jack@email.com", Phone = "0000" }
            };

            await context.Members.AddRangeAsync(members);
            await context.SaveChangesAsync();

            var loans = new[]
            {
                new Loan { BookId = books[0].Id, MemberId = members[0].Id, LoanDate = DateTime.Now.AddDays(-2), DueDate = DateTime.Now.AddDays(5), ReturnedDate = null },
                new Loan { BookId = books[1].Id, MemberId = members[1].Id, LoanDate = DateTime.Now.AddDays(-3), DueDate = DateTime.Now.AddDays(4), ReturnedDate = null },
                new Loan { BookId = books[2].Id, MemberId = members[2].Id, LoanDate = DateTime.Now.AddDays(-10), DueDate = DateTime.Now.AddDays(-2), ReturnedDate = null },
                new Loan { BookId = books[3].Id, MemberId = members[3].Id, LoanDate = DateTime.Now.AddDays(-8), DueDate = DateTime.Now.AddDays(-1), ReturnedDate = null },
                new Loan { BookId = books[4].Id, MemberId = members[4].Id, LoanDate = DateTime.Now.AddDays(-10), DueDate = DateTime.Now.AddDays(-5), ReturnedDate = DateTime.Now.AddDays(-3) },
                new Loan { BookId = books[5].Id, MemberId = members[5].Id, LoanDate = DateTime.Now.AddDays(-6), DueDate = DateTime.Now.AddDays(-2), ReturnedDate = DateTime.Now.AddDays(-1) },
                new Loan { BookId = books[6].Id, MemberId = members[6].Id, LoanDate = DateTime.Now.AddDays(-4), DueDate = DateTime.Now.AddDays(2), ReturnedDate = null },
                new Loan { BookId = books[7].Id, MemberId = members[7].Id, LoanDate = DateTime.Now.AddDays(-7), DueDate = DateTime.Now.AddDays(-1), ReturnedDate = null },
                new Loan { BookId = books[8].Id, MemberId = members[8].Id, LoanDate = DateTime.Now.AddDays(-5), DueDate = DateTime.Now.AddDays(1), ReturnedDate = null },
                new Loan { BookId = books[9].Id, MemberId = members[9].Id, LoanDate = DateTime.Now.AddDays(-9), DueDate = DateTime.Now.AddDays(-3), ReturnedDate = DateTime.Now.AddDays(-2) },
                new Loan { BookId = books[10].Id, MemberId = members[0].Id, LoanDate = DateTime.Now.AddDays(-2), DueDate = DateTime.Now.AddDays(3), ReturnedDate = null },
                new Loan { BookId = books[11].Id, MemberId = members[1].Id, LoanDate = DateTime.Now.AddDays(-1), DueDate = DateTime.Now.AddDays(6), ReturnedDate = null },
                new Loan { BookId = books[12].Id, MemberId = members[2].Id, LoanDate = DateTime.Now.AddDays(-6), DueDate = DateTime.Now.AddDays(-2), ReturnedDate = null },
                new Loan { BookId = books[13].Id, MemberId = members[3].Id, LoanDate = DateTime.Now.AddDays(-3), DueDate = DateTime.Now.AddDays(2), ReturnedDate = null },
                new Loan { BookId = books[14].Id, MemberId = members[4].Id, LoanDate = DateTime.Now.AddDays(-8), DueDate = DateTime.Now.AddDays(-4), ReturnedDate = DateTime.Now.AddDays(-1) }
            };

            await context.Loans.AddRangeAsync(loans);

            foreach (var loan in loans)
            {
                if (loan.ReturnedDate == null)
                {
                    var book = await context.Books.FindAsync(loan.BookId);
                    if (book != null)
                    {
                        book.IsAvailable = false;
                    }
                }
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedAdminUserAndRolesAsync(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            string adminRole = "Admin";
            string adminEmail = "admin@library.com";
            string adminPassword = "Admin123!";

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }
            else
            {
                if (!await userManager.IsInRoleAsync(adminUser, adminRole))
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }
        }
    }
}
