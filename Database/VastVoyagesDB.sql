USE [master]
GO
/****** Object:  Database [VastVoyages]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  Table [dbo].[Departments]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [varchar](255) NOT NULL,
	[DepartmentDescription] [varchar](255) NOT NULL,
	[InvocationDate] [datetime2](7) NOT NULL,
	[RecordVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeReviews]    Script Date: 2021-05-14 11:52:14 AM ******/
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
	[ReviewerId] [int] NOT NULL,
 CONSTRAINT [PK_EmployeeReviews] PRIMARY KEY CLUSTERED 
(
	[EmployeeReviewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 2021-05-14 11:52:14 AM ******/
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
	[OfficeLocation] [varchar](255) NULL,
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
/****** Object:  Table [dbo].[EmployeeStatus]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  Table [dbo].[Items]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  Table [dbo].[ItemStatus]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  Table [dbo].[JobAssignment]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  Table [dbo].[Login]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[EmployeeId] [int] NOT NULL,
	[Password] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POs]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  Table [dbo].[POStatus]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  Table [dbo].[Ratings]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  Table [dbo].[ReviewReminderEmail]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReviewReminderEmail](
	[ReviewReminderEmailId] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ReviewReminderEmail] PRIMARY KEY CLUSTERED 
(
	[ReviewReminderEmailId] ASC
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
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName], [DepartmentDescription], [InvocationDate]) VALUES (5, N'Test', N'test', CAST(N'2021-05-15T00:00:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Departments] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeReviews] ON 
GO
INSERT [dbo].[EmployeeReviews] ([EmployeeReviewId], [Date], [Comment], [EmployeeId], [RatingId], [ReviewerId]) VALUES (1, CAST(N'2021-05-07T00:00:00.0000000' AS DateTime2), N'good', 10000005, 2, 10000001)
GO
INSERT [dbo].[EmployeeReviews] ([EmployeeReviewId], [Date], [Comment], [EmployeeId], [RatingId], [ReviewerId]) VALUES (2, CAST(N'2021-01-12T00:00:00.0000000' AS DateTime2), N'Excellent work this quarter.', 10000008, 1, 10000010)
GO
SET IDENTITY_INSERT [dbo].[EmployeeReviews] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 
GO
INSERT [dbo].[Employees] ([EmployeeId], [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [OfficeLocation], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES (10000000, N'ReynoldsA', N'Anna', N'C', N'Reynolds', CAST(N'1980-03-11T00:00:00.0000000' AS DateTime2), N'398 Ryan Street', N'Moncton', N'NB', N'Canada', N'E1E 4N8', N'506-750-5182', N'506-879-8595', N'Anna@VastVoyages.ca', N'123 Mountain Road, Moncton, NB', CAST(N'1999-12-01T00:00:00.0000000' AS DateTime2), CAST(N'1999-12-01T00:00:00.0000000' AS DateTime2), NULL, N'325-745-124', NULL, NULL, 1, 1, 1)
GO
INSERT [dbo].[Employees] ([EmployeeId], [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [OfficeLocation], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES (10000001, N'ThomasC', N'Charles', N'R', N'Thomas', CAST(N'1983-06-06T00:00:00.0000000' AS DateTime2), N'1234 Main Street', N'Moncton', N'NB', N'Canada', N'E1X 4W2', N'506-521-0685', N'506-124-5745', N'Charles@VastVoyages.ca', N'123 Mountain Road, Moncton, NB', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), NULL, N'128-301-214', 10000000, 1, 2, 1, 2)
GO
INSERT [dbo].[Employees] ([EmployeeId], [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [OfficeLocation], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES (10000002, N'TorresJ', N'Jane', NULL, N'Torres', CAST(N'1992-09-17T00:00:00.0000000' AS DateTime2), N'87 King Street', N'Dieppe', N'NB', N'Canada', N'I9A 7B2', N'506-457-1248', N'506-784-4578', N'Jane@VastVoyages.ca', N'123 Mountain Road, Moncton, NB', CAST(N'2019-05-01T00:00:00.0000000' AS DateTime2), CAST(N'2018-04-01T00:00:00.0000000' AS DateTime2), NULL, N'245-487-124', 10000000, 1, 4, 1, 3)
GO
INSERT [dbo].[Employees] ([EmployeeId], [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [OfficeLocation], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES (10000003, N'CasperM', N'Mandy', NULL, N'Casper', CAST(N'1985-04-25T00:00:00.0000000' AS DateTime2), N'1671 Victora Street', N'Moncton', N'NB', N'Canada', N'K3R 8S1', N'506-757-0454', N'506-354-9842', N'Mandy@VastVoyages.ca', N'123 Mountain Road, Moncton, NB', CAST(N'2020-11-05T00:00:00.0000000' AS DateTime2), CAST(N'2018-01-25T00:00:00.0000000' AS DateTime2), NULL, N'578-548-488', 10000000, 1, 3, 1, 4)
GO
INSERT [dbo].[Employees] ([EmployeeId], [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [OfficeLocation], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES (10000004, N'RiveraA', N'Angela', N'J', N'Rivera', CAST(N'1990-02-18T00:00:00.0000000' AS DateTime2), N'41 Eglinton Ave', N'Moncton', N'NB', N'Canada', N'R2W 1V3', N'506-785-2188', N'506-956-1578', N'Anglea@VastVoyages.ca', N'123 Mountain Road, Moncton, NB', CAST(N'2019-12-07T00:00:00.0000000' AS DateTime2), CAST(N'2019-12-07T00:00:00.0000000' AS DateTime2), NULL, N'457-123-789', 10000002, NULL, 4, 1, 5)
GO
INSERT [dbo].[Employees] ([EmployeeId], [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [OfficeLocation], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES (10000005, N'LandryL', N'Lois', NULL, N'Landry', CAST(N'1976-06-10T00:00:00.0000000' AS DateTime2), N'632 Bay Street', N'Dieppe', N'NB', N'Canada', N'L8C 2G1', N'506-354-8516', N'506-154-4875', N'Lois@VastVoyages.ca', N'123 Mountain Road, Moncton, NB', CAST(N'2005-02-04T00:00:00.0000000' AS DateTime2), CAST(N'2005-02-04T00:00:00.0000000' AS DateTime2), NULL, N'687-781-715', 10000001, NULL, 2, 1, 6)
GO
INSERT [dbo].[Employees] ([EmployeeId], [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [OfficeLocation], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES (10000006, N'BetheaF', N'Frances', NULL, N'Bethea', CAST(N'1998-09-18T00:00:00.0000000' AS DateTime2), N'28 Main Street', N'Moncton', N'NB', N'Canada', N'O9D 3V1', N'506-651-7802', N'506-248-4812', N'Frances@VastVoyages.ca', N'123 Mountain Road, Moncton, NB', CAST(N'2017-06-07T00:00:00.0000000' AS DateTime2), CAST(N'2015-05-17T00:00:00.0000000' AS DateTime2), CAST(N'2019-04-08T00:00:00.0000000' AS DateTime2), N'685-712-127', 10000003, NULL, 3, 2, 7)
GO
INSERT [dbo].[Employees] ([EmployeeId], [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [OfficeLocation], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES (10000007, N'DillionC', N'Clara ', N'D', N'Dillion', CAST(N'1960-03-09T00:00:00.0000000' AS DateTime2), N'51 Campsite Road', N'Moncton', N'NB', N'Canada', N'T7X 2Y7', N'506-858-7455', N'506-958-1248', N'Clara@VastVoyates.ca', N'123 Mountain Road, Moncton, NB', CAST(N'2001-05-03T00:00:00.0000000' AS DateTime2), CAST(N'2000-08-07T00:00:00.0000000' AS DateTime2), CAST(N'2020-04-26T00:00:00.0000000' AS DateTime2), N'578-874-487', 10000001, NULL, 2, 3, 6)
GO
INSERT [dbo].[Employees] ([EmployeeId], [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [OfficeLocation], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES (10000008, N'ShieldsD', N'Delphia', N'S', N'Shields', CAST(N'1993-02-24T00:00:00.0000000' AS DateTime2), N'203 Rose Street', N'Moncton', N'NB', N'Canada', N'D1A 3C2', N'506-871-8135', N'506-921-5752', N'Shields@VastVoyages.ca', N'123 Mountain Road, Moncton, NB', CAST(N'2009-05-02T00:00:00.0000000' AS DateTime2), CAST(N'2009-05-02T00:00:00.0000000' AS DateTime2), NULL, N'548-578-215', 10000010, 0, 2, 1, 6)
GO
INSERT [dbo].[Employees] ([EmployeeId], [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [OfficeLocation], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES (10000009, N'ParkerL', N'Loisue', NULL, N'Parker ', CAST(N'2000-06-07T00:00:00.0000000' AS DateTime2), N'60 Union Street', N'Moncton', N'NB', N'Canada', N'8X1 4E2', N'506-324-9645', N'506-124-5785', N'Parker@VastVoyeas.ca', N'123 Mountain Road, Moncton, NB', CAST(N'2020-06-09T00:00:00.0000000' AS DateTime2), CAST(N'2020-06-09T00:00:00.0000000' AS DateTime2), NULL, N'321-548-875', 10000001, NULL, 2, 1, 6)
GO
INSERT [dbo].[Employees] ([EmployeeId], [UserName], [FirstName], [MiddleInit], [LastName], [DateOfBirth], [Street], [City], [Province], [Country], [PostalCode], [WorkPhone], [CellPhone], [Email], [OfficeLocation], [JobStartDate], [SeniorityDate], [EndDate], [SIN], [SupervisorId], [IsHeadSupervisor], [DepartmentId], [EmployeeStatusId], [JobAssignmentId]) VALUES (10000010, N'TorresJ', N'Joseph ', N'M', N'Torres', CAST(N'1980-02-13T00:00:00.0000000' AS DateTime2), N'45 MacLaren Street', N'Moncton', N'NB', N'Canada', N'K1P 5M7', N'506-255-5648', N'506-127-5481', N'Torres@VastVoyages.ca', N'123 Mountain Road, Moncton, NB', CAST(N'2021-02-09T00:00:00.0000000' AS DateTime2), CAST(N'2021-02-09T00:00:00.0000000' AS DateTime2), NULL, N'657-518-548', 10000000, NULL, 2, 1, 2)
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
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
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (1, N'Red pen', N'Red color pen', N'Need more pens', N'Staples', CAST(3.99 AS Decimal(19, 2)), 2, NULL, 101, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (2, N'Blue pen', N'Blue color pen', N'Need more pens', N'Staples', CAST(3.59 AS Decimal(19, 2)), 11, NULL, 101, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (4, N'Keyboard', N'Keyboard for PC', N'Need new one', N'Canadian Tire', CAST(35.99 AS Decimal(19, 2)), 5, NULL, 103, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (5, N'Mouse', N'Mouse for PC', N'Need new one', N'Staples', CAST(20.50 AS Decimal(19, 2)), 5, NULL, 103, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (6, N'Note', N'Basic note', N'Meeting', N'Costco', CAST(3.25 AS Decimal(19, 2)), 9, NULL, 104, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (7, N'Ink', N'Printer Ink(Color, Black)', N'Old one was broken', N'Best Buy', CAST(59.90 AS Decimal(19, 2)), 1, NULL, 105, 2)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (8, N'Printer', N'Printer', N'Need new one', N'Best Buy', CAST(50.80 AS Decimal(19, 2)), 1, N'Old one still works', 105, 3)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (9, N'Pencil', N'2HB Pencil', N'Office supplies', N'Staples', CAST(1.99 AS Decimal(19, 2)), 3, NULL, 106, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (10, N'Copy paper', N'multi purpose paper', N'Need office supplies', N'Staples', CAST(5.99 AS Decimal(19, 2)), 3, NULL, 106, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (13, N'Tape', N'No longer needed', N'office supplies', N'Staples', CAST(1.99 AS Decimal(19, 2)), 0, NULL, 107, 3)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (15, N'Black Pen', N'Black color pen', N'office work', N'Staples', CAST(1.99 AS Decimal(19, 2)), 6, NULL, 107, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (16, N'Red Pen', N'Red color pen', N'office work', N'Staples', CAST(1.50 AS Decimal(19, 2)), 3, NULL, 107, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (18, N'Snacks', N'Muffins, Cookies', N'For Employees', N'Superstore', CAST(2.50 AS Decimal(19, 2)), 10, NULL, 108, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (19, N'Water', N'Bottled water', N'Ran out', N'Superstore', CAST(2.99 AS Decimal(19, 2)), 10, NULL, 108, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (20, N'Ram 16GB', N'Ram 16GB', N'PC Upgrade', N'Best Buy', CAST(59.99 AS Decimal(19, 2)), 1, NULL, 109, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (21, N'Green pen', N'Green color pen', N'Need more pens', N'Staples', CAST(2.50 AS Decimal(19, 2)), 5, NULL, 101, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (22, N'Binder pouch', N'BINDER POUCH-3-RING 100 SHEET CAP', N'Office supplies', N'Staples', CAST(19.00 AS Decimal(19, 2)), 5, NULL, 110, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (23, N'Note', N'Basic Note', N'Office', N'Staples', CAST(2.00 AS Decimal(19, 2)), 2, N'Need more', 111, 2)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (24, N'Black Pen', N'Black color pen', N'Office', N'Staples', CAST(1.00 AS Decimal(19, 2)), 2, NULL, 111, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (25, N'Headset', N'Headset', N'office', N'Best Buy', CAST(99.99 AS Decimal(19, 2)), 1, NULL, 112, 2)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (26, N'Planner', N'Planner for employees', N'office supplies', N'Costco', CAST(8.99 AS Decimal(19, 2)), 10, NULL, 113, 2)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (27, N'Tape', N'Transparent Tape', N'Office work', N'Staples', CAST(3.50 AS Decimal(19, 2)), 10, N'No need', 113, 3)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (28, N'Post-it', N'3M Post-it', N'Office supplies', N'Staples', CAST(2.99 AS Decimal(19, 2)), 2, NULL, 113, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (29, N'Pen', N'Pen', N'Office work', N'Staple', CAST(1.00 AS Decimal(19, 2)), 2, NULL, 113, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (30, N'Erasers', N'Erasers, 2 Pack, White', N'Office work', N'Staples', CAST(2.29 AS Decimal(19, 2)), 6, NULL, 114, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (31, N'Pencil', N'Pencil', N'Office work', N'Staples', CAST(2.50 AS Decimal(19, 2)), 5, NULL, 114, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (32, N'Blue pen', N'Blue color pen', N'office work', N'Staples', CAST(1.00 AS Decimal(19, 2)), 5, NULL, 115, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (33, N'Red pen', N'Red color pen', N'office work', N'Staples', CAST(1.00 AS Decimal(19, 2)), 5, NULL, 115, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (34, N'SSD', N'No longer needed', N'PC Upgrade', N'Best Buy', CAST(59.99 AS Decimal(19, 2)), 0, NULL, 116, 3)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (35, N'Pencil', N'Pencil', N'Pencil', N'Staples', CAST(2.00 AS Decimal(19, 2)), 2, NULL, 116, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (36, N'Pencil', N'Pencil', N'Pencil', N'Staples', CAST(2.00 AS Decimal(19, 2)), 1, NULL, 117, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (37, N'Green pen', N'Green pen', N'Green pen', N'Staples', CAST(3.00 AS Decimal(19, 2)), 1, NULL, 117, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (38, N'Pencil', N'Pencil', N'Meeting', N'Staples', CAST(2.99 AS Decimal(19, 2)), 10, NULL, 104, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (39, N'Office phone', N'Phone for business', N'New employees', N'Best Buy', CAST(68.99 AS Decimal(19, 2)), 2, NULL, 118, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (40, N'WAP', N'Wireless Access Point', N'Wifi set up', N'Newegg.ca', CAST(318.90 AS Decimal(19, 2)), 1, NULL, 118, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (41, N'Coffee', N'No longer needed', N'For employees', N'Walmart', CAST(1.99 AS Decimal(19, 2)), 0, NULL, 119, 3)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (42, N'Water', N'No longer needed', N'For employees', N'Walmart', CAST(1.99 AS Decimal(19, 2)), 0, NULL, 119, 3)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (44, N'RAM 64GB', N'RAM 64GB', N'PC Upgrade', N'Best Buy', CAST(89.90 AS Decimal(19, 2)), 1, NULL, 109, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (45, N'SSD', N'SSD 256GB', N'PC Upgrade', N'Best Buy', CAST(125.59 AS Decimal(19, 2)), 1, NULL, 109, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (46, N'HDMI Cabel', N'HDMI Cabel', N'Meeting room', N'Best Buy', CAST(9.99 AS Decimal(19, 2)), 2, NULL, 109, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (47, N'Note', N'Note', N'office work', N'Staples', CAST(1.50 AS Decimal(19, 2)), 2, NULL, 120, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (48, N'Pencil', N'Pencil', N'office work', N'Staples', CAST(1.50 AS Decimal(19, 2)), 2, NULL, 120, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (49, N'Binder', N'No longer needed', N'Office work', N'Staples', CAST(2.50 AS Decimal(19, 2)), 0, NULL, 121, 3)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (50, N'Pen', N'Pen', N'office work', N'Staples', CAST(2.50 AS Decimal(19, 2)), 2, NULL, 121, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (51, N'Note', N'Note', N'Office work', N'Staples', CAST(3.50 AS Decimal(19, 2)), 3, NULL, 122, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (52, N'Pen', N'Pen', N'Office work', N'Staples', CAST(2.50 AS Decimal(19, 2)), 1, N'No need', 122, 3)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (53, N'Pencil', N'Pencil', N'Office work', N'Staples', CAST(1.00 AS Decimal(19, 2)), 1, NULL, 123, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (54, N'Pen', N'Pen', N'Office work', N'Staples', CAST(1.00 AS Decimal(19, 2)), 2, NULL, 123, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (55, N'Note', N'Note', N'Office work', N'Staples', CAST(1.00 AS Decimal(19, 2)), 2, NULL, 123, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (57, N'Pen', N'No longer needed', N'Office work', N'Staples', CAST(2.00 AS Decimal(19, 2)), 0, NULL, 124, 3)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (58, N'Pencil', N'Pencil', N'Office work', N'Staples', CAST(2.00 AS Decimal(19, 2)), 1, NULL, 124, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (59, N'Bag', N'Bag', N'Packing', N'Walmart', CAST(20.50 AS Decimal(19, 2)), 20, NULL, 125, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (60, N'Ribbon', N'Ribbon', N'Packing', N'Walmart', CAST(10.00 AS Decimal(19, 2)), 15, NULL, 125, 1)
GO
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemDescription], [Justification], [Location], [Price], [Quantity], [DescisionReason], [PONumber], [ItemStatusId]) VALUES (61, N'Glue', N'Glue', N'Packing', N'Walmart', CAST(3.99 AS Decimal(19, 2)), 1, NULL, 125, 1)
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
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000001, N'14821022484100121216110182210519116271624220112113842551011521803218726683170191124')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000002, N'14821022484100121216110182210519116271624220112113842551011521803218726683170191124')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000003, N'14821022484100121216110182210519116271624220112113842551011521803218726683170191124')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000004, N'14821022484100121216110182210519116271624220112113842551011521803218726683170191124')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000005, N'14821022484100121216110182210519116271624220112113842551011521803218726683170191124')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000006, N'14821022484100121216110182210519116271624220112113842551011521803218726683170191124')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000007, N'14821022484100121216110182210519116271624220112113842551011521803218726683170191124')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000000, N'14821022484100121216110182210519116271624220112113842551011521803218726683170191124')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000008, N'14821022484100121216110182210519116271624220112113842551011521803218726683170191124')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000009, N'14821022484100121216110182210519116271624220112113842551011521803218726683170191124')
GO
INSERT [dbo].[Login] ([EmployeeId], [Password]) VALUES (10000010, N'14821022484100121216110182210519116271624220112113842551011521803218726683170191124')
GO
SET IDENTITY_INSERT [dbo].[POs] ON 
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (101, CAST(N'2021-04-26T00:47:27.1866667' AS DateTime2), CAST(59.97 AS Decimal(19, 2)), CAST(9.00 AS Decimal(19, 2)), 10000005, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (103, CAST(N'2021-04-27T10:30:00.0000000' AS DateTime2), CAST(282.45 AS Decimal(19, 2)), CAST(42.37 AS Decimal(19, 2)), 10000005, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (104, CAST(N'2021-04-27T15:25:00.0000000' AS DateTime2), CAST(59.15 AS Decimal(19, 2)), CAST(8.87 AS Decimal(19, 2)), 10000001, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (105, CAST(N'2021-04-27T16:45:00.0000000' AS DateTime2), CAST(110.70 AS Decimal(19, 2)), CAST(16.60 AS Decimal(19, 2)), 10000005, 3)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (106, CAST(N'2021-04-29T01:22:18.5000000' AS DateTime2), CAST(23.94 AS Decimal(19, 2)), CAST(3.59 AS Decimal(19, 2)), 10000008, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (107, CAST(N'2021-04-29T16:54:26.9933333' AS DateTime2), CAST(16.44 AS Decimal(19, 2)), CAST(2.47 AS Decimal(19, 2)), 10000009, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (108, CAST(N'2021-04-29T19:33:17.3766667' AS DateTime2), CAST(54.90 AS Decimal(19, 2)), CAST(8.68 AS Decimal(19, 2)), 10000005, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (109, CAST(N'2021-05-06T10:29:26.2535935' AS DateTime2), CAST(295.46 AS Decimal(19, 2)), CAST(44.32 AS Decimal(19, 2)), 10000005, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (110, CAST(N'2021-04-30T21:05:14.4800000' AS DateTime2), CAST(95.00 AS Decimal(19, 2)), CAST(14.25 AS Decimal(19, 2)), 10000004, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (111, CAST(N'2021-05-04T01:00:57.9300034' AS DateTime2), CAST(6.00 AS Decimal(19, 2)), CAST(0.90 AS Decimal(19, 2)), 10000001, 2)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (112, CAST(N'2021-05-04T01:19:11.1808378' AS DateTime2), CAST(99.99 AS Decimal(19, 2)), CAST(15.00 AS Decimal(19, 2)), 10000001, 3)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (113, CAST(N'2021-05-05T10:00:56.6778423' AS DateTime2), CAST(132.88 AS Decimal(19, 2)), CAST(19.93 AS Decimal(19, 2)), 10000005, 2)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (114, CAST(N'2021-05-05T10:41:22.4045378' AS DateTime2), CAST(26.24 AS Decimal(19, 2)), CAST(3.94 AS Decimal(19, 2)), 10000008, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (115, CAST(N'2021-05-05T10:43:54.4734970' AS DateTime2), CAST(10.00 AS Decimal(19, 2)), CAST(1.50 AS Decimal(19, 2)), 10000008, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (116, CAST(N'2021-05-05T17:34:17.3870733' AS DateTime2), CAST(4.00 AS Decimal(19, 2)), CAST(9.60 AS Decimal(19, 2)), 10000010, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (117, CAST(N'2021-05-05T17:38:03.7357407' AS DateTime2), CAST(5.00 AS Decimal(19, 2)), CAST(0.75 AS Decimal(19, 2)), 10000010, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (118, CAST(N'2021-05-06T12:36:52.4733757' AS DateTime2), CAST(456.88 AS Decimal(19, 2)), CAST(68.53 AS Decimal(19, 2)), 10000001, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (119, CAST(N'2021-05-06T12:42:24.9985866' AS DateTime2), CAST(0.00 AS Decimal(19, 2)), CAST(0.00 AS Decimal(19, 2)), 10000001, 3)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (120, CAST(N'2021-05-06T23:32:45.4875022' AS DateTime2), CAST(6.00 AS Decimal(19, 2)), CAST(0.90 AS Decimal(19, 2)), 10000008, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (121, CAST(N'2021-05-07T00:10:15.0697602' AS DateTime2), CAST(5.00 AS Decimal(19, 2)), CAST(0.75 AS Decimal(19, 2)), 10000008, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (122, CAST(N'2021-05-07T00:11:12.9893976' AS DateTime2), CAST(13.00 AS Decimal(19, 2)), CAST(1.95 AS Decimal(19, 2)), 10000005, 2)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (123, CAST(N'2021-05-07T00:39:43.6858132' AS DateTime2), CAST(6.00 AS Decimal(19, 2)), CAST(0.90 AS Decimal(19, 2)), 10000009, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (124, CAST(N'2021-05-07T12:40:50.5015101' AS DateTime2), CAST(2.00 AS Decimal(19, 2)), CAST(0.30 AS Decimal(19, 2)), 10000004, 1)
GO
INSERT [dbo].[POs] ([PONumber], [SubmissionDate], [SubTotal], [Tax], [EmployeeId], [POStatusId]) VALUES (125, CAST(N'2021-05-12T15:57:47.2184118' AS DateTime2), CAST(563.99 AS Decimal(19, 2)), CAST(84.60 AS Decimal(19, 2)), 10000008, 1)
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
ALTER TABLE [dbo].[EmployeeReviews]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeReviews_ReviewerId] FOREIGN KEY([ReviewerId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[EmployeeReviews] CHECK CONSTRAINT [FK_EmployeeReviews_ReviewerId]
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
/****** Object:  StoredProcedure [dbo].[spCheckDuplicateUsername]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  StoredProcedure [dbo].[spCheckHeadSupervisorIdOfPO]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spCheckHeadSupervisorIdOfPO]
	@PONumber INT,
	@SupervisorId INT
AS
BEGIN
	BEGIN TRY
		DECLARE @EmployeeId INT

		SET @EmployeeId = (SELECT Employees.EmployeeId FROM POs JOIN Employees ON Employees.EmployeeId = POs.EmployeeId WHERE POs.PONumber = @PONumber)

		-- if employee id is head supervisor
		IF @SupervisorId = (SELECT EmployeeId FROM Employees WHERE SupervisorId IS NULL)
			SELECT @SupervisorId AS HeadSupervisor
		
		-- if employee id is supervisor
		ELSE IF (SELECT SupervisorId FROM Employees WHERE EmployeeId = @EmployeeId) = (SELECT EmployeeId FROM Employees WHERE SupervisorId IS NULL)
			SELECT EmployeeId AS HeadSupervisor FROM Employees WHERE SupervisorId IS NULL 
		
		ELSE
			BEGIN
				(SELECT EmployeeId AS HeadSupervisor FROM Employees WHERE Employees.DepartmentId = (SELECT DepartmentId FROM Employees WHERE Employeeid = @EmployeeId) AND IsHeadSupervisor = 1)
			END

	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteDepartment]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spDeleteDepartment]
	@RecordVersion ROWVERSION OUTPUT,
	@DepartmentId INT
AS 

BEGIN
	BEGIN TRY
		--Concurrency. Compare record version--
			IF(SELECT [RecordVersion] FROM Departments WHERE DepartmentId = @DepartmentId) <> @RecordVersion
				THROW 53001, 'The department record has been updated since last time you retrieved it', 1

		DELETE FROM Departments WHERE DepartmentId = @DepartmentId;

		SET @RecordVersion = (SELECT RecordVersion from Departments where DepartmentId = @DepartmentId);

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spDeleteItemByItemId]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  StoredProcedure [dbo].[spFindDuplicatedItems]    Script Date: 2021-05-14 11:52:14 AM ******/
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
		SELECT * FROM Items 
		WHERE
			PONumber = @PONumber AND
			ItemName = @ItemName AND
			ItemDescription = @ItemDescription AND
			Justification = @Justification AND
			Location = @Location AND
			Price = @Price AND
			(@ItemId IS NULL OR ItemId <> @ItemId)
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetAllEmployees]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spGetAllEmployees]
AS 

BEGIN
	BEGIN TRY

	SELECT * FROM Employees
		INNER JOIN JobAssignment
			ON Employees.JobAssignmentId = JobAssignment.JobAssignmentId;

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spGetCEO]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  StoredProcedure [dbo].[spGetDepartmentById]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spGetDepartmentById]
	@DepartmentId INT
AS 

BEGIN
	BEGIN TRY
			SELECT * FROM Departments WHERE DepartmentId = @DepartmentId

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spGetDepartments]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  StoredProcedure [dbo].[spGetEmailSentToday]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spGetEmailSentToday]
AS 

BEGIN
	BEGIN TRY

	SELECT * FROM ReviewReminderEmail WHERE CAST( Date AS Date ) = CAST( GETDATE() AS Date );

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spGetEmployeeById]    Script Date: 2021-05-14 11:52:14 AM ******/
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
					   (SELECT FirstName + ' ' +  COALESCE(MiddleInit + ' ', '') + LastName FROM Employees WHERE EmployeeId = Supervisor.EmployeeId) AS Supervisor,
					   Employees.IsHeadSupervisor
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
/****** Object:  StoredProcedure [dbo].[spGetEmployeeReviews]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE    PROCEDURE [dbo].[spGetEmployeeReviews]
	@EmployeeId INT
AS 

BEGIN
	BEGIN TRY

		SELECT * FROM EmployeeReviews WHERE EmployeeId = @EmployeeId;

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spGetEmployeesByDepartment]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spGetEmployeesByDepartment]
	@DepartmentId INT
AS 

BEGIN
	BEGIN TRY

		SELECT * FROM Employees WHERE DepartmentId = @DepartmentId;

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spGetEmployeeStatus]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE    PROCEDURE [dbo].[spGetEmployeeStatus]
AS 

BEGIN
	BEGIN TRY

		SELECT * FROM EmployeeStatus;

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spGetEmployeeToModify]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spGetEmployeeToModify]
	@EmployeeId INT
AS 

BEGIN
	BEGIN TRY
			SELECT * FROM Employees WHERE EmployeeId = @EmployeeId;
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spGetHeadSupervisor]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--add employeeid -- will need to modify create and update employee ucs
CREATE   PROCEDURE [dbo].[spGetHeadSupervisor]
	@DepartmentId INT
	--@EmployeeId INT
AS 

BEGIN
	BEGIN TRY
			SELECT * FROM Employees WHERE DepartmentId = @DepartmentId AND IsHeadSupervisor = 1; --AND EmployeeId <> @EmployeeId
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spGetHeadSupervisorId]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--E1 but added later
CREATE   PROCEDURE [dbo].[spGetHeadSupervisorId]
	@HeadSupervisorId INT OUTPUT,
	@DepartmentId INT
AS 

BEGIN
	BEGIN TRY
			SET @HeadSupervisorId = (SELECT EmployeeId FROM Employees WHERE DepartmentId = @DepartmentId AND IsHeadSupervisor = 1);
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spGetItemByItemId]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spGetItemByItemId]
	@ItemId INT,
	@EmployeeId INT = NULL,
	@SupervisorId INT = NULL
AS
BEGIN
	BEGIN TRY
		IF NOT EXISTS (SELECT ItemId FROM Items WHERE ItemId = @ItemId)
			THROW 55001, 'The item is not existing. Please try again.', 1
		ELSE 
			BEGIN
				SELECT TOP 1 ItemId, ItemName, ItemDescription, Justification, Location, Price, Quantity, 
				(SELECT REPLICATE('0',8-LEN(Items.PONumber)) + RTRIM(Items.PONumber)) AS PONumber, POs.POStatusId, Items.ItemStatusId, ItemStatus.ItemStatus, DescisionReason,
				POs.EmployeeId, Employees.SupervisorId, CAST(CASE WHEN Employees.SupervisorId = (SELECT EmployeeId FROM Employees WHERE SupervisorId IS NULL) THEN (SELECT EmployeeId FROM Employees WHERE SupervisorId IS NULL) ELSE HeadSupervisor.EmployeeId END AS INT) AS HeadSupervisorId,
				Items.RecordVersion
				FROM Items 
				LEFT JOIN ItemStatus ON ItemStatus.ItemStatusId = Items.ItemStatusId
				LEFT JOIN POs ON POs.PONumber = Items.PONumber
				LEFT JOIN Employees ON Employees.EmployeeId = POs.EmployeeId
				LEFT JOIN Employees AS Supervisor ON Supervisor.EmployeeId = Employees.SupervisorId 
				LEFT JOIN Employees AS HeadSupervisor ON Employees.DepartmentId = HeadSupervisor.DepartmentId
				WHERE ItemId = @ItemId AND
						(@EmployeeId IS NULL OR POs.EmployeeId = @EmployeeId) AND
						(@SupervisorID IS NULL OR HeadSupervisor.IsHeadSupervisor = 1)	
			END
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetItemByPONumber]    Script Date: 2021-05-14 11:52:14 AM ******/
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
				(SELECT REPLICATE('0',8-LEN(PONumber)) + RTRIM(PONumber)) AS PONumber, (SELECT POStatusId FROM POs WHERE PONumber = @PurchaseOrderNumber) AS POStatusId, 
				ItemStatus.ItemStatus, DescisionReason
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
/****** Object:  StoredProcedure [dbo].[spGetItemStatusForLookup]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  StoredProcedure [dbo].[spGetJobAssignments]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  StoredProcedure [dbo].[spGetPOStatusForLookup]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  StoredProcedure [dbo].[spGetPurchaseOrderByEmployee]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROC [dbo].[spGetPurchaseOrderByEmployee]
	@EmployeeId INT,
	@PurchaseOrderNumber VARCHAR(8) = NULL,
	@StartDate DATETIME2 = NULL,
	@EndDate DATETIME2 = NULL,
	@Status INT = NULL
AS
BEGIN
	BEGIN TRY
		BEGIN	
			SELECT (SELECT REPLICATE('0',8-LEN(PONumber)) + RTRIM(PONumber)) AS PONumber, SubmissionDate, SubTotal, Tax, (SubTotal+Tax) AS Total,
			POs.EmployeeId, (Employees.FirstName + ' ' +  COALESCE(Employees.MiddleInit + ' ', '') + Employees.LastName) AS Employee,
			(SELECT FirstName + ' ' +  COALESCE(MiddleInit + ' ', '') + LastName FROM Employees WHERE EmployeeId = Supervisor.EmployeeId) AS Supervisor,
			POStatus.POStatus AS POStatus, POs.RecordVersion
			FROM POs 
			LEFT JOIN Employees ON Employees.EmployeeId = POs.EmployeeId 
			LEFT JOIN Employees AS Supervisor ON Supervisor.EmployeeId = Employees.SupervisorId 
			LEFT JOIN POStatus ON POStatus.POStatusId = POs.POStatusId
			WHERE POs.EmployeeId = @EmployeeId AND 
				  (@PurchaseOrderNumber IS NULL or POs.PONumber = @PurchaseOrderNumber) AND
				  (@StartDate IS NULL or CAST(POs.SubmissionDate as date) >= CAST(@StartDate as date)) AND
				  (@EndDate IS NULL or CAST(POs.SubmissionDate as date) <= CAST(@EndDate as date)) AND
				  (@Status IS NULL or POs.POStatusId = @Status)
			ORDER BY SubmissionDate DESC
		END
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetPurchaseOrderByPOnumber]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spGetPurchaseOrderByPOnumber]
	@PurchaseOrderNumber INT,
	@EmployeeID INT = NULL,
	@SupervisorID INT = NULL
AS
BEGIN
	BEGIN TRY
			-- if the purchase order is not existing
			IF NOT EXISTS (SELECT PONumber FROM POs WHERE PONumber = @PurchaseOrderNumber)
				THROW 54001, 'The purchase order is not existing. Please try again.', 1

			ELSE
				BEGIN
					SELECT TOP 1 (SELECT REPLICATE('0',8-LEN(PONumber)) + RTRIM(PONumber)) AS PONumber, SubmissionDate, SubTotal, Tax, (SubTotal+Tax) AS Total,
					POs.EmployeeId, (Employees.FirstName + ' ' +  COALESCE(Employees.MiddleInit + ' ', '') + Employees.LastName) AS Employee,
					(SELECT FirstName + ' ' +  COALESCE(MiddleInit + ' ', '') + LastName FROM Employees WHERE EmployeeId = Supervisor.EmployeeId) AS Supervisor,
					Employees.SupervisorId, CAST(CASE WHEN Employees.SupervisorId = 
												 (SELECT EmployeeId FROM Employees WHERE SupervisorId IS NULL) THEN 
												 (SELECT EmployeeId FROM Employees WHERE SupervisorId IS NULL) 
												 ELSE HeadSupervisor.EmployeeId END AS INT) AS HeadSupervisorId, POStatus.POStatus AS POStatus, 
					POs.RecordVersion
					FROM POs 
					LEFT JOIN Employees ON Employees.EmployeeId = POs.EmployeeId 
					LEFT JOIN Employees AS Supervisor ON Supervisor.EmployeeId = Employees.SupervisorId 
					LEFT JOIN POStatus ON POStatus.POStatusId = POs.POStatusId
					LEFT JOIN Employees AS HeadSupervisor ON Employees.DepartmentId = HeadSupervisor.DepartmentId
					WHERE POs.PONumber = @PurchaseOrderNumber AND
						  (@EmployeeID IS NULL OR POs.EmployeeId = @EmployeeID) AND
						  (@SupervisorID IS NULL OR HeadSupervisor.IsHeadSupervisor = 1)
				END
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetPurchaseOrderBySupervisor]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROC [dbo].[spGetPurchaseOrderBySupervisor]
	@SupervisorId INT,
	@Status INT = NULL,
	@EmployeeId INT = NULL,
	@EmployeeName VARCHAR(120) = NULL,
	@StartDate DATETIME2 = NULL,
	@EndDate DATETIME2 = NULL,
	@PurchaseOrderNumber INT = NULL
AS
BEGIN
	BEGIN TRY
		DECLARE @DepartmentId INT,
				@IsHeadSupervisor BIT,
				@CEOId INT

		SET @DepartmentId = (SELECT DepartmentId FROM Employees WHERE EmployeeId = @SupervisorId)
		SET @IsHeadSupervisor = (SELECT IsHeadSupervisor FROM Employees WHERE EmployeeId = @SupervisorId)
		SET @CEOId = (SELECT EmployeeId FROM Employees WHERE SupervisorId IS NULL)

		IF @DepartmentId <> 1
			BEGIN
				SELECT (SELECT REPLICATE('0',8-LEN(PONumber)) + RTRIM(PONumber)) AS PONumber, SubmissionDate, SubTotal, Tax, (SubTotal+Tax) AS Total,
				POs.EmployeeId, (Employees.FirstName + ' ' +  COALESCE(Employees.MiddleInit + ' ', '') + Employees.LastName) as Employee,
				(SELECT FirstName + ' ' +  COALESCE(MiddleInit + ' ', '') + LastName FROM Employees WHERE EmployeeId = Supervisor.EmployeeId) AS Supervisor,
				POStatus.POStatus AS POStatus
				FROM POs 
				JOIN Employees ON Employees.EmployeeId = POs.EmployeeId 
				JOIN Employees AS Supervisor ON Supervisor.EmployeeId = Employees.SupervisorId 
				JOIN POStatus ON POStatus.POStatusId = POs.POStatusId
				WHERE (POs.EmployeeId IN (SELECT EmployeeId FROM Employees WHERE DepartmentId = @DepartmentId)) AND Supervisor.EmployeeId <> @CEOId AND POs.SubmissionDate IS NOT NULL AND
						(@EmployeeName IS NULL or (Employees.FirstName LIKE '%' + @EmployeeName + '%' OR 
												Employees.LastName LIKE '%' + @EmployeeName + '%' OR 
												(Employees.FirstName + ' ' +  COALESCE(Employees.MiddleInit + ' ', '') + Employees.LastName) = @EmployeeName)) AND
						(@StartDate IS NULL or CAST(POs.SubmissionDate as date) >= CAST(@StartDate as date)) AND
						(@EndDate IS NULL or CAST(POs.SubmissionDate as date) <= CAST(@EndDate as date)) AND
						(@Status IS NULL or POs.POStatusId = @Status) AND
						(@IsHeadSupervisor IS NOT NULL or Employees.SupervisorId = @SupervisorId) AND
						(@PurchaseOrderNumber IS NULL or POs.PONumber = @PurchaseOrderNumber)

				ORDER BY SubmissionDate
			END
		ELSE 
			BEGIN
				SELECT (SELECT REPLICATE('0',8-LEN(PONumber)) + RTRIM(PONumber)) AS PONumber, SubmissionDate, SubTotal, Tax, (SubTotal+Tax) AS Total,
				POs.EmployeeId, (Employees.FirstName + ' ' +  COALESCE(Employees.MiddleInit + ' ', '') + Employees.LastName) as Employee,
				(SELECT FirstName + ' ' +  COALESCE(MiddleInit + ' ', '') + LastName FROM Employees WHERE EmployeeId = Supervisor.EmployeeId) AS Supervisor,
				POStatus.POStatus AS POStatus
				FROM POs 
				JOIN Employees ON Employees.EmployeeId = POs.EmployeeId 
				JOIN Employees AS Supervisor ON Supervisor.EmployeeId = Employees.SupervisorId 
				JOIN POStatus ON POStatus.POStatusId = POs.POStatusId
				WHERE Supervisor.EmployeeId = @CEOId AND POs.SubmissionDate IS NOT NULL AND
					  (@EmployeeName IS NULL or (Employees.FirstName LIKE '%' + @EmployeeName + '%' OR 
											    Employees.LastName LIKE '%' + @EmployeeName + '%' OR 
											   (Employees.FirstName + ' ' +  COALESCE(Employees.MiddleInit + ' ', '') + Employees.LastName) = @EmployeeName)) AND
					  (@StartDate IS NULL or CAST(POs.SubmissionDate as date) >= CAST(@StartDate as date)) AND
					  (@EndDate IS NULL or CAST(POs.SubmissionDate as date) <= CAST(@EndDate as date)) AND
					  (@Status IS NULL or POs.POStatusId = @Status) AND
					  (@PurchaseOrderNumber IS NULL or POs.PONumber = @PurchaseOrderNumber)
				ORDER BY SubmissionDate
			END
	END TRY	
	BEGIN CATCH
		;THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetReviewById]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spGetReviewById]
	@ReviewId INT
AS 

BEGIN
	BEGIN TRY

	SELECT *, FirstName, MiddleInit, LastName FROM EmployeeReviews
		INNER JOIN Employees
			ON Employees.EmployeeId = EmployeeReviews.ReviewerId
		WHERE EmployeeReviewId = @ReviewId;

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spGetSuperEmployeeCount]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  StoredProcedure [dbo].[spGetSupervisors]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  StoredProcedure [dbo].[spInsertDepartment]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--updated to add record version
CREATE    PROCEDURE [dbo].[spInsertDepartment]
	@RecordVersion ROWVERSION OUTPUT,
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

		SET @RecordVersion = (SELECT RecordVersion from Departments where DepartmentId = @DepartmentId);

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spInsertEmployee]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--updated to add officelocation
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
	@OfficeLocation VARCHAR(255),
	
	@JobStartDate DATETIME, 
	@SeniorityDate DATETIME,
	@SIN VARCHAR(11), 

	@SupervisorId INT, 
	@IsHeadSupervisor BIT,
	@DepartmentId INT, 
	@EmployeeStatusId INT, 
	@JobAssignmentId INT
AS 

BEGIN
	BEGIN TRY			
			
		INSERT INTO Employees 
			(
				UserName, FirstName, MiddleInit, LastName, DateOfBirth, 
				Street, City, Province, Country, PostalCode, WorkPhone, CellPhone, Email, OfficeLocation, JobStartDate, SeniorityDate, SIN, 
				SupervisorId, IsHeadSupervisor, DepartmentId, EmployeeStatusId, JobAssignmentId
			)
		VALUES
			(
				@UserName, @FirstName, @MiddleInit, @LastName, @DateOfBirth, 
				@Street, @City, @Province, @Country, @PostalCode, @WorkPhone, @CellPhone, @Email, @OfficeLocation, @JobStartDate, @SeniorityDate, 
				@SIN, @SupervisorId, @IsHeadSupervisor, @DepartmentId, 1, @JobAssignmentId
			);

		SET @EmployeeId = SCOPE_IDENTITY();

		SET @RecordVersion = (SELECT RecordVersion from Employees where EmployeeId = @EmployeeId);
		
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spInsertItems]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[spInsertItems]
	@ItemId INT OUTPUT,
	@PORecordVersion ROWVERSION OUTPUT,
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
			IF(SELECT [RecordVersion] FROM POs WHERE PONumber = @PONumber) <> @PORecordVersion
				THROW 53001, 'The purchase order record has been updated since last time you retrieved it', 1

			INSERT INTO Items
			(ItemName, ItemDescription, Justification, Location, Price, Quantity, DescisionReason, PONumber, ItemStatusId)
			VALUES(@ItemName, @ItemDescription, @Justification, @Location, @Price, @Quantity, null, @PONumber, 1)
						
			SET @ItemId = IDENT_CURRENT('Items');

			UPDATE POs
				SET SubTotal = (SELECT SUM(Price * Quantity) FROM Items WHERE PONumber = @PONumber),
					Tax = (SELECT SUM(Price * Quantity) FROM Items WHERE PONumber = @PONumber) * 0.15					
				WHERE PONumber = @PONumber;

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
/****** Object:  StoredProcedure [dbo].[spInsertPassword]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Modify insert password stored procedure
CREATE   PROCEDURE [dbo].[spInsertPassword]
	@EmployeeId INT,
	@Password NVARCHAR(MAX)
AS 

BEGIN
	BEGIN TRY
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
/****** Object:  StoredProcedure [dbo].[spInsertPurchaseOrder]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  StoredProcedure [dbo].[spInsertReview]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE    PROCEDURE [dbo].[spInsertReview]
	@EmployeeReviewId INT OUTPUT,
	@Date DATETIME,
	@Comment VARCHAR(255),
	@EmployeeId INT,
	@ReviewerId INT,
	@RatingId INT
AS 

BEGIN
	BEGIN TRY

		INSERT INTO EmployeeReviews 
			(Date, Comment, EmployeeId, ReviewerId, RatingId)
		VALUES
			(@Date, @Comment, @EmployeeId, @ReviewerId, @RatingId);

		SET @EmployeeReviewId = SCOPE_IDENTITY();

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spInsertReviewReminderEmail]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spInsertReviewReminderEmail]
AS 

BEGIN
	BEGIN TRY

	INSERT INTO ReviewReminderEmail (Date)
	VALUES
	(CAST( GETDATE() AS Date ));

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spLogin]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Modify login stored procedure
CREATE    PROCEDURE [dbo].[spLogin]
	@EmployeeId INT,
	@Password NVARCHAR(MAX)
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
/****** Object:  StoredProcedure [dbo].[spSearchEmployeesById]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  StoredProcedure [dbo].[spSearchEmployeesByLastName]    Script Date: 2021-05-14 11:52:14 AM ******/
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
/****** Object:  StoredProcedure [dbo].[spUpdateDepartment]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spUpdateDepartment]
	@RecordVersion ROWVERSION OUTPUT,
	@DepartmentId INT,
	@DepartmentName VARCHAR(255),
	@DepartmentDescription VARCHAR(255),
	@InvocationDate DATETIME
AS 

BEGIN
	BEGIN TRY
	--Concurrency. Compare record version--
			IF(SELECT [RecordVersion] FROM Departments WHERE DepartmentId = @DepartmentId) <> @RecordVersion
				THROW 53001, 'The employee record has been updated since last time you retrieved it', 1

			UPDATE Departments
			SET
				DepartmentName = @DepartmentName,
				DepartmentDescription = @DepartmentDescription,
				InvocationDate = @InvocationDate
			WHERE DepartmentId = @DepartmentId;

			SET @RecordVersion = (SELECT RecordVersion from Departments where DepartmentId = @DepartmentId);

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spUpdateEmployee]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spUpdateEmployee]
	@RecordVersion ROWVERSION OUTPUT,
	@EmployeeId INT OUTPUT,
	@FirstName VARCHAR(40), 
	@MiddleInit VARCHAR(40), 
	@LastName VARCHAR(40), 
	@DateOfBirth DATETIME, 
	
	@Street VARCHAR(50), 
	@City VARCHAR(20), 
	@Province VARCHAR(2), 
	@Country VARCHAR(100), 
	@PostalCode VARCHAR(10), 
	
	@JobStartDate DATETIME, 
	@EndDate DATETIME, 
	@SIN VARCHAR(11), 	

	@SupervisorId INT,
	@IsHeadSupervisor BIT, 
	@DepartmentId INT, 
	@EmployeeStatusId INT, 
	@JobAssignmentId INT
AS 

BEGIN
	BEGIN TRY
	--Concurrency. Compare record version--
			IF(SELECT [RecordVersion] FROM Employees WHERE EmployeeId = @EmployeeId) <> @RecordVersion
				THROW 53001, 'The employee record has been updated since last time you retrieved it', 1

			UPDATE Employees
			SET 
				FirstName = @FirstName, 
				MiddleInit = @MiddleInit,
				LastName= @LastName, 
				DateOfBirth = @DateOfBirth, 
				Street = @Street, 
				City = @City,
				Province = @Province,
				Country = @Country, 
				PostalCode = @PostalCode,
				JobStartDate = @JobStartDate,
				EndDate = @EndDate,
				SIN = @SIN,	
				SupervisorId = @SupervisorId,
				IsHeadSupervisor = @IsHeadSupervisor, 
				DepartmentId = @DepartmentId, 
				EmployeeStatusId = @EmployeeStatusId,
				JobAssignmentId = @JobAssignmentId
			WHERE EmployeeId = @EmployeeId

			SET @RecordVersion = (SELECT RecordVersion from Employees where EmployeeId = @EmployeeId);

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spUpdateItems]    Script Date: 2021-05-14 11:52:14 AM ******/
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
	@DescisionReason VARCHAR(255) = NULL,
	@PONumber INT,
	@ItemStatusId INT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN		
			--Concurrency. Compare record version--
			IF(SELECT [RecordVersion] FROM Items WHERE ItemId = @ItemId) <> @RecordVersion
				THROW 53001, 'The item record has been updated since last time you retrieved it', 1
			IF(SELECT [RecordVersion] FROM POs WHERE PONumber = @PONumber) <> @PORecordVersion
				THROW 53001, 'The purchase order record has been updated since last time you retrieved it', 1

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
				SET SubTotal = (SELECT SUM(Price * Quantity) FROM Items WHERE PONumber = @PONumber),
					Tax = (SELECT SUM(Price * Quantity) FROM Items WHERE PONumber = @PONumber) * 0.15
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
/****** Object:  StoredProcedure [dbo].[spUpdatePersonalInfoWeb]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[spUpdatePersonalInfoWeb]
	@RecordVersion ROWVERSION OUTPUT,
	@EmployeeId INT,

	@Street VARCHAR(50), 
	@City VARCHAR(20), 
	@Province VARCHAR(2), 
	@Country VARCHAR(100), 
	@PostalCode VARCHAR(10), 
	@WorkPhone VARCHAR(13), 
	@CellPhone VARCHAR(13)
AS 

BEGIN
	BEGIN TRY
	--Concurrency. Compare record version--
			IF(SELECT [RecordVersion] FROM Employees WHERE EmployeeId = @EmployeeId) <> @RecordVersion
				THROW 53001, 'The employee record has been updated since last time you retrieved it', 1

			UPDATE Employees
			SET 
				Street = @Street,
				City= @City,
				Province= @Province,
				Country= @Country, 
				PostalCode= @PostalCode, 
				WorkPhone= @WorkPhone, 
				CellPhone= @CellPhone
			WHERE EmployeeId = @EmployeeId;

			SET @RecordVersion = (SELECT RecordVersion from Employees where EmployeeId = @EmployeeId);

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[spUpdatePurchaseOrder]    Script Date: 2021-05-14 11:52:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     PROC [dbo].[spUpdatePurchaseOrder]
	@RecordVersion ROWVERSION OUTPUT,
	@PONumber INT,	
	@SubmissionDate DATETIME2 = GETDATE,
	@SubTotal DECIMAL(19,2),
	@Tax DECIMAL(19,2),
	@POStatusId INT = 1
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN		
			--Concurrency. Compare record version--
			IF(SELECT [RecordVersion] FROM POs WHERE PONumber = @PONumber) <> @RecordVersion
				THROW 53001, 'The purchase order record has been updated since last time you retrieved it', 1

			UPDATE POs
			   SET SubmissionDate = @SubmissionDate
				  ,SubTotal = @SubTotal
				  ,Tax = @Tax
				  ,POStatusId = @POStatusId
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
USE [master]
GO
ALTER DATABASE [VastVoyages] SET  READ_WRITE 
GO
