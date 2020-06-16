using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WhatsappIntegration.Entity.Concrete;

namespace WhatsappIntegration.DAL.Context
{
    public class IdentityContext : IdentityDbContext<UserAgent>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessages> ChatMessages { get; set; }
        public DbSet<ChatTypes> ChatTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Directory> Directory { get; set; }
        public DbSet<SmartReplies> SmartReplies { get; set; }
        public DbSet<VChatList> VChatList { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<VChatList>(cl => {
                cl.HasNoKey();
                cl.ToView("VChatList");
            });
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
