USE [master]
GO
/****** Object:  Database [VastVoyages]    Script Date: 2021-04-30 9:06:34 PM ******/
DROP DATABASE IF EXISTS [VastVoyages] 
CREATE DATABASE [VastVoyages]
ALTER DATABASE [VastVoyages] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VastVoyages].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VastVoyages] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VastVoyages] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VastVoyages] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VastVoyages] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VastVoyages] SET ARITHABORT OFF 
GO
ALTER DATABASE [VastVoyages] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VastVoyages] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VastVoyages] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VastVoyages] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VastVoyages] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VastVoyages] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VastVoyages] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VastVoyages] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VastVoyages] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VastVoyages] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VastVoyages] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VastVoyages] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VastVoyages] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VastVoyages] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VastVoyages] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VastVoyages] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VastVoyages] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VastVoyages] SET RECOVERY FULL 
GO
ALTER DATABASE [VastVoyages] SET  MULTI_USER 
GO
ALTER DATABASE [VastVoyages] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VastVoyages] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VastVoyages] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VastVoyages] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VastVoyages] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'VastVoyages', N'ON'
GO
ALTER DATABASE [VastVoyages] SET QUERY_STORE = OFF
GO
USE [VastVoyages]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [varchar](255) NOT NULL,
	[DepartmentDescription] [varchar](255) NOT NULL,
	[InvocationDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeReviews]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeReviews](
	[EmployeeReviewId] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Comment] [varchar](255) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[RatingId] [int] NOT NULL,
 CONSTRAINT [PK_EmployeeReviews] PRIMARY KEY CLUSTERED 
(
	[EmployeeReviewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] IDENTITY(10000000,1) NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[FirstName] [varchar](40) NOT NULL,
	[MiddleInit] [varchar](40) NULL,
	[LastName] [varchar](40) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[Street] [varchar](50) NOT NULL,
	[City] [varchar](20) NOT NULL,
	[Province] [varchar](2) NOT NULL,
	[Country] [varchar](100) NOT NULL,
	[PostalCode] [varchar](10) NOT NULL,
	[WorkPhone] [varchar](13) NOT NULL,
	[CellPhone] [varchar](13) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[JobStartDate] [datetime2](7) NOT NULL,
	[SeniorityDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NULL,
	[SIN] [varchar](11) NOT NULL,
	[SupervisorId] [int] NULL,
	[IsHeadSupervisor] [bit] NULL,
	[DepartmentId] [int] NOT NULL,
	[EmployeeStatusId] [int] NOT NULL,
	[JobAssignmentId] [int] NOT NULL,
	[RecordVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeStatus]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeStatus](
	[EmployeeStatusId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeStatus] [varchar](50) NOT NULL,
 CONSTRAINT [PK_EmployeeStatus] PRIMARY KEY CLUSTERED 
(
	[EmployeeStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [varchar](50) NOT NULL,
	[ItemDescription] [varchar](100) NOT NULL,
	[Justification] [varchar](80) NOT NULL,
	[Location] [varchar](50) NOT NULL,
	[Price] [decimal](19, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[DescisionReason] [varchar](255) NULL,
	[PONumber] [int] NOT NULL,
	[ItemStatusId] [int] NOT NULL,
	[RecordVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemStatus]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemStatus](
	[ItemStatusId] [int] IDENTITY(1,1) NOT NULL,
	[ItemStatus] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ItemStatus] PRIMARY KEY CLUSTERED 
(
	[ItemStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobAssignment]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobAssignment](
	[JobAssignmentId] [int] IDENTITY(1,1) NOT NULL,
	[JobAssignment] [varchar](100) NOT NULL,
 CONSTRAINT [PK_JobAssignment] PRIMARY KEY CLUSTERED 
(
	[JobAssignmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[EmployeeId] [int] NOT NULL,
	[Password] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POs]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POs](
	[PONumber] [int] IDENTITY(101,1) NOT NULL,
	[SubmissionDate] [datetime2](7) NULL,
	[SubTotal] [decimal](19, 2) NOT NULL,
	[Tax] [decimal](19, 2) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[POStatusId] [int] NOT NULL,
	[RecordVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_PORs] PRIMARY KEY CLUSTERED 
(
	[PONumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POStatus]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POStatus](
	[POStatusId] [int] IDENTITY(1,1) NOT NULL,
	[POStatus] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PORStatus] PRIMARY KEY CLUSTERED 
(
	[POStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ratings]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ratings](
	[RatingId] [int] IDENTITY(1,1) NOT NULL,
	[Rating] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Ratings] PRIMARY KEY CLUSTERED 
(
	[RatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Departments] ON 
GO
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName], [DepartmentDescription], [InvocationDate]) VALUES (1, N'CEO', N'CEO', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName], [DepartmentDescription], [InvocationDate]) VALUES (2, N'HR', N'Human Resources', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName], [DepartmentDescription], [InvocationDate]) VALUES (3, N'DEV', N'Development', CAST(N'2001-05-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName], [DepartmentDescription], [InvocationDate]) VALUES (4, N'SALES', N'Sales', CAST(N'2001-03-01T00:00:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Departments] OFF
GO
INSERT [dbo].[Employees] ( [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES ( N'ReynoldsA', N'Anna', N'C', N'Reynolds', CAST(N'1980-03-11T00:00:00.0000000' AS DateTime2), N'398 Ryan Street', N'Moncton', N'NB', N'Canada', N'E1E 4N8', N'506-750-5182', N'506-879-8595', N'Anna@VastVoyages.ca', CAST(N'1999-12-01T00:00:00.0000000' AS DateTime2), CAST(N'1999-12-01T00:00:00.0000000' AS DateTime2), NULL, N'325-745-124', NULL, NULL, 1, 1, 1)
GO
INSERT [dbo].[Employees] ( [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES ( N'ThomasC', N'Charles', N'R', N'Thomas', CAST(N'1983-06-06T00:00:00.0000000' AS DateTime2), N'1234 Main Street', N'Moncton', N'NB', N'Canada', N'E1X 4W2', N'506-521-0685', N'506-124-5745', N'Charles@VastVoyages.ca', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), NULL, N'128-301-214', 10000000, 1, 2, 1, 2)
GO
INSERT [dbo].[Employees] ( [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES ( N'TorresJ', N'Jane', NULL, N'Torres', CAST(N'1992-09-17T00:00:00.0000000' AS DateTime2), N'87 King Street', N'Dieppe', N'NB', N'Canada', N'I9A 7B2', N'506-457-1248', N'506-784-4578', N'Jane@VastVoyages.ca', CAST(N'2019-05-01T00:00:00.0000000' AS DateTime2), CAST(N'2018-04-01T00:00:00.0000000' AS DateTime2), NULL, N'245-487-124', 10000000, 1, 4, 1, 3)
GO
INSERT [dbo].[Employees] ( [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES ( N'CasperM', N'Mandy', NULL, N'Casper', CAST(N'1985-04-25T00:00:00.0000000' AS DateTime2), N'1671 Victora Street', N'Moncton', N'NB', N'Canada', N'K3R 8S1', N'506-757-0454', N'506-354-9842', N'Mandy@VastVoyages.ca', CAST(N'2020-11-05T00:00:00.0000000' AS DateTime2), CAST(N'2018-01-25T00:00:00.0000000' AS DateTime2), NULL, N'578-548-488', 10000000, 1, 3, 1, 4)
GO
INSERT [dbo].[Employees] ( [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES ( N'RiveraA', N'Angela', N'J', N'Rivera', CAST(N'1990-02-18T00:00:00.0000000' AS DateTime2), N'41 Eglinton Ave', N'Moncton', N'NB', N'Canada', N'R2W 1V3', N'506-785-2188', N'506-956-1578', N'Anglea@VastVoyages.ca', CAST(N'2019-12-07T00:00:00.0000000' AS DateTime2), CAST(N'2019-12-07T00:00:00.0000000' AS DateTime2), NULL, N'457-123-789', 10000002, NULL, 4, 1, 5)
GO
INSERT [dbo].[Employees] ( [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES ( N'LandryL', N'Lois', NULL, N'Landry', CAST(N'1976-06-10T00:00:00.0000000' AS DateTime2), N'632 Bay Street', N'Dieppe', N'NB', N'Canada', N'L8C 2G1', N'506-354-8516', N'506-154-4875', N'Lois@VastVoyages.ca', CAST(N'2005-02-04T00:00:00.0000000' AS DateTime2), CAST(N'2005-02-04T00:00:00.0000000' AS DateTime2), NULL, N'687-781-715', 10000001, NULL, 2, 1, 6)
GO
INSERT [dbo].[Employees] ( [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES ( N'BetheaF', N'Frances', NULL, N'Bethea', CAST(N'1998-09-18T00:00:00.0000000' AS DateTime2), N'28 Main Street', N'Moncton', N'NB', N'Canada', N'O9D 3V1', N'506-651-7802', N'506-248-4812', N'Frances@VastVoyages.ca', CAST(N'2017-06-07T00:00:00.0000000' AS DateTime2), CAST(N'2015-05-17T00:00:00.0000000' AS DateTime2), CAST(N'2019-04-08T00:00:00.0000000' AS DateTime2), N'685-712-127', 10000003, NULL, 3, 2, 7)
GO
INSERT [dbo].[Employees] ( [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES ( N'DillionC', N'Clara ', N'D', N'Dillion', CAST(N'1960-03-09T00:00:00.0000000' AS DateTime2), N'51 Campsite Road', N'Moncton', N'NB', N'Canada', N'T7X 2Y7', N'506-858-7455', N'506-958-1248', N'Clara@VastVoyates.ca', CAST(N'2001-05-03T00:00:00.0000000' AS DateTime2), CAST(N'2000-08-07T00:00:00.0000000' AS DateTime2), CAST(N'2020-04-26T00:00:00.0000000' AS DateTime2), N'578-874-487', 10000001, NULL, 2, 3, 6)
GO
SET IDENTITY_INSERT [dbo].[EmployeeStatus] ON 
GO
INSERT [dbo].[EmployeeStatus] ([EmployeeStatusId], [EmployeeStatus]) VALUES (1, N'Active')
GO
INSERT [dbo].[EmployeeStatus] ([EmployeeStatusId], [EmployeeStatus]) VALUES (2, N'Terminated')
GO
INSERT [dbo].[EmployeeStatus] ([EmployeeStatusId], [EmployeeStatus]) VALUES (3, N'Retired')
GO
SET IDENTITY_INSERT [dbo].[EmployeeStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[Items] ON 
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (1, N'Red pen', N'Red color pen', N'Need more pens', N'Staples', CAST(3.99 AS Decimal(19, 2)), 4, NULL, 101, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (2, N'Blue pen', N'Blue color pen', N'Need more pens', N'Staples', CAST(3.59 AS Decimal(19, 2)), 11, NULL, 101, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (4, N'Keyboard', N'Keyboard for PC', N'Need new one', N'Canadian Tire', CAST(35.99 AS Decimal(19, 2)), 5, NULL, 103, 2)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (5, N'Mouse', N'Mouse for PC', N'Need new one', N'Staples', CAST(20.50 AS Decimal(19, 2)), 5, NULL, 103, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (6, N'Note', N'Basic note', N'Meeting', N'Costco', CAST(3.25 AS Decimal(19, 2)), 10, NULL, 104, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (7, N'Ink', N'Printer Ink(Color, Black)', N'Old one was broken', N'Best Buy', CAST(59.90 AS Decimal(19, 2)), 1, NULL, 105, 2)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (8, N'Printer', N'Printer', N'Need new one', N'Best Buy', CAST(50.80 AS Decimal(19, 2)), 1, N'Old one still works', 105, 3)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (9, N'Pencil', N'2HB Pencil', N'Office supplies', N'Staples', CAST(1.99 AS Decimal(19, 2)), 2, NULL, 106, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (10, N'Copy paper', N'multi purpose paper', N'Need office supplies', N'Staples', CAST(5.99 AS Decimal(19, 2)), 3, NULL, 106, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (13, N'Tape', N'Transparent tape', N'office supplies', N'Staples', CAST(1.99 AS Decimal(19, 2)), 2, NULL, 107, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (15, N'Black Pen', N'Black color pen', N'office work', N'Staples', CAST(1.99 AS Decimal(19, 2)), 5, NULL, 107, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (16, N'Red Pen', N'Red color pen', N'office work', N'Staples', CAST(1.50 AS Decimal(19, 2)), 3, NULL, 107, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (18, N'Snacks', N'Muffins, Cookies', N'For Employees', N'Superstore', CAST(2.50 AS Decimal(19, 2)), 10, NULL, 108, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (19, N'Water', N'Bottled water', N'Ran out', N'Superstore', CAST(2.99 AS Decimal(19, 2)), 11, NULL, 108, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (20, N'Ram 16G', N'Ram 16GB', N'PC Upgrade', N'Best Buy', CAST(59.99 AS Decimal(19, 2)), 1, NULL, 109, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (21, N'Green pen', N'Green color pen', N'Need more pens', N'Staples', CAST(2.50 AS Decimal(19, 2)), 5, NULL, 101, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (22, N'Binder pouch', N'BINDER POUCH-3-RING 100 SHEET CAP', N'Office supplies', N'Staples', CAST(19.00 AS Decimal(19, 2)), 5, NULL, 110, 1)
GO
SET IDENTITY_INSERT [dbo].[Items] OFF
GO
SET IDENTITY_INSERT [dbo].[ItemStatus] ON 
GO
INSERT [dbo].[ItemStatus] ([ItemStatusId], [ItemStatus]) VALUES (1, N'Pending')
GO
INSERT [dbo].[ItemStatus] ([ItemStatusId], [ItemStatus]) VALUES (2, N'Approved')
GO
INSERT [dbo].[ItemStatus] ([ItemStatusId], [ItemStatus]) VALUES (3, N'Denied')
GO
SET IDENTITY_INSERT [dbo].[ItemStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[JobAssignment] ON 
GO
INSERT [dbo].[JobAssignment] ([JobAssignmentId], [JobAssignment]) VALUES (1, N'CEO')
GO
INSERT [dbo].[JobAssignment] ([JobAssignmentId], [JobAssignment]) VALUES (2, N'HR Manager')
GO
INSERT [dbo].[JobAssignment] ([JobAssignmentId], [JobAssignment]) VALUES (3, N'Sales Manager')
GO
INSERT [dbo].[JobAssignment] ([JobAssignmentId], [JobAssignment]) VALUES (4, N'Dev Manager')
GO
INSERT [dbo].[JobAssignment] ([JobAssignmentId], [JobAssignment]) VALUES (5, N'Marketing')
GO
INSERT [dbo].[JobAssignment] ([JobAssignmentId], [JobAssignment]) VALUES (6, N'HR')
GO
INSERT [dbo].[JobAssignment] ([JobAssignmentId], [JobAssignment]) VALUES (7, N'Software Developer')
GO
SET IDENTITY_INSERT [dbo].[JobAssignment] OFF
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000001, N'Test!234')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000002, N'Test!234')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000003, N'Test!234')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000004, N'Test!234')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000005, N'Test!234')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000006, N'Test!234')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000007, N'Test!234')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000000, N'Test!234')
GO
SET IDENTITY_INSERT [dbo].[POs] ON 
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (101, CAST(N'2021-04-26T13:00:00.0000000' AS DateTime2), CAST(67.95 AS Decimal(19, 2)), CAST(6.28 AS Decimal(19, 2)), 10000005, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (103, CAST(N'2021-04-27T10:30:00.0000000' AS DateTime2), CAST(282.45 AS Decimal(19, 2)), CAST(42.37 AS Decimal(19, 2)), 10000005, 2)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (104, CAST(N'2021-04-27T15:25:00.0000000' AS DateTime2), CAST(32.50 AS Decimal(19, 2)), CAST(4.88 AS Decimal(19, 2)), 10000001, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (105, CAST(N'2021-04-27T16:45:00.0000000' AS DateTime2), CAST(110.70 AS Decimal(19, 2)), CAST(16.60 AS Decimal(19, 2)), 10000005, 3)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (106, CAST(N'2021-04-29T01:22:18.5000000' AS DateTime2), CAST(21.95 AS Decimal(19, 2)), CAST(3.29 AS Decimal(19, 2)), 10000005, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (107, CAST(N'2021-04-29T16:54:26.9933333' AS DateTime2), CAST(19.93 AS Decimal(19, 2)), CAST(1.60 AS Decimal(19, 2)), 10000005, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (108, CAST(N'2021-04-29T19:33:17.3766667' AS DateTime2), CAST(57.89 AS Decimal(19, 2)), CAST(8.68 AS Decimal(19, 2)), 10000005, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (109, NULL, CAST(59.99 AS Decimal(19, 2)), CAST(9.00 AS Decimal(19, 2)), 10000005, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (110, CAST(N'2021-04-30T21:05:14.4800000' AS DateTime2), CAST(95.00 AS Decimal(19, 2)), CAST(14.25 AS Decimal(19, 2)), 10000004, 1)
GO
SET IDENTITY_INSERT [dbo].[POs] OFF
GO
SET IDENTITY_INSERT [dbo].[POStatus] ON 
GO
INSERT [dbo].[POStatus] ([POStatusId], [POStatus]) VALUES (1, N'Pending')
GO
INSERT [dbo].[POStatus] ([POStatusId], [POStatus]) VALUES (2, N'Under Review')
GO
INSERT [dbo].[POStatus] ([POStatusId], [POStatus]) VALUES (3, N'Closed')
GO
INSERT [dbo].[POStatus] ([POStatusId], [POStatus]) VALUES (4, N'Temporary')
GO
SET IDENTITY_INSERT [dbo].[POStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[Ratings] ON 
GO
INSERT [dbo].[Ratings] ([RatingId], [Rating]) VALUES (1, N'Below Expectations')
GO
INSERT [dbo].[Ratings] ([RatingId], [Rating]) VALUES (2, N'Meets Expectations')
GO
INSERT [dbo].[Ratings] ([RatingId], [Rating]) VALUES (3, N'Exceeds Expectations')
GO
SET IDENTITY_INSERT [dbo].[Ratings] OFF
GO
ALTER TABLE [dbo].[EmployeeReviews]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeReviews_EmpId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[EmployeeReviews] CHECK CONSTRAINT [FK_EmployeeReviews_EmpId]
GO
ALTER TABLE [dbo].[EmployeeReviews]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeReviews_Rating] FOREIGN KEY([RatingId])
REFERENCES [dbo].[Ratings] ([RatingId])
GO
ALTER TABLE [dbo].[EmployeeReviews] CHECK CONSTRAINT [FK_EmployeeReviews_Rating]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([DepartmentId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Department]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_JobAssignment] FOREIGN KEY([JobAssignmentId])
REFERENCES [dbo].[JobAssignment] ([JobAssignmentId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_JobAssignment]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Status] FOREIGN KEY([EmployeeStatusId])
REFERENCES [dbo].[EmployeeStatus] ([EmployeeStatusId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Status]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Supervisor] FOREIGN KEY([SupervisorId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Supervisor]
GO
ALTER TABLE [dbo].[Login]  WITH CHECK ADD  CONSTRAINT [FK_Login_EmpId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Login] CHECK CONSTRAINT [FK_Login_EmpId]
GO
ALTER TABLE [dbo].[POs]  WITH CHECK ADD  CONSTRAINT [FK_POs_EmpId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[POs] CHECK CONSTRAINT [FK_POs_EmpId]
GO
ALTER TABLE [dbo].[POs]  WITH CHECK ADD  CONSTRAINT [FK_POs_Status] FOREIGN KEY([POStatusId])
REFERENCES [dbo].[POStatus] ([POStatusId])
GO
ALTER TABLE [dbo].[POs] CHECK CONSTRAINT [FK_POs_Status]
GO
/****** Object:  StoredProcedure [dbo].[spCheckDuplicateUsername]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spCheckDuplicateUsername]
	@UserName VARCHAR(100),
	@UNameCount INT OUTPUT
AS 

BEGIN
	BEGIN TRY

		SELECT * FROM Employees WHERE UserName LIKE @UserName + '%';
		
		SET @UNameCount = (SELECT COUNT(*) FROM Employees WHERE UserName LIKE (@UserName + '%'));

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spDeleteItemByItemId]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spDeleteItemByItemId]
	@ItemId INT
AS
BEGIN
	BEGIN TRY
		IF NOT EXISTS (SELECT ItemId FROM Items WHERE ItemId = @ItemId)
			THROW 51001, 'The item is not existing. Please try again.', 1
		ELSE 
			BEGIN
				DELETE FROM Items WHERE ItemId = @ItemId 		
			END
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spFindDuplicatedItems]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spFindDuplicatedItems]
	@ItemId INT = NULL,
	@PONumber INT,
	@ItemName VARCHAR(50),
	@ItemDescription VARCHAR(100),
	@Justification VARCHAR(80),
	@Location VARCHAR(50),
	@Price DECIMAL(19,2)
AS
BEGIN
	BEGIN TRY
		IF @ItemId IS NULL
			BEGIN
				SELECT * FROM Items 
				WHERE
					PONumber = @PONumber AND
					ItemName = @ItemName AND
					ItemDescription = @ItemDescription AND
					Justification = @Justification AND
					Location = @Location AND
					Price = @Price
			END
		ELSE 
			BEGIN
				SELECT * FROM Items 
				WHERE
					PONumber = @PONumber AND
					ItemName = @ItemName AND
					ItemDescription = @ItemDescription AND
					Justification = @Justification AND
					Location = @Location AND
					Price = @Price AND
					ItemId <> @ItemId
			END		
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetAllEmployees]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spGetAllEmployees]
AS 

BEGIN
	BEGIN TRY

	SELECT * FROM Employees;

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spGetCEO]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spGetCEO]
AS 

BEGIN
	BEGIN TRY

		SELECT * FROM Employees WHERE SupervisorId IS NULL; 
	
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spGetDepartments]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spGetDepartments]
AS 

BEGIN
	BEGIN TRY

		SELECT * FROM Departments WHERE DepartmentName != 'CEO';

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spGetEmployeeById]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     PRoC [dbo].[spGetEmployeeById]
	@EmployeeId VARCHAR(8)
AS
BEGIN
	BEGIN TRY
		IF NOT EXISTS (SELECT EmployeeId, Password FROM Login WHERE EmployeeId = @EmployeeId)
			THROW 51001, 'The employee is not existing. Please try again.', 1
		ELSE 
			BEGIN						
				SELECT Employees.EmployeeId, Employees.UserName, Employees.FirstName + ' ' +  COALESCE(Employees.MiddleInit + ' ', '') + Employees.LastName AS FullName,
					   JobAssignment.JobAssignment, Employees.DepartmentId, Departments.DepartmentName, Employees.SupervisorId, 
					   (SELECT FirstName + ' ' +  COALESCE(MiddleInit + ' ', '') + LastName FROM Employees WHERE EmployeeId = Supervisor.EmployeeId) AS Supervisor
				FROM Employees 
				LEFT JOIN Departments ON Departments.DepartmentId = Employees.DepartmentId
				LEFT JOIN Employees AS Supervisor ON Supervisor.EmployeeId = Employees.SupervisorId 
				LEFT JOIN JobAssignment ON JobAssignment.JobAssignmentId = Employees.JobAssignmentId
				WHERE Employees.EmployeeId = @EmployeeId
			END
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetItemByItemId]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spGetItemByItemId]
	@ItemId INT
AS
BEGIN
	BEGIN TRY
		IF NOT EXISTS (SELECT ItemId FROM Items WHERE ItemId = @ItemId)
			THROW 51001, 'The item is not existing. Please try again.', 1
		ELSE 
			BEGIN
				SELECT ItemId, ItemName, ItemDescription, Justification, Location, Price, Quantity, 
				(SELECT REPLICATE('0',8-LEN(PONumber)) + RTRIM(PONumber)) AS PONumber, ItemStatus.ItemStatus, DescisionReason, RecordVersion
				FROM Items 
				JOIN ItemStatus ON ItemStatus.ItemStatusId = Items.ItemStatusId
				WHERE ItemId = @ItemId				
			END
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetItemByPONumber]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spGetItemByPONumber]
	@PurchaseOrderNumber INT
AS
BEGIN
	BEGIN TRY
		IF NOT EXISTS (SELECT PONumber FROM POs WHERE PONumber = @PurchaseOrderNumber)
			THROW 51001, 'The purchase order is not existing. Please try again.', 1
		ELSE 
			BEGIN
				SELECT ItemId, ItemName, ItemDescription, Justification, Location, Price, Quantity, Items.ItemStatusId, 
				(SELECT REPLICATE('0',8-LEN(PONumber)) + RTRIM(PONumber)) AS PONumber, ItemStatus.ItemStatus, DescisionReason
				FROM Items 
				JOIN ItemStatus ON ItemStatus.ItemStatusId = Items.ItemStatusId
				WHERE PONumber = @PurchaseOrderNumber				
			END
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetItemStatusForLookup]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spGetItemStatusForLookup]
AS
BEGIN
	SELECT ItemStatusId, ItemStatus FROM ItemStatus
END
GO
/****** Object:  StoredProcedure [dbo].[spGetJobAssignments]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spGetJobAssignments]
AS 

BEGIN
	BEGIN TRY

		SELECT * FROM JobAssignment WHERE JobAssignment NOT LIKE 'CEO';

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spGetPOStatusForLookup]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spGetPOStatusForLookup]
AS
BEGIN
	SELECT POStatusId, POStatus FROM POStatus
END
GO
/****** Object:  StoredProcedure [dbo].[spGetPurchaseOrderByDepartmentId]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spGetPurchaseOrderByDepartmentId]
	@DepartmentId INT
AS
BEGIN
	BEGIN TRY
		IF @DepartmentId <> 1
			BEGIN
				SELECT (SELECT REPLICATE('0',8-LEN(PONumber)) + RTRIM(PONumber)) AS PONumber, SubmissionDate, SubTotal, Tax, (SubTotal+Tax) AS Total,
				(Employees.FirstName + ' ' +  COALESCE(Employees.MiddleInit + ' ', '') + Employees.LastName) as Employee,
				(SELECT FirstName + ' ' +  COALESCE(MiddleInit + ' ', '') + LastName FROM Employees WHERE EmployeeId = Supervisor.EmployeeId) AS Supervisor,
				POStatus.POStatus AS POStatus
				FROM POs 
				JOIN Employees ON Employees.EmployeeId = POs.EmployeeId 
				JOIN Employees AS Supervisor ON Supervisor.EmployeeId = Employees.SupervisorId 
				JOIN POStatus ON POStatus.POStatusId = POs.POStatusId
				WHERE (POs.EmployeeId IN (SELECT EmployeeId FROM Employees WHERE DepartmentId = @DepartmentId)) AND POs.POStatusId <> 4 AND Supervisor.EmployeeId <> '10000000'
				ORDER BY SubmissionDate DESC
			END
		ELSE 
			BEGIN
				SELECT (SELECT REPLICATE('0',8-LEN(PONumber)) + RTRIM(PONumber)) AS PONumber, SubmissionDate, SubTotal, Tax, (SubTotal+Tax) AS Total,
				(Employees.FirstName + ' ' +  COALESCE(Employees.MiddleInit + ' ', '') + Employees.LastName) as Employee,
				(SELECT FirstName + ' ' +  COALESCE(MiddleInit + ' ', '') + LastName FROM Employees WHERE EmployeeId = Supervisor.EmployeeId) AS Supervisor,
				POStatus.POStatus AS POStatus
				FROM POs 
				JOIN Employees ON Employees.EmployeeId = POs.EmployeeId 
				JOIN Employees AS Supervisor ON Supervisor.EmployeeId = Employees.SupervisorId 
				JOIN POStatus ON POStatus.POStatusId = POs.POStatusId
				WHERE POs.POStatusId <> 4 AND Supervisor.EmployeeId = '10000000'
				ORDER BY SubmissionDate DESC
			END
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetPurchaseOrderByEmployee]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spGetPurchaseOrderByEmployee]
	@EmployeeId INT
AS
BEGIN
	BEGIN TRY
		IF NOT EXISTS (SELECT EmployeeId FROM Employees WHERE EmployeeId = @EmployeeId)
			THROW 51001, 'The employee is not existing. Please try again.', 1
		ELSE 
			BEGIN	
				SELECT (SELECT REPLICATE('0',8-LEN(PONumber)) + RTRIM(PONumber)) AS PONumber, SubmissionDate, SubTotal, Tax, (SubTotal+Tax) AS Total,
				(Employees.FirstName + ' ' +  COALESCE(Employees.MiddleInit + ' ', '') + Employees.LastName) AS Employee,
				(SELECT FirstName + ' ' +  COALESCE(MiddleInit + ' ', '') + LastName FROM Employees WHERE EmployeeId = Supervisor.EmployeeId) AS Supervisor,
				POStatus.POStatus AS POStatus
				FROM POs 
				LEFT JOIN Employees ON Employees.EmployeeId = POs.EmployeeId 
				LEFT JOIN Employees AS Supervisor ON Supervisor.EmployeeId = Employees.SupervisorId 
				LEFT JOIN POStatus ON POStatus.POStatusId = POs.POStatusId
				WHERE POs.EmployeeId = @EmployeeId AND POs.POStatusId <> 2
				ORDER BY SubmissionDate DESC
			END
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetPurchaseOrderByPOnumber]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spGetPurchaseOrderByPOnumber]
	@PurchaseOrderNumber INT,
	@EmployeeID INT = NULL
AS
BEGIN
	BEGIN TRY
		IF(@EmployeeID IS NULL) 
			BEGIN
				IF NOT EXISTS (SELECT PONumber FROM POs WHERE PONumber = @PurchaseOrderNumber)
					THROW 51001, 'The purchase order is not existing. Please try again.', 1
				ELSE 
					BEGIN	
						SELECT (SELECT REPLICATE('0',8-LEN(PONumber)) + RTRIM(PONumber)) AS PONumber, SubmissionDate, SubTotal, Tax, (SubTotal+Tax) AS Total,
						(Employees.FirstName + ' ' +  COALESCE(Employees.MiddleInit + ' ', '') + Employees.LastName) AS Employee,
						(SELECT FirstName + ' ' +  COALESCE(MiddleInit + ' ', '') + LastName FROM Employees WHERE EmployeeId = Supervisor.EmployeeId) AS Supervisor,
						POStatus.POStatus AS POStatus, POs.RecordVersion
						FROM POs 
						LEFT JOIN Employees ON Employees.EmployeeId = POs.EmployeeId 
						LEFT JOIN Employees AS Supervisor ON Supervisor.EmployeeId = Employees.SupervisorId 
						LEFT JOIN POStatus ON POStatus.POStatusId = POs.POStatusId
						WHERE POs.PONumber = @PurchaseOrderNumber
					END
			END
		ELSE
			BEGIN
				IF NOT EXISTS (SELECT PONumber FROM POs WHERE PONumber = @PurchaseOrderNumber AND EmployeeId = @EmployeeID)
					THROW 51001, 'The purchase order is not existing. Please try again.', 1
				ELSE 
					BEGIN	
						SELECT (SELECT REPLICATE('0',8-LEN(PONumber)) + RTRIM(PONumber)) AS PONumber, SubmissionDate, SubTotal, Tax, (SubTotal+Tax) AS Total,
						(Employees.FirstName + ' ' +  COALESCE(Employees.MiddleInit + ' ', '') + Employees.LastName) AS Employee,
						(SELECT FirstName + ' ' +  COALESCE(MiddleInit + ' ', '') + LastName FROM Employees WHERE EmployeeId = Supervisor.EmployeeId) AS Supervisor,
						POStatus.POStatus AS POStatus, POs.RecordVersion
						FROM POs 
						LEFT JOIN Employees ON Employees.EmployeeId = POs.EmployeeId 
						LEFT JOIN Employees AS Supervisor ON Supervisor.EmployeeId = Employees.SupervisorId 
						LEFT JOIN POStatus ON POStatus.POStatusId = POs.POStatusId
						WHERE POs.PONumber = @PurchaseOrderNumber AND POs.EmployeeId = @EmployeeID
					END
			END
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetSuperEmployeeCount]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spGetSuperEmployeeCount]
	@EmployeeCount INT OUTPUT,
	--@SupervisorCount INT OUTPUT,
	@DepartmentId INT,
	@SupervisorId INT
AS 

BEGIN
	BEGIN TRY

		DECLARE @CEOId INT = (SELECT EmployeeId FROM Employees WHERE SupervisorId IS NULL);

		SELECT * FROM Employees;
		
		SET @EmployeeCount = (SELECT COUNT(*) FROM Employees WHERE (SupervisorId LIKE @SupervisorId AND DepartmentId LIKE @DepartmentId));

		--SET @SupervisorCount = (SELECT COUNT(*) FROM Employees WHERE (SupervisorId LIKE @CEOId AND DepartmentId LIKE @DepartmentId));

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spGetSupervisors]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spGetSupervisors] 
	@DepartmentId INT
AS 

BEGIN
	BEGIN TRY

		DECLARE @CEOId INT = (SELECT EmployeeId FROM Employees WHERE SupervisorId IS NULL);

		--SELECT * FROM Employees WHERE (SupervisorId LIKE 10000000 AND DepartmentId LIKE @DepartmentId) OR SupervisorId IS NULL; 
		SELECT * FROM Employees WHERE (SupervisorId LIKE @CEOId AND DepartmentId LIKE @DepartmentId); 

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spInsertDepartment]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spInsertDepartment]
	@DepartmentId INT OUTPUT,
	@DepartmentName VARCHAR(255),
	@DepartmentDescription VARCHAR(255),
	@InvocationDate DATETIME
AS 

BEGIN
	BEGIN TRY

		INSERT INTO Departments 
			(DepartmentName, DepartmentDescription, InvocationDate)
		VALUES
			(@DepartmentName, @DepartmentDescription, @InvocationDate);

		SET @DepartmentId = SCOPE_IDENTITY();

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spInsertEmployee]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spInsertEmployee]
	@RecordVersion ROWVERSION OUTPUT,
	@EmployeeId INT OUTPUT,
	@UserName VARCHAR(100), 
	@FirstName VARCHAR(40), 
	@MiddleInit VARCHAR(40), 
	@LastName VARCHAR(40), 

	@DateOfBirth DATETIME, 
	@Street VARCHAR(50), 
	@City VARCHAR(20), 
	@Province VARCHAR(2), 
	@Country VARCHAR(100), 

	@PostalCode VARCHAR(10), 
	@WorkPhone VARCHAR(13), 
	@CellPhone VARCHAR(13), 
	@Email VARCHAR(50), 
	@JobStartDate DATETIME, 

	@SeniorityDate DATETIME,
	@SIN VARCHAR(11), 
	@SupervisorId INT, 
	@DepartmentId INT, 
	@EmployeeStatusId INT, 
	@JobAssignmentId INT
AS 

BEGIN
	BEGIN TRY			
			
		INSERT INTO Employees 
			(
				UserName, FirstName, MiddleInit, LastName, DateOfBirth, 
				Street, City, Province, Country, PostalCode, WorkPhone, CellPhone, Email, JobStartDate, SeniorityDate, SIN, 
				SupervisorId, DepartmentId, EmployeeStatusId, JobAssignmentId
			)
		VALUES
			(
				@UserName, @FirstName, @MiddleInit, @LastName, @DateOfBirth, 
				@Street, @City, @Province, @Country, @PostalCode, @WorkPhone, @CellPhone, @Email, @JobStartDate, @SeniorityDate, @SIN, 
				@SupervisorId, @DepartmentId, 1, @JobAssignmentId
			);

		SET @RecordVersion = (SELECT RecordVersion from Employees where EmployeeId = @EmployeeId);
		
		SET @EmployeeId = SCOPE_IDENTITY();

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spInsertItems]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spInsertItems]
	@ItemId INT OUTPUT,
	@RecordVersion ROWVERSION,
	@PONumber INT,
	@ItemName VARCHAR(50),
	@ItemDescription VARCHAR(100),
	@Justification VARCHAR(80),
	@Location VARCHAR(50),
	@Price DECIMAL(19,2),
	@Quantity INT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN		
			--Concurrency. Compare record version--
			IF(SELECT [RecordVersion] FROM POs WHERE PONumber = @PONumber) <> @RecordVersion
				THROW 53001, 'The purchase order record has been updated since last time you retrieved it', 1

			INSERT INTO Items
			(ItemName, ItemDescription, Justification, Location, Price, Quantity, DescisionReason, PONumber, ItemStatusId)
			VALUES(@ItemName, @ItemDescription, @Justification, @Location, @Price, @Quantity, null, @PONumber, 1)
						
			SET @ItemId = IDENT_CURRENT('Items');

			UPDATE POs
				SET SubTotal = (SELECT SUM(Price * Quantity) FROM Items WHERE PONumber = @PONumber)
				WHERE PONumber = @PONumber;

		COMMIT TRAN;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN;
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertPassword]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spInsertPassword]
	@EmployeeId INT,
	@Password VARCHAR(100)
AS 

BEGIN
	BEGIN TRY

	--DECLARE @randIndex INT = floor(RAND()*(9-1)+1);

	--	SET @Password = CONCAT (
	--	SUBSTRING('!@#$%^&*', @randIndex, 1),
	--	SUBSTRING(CONVERT(varchar(255), NEWID()),1, 8)		
	--	 );

		INSERT INTO Login
			(EmployeeId, Password)
		VALUES
			(@EmployeeId, @Password);

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spInsertPurchaseOrder]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spInsertPurchaseOrder]
	@RecordVersion ROWVERSION OUTPUT,
	@PONumberOutputParm VARCHAR(8) OUTPUT,
	@EmployeeId VARCHAR(8),
	@ItemName VARCHAR(50),
	@ItemDescription VARCHAR(100),
	@Justification VARCHAR(80),
	@Location VARCHAR(50),
	@Price DECIMAL(19,2),
	@Quantity INT,
	@SubTotal DECIMAL(19,2),
	@Tax DECIMAL(19,2)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			DECLARE @PONumber INT

			INSERT INTO POs
			(SubmissionDate, SubTotal, Tax, EmployeeId, POStatusId)
			VALUES(null, @SubTotal, @Tax, @EmployeeId, 1)
			
			SET @PONumber = IDENT_CURRENT('POs');

			INSERT INTO Items
			(ItemName, ItemDescription, Justification, Location, Price, Quantity, DescisionReason, PONumber, ItemStatusId)
			VALUES(@ItemName, @ItemDescription, @Justification, @Location, @Price, @Quantity, null, @PONumber, 1)
								
			SET @RecordVersion = (SELECT RecordVersion FROM POs WHERE PONumber = @PONumber)
			SET @PONumberOutputParm = (SELECT REPLICATE('0',8-LEN(@PONumber)) + RTRIM(@PONumber))
		COMMIT TRAN;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN;
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spLogin]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE    PRoC [dbo].[spLogin]
	@EmployeeId INT,
	@Password NVARCHAR(100)
AS
BEGIN
	BEGIN TRY
		IF NOT EXISTS (SELECT EmployeeId, Password FROM Login WHERE EmployeeId = @EmployeeId AND Password = @Password)
			THROW 50001, 'Incorrect employee id and password combination. Please try again.', 1
		ELSE 
			BEGIN
				IF (SELECT EmployeeStatusId FROM Employees WHERE EmployeeId = @EmployeeId) <> 1
					THROW 50002, 'Incorrect employee id and password combination, Please try again.', 1
				SELECT Employees.EmployeeId, Employees.UserName, Employees.FirstName, Employees.MiddleInit, Employees.LastName, 
					   Employees.DepartmentId, Departments.DepartmentName, Supervisor.SupervisorId, 
					   (SELECT FirstName + ' ' +  COALESCE(MiddleInit + ' ', '') + LastName FROM Employees WHERE EmployeeId = Supervisor.EmployeeId) AS Supervisor  
				FROM Employees 
				LEFT JOIN Departments ON Departments.DepartmentId = Employees.DepartmentId
				LEFT JOIN Employees AS Supervisor ON Supervisor.EmployeeId = Employees.SupervisorId 
				WHERE Employees.EmployeeId = @EmployeeId
			END
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spSearchEmployeesById]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--CREATE OR ALTER PROCEDURE spSearchEmployees
--	@EmployeeId INT,
--	@LastName VARCHAR(40)
--AS 

--BEGIN
--	BEGIN TRY
	
--		IF (@EmployeeId IS NOT NULL)
--			SELECT * FROM Employees WHERE EmployeeId = @EmployeeId ORDER BY LastName;
--		ELSE IF (@LastName IS NOT NULL)
--			SELECT * FROM Employees WHERE LastName = @LastName ORDER BY LastName;
--		ELSE
--			SELECT * FROM Employees ORDER BY LastName;
	
--	END TRY
--	BEGIN CATCH
--		;THROW
--	END CATCH
--END

--GO

CREATE   PROCEDURE [dbo].[spSearchEmployeesById]
	@EmployeeId INT
AS 

BEGIN
	BEGIN TRY
			SELECT * FROM Employees WHERE EmployeeId = @EmployeeId ORDER BY LastName;	
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spSearchEmployeesByLastName]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spSearchEmployeesByLastName]
	@LastName VARCHAR(40)
AS 

BEGIN
	BEGIN TRY
			SELECT * FROM Employees WHERE LastName LIKE @LastName + '%' ORDER BY LastName;
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spSubmitPurchaseOrder]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spSubmitPurchaseOrder]
	@RecordVersion ROWVERSION OUTPUT,
	@PONumber INT,
	@SubTotal DECIMAL(19,2),
	@Tax DECIMAL(19,2)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN		
			--Concurrency. Compare record version--
			IF(SELECT [RecordVersion] FROM POs WHERE PONumber = @PONumber) <> @RecordVersion
				THROW 53001, 'The purchase order record has been updated since last time you retrieved it', 1

			UPDATE POs
			   SET SubmissionDate = GETDATE()
				  ,SubTotal = @SubTotal
				  ,Tax = @Tax
			 WHERE PONumber = @PONumber
								  
			 SET @RecordVersion = (SELECT RecordVersion FROM POs WHERE PONumber = @PONumber)
		COMMIT TRAN;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN;
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateItems]    Script Date: 2021-04-30 9:06:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spUpdateItems]
	@RecordVersion ROWVERSION OUTPUT,
	@PORecordVersion ROWVERSION OUTPUT,
	@ItemId INT,
	@ItemName VARCHAR(50),
	@ItemDescription VARCHAR(100),
	@Justification VARCHAR(80),
	@Location VARCHAR(50),
	@Price DECIMAL(19,2),
	@Quantity INT,
	@DescisionReason VARCHAR(255),
	@PONumber INT,
	@ItemStatusId INT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN		
			--Concurrency. Compare record version--
			IF(SELECT [RecordVersion] FROM Items WHERE ItemId = @ItemId) <> @RecordVersion
				THROW 53001, 'The item record has been updated since last time you retrieved it', 1

			UPDATE Items
				SET ItemName = @ItemName
				   ,ItemDescription = @ItemDescription
				   ,Justification = @Justification
				   ,Location = @Location
				   ,Price = @Price
				   ,Quantity = @Quantity
				   ,DescisionReason = CASE WHEN @DescisionReason = '' THEN (SELECT DescisionReason FROM Items WHERE ItemId = @ItemId) ELSE @DescisionReason END
				   ,ItemStatusId = @ItemStatusId
				WHERE ItemId = @ItemId

			UPDATE POs
				SET SubTotal = (SELECT SUM(Price * Quantity) FROM Items WHERE PONumber = @PONumber)
				WHERE PONumber = @PONumber;
								 
			SET @RecordVersion = (SELECT RecordVersion FROM Items WHERE ItemId = @ItemId)
			SET @PORecordVersion = (SELECT RecordVersion FROM POs WHERE PONumber = @PONumber)
		COMMIT TRAN;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN;
		;THROW
	END CATCH
END
GO
USE [master]
GO
ALTER DATABASE [VastVoyages] SET  READ_WRITE 
GO
