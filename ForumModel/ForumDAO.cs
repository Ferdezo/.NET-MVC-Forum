using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumData5
{
    public class ForumDAO
    {
        private static ForumDAO instance;
        private ForumDAO()
        { }

        public static ForumDAO GetInstance()
        {
            if (instance == null)
                instance = new ForumDAO();

            return instance;
        }

        public User Login(String login, String password)
        {
            using (var db = new ForumContext())
            {
                var query = from u in db.users
                            where u.login.Equals(login) && u.password.Equals(password)
                            select u;

                if (query.Any())
                    return query.ToArray<User>().First();
                else
                    return null;
            }
        }

        public bool IsAdmin(User user)
        {
            using (var db = new ForumContext())
            {
                var query = from u in db.users
                            where u.usrID == user.usrID  && u.role == Role.ADMIN
                            select u;

                if (query.Any())
                    return true;
                else
                    return false;
            }
        }

        public List<User> GetUsers()
        {
            List<User> results = null;
            using (var db = new ForumContext())
            {
                var query = from u in db.users
                            select u;
                results = query.ToList<User>();
            }

            return results;
        }

        public List<Topic> GetTopics()
        {
            List<Topic> retults = null;
            using (var db = new ForumContext())
            {
                var query = from t in db.topics
                            select t;
                retults = query.ToList<Topic>();

                foreach (Topic topic in retults)
                {
                    var user = topic.author;
                    string login = null;
                    if (user != null)
                        login = user.login;
                }
            }



            return retults;
        }

        public List<Post> GetPosts(Topic topic)
        {
            List<Post> results = null;
            using (var db = new ForumContext())
            {
                var query = from p in db.posts
                            where p.topID == topic.topID
                            select p;
                results = query.ToList<Post>();

                foreach (Post post in results)
                {
                    var user = post.user;
                    string login = null;
                    if (user != null)
                        login = user.login;
                }
            }

            return results;
        }

        public List<Post> GetPosts(int topID)
        {
            List<Post> results = null;
            using (var db = new ForumContext())
            {
                var query = from p in db.posts
                            where p.topID == topID
                            select p;
                results = query.ToList<Post>();

                foreach (Post post in results)
                {
                    var user = post.user;
                    string login = null;
                    if (user != null)
                        login = user.login;
                }
            }

            return results;
        }

        public bool EditPostContent(int posID, string newContent)
        {
            using (var db = new ForumContext())
            {
                var query = from p in db.posts
                            where p.posID == posID
                            select p;

                if (!query.Any())
                    return false;

                var toEdit = query.First();
                toEdit.modifiedAt = DateTime.Now;
                toEdit.content = newContent;
                db.SaveChanges();
            }
            return true;
        }

        public bool EditTopicTitle(int topID, string newTitle)
        {
            using (var db = new ForumContext())
            {
                var query = from t in db.topics
                            where t.topID == topID
                            select t;

                if (!query.Any())
                    return false;

                var toEdit = query.First();
                toEdit.modifiedAt = DateTime.Now;
                toEdit.title = newTitle;
                db.SaveChanges();               
            }
            return true;
        }

        public void AddTopic(String title, User user)
        {
            using (var db = new ForumContext())
            {
                var topic = new Topic();
                topic.title = title;
                topic.createdAt = DateTime.Now;
                
                var founded = db.users.Find(user.usrID);
                if(founded != null)
                    founded.topics.Add(topic);
                topic.author = founded;
                db.topics.Add(topic);
                db.SaveChanges();
            }
        }

        public void AddPost(String content, Topic topic, User user)
        {
            using (var db = new ForumContext())
            {
                var post = new Post();
                post.content = content;
                post.createdAt = DateTime.Now;

                var findTopic = db.topics.Find(topic.topID);
                if (findTopic != null)
                {
                    findTopic.posts.Add(post);
                    post.topic = findTopic;
                }

                var findUser = db.users.Find(user.usrID);
                if (findUser != null)
                {
                    findUser.posts.Add(post);
                    post.user = findUser;
                }

                db.SaveChanges();
            }
        }

        public void AddPost(String content, int topID, User user)
        {
            using (var db = new ForumContext())
            {
                var post = new Post();
                post.content = content;
                post.createdAt = DateTime.Now;


                var query = from t in db.topics
                                where t.topID == topID
                                select t;

                List<Topic> results = query.ToList<Topic>();
                Topic findTopic = results[0];

                if (findTopic != null)
                {
                    findTopic.posts.Add(post);
                    post.topic = findTopic;
                }

                var findUser = db.users.Find(user.usrID);
                if (findUser != null)
                {
                    findUser.posts.Add(post);
                    post.user = findUser;
                }

                db.SaveChanges();
            }
        }

        public bool AddUser(String login, String pswd, Role role)
        {
            using (var db = new ForumContext())
            {
                //check if exists
                var query = from u in db.users
                            where u.login.Equals(login)
                            select u;

                if(query.Any())
                {
                    return false;
                }

                User newUser = new User()
                {
                    login = login,
                    password = pswd,
                    role = role
                };

                db.users.Add(newUser);
                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteUser(User user)
        {
            using (var db = new ForumContext())
            {
                var found = db.users.Find(user.usrID);
                if (found.posts.Any() || found.topics.Any())
                    return false;
                db.users.Remove(found);
                db.SaveChanges();
                return true;
            }
        }

        public bool EditUser(User user)
        {
            using (var db = new ForumContext())
            {
                //check if exists
                var query = from u in db.users
                            where u.login.Equals(user.login) && u.usrID != user.usrID
                            select u;

                if (query.Any())
                    return false;

                var found = db.users.Find(user.usrID);
                if (found == null)
                    return false;
                found.login = user.login;
                found.password = user.password;
                found.role = user.role;
                db.SaveChanges();
            }
            return true;
        }
    }
}
