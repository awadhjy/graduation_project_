USE [master]
GO
/****** Object:  Database [clinic]    Script Date: 5/21/2022 11:31:59 PM ******/
CREATE DATABASE [clinic]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'clinic', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\clinic.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'clinic_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\clinic_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [clinic] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [clinic].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [clinic] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [clinic] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [clinic] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [clinic] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [clinic] SET ARITHABORT OFF 
GO
ALTER DATABASE [clinic] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [clinic] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [clinic] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [clinic] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [clinic] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [clinic] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [clinic] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [clinic] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [clinic] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [clinic] SET  DISABLE_BROKER 
GO
ALTER DATABASE [clinic] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [clinic] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [clinic] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [clinic] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [clinic] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [clinic] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [clinic] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [clinic] SET RECOVERY FULL 
GO
ALTER DATABASE [clinic] SET  MULTI_USER 
GO
ALTER DATABASE [clinic] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [clinic] SET DB_CHAINING OFF 
GO
ALTER DATABASE [clinic] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [clinic] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [clinic] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'clinic', N'ON'
GO
ALTER DATABASE [clinic] SET QUERY_STORE = OFF
GO
USE [clinic]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 5/21/2022 11:31:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[questionID] [int] NOT NULL,
	[answer] [nvarchar](max) NOT NULL,
	[answerBy] [int] NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_answers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 5/21/2022 11:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[clinicID] [int] NOT NULL,
	[doctorID] [int] NOT NULL,
	[personID] [int] NOT NULL,
	[active] [bit] NULL,
	[date] [datetime] NOT NULL,
	[note] [nvarchar](50) NULL,
 CONSTRAINT [PK_booking] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clinic]    Script Date: 5/21/2022 11:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clinic](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[address] [nvarchar](50) NOT NULL,
	[description] [nvarchar](50) NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_clinic] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 5/21/2022 11:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[personID] [int] NOT NULL,
	[Specialtie] [int] NOT NULL,
 CONSTRAINT [PK_Doctor_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 5/21/2022 11:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](80) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonRole]    Script Date: 5/21/2022 11:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonRole](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[personID] [int] NOT NULL,
	[roleID] [int] NOT NULL,
 CONSTRAINT [PK_PersonRole] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 5/21/2022 11:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[question] [nvarchar](max) NOT NULL,
	[date] [datetime] NOT NULL,
	[active] [bit] NULL,
 CONSTRAINT [PK_questions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 5/21/2022 11:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_doctor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialization]    Script Date: 5/21/2022 11:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialization](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](80) NOT NULL,
 CONSTRAINT [PK_Specialization] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Person] ON 

INSERT [dbo].[Person] ([ID], [name], [email], [password], [active]) VALUES (19, N'super', N'super@admin.com', N'1b3231655cebb7a1f783eddf27d254ca', 1)
SET IDENTITY_INSERT [dbo].[Person] OFF
SET IDENTITY_INSERT [dbo].[PersonRole] ON 

INSERT [dbo].[PersonRole] ([ID], [personID], [roleID]) VALUES (14, 19, 1)
SET IDENTITY_INSERT [dbo].[PersonRole] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([ID], [name]) VALUES (1, N'super')
INSERT [dbo].[Role] ([ID], [name]) VALUES (2, N'admin')
INSERT [dbo].[Role] ([ID], [name]) VALUES (3, N'doctor')
INSERT [dbo].[Role] ([ID], [name]) VALUES (4, N'reviewer')
SET IDENTITY_INSERT [dbo].[Role] OFF
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_Answer_Doctor] FOREIGN KEY([answerBy])
REFERENCES [dbo].[Doctor] ([ID])
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_Answer_Doctor]
GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_answers_questions] FOREIGN KEY([questionID])
REFERENCES [dbo].[Question] ([ID])
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_answers_questions]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_booking_clinic] FOREIGN KEY([clinicID])
REFERENCES [dbo].[Clinic] ([ID])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_booking_clinic]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Doctor] FOREIGN KEY([doctorID])
REFERENCES [dbo].[Doctor] ([ID])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Doctor]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Person2] FOREIGN KEY([personID])
REFERENCES [dbo].[Person] ([ID])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Person2]
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Person] FOREIGN KEY([personID])
REFERENCES [dbo].[Person] ([ID])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor_Person]
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Specialization] FOREIGN KEY([Specialtie])
REFERENCES [dbo].[Specialization] ([ID])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor_Specialization]
GO
ALTER TABLE [dbo].[PersonRole]  WITH CHECK ADD  CONSTRAINT [FK_PersonRole_Person] FOREIGN KEY([personID])
REFERENCES [dbo].[Person] ([ID])
GO
ALTER TABLE [dbo].[PersonRole] CHECK CONSTRAINT [FK_PersonRole_Person]
GO
ALTER TABLE [dbo].[PersonRole]  WITH CHECK ADD  CONSTRAINT [FK_PersonRole_Role] FOREIGN KEY([roleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[PersonRole] CHECK CONSTRAINT [FK_PersonRole_Role]
GO
USE [master]
GO
ALTER DATABASE [clinic] SET  READ_WRITE 
GO
