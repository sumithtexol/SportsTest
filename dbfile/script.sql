CREATE TABLE [dbo].[MemberTestMapping] (
    [MemberTestId]  INT          IDENTITY (1, 1) NOT NULL,
    [MemberId]      INT          NULL,
    [TestDetailsId] INT          NULL,
    [CreatedDate]   DATETIME     DEFAULT (getdate()) NOT NULL,
    [Distance]      DECIMAL (18) NULL,
    PRIMARY KEY CLUSTERED ([MemberTestId] ASC)
);

CREATE TABLE [dbo].[Memders] (
    [MemberId]   INT            IDENTITY (1, 1) NOT NULL,
    [MemberName] NVARCHAR (150) NOT NULL,
    [UserType]   INT            NOT NULL,
    [CreateDate] DATETIME       DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([MemberId] ASC)
);

CREATE TABLE [dbo].[TestDetails] (
    [TestDetailsId]    INT      IDENTITY (1, 1) NOT NULL,
    [TestId]           INT      NULL,
    [NoOfParticipants] INT      NULL,
    [Date]             DATE     NULL,
    [CreatedDate]      DATETIME DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([TestDetailsId] ASC)
);

CREATE TABLE [dbo].[Tests] (
    [TestId]     INT           IDENTITY (1, 1) NOT NULL,
    [SportsName] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([TestId] ASC)
);

