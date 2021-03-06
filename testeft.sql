USE [master]
GO
/****** Object:  Database [testeft]    Script Date: 20/08/2018 12:13:51 ******/
CREATE DATABASE [testeft]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'testeft', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\testeft.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'testeft_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\testeft_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [testeft] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [testeft].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [testeft] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [testeft] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [testeft] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [testeft] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [testeft] SET ARITHABORT OFF 
GO
ALTER DATABASE [testeft] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [testeft] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [testeft] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [testeft] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [testeft] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [testeft] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [testeft] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [testeft] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [testeft] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [testeft] SET  DISABLE_BROKER 
GO
ALTER DATABASE [testeft] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [testeft] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [testeft] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [testeft] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [testeft] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [testeft] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [testeft] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [testeft] SET RECOVERY FULL 
GO
ALTER DATABASE [testeft] SET  MULTI_USER 
GO
ALTER DATABASE [testeft] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [testeft] SET DB_CHAINING OFF 
GO
ALTER DATABASE [testeft] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [testeft] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [testeft] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'testeft', N'ON'
GO
ALTER DATABASE [testeft] SET QUERY_STORE = OFF
GO
USE [testeft]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [testeft]
GO
/****** Object:  Table [dbo].[configurations]    Script Date: 20/08/2018 12:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[configurations](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[interval] [int] NOT NULL,
 CONSTRAINT [PK_configurations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[occurrences]    Script Date: 20/08/2018 12:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[occurrences](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[station_id] [int] NOT NULL,
	[occurred_when] [datetime] NOT NULL,
 CONSTRAINT [PK_ocurrences] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[processes]    Script Date: 20/08/2018 12:13:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[processes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[occurrences_id] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_processos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[stations]    Script Date: 20/08/2018 12:13:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stations](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[is_active] [bit] NOT NULL,
	[cnpj] [varchar](50) NOT NULL,
	[company_name] [varchar](200) NOT NULL,
 CONSTRAINT [PK_maquinas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[configurations] ADD  CONSTRAINT [DF_configurations_interval]  DEFAULT ((30)) FOR [interval]
GO
ALTER TABLE [dbo].[occurrences]  WITH CHECK ADD  CONSTRAINT [FK_occurrences_stations] FOREIGN KEY([station_id])
REFERENCES [dbo].[stations] ([id])
GO
ALTER TABLE [dbo].[occurrences] CHECK CONSTRAINT [FK_occurrences_stations]
GO
ALTER TABLE [dbo].[processes]  WITH CHECK ADD  CONSTRAINT [FK_processes_occurrences] FOREIGN KEY([occurrences_id])
REFERENCES [dbo].[occurrences] ([id])
GO
ALTER TABLE [dbo].[processes] CHECK CONSTRAINT [FK_processes_occurrences]
GO
USE [master]
GO
ALTER DATABASE [testeft] SET  READ_WRITE 
GO
