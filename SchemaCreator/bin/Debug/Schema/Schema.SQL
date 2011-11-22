
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKFFB044305958A979]') AND parent_object_id = OBJECT_ID('[Article]'))
alter table [Article]  drop constraint FKFFB044305958A979


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3300FC24233FFD71]') AND parent_object_id = OBJECT_ID('[SalesmanArticle]'))
alter table [SalesmanArticle]  drop constraint FK3300FC24233FFD71


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3300FC247864F86D]') AND parent_object_id = OBJECT_ID('[SalesmanArticle]'))
alter table [SalesmanArticle]  drop constraint FK3300FC247864F86D


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC96CECDEBA780B82]') AND parent_object_id = OBJECT_ID('[LoanedItem]'))
alter table [LoanedItem]  drop constraint FKC96CECDEBA780B82


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE2AB67FFBA780B82]') AND parent_object_id = OBJECT_ID('[TeamEmployee]'))
alter table [TeamEmployee]  drop constraint FKE2AB67FFBA780B82


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE2AB67FF10607B08]') AND parent_object_id = OBJECT_ID('[TeamEmployee]'))
alter table [TeamEmployee]  drop constraint FKE2AB67FF10607B08


    if exists (select * from dbo.sysobjects where id = object_id(N'[Article]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Article]

    if exists (select * from dbo.sysobjects where id = object_id(N'[GoodsGroup]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [GoodsGroup]

    if exists (select * from dbo.sysobjects where id = object_id(N'[SalesmanArticle]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [SalesmanArticle]

    if exists (select * from dbo.sysobjects where id = object_id(N'[Salesman]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Salesman]

    if exists (select * from dbo.sysobjects where id = object_id(N'[Employee]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Employee]

    if exists (select * from dbo.sysobjects where id = object_id(N'[LoanedItem]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [LoanedItem]

    if exists (select * from dbo.sysobjects where id = object_id(N'[TeamEmployee]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [TeamEmployee]

    if exists (select * from dbo.sysobjects where id = object_id(N'[Team]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Team]

    create table [Article] (
        ArticleId INT IDENTITY NOT NULL,
       Description NVARCHAR(255) null,
       GoodsGroupId INT null,
       primary key (ArticleId)
    )

    create table [GoodsGroup] (
        GoodsGroupId INT IDENTITY NOT NULL,
       Description NVARCHAR(255) null,
       primary key (GoodsGroupId)
    )

    create table [SalesmanArticle] (
        SalesmanArticleId INT IDENTITY NOT NULL,
       Amount INT null,
       Cost DECIMAL(19,5) null,
       Date DATETIME null,
       ArticleId INT null,
       SalesmanId INT null,
       primary key (SalesmanArticleId)
    )

    create table [Salesman] (
        SalesmanId INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       Place NVARCHAR(255) null,
       primary key (SalesmanId)
    )

    create table [Employee] (
        EmployeeId INT IDENTITY NOT NULL,
       EMail NVARCHAR(255) null,
       LastName NVARCHAR(255) null,
       FirstName NVARCHAR(255) null,
       Infos NVARCHAR(255) not null,
       Count INT null,
       primary key (EmployeeId)
    )

    create table [LoanedItem] (
        LoanedItemId INT IDENTITY NOT NULL,
       LoanedItemType NVARCHAR(255) not null,
       PublisherName NVARCHAR(255) null,
       PublisherHomepage NVARCHAR(255) null,
       ReleaseDate DATETIME null,
       ReleaseNumber INT null,
       DateOfIssue DATETIME null,
       IncludesCDDVD BIT null,
       IsLoaned BIT null,
       Name NVARCHAR(255) null,
       EmployeeID INT null,
       Author NVARCHAR(255) null,
       Isbn NVARCHAR(255) null,
       primary key (LoanedItemId)
    )

    create table [TeamEmployee] (
        TeamEmployeeId INT IDENTITY NOT NULL,
       EmployeeId INT null,
       TeamId INT null,
       primary key (TeamEmployeeId)
    )

    create table [Team] (
        TeamId INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       primary key (TeamId)
    )

    alter table [Article] 
        add constraint FKFFB044305958A979 
        foreign key (GoodsGroupId) 
        references [GoodsGroup]

    alter table [SalesmanArticle] 
        add constraint FK3300FC24233FFD71 
        foreign key (ArticleId) 
        references [Article]

    alter table [SalesmanArticle] 
        add constraint FK3300FC247864F86D 
        foreign key (SalesmanId) 
        references [Salesman]

    alter table [LoanedItem] 
        add constraint FKC96CECDEBA780B82 
        foreign key (EmployeeID) 
        references [Employee]

    alter table [TeamEmployee] 
        add constraint FKE2AB67FFBA780B82 
        foreign key (EmployeeId) 
        references [Employee]

    alter table [TeamEmployee] 
        add constraint FKE2AB67FF10607B08 
        foreign key (TeamId) 
        references [Team]
