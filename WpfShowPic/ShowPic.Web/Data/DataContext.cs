//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging.Debug;
//using ShowPic.Entity;

//namespace ShowPic.Web.Data
//{
//    public class DataContext:DbContext
//    {
//        private IConfiguration configuration;
//        public DbSet<PictureEntity> Pictures { get; set; }
//        public DbSet<PictureEditor> Editors { get; set; }
//        private static readonly LoggerFactory Logger = new LoggerFactory(new[] { new DebugLoggerProvider()});

//        public DataContext() { }
//        public DataContext(DbContextOptions options) : base(options)
//        {

//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);
//            modelBuilder.Entity<PictureEntity>().ToTable("Picture");
//            modelBuilder.Entity<PictureEditor>().ToTable("Editor");
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            string connetorStr = configuration.GetConnectionString("Default");
//            optionsBuilder.UseLoggerFactory(Logger);
//            optionsBuilder.UseMySql(connetorStr, ServerVersion.AutoDetect(connetorStr),null);
//        }
//    }

//}
