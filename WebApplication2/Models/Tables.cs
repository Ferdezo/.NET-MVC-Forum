using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumData5
{
    public enum Role
    {
        ADMIN, USER
    }

    public class User
    {
        public User()
        {
            this.topics = new List<Topic>();
            this.posts = new List<Post>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int usrID { get; set; }
        public String login { get; set; }
        public String password { get; set; }
        public Role role { get; set; }

        public virtual List<Topic> topics { get; set; }
        public virtual List<Post> posts { get; set; }
    }

    public class Topic
    {
        public Topic()
        {
            this.posts = new List<Post>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int topID { get; set; }
        public String title { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? modifiedAt { get; set; }
        public virtual User author { get; set; }
        public virtual List<Post> posts { get; set; }
    }

    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int posID { get; set; }
        public String content { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? modifiedAt { get; set; }

        public int usrID { get; set; }
        public virtual User user { get; set; }

        public int topID { get; set; }
        public virtual Topic topic { get; set; }
    }

    public class ForumContext : DbContext
    {
        public DbSet<Topic> topics { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<User> users { get; set; }
    }
}
