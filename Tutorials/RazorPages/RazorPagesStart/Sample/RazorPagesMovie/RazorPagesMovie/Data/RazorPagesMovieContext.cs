using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RazorPagesMovie.Models
{
    public class RazorPagesMovieContext : DbContext
    {
        //通过调用 DbContextOptions 对象中的一个方法将连接字符串名称传递到上下文。 进行本地开发时， ASP.NET Core 配置系统 在 appsettings.json 文件中读取数据库连接字符串。
        public RazorPagesMovieContext (DbContextOptions<RazorPagesMovieContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPagesMovie.Models.Movie> Movie { get; set; }
    }
}
