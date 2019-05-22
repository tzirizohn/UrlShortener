﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Url.Data;

namespace Url.Data.Migrations
{
    [DbContext(typeof(UrlContext))]
    [Migration("20190522041448_initialw")]
    partial class initialw
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Url.Data.URL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<string>("LongUrl");

                    b.Property<string>("ShortUrl");

                    b.Property<int>("userid");

                    b.Property<int?>("usersid");

                    b.HasKey("Id");

                    b.HasIndex("usersid");

                    b.ToTable("url");
                });

            modelBuilder.Entity("Url.Data.Users", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Url.Data.URL", b =>
                {
                    b.HasOne("Url.Data.Users", "users")
                        .WithMany("url")
                        .HasForeignKey("usersid");
                });
#pragma warning restore 612, 618
        }
    }
}
