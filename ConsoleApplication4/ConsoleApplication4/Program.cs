using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // Create and save a new Blog 
                Console.Write("Enter a name for a new Blog: ");
                db.Database.Log = Console.Write;
                var name = Console.ReadLine();

                var blog = new Blog {
                    Name = name,
                    Posts = new List<Post>()
                };
                blog.Posts.Add(new Post { Blog = blog, Content = "some", Title = "fgfg" });
                blog.Posts.Add(new Post { Blog = blog, Content = "some1", Title = "fgfg1" });
                db.Blogs.Add(blog);
                db.SaveChanges();

                // Display all Blogs from the database 
                var query = from b in db.Blogs orderby b.Name select b;

                var blog2 = new Blog
                {
                    Name = name,
                    Posts = new List<Post>()
                };
                blog2.Posts.Add(new Post { Content = "some", Title = "fgfg1" });
                blog2.Posts.Add(new Post { PostId = 1, Content = "some", Title = "updated" });
                var first = query.FirstOrDefault();

                first.Posts.Remove(first.Posts.FirstOrDefault());
                db.SaveChanges();

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
