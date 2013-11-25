
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_Script_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/* Generated with PragmaSQL on 29-12-2006 11:25:30 by ali.ozgur */
CREATE PROCEDURE [dbo].[spPragmaSQL_Script_Get]
@ScriptID int
AS BEGIN
	SELECT
		ScriptID
		, ParentID
		, Name
		, Script
		, CreatedBy
		, CreatedOn
		, ModifiedBy
		, ModifiedOn
		, IsDeleted
		, Type
		, DatabaseID
		, HelpText
		, UID
	FROM Script
END
' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_Script_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/* Generated with PragmaSQL on 29-12-2006 11:33:56 by ali.ozgur */
CREATE PROCEDURE [dbo].[spPragmaSQL_Script_Delete]
@ScriptID int
AS BEGIN
  -- Delete child objects recursively
  DECLARE @Param int

  DECLARE oCur CURSOR LOCAL READ_ONLY FAST_FORWARD FOR
  SELECT ScriptID
  FROM   Script where ParentID = @ScriptID

  OPEN oCur
  WHILE (1=1) BEGIN
    FETCH NEXT FROM oCur INTO @Param
    IF @@FETCH_STATUS <> 0  BREAK

    exec dbo.[spPragmaSQL_Script_Delete] @Param

  END
  CLOSE oCur
  DEALLOCATE oCur


	UPDATE Script
  SET IsDeleted = 1
	WHERE
		( ScriptID = @ScriptID )

END
' 
END
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[Script]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Script](
	[ScriptID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NULL,
	[Name] [varchar](255) NULL,
	[Script] [text] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedOn] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Type] [int] NULL,
	[DatabaseID] [int] NULL,
	[HelpText] [text] NULL,
	[UID] [varchar](50) NULL,
 CONSTRAINT [PK_Script] PRIMARY KEY CLUSTERED 
(
	[ScriptID] ASC
)--WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_ScriptHist_GetHistByName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[spPragmaSQL_ScriptHist_GetHistByName]
@ScriptName int
AS BEGIN
	SELECT
		SH.ScriptHistID
		, SH.ScriptID
		, SH.Script
		, SH.ModifiedOn
		, SH.ModifiedBy
	FROM S.Script
	LEFT OUTER JOIN ScriptHist SH on S.ScriptID = SH.ScriptID
  WHERE S.Name Like ''%'' + ISNULL(@ScriptName,'''') + ''%''
END
' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_CodeSnippet_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[spPragmaSQL_CodeSnippet_Delete]
@SnippetID int
AS BEGIN
  UPDATE CodeSnippet
  SET IsDeleted = 1
  WHERE SnippetID = @SnippetID
END

' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_CodeSnippet_List]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[spPragmaSQL_CodeSnippet_List]
@ParentID int
as begin
  select * from  CodeSnippet
  where 
  ( ISNULL(ParentID,-1) = ISNULL(@ParentID,-1) )
    AND
  ( ISNULL(IsDeleted,0) = 0 )
  order by  Type
end
' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[CodeSnippet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CodeSnippet](
	[SnippetID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NULL,
	[ParentID] [int] NULL,
	[Name] [varchar](255) NULL,
	[Snippet] [text] NULL,
	[Description] [text] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_CodeSnippet] PRIMARY KEY CLUSTERED 
(
	[SnippetID] ASC
)--WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[Server]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Server](
	[ServerID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Server] PRIMARY KEY CLUSTERED 
(
	[ServerID] ASC
)--WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[ScriptHist]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ScriptHist](
	[ScriptHistID] [int] IDENTITY(1,1) NOT NULL,
	[ScriptID] [int] NULL,
	[Script] [text] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_ScriptHist] PRIMARY KEY CLUSTERED 
(
	[ScriptHistID] ASC
)--WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[Object]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Object](
	[ObjectID] [int] IDENTITY(1,1) NOT NULL,
	[DatabaseID] [int] NULL,
	[Name] [varchar](2048) NULL,
	[Type] [varchar](10) NULL,
 CONSTRAINT [PK_Object] PRIMARY KEY CLUSTERED 
(
	[ObjectID] ASC
)--WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[ObjectChangeHist]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ObjectChangeHist](
	[ObjectChangeHistID] [int] IDENTITY(1,1) NOT NULL,
	[ObjectID] [int] NULL,
	[ObjectScript] [text] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[Comment] [varchar](2048) NULL,
 CONSTRAINT [PK_ObjectChangeHist] PRIMARY KEY CLUSTERED 
(
	[ObjectChangeHistID] ASC
)--WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[Database]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Database](
	[DatabaseID] [int] IDENTITY(1,1) NOT NULL,
	[ServerID] [int] NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Database] PRIMARY KEY CLUSTERED 
(
	[DatabaseID] ASC
)--WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_Script_GetChildren]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[spPragmaSQL_Script_GetChildren]
@ParentID int
AS BEGIN
	SELECT
	  SC.*
	, D.Name as DatabaseName
	, S.Name as ServerName
	FROM Script SC
	LEFT OUTER JOIN [Database] D on SC.DatabaseID = D.DatabaseID
  LEFT OUTER JOIN [Server] S on D.ServerID = S.ServerID
	WHERE
  ( ISNULL(SC.ParentID,-1) = ISNULL(@ParentID,-1) )
    AND
  ( ISNULL(SC.IsDeleted,0) = 0 )
END






' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_Script_List]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[spPragmaSQL_Script_List]
@IsDeleted bit = 0
AS BEGIN
	SELECT
		S.ScriptID
		, S.ParentID
		, S.Name
		, S.Script
		, S.CreatedBy
		, S.CreatedOn
		, S.ModifiedBy
		, S.ModifiedOn
		, S.IsDeleted
		, S.Type
		, S.DatabaseID
		, D.Name as ''DatabaseName''
		, SV.Name as ''ServerName''
	  , S.HelpText
		, S.UID
	FROM Script S
	LEFT OUTER JOIN [Database] D on S.DatabaseID = D.DatabaseID
	LEFT OUTER JOIN Server SV on D.ServerID = SV.ServerID
	WHERE ISNULL(S.IsDeleted,0) = ISNULL(@IsDeleted,ISNULL(S.IsDeleted,0))
END
' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_Script_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
/* Generated with PragmaSQL on 29-12-2006 11:33:56 by ali.ozgur */
CREATE PROCEDURE [dbo].[spPragmaSQL_Script_Update]
@ScriptID int
, @ParentID int
, @Name varchar(255)
, @Script text
, @ModifiedBy varchar(50)
, @HelpText text
AS BEGIN
	UPDATE Script
	SET
		ParentID = @ParentID
		, Name = @Name
		, Script = @Script
		, ModifiedBy = @ModifiedBy
		, ModifiedOn = GetDate()
		, HelpText = @HelpText
	WHERE
		( ScriptID = @ScriptID )

END

' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_ObjectChangeHist_List]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[spPragmaSQL_ObjectChangeHist_List]
@ServerName varchar(50)
, @DatabaseName varchar(50)
, @ObjectName VARCHAR(2048) = NULL
, @ObjectType VARCHAR(10) = NULL
AS BEGIN

SELECT 
  OCH.ObjectChangeHistID
  , S.Name AS ServerName
  , D.Name AS DatabaseName
  , O.Name AS ObjectName
  , O.Type AS ObjectType
  , OCH.ObjectScript
  , OCH.CreatedBy
  , OCH.CreatedOn
  , OCH.Comment
FROM  ObjectChangeHist OCH
JOIN Object O ON OCH.ObjectID = O.ObjectID
JOIN [DATABASE] D ON O.DatabaseID = D.DatabaseID
JOIN [Server] S ON D.ServerID = S.ServerID
WHERE
( ISNULL(S.[Name],'''') Like( ISNULL( @ServerName, ISNULL(S.[Name],'''') ) + ''%'') )
AND 
( ISNULL(D.[Name],'''') Like( ISNULL( @DatabaseName, ISNULL(D.[Name],'''') ) + ''%'') )
AND 
  ( ISNULL( O.[Name], '''')  Like ( ISNULL(@ObjectName, ISNULL(O.[Name],'''') ) + ''%'')  ) 
  AND 
  ( LOWER( ISNULL( O.[Type],'''') ) = LOWER( ISNULL(@ObjectType, ISNULL(O.[Type],'''') ) )) 
ORDER BY 
O.[Type]
,O.[Name]
END
' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_EnsureObject]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE PROCEDURE [dbo].[spPragmaSQL_EnsureObject]
@DatabaseID int
, @ObjName varchar(2048)
, @ObjType varchar(10)
as begin
  if ISNULL(@DatabaseID,-1) = -1 or ISNULL(@ObjName,'''') = '''' or ISNULL(@ObjType,'''') = '''' begin
    return
  end
  
  if not exists(select * from [Object] where LOWER([Name]) = LOWER(@ObjName) and LOWER([Type]) = LOWER(@ObjType) and DatabaseID = @DatabaseID) begin
    insert into [Object]([Name],[Type], DatabaseID)
    values(@ObjName,@ObjType, @DatabaseID)
  end
end
' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON


IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_ScriptHist_List]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/* Generated with PragmaSQL on 29-12-2006 11:36:39 by ali.ozgur */
CREATE PROCEDURE [dbo].[spPragmaSQL_ScriptHist_List]
AS BEGIN
	SELECT
		ScriptHistID
		, ScriptID
		, Script
		, ModifiedOn
		, ModifiedBy
	FROM ScriptHist
END
' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON


IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_ScriptHist_GetHist]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[spPragmaSQL_ScriptHist_GetHist]
@ScriptID int
AS BEGIN
	SELECT
		ScriptHistID
		, ScriptID
		, Script
		, ModifiedOn
		, ModifiedBy
	FROM ScriptHist
  WHERE ScriptID = @ScriptID
END
' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON


IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_ScriptHist_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[spPragmaSQL_ScriptHist_Insert]
@ScriptID int
, @Script text
, @ModifiedBy varchar(50)
AS BEGIN
	INSERT INTO ScriptHist( ScriptID, Script, ModifiedOn, ModifiedBy )
	VALUES( @ScriptID, @Script, GetDate(), @ModifiedBy )
END
' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON


IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_ScriptHist_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[spPragmaSQL_ScriptHist_Delete]
@ScriptHistID int
AS BEGIN
	DELETE ScriptHist
	WHERE
		( ScriptHistID = @ScriptHistID )

END
' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON


IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_ScriptHist_DeleteAllForScript]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[spPragmaSQL_ScriptHist_DeleteAllForScript]
@ScriptID int
AS BEGIN
	DELETE ScriptHist
	WHERE
		( ScriptID = @ScriptID )

END
' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON


IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_CodeSnippet_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[spPragmaSQL_CodeSnippet_Insert]

@Type int
, @ParentID int
, @Name varchar(255)
, @Snippet text
, @Description text
, @CreatedBy varchar(50)
, @SnippetID int out
AS BEGIN
  INSERT INTO CodeSnippet
  (
    Type
  	, ParentID
  	, Name
  	, Snippet
  	, Description
  	, CreatedBy
  	, CreatedOn
  )
  VALUES
  (
      @Type
    , @ParentID
    , @Name
    , @Snippet
    , @Description
    , @CreatedBy
    , GetDate()
  )

  set @SnippetID = @@IDENTITY
END
' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON


IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_CodeSnippet_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[spPragmaSQL_CodeSnippet_Update]
@SnippetID int
, @ParentID int
, @Name varchar(255)
, @Snippet text
, @Description text
, @UpdatedBy varchar(50)
AS BEGIN
  if not exists(select SnippetID from CodeSnippet where SnippetID = @SnippetID) begin
    raiserror(''Snippet "%d:%s" was removed or does not exist!'',16,1,@SnippetID, @Name)
    return 
  end
  
  UPDATE CodeSnippet
  SET
 	  ParentID = @ParentID
 	  , Name = @Name
  	, Snippet = @Snippet
  	, Description = @Description
  	, UpdatedBy = @UpdatedBy
   WHERE SnippetID = @SnippetID
END

' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON


IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_CodeSnippet_ListAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[spPragmaSQL_CodeSnippet_ListAll]
as begin
  select * from  CodeSnippet
  where ISNULL(IsDeleted,0) = 0 AND Type = 1
end



' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON


IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_EnsureServer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[spPragmaSQL_EnsureServer]
@ServerName varchar(50)
as begin
  if ISNULL(@ServerName,'''') = '''' begin
    return
  end

  if not exists(select ServerID from [Server] where [Name] = @ServerName) begin
    insert into [Server]([Name])
    values(@ServerName)
  end

end
' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON


IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_EnsureDatabase]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[spPragmaSQL_EnsureDatabase]
@ServerName varchar(50)
,@DatabaseName varchar(50)
as begin

  if ( ISNULL(@ServerName,'''') = '''' ) or ( ISNULL(@DatabaseName,'''') = '''' ) begin
    return
  end

  declare @ServerID int
  select @ServerID = NULL

  exec dbo.spPragmaSQL_EnsureServer @ServerName    
  
  select @ServerID = ServerID from [Server] where [Name] = @ServerName

  if @ServerID is null begin
    raiserror(''Can not locate server with name %s'',16,1, @ServerName)
    return
  end

  if not exists(select * from [Database] where ServerID = @ServerID and [Name] = @DatabaseName )
  begin
    insert into [Database](ServerID, [Name])
    values(@ServerID,@DatabaseName)
  end

end

' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON


IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_Script_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[spPragmaSQL_Script_Insert]
@ParentID int
, @Name varchar(255)
, @Script text
, @CreatedBy varchar(50)
, @Type int
, @ServerName varchar(50)
, @DatabaseName varchar(50)
, @HelpText text
, @UID varchar(50)
, @ScriptID int out
AS BEGIN
  exec dbo.spPragmaSQL_EnsureDatabase @ServerName, @DatabaseName


  declare @DatabaseID int

  select @DatabaseID = NULL

  select @DatabaseID = D.DatabaseID
  from [Database] D
  join [Server] S on D.ServerID = S.ServerID
  where
    D.[Name] = @DatabaseName
    and (S.[Name] = @ServerName)
  
	INSERT INTO Script( ParentID, Name, [Script], CreatedBy, CreatedOn, Type, DatabaseID, HelpText,UID  )
	VALUES( @ParentID, @Name, @Script, @CreatedBy, GetDate(), @Type, @DatabaseID, @HelpText,@UID  )
	
	select @ScriptID = @@identity
END



' 
END

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON


IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[spPragmaSQL_ObjectChangeHist_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[spPragmaSQL_ObjectChangeHist_Insert]
@ServerName varchar(50)
, @DatabaseName varchar(50)
, @ObjectName varchar(2048)
, @ObjectScript text
, @ObjectType varchar(10)
, @CreatedBy varchar(50)
, @Comment varchar(2048)
as begin
    
  exec dbo.spPragmaSQL_EnsureDatabase @ServerName, @DatabaseName
  
  
  declare @DatabaseID int
  declare @ObjectID int

  select @DatabaseID = NULL, @ObjectID = NULL

  select @DatabaseID = D.DatabaseID 
  from [Database] D 
  join [Server] S on D.ServerID = S.ServerID
  where 
    D.[Name] = @DatabaseName
    and (S.[Name] = @ServerName)

  if @DatabaseID is null begin
    raiserror(''Can not locate database with name %s on server %s.'',16,1,@DatabaseName, @ServerName)
    return
  end
  
  exec dbo.spPragmaSQL_EnsureObject @DatabaseID,@ObjectName, @ObjectType
  
  select @ObjectID = ObjectID from [Object] where LOWER([Name]) = LOWER(@ObjectName) and LOWER([Type]) = LOWER(@ObjectType) and DatabaseID = @DatabaseID
  if @ObjectID is null begin
    raiserror(''Can not locate object with name %s .'',16,1,@ObjectName)
    return
  end

  insert into ObjectChangeHist(  ObjectID , ObjectScript ,CreatedBy , CreatedOn, Comment )
  values(@ObjectID , @ObjectScript , @CreatedBy , GetDate(), @Comment )
end
' 
END

IF NOT EXISTS (SELECT * FROM sysforeignkeys WHERE constid = OBJECT_ID(N'[dbo].[FK_ScriptHist_Script]') )
ALTER TABLE [dbo].[ScriptHist]  WITH CHECK ADD  CONSTRAINT [FK_ScriptHist_Script] FOREIGN KEY([ScriptID])
REFERENCES [dbo].[Script] ([ScriptID])

IF NOT EXISTS (SELECT * FROM sysforeignkeys WHERE constid = OBJECT_ID(N'[dbo].[FK_Object_Database]') )
ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK_Object_Database] FOREIGN KEY([DatabaseID])
REFERENCES [dbo].[Database] ([DatabaseID])

IF NOT EXISTS (SELECT * FROM sysforeignkeys WHERE constid = OBJECT_ID(N'[dbo].[FK_ObjectChangeHist_Object]') )
ALTER TABLE [dbo].[ObjectChangeHist]  WITH CHECK ADD  CONSTRAINT [FK_ObjectChangeHist_Object] FOREIGN KEY([ObjectID])
REFERENCES [dbo].[Object] ([ObjectID])

IF NOT EXISTS (SELECT * FROM sysforeignkeys WHERE constid = OBJECT_ID(N'[dbo].[FK_Database_Server]') )
ALTER TABLE [dbo].[Database]  WITH CHECK ADD  CONSTRAINT [FK_Database_Server] FOREIGN KEY([ServerID])
REFERENCES [dbo].[Server] ([ServerID])
