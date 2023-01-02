﻿

using Microsoft.EntityFrameworkCore;
using Notes.Application.interfaces;
using Notes.Domain;
using Notes.Persistence.EntityTypeConfiguration;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Persistence
{
    public class NotesDbContext : DbContext, INotesDbContext
    {
        public DbSet<Note> Notes { get; set; }

        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
