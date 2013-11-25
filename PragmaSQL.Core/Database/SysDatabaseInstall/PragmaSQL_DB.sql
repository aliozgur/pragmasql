/*
if not exists( select * from sysdatabases where [name] = N'{0}')
begin
  CREATE DATABASE [{0}]
  EXEC dbo.sp_dbcmptlevel @dbname=N'{1}', @new_cmptlevel=90
  IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
  begin
  EXEC [{0}].[dbo].[sp_fulltext_database] @action = 'disable'
  end
  ALTER DATABASE [{0}] SET ANSI_NULL_DEFAULT OFF 
  ALTER DATABASE [{0}] SET ANSI_NULLS OFF 
  ALTER DATABASE [{0}] SET ANSI_PADDING OFF 
  ALTER DATABASE [{0}] SET ANSI_WARNINGS OFF 
  ALTER DATABASE [{0}] SET ARITHABORT OFF 
  ALTER DATABASE [{0}] SET AUTO_CLOSE OFF 
  ALTER DATABASE [{0}] SET AUTO_CREATE_STATISTICS ON 
  ALTER DATABASE [{0}] SET AUTO_SHRINK OFF 
  ALTER DATABASE [{0}] SET AUTO_UPDATE_STATISTICS ON 
  ALTER DATABASE [{0}] SET CURSOR_CLOSE_ON_COMMIT OFF 
  ALTER DATABASE [{0}] SET CURSOR_DEFAULT  GLOBAL 
  ALTER DATABASE [{0}] SET CONCAT_NULL_YIELDS_NULL OFF 
  ALTER DATABASE [{0}] SET NUMERIC_ROUNDABORT OFF 
  ALTER DATABASE [{0}] SET QUOTED_IDENTIFIER OFF 
  ALTER DATABASE [{0}] SET RECURSIVE_TRIGGERS OFF 
  ALTER DATABASE [{0}] SET  READ_WRITE 
  ALTER DATABASE [{0}] SET RECOVERY FULL 
  ALTER DATABASE [{0}] SET  MULTI_USER 
end else begin
  raiserror('{0} System Database already installed!',16,1,1)
end
*/

if not exists( select * from sysdatabases where [name] = N'{0}')
begin
	CREATE DATABASE [{0}]

	exec sp_dboption N'{0}', N'autoclose', N'false'
	exec sp_dboption N'{0}', N'bulkcopy', N'false'
	exec sp_dboption N'{0}', N'trunc. log', N'false'
	exec sp_dboption N'{0}', N'torn page detection', N'true'
	exec sp_dboption N'{0}', N'read only', N'false'
	exec sp_dboption N'{0}', N'dbo use', N'false'
	exec sp_dboption N'{0}', N'single', N'false'
	exec sp_dboption N'{0}', N'autoshrink', N'false'
	exec sp_dboption N'{0}', N'ANSI null default', N'false'
	exec sp_dboption N'{0}', N'recursive triggers', N'false'
	exec sp_dboption N'{0}', N'ANSI nulls', N'false'
	exec sp_dboption N'{0}', N'concat null yields null', N'false'
	exec sp_dboption N'{0}', N'cursor close on commit', N'false'
	exec sp_dboption N'{0}', N'default to local cursor', N'false'
	exec sp_dboption N'{0}', N'quoted identifier', N'false'
	exec sp_dboption N'{0}', N'ANSI warnings', N'false'
	exec sp_dboption N'{0}', N'auto create statistics', N'true'
	exec sp_dboption N'{0}', N'auto update statistics', N'true'
	if( (@@microsoftversion / power(2, 24) = 8) and (@@microsoftversion & 0xffff >= 724) )
		exec sp_dboption N'{0}', N'db chaining', N'false'
end else begin
  raiserror('PragmaSQL System Database already installed!',16,1,1)
end
