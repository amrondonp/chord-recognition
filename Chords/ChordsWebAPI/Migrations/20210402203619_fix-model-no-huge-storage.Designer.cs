﻿// <auto-generated />
using System;
using ChordsWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ChordsWebAPI.Migrations
{
    [DbContext(typeof(PredictionContext))]
    [Migration("20210402203619_fix-model-no-huge-storage")]
    partial class fixmodelnohugestorage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ChordsWebAPI.Models.ChordWithKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("PredictionId")
                        .HasColumnType("integer");

                    b.Property<int>("SampleLength")
                        .HasColumnType("integer");

                    b.Property<int>("SampleRate")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PredictionId");

                    b.ToTable("ChordWithKey");
                });

            modelBuilder.Entity("ChordsWebAPI.Models.Prediction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("AutoBorder")
                        .HasColumnType("boolean");

                    b.Property<string>("FilePath")
                        .HasColumnType("text");

                    b.Property<string>("ModelName")
                        .HasColumnType("text");

                    b.Property<int>("Progress")
                        .HasColumnType("integer");

                    b.Property<int>("WindowInMs")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Predictions");
                });

            modelBuilder.Entity("ChordsWebAPI.Models.ChordWithKey", b =>
                {
                    b.HasOne("ChordsWebAPI.Models.Prediction", null)
                        .WithMany("Chords")
                        .HasForeignKey("PredictionId");
                });

            modelBuilder.Entity("ChordsWebAPI.Models.Prediction", b =>
                {
                    b.Navigation("Chords");
                });
#pragma warning restore 612, 618
        }
    }
}
