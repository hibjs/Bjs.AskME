﻿// <auto-generated />
using AskME.Core.Entities.Enum;
using AskME.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace AskME.Infrastructure.Migrations
{
    [DbContext(typeof(AskMEDbContext))]
    [Migration("20180419092242_20180419_V1")]
    partial class _20180419_V1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AskME.Core.Entities.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Adoption");

                    b.Property<string>("AnserContent");

                    b.Property<int>("Down");

                    b.Property<int>("QuestionId");

                    b.Property<DateTime>("SubTime");

                    b.Property<int>("Up");

                    b.Property<int>("UserId");

                    b.Property<int?>("UserInfoId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("AskME.Core.Entities.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("SubTime");

                    b.Property<string>("Title");

                    b.Property<int>("UserInfoId");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Bolg");
                });

            modelBuilder.Entity("AskME.Core.Entities.BlogTag", b =>
                {
                    b.Property<int>("TagId");

                    b.Property<int>("BlogId");

                    b.HasKey("TagId", "BlogId");

                    b.HasIndex("BlogId");

                    b.ToTable("BolgTag");
                });

            modelBuilder.Entity("AskME.Core.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("Down");

                    b.Property<DateTime>("SubTime");

                    b.Property<string>("Title")
                        .HasMaxLength(64);

                    b.Property<int>("Up");

                    b.Property<int>("UserInfoId");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("AskME.Core.Entities.QuestionTags", b =>
                {
                    b.Property<int>("TagId");

                    b.Property<int>("QuestionId");

                    b.HasKey("TagId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionTags");
                });

            modelBuilder.Entity("AskME.Core.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("Disabled");

                    b.Property<DateTime>("SubTime");

                    b.Property<string>("TagName")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("AskME.Core.Entities.TagFollower", b =>
                {
                    b.Property<int>("TagId");

                    b.Property<int>("UserInfoId");

                    b.HasKey("TagId", "UserInfoId");

                    b.HasIndex("UserInfoId");

                    b.ToTable("TagFollower");
                });

            modelBuilder.Entity("AskME.Core.Entities.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Bio");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Email")
                        .HasMaxLength(64);

                    b.Property<bool>("EmailValidated");

                    b.Property<string>("Icon")
                        .HasMaxLength(128);

                    b.Property<string>("Password")
                        .HasMaxLength(32);

                    b.Property<string>("QQ")
                        .HasMaxLength(32);

                    b.Property<string>("SelfInfo");

                    b.Property<string>("TelNumber")
                        .HasMaxLength(11);

                    b.Property<string>("UId")
                        .HasMaxLength(32);

                    b.Property<string>("UserName")
                        .HasMaxLength(32);

                    b.Property<string>("WeChat")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("AskME.Core.Entities.Answer", b =>
                {
                    b.HasOne("AskME.Core.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AskME.Core.Entities.UserInfo")
                        .WithMany("Answers")
                        .HasForeignKey("UserInfoId");
                });

            modelBuilder.Entity("AskME.Core.Entities.Blog", b =>
                {
                    b.HasOne("AskME.Core.Entities.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AskME.Core.Entities.BlogTag", b =>
                {
                    b.HasOne("AskME.Core.Entities.Blog", "Blog")
                        .WithMany("BlogTags")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AskME.Core.Entities.Tag", "Tag")
                        .WithMany("BlogTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AskME.Core.Entities.Question", b =>
                {
                    b.HasOne("AskME.Core.Entities.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AskME.Core.Entities.QuestionTags", b =>
                {
                    b.HasOne("AskME.Core.Entities.Question", "Question")
                        .WithMany("QuestionTags")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AskME.Core.Entities.Tag", "Tag")
                        .WithMany("QuestionTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AskME.Core.Entities.TagFollower", b =>
                {
                    b.HasOne("AskME.Core.Entities.Tag", "Tag")
                        .WithMany("TagFollowers")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AskME.Core.Entities.UserInfo", "UserInfo")
                        .WithMany("TagFollowers")
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
