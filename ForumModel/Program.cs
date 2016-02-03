using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumData5
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ForumContext())
            {
                var user = new User()
                {
                    login = "admin",
                    password = "nimda",
                    role = Role.ADMIN
                };

                var topic = new Topic()
                {
                    title = "Java",
                    createdAt = DateTime.Now

                };
                

                var post = new Post()
                {
                    content = "Java 8 jest fajna!",
                    createdAt = DateTime.Now,
                };

                post.topic = topic;
                post.user = user;

                topic.posts.Add(post);

                user.topics.Add(topic);
                user.posts.Add(post);

                db.users.Add(user);
                db.topics.Add(topic);
                db.posts.Add(post);
                db.SaveChanges();
            }
        }
    }
}
