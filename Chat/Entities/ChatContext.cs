using Microsoft.EntityFrameworkCore;

namespace Chat.Entities
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {

            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.UserName).IsRequired().HasMaxLength(64);
            modelBuilder.Entity<User>().Property(x => x.Email).IsRequired().HasMaxLength(256);
            modelBuilder.Entity<User>().Property(x => x.DateCreated).HasDefaultValueSql("SYSDATETIME()");

            modelBuilder.Entity<Message>().HasKey(x => x.Id);
            modelBuilder.Entity<Message>().Property(x => x.DateCreated).HasDefaultValueSql("SYSDATETIME()");
            modelBuilder.Entity<Message>().Property(x => x.Text).IsRequired().HasMaxLength(1024);

            modelBuilder.Entity<Chat>().HasOne(c => c.Sender).WithMany(u => u.SendedMessages).HasForeignKey(u => u.SenderId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Chat>().HasOne(c => c.Receiver).WithMany(u => u.ReceivedMessages).HasForeignKey(u => u.ReceiverId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Chat>().HasOne(c => c.Message).WithMany(u => u.Chat).HasForeignKey(u => u.MessageId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Chat>().Property(x => x.DateCreated).HasDefaultValueSql("SYSDATETIME()");
            modelBuilder.Entity<Chat>().HasKey(x => x.Id);



        }
    }
}
