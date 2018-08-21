using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebNotes.data.Entities;

namespace WebNotes.data
{
    public class NotesContext:IdentityDbContext<User>
    {
        public NotesContext(): base("DefaultConnection")
        { }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Note> Notes { get; set; }

    }

    //public class NotesDataInitializer : DropCreateDatabaseIfModelChanges<NotesContext>
    //{
    //    protected override void Seed(NotesContext context)
    //    {
    //        var UsersList = new List<User>()
    //        {
    //            new User() {Login = "mr.viper77",Password = HashPassword("qwer1234"),Name = "Borys",Surname = "Varhatyi",
    //            Email = "mr.viper77@gmail.com",City="Lviv",Phone="0965438256"},
    //            new User() {Login = "TheWorstKnight",Password = HashPassword("qwertyzxcv1488"),Name = "Vasya",Surname = "Gavrilov",
    //            Email = "gavv@gmail.com",City="Kyiv",Phone="3241782462"}
    //        };
    //        UsersList.ForEach(u => context.Users.Add(u));
    //        context.SaveChanges();

    //    }
    //    private static string HashPassword(string password)
    //    {
    //        byte[] salt;
    //        byte[] buffer2;
    //        if (password == null)
    //        {
    //            throw new ArgumentNullException("password");
    //        }
    //        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
    //        {
    //            salt = bytes.Salt;
    //            buffer2 = bytes.GetBytes(0x20);
    //        }
    //        byte[] dst = new byte[0x31];
    //        Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
    //        Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
    //        return Convert.ToBase64String(dst);
    //    }
    //}
}
