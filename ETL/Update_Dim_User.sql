USE [TravelH2V_DW]
GO

/****** Object:  StoredProcedure [dbo].[Update_Dim_User]    Script Date: 12/26/2011 13:46:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Dim_User]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Update_Dim_User]
GO

USE [TravelH2V_DW]
GO

/****** Object:  StoredProcedure [dbo].[Update_Dim_User]    Script Date: 12/26/2011 13:46:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[Update_Dim_User]
@user_key int,
@email nvarchar(100)
As
Begin

update dim_user
set email = @email
where user_key = @user_key

End
GO


