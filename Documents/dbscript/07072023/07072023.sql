Go

alter table PIDF_IPD_PatentDetails 
Add CountryId int
,PatentStrategy int
,PatentStrategyOther nvarchar(100)
, BasicPatentExpiry date
, OtherLmitingPatentDate1 date
, OtherLmitingPatentDate2 date
, EarliestLaunchDate date
, AnyPatentstobeFiled bit
, EarliestMarketEntry date
, stimatedNumberofgenericsinthe nvarchar(100)
, Lawfirmbeingused nvarchar(100)


GO

/****** Object:  Table [dbo].[Master_Country]    Script Date: 07-07-2023 10:37:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Master_Patent_Strategy](
	[PatentStrategyID] [int] IDENTITY(1,1) NOT NULL,
	[PatentStrategyName] [nvarchar](100) NULL,
	
 CONSTRAINT [PK_Master_Patent_Strategy] PRIMARY KEY CLUSTERED 
(
	[PatentStrategyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

GO
/****** Object:  Table [dbo].[Master_WishListType]    Script Date: 07-07-2023 15:17:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_WishListType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Master_WishListType](
	[WishListTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[WishListTyp] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK__Master_W__15F0A6D83940699C] PRIMARY KEY CLUSTERED 
(
	[WishListTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tbl_WishList]    Script Date: 07-07-2023 15:17:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_WishList]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_WishList](
	[WishListId] [bigint] IDENTITY(1,1) NOT NULL,
	[WishListTypeId] [int] NULL,
	[GeographyId] [int] NULL,
	[CountryId] [int] NULL,
	[MoleculeName] [nvarchar](100) NULL,
	[Strength] [nvarchar](50) NULL,
	[IsInhouseOrInLicensed] [char](20) NULL,
	[DateOfFiling] [datetime] NULL,
	[DateOfApproval] [datetime] NULL,
	[NameofVendor] [nvarchar](100) NULL,
	[VendorEvaluationRemark] [nvarchar](50) NULL,
	[ReferenceDrugProduct] [nvarchar](200) NULL,
	[Remarks] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK__Tbl_Wish__E41F87876701DC93] PRIMARY KEY CLUSTERED 
(
	[WishListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Master_WishListType_CreatedOn]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Master_WishListType] ADD  CONSTRAINT [DF_Master_WishListType_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Master_WishListType_IsActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Master_WishListType] ADD  CONSTRAINT [DF_Master_WishListType_IsActive]  DEFAULT ((1)) FOR [IsActive]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Tbl_WishList_CreatedOn]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Tbl_WishList] ADD  CONSTRAINT [DF_Tbl_WishList_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Tbl_WishList_IsActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Tbl_WishList] ADD  CONSTRAINT [DF_Tbl_WishList_IsActive]  DEFAULT ((1)) FOR [IsActive]
END
GO
/****** Object:  StoredProcedure [dbo].[GetWishList]    Script Date: 07-07-2023 15:17:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetWishList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetWishList] AS' 
END
GO

--exec GetWishList  @PageSize = 10, @CurrentPageNumber = 0 ,@typeId=1
ALTER Proc [dbo].[GetWishList]
(          
@typeId int=0,          
@CurrentPageNumber INT = 0,                      
@PageSize INT = 25,                      
@SortColumn VARCHAR(100) = 'CreatedOn',                      
@SortDirection VARCHAR(5) = 'DESC',                      
@SearchText VARCHAR(MAX) =''          
)                  
AS                    
BEGIN              
          
SET NOCOUNT ON; 
Set @SearchText = IsNull(@SearchText, '')  
Select * from (          
SELECT Count(WishListId) OVER() TotalCount, * , ROW_NUMBER() OVER           
(    
ORDER BY           
CASE                    
WHEN @SortDirection <> 'DESC' THEN 0                    
WHEN @SortColumn = 'WishListId' THEN  WishListId                     
END DESC                     
,CASE                    
WHEN @SortDirection <> 'ASC' THEN 0                    
WHEN @SortColumn = 'WishListId' THEN WishListId          
END ASC           
, CASE                    
WHEN @SortDirection <> 'DESC' THEN null                    
WHEN @SortColumn = 'CreatedOn' THEN CreatedOn                
END DESC                     
,CASE                    
WHEN @SortDirection <> 'ASC' THEN null                    
WHEN @SortColumn = 'CreatedOn' THEN CreatedOn                    
END ASC
,
CASE                    
WHEN @SortDirection <> 'DESC' THEN null                    
WHEN @SortColumn = 'MoleculeName' THEN MoleculeName                
END DESC                     
,CASE                    
WHEN @SortDirection <> 'ASC' THEN null                    
WHEN @SortColumn = 'WishListTyp' THEN WishListTyp                    
END ASC
,
CASE                    
WHEN @SortDirection <> 'ASC' THEN null                    
WHEN @SortColumn = 'WishListTyp' THEN WishListTyp                    
END ASC
)           
RowNumber from (          
SELECT  COUNT(u.WishListId) OVER() TotalRecord,WishListId,U.WishListTypeId,WT.WishListTyp,GeographyId,u.CountryId,
MoleculeName,Strength,IsInhouseOrInLicensed,DateOfFiling,DateOfApproval,NameofVendor,
VendorEvaluationRemark,ReferenceDrugProduct,Remarks,U.CreatedOn,U.UpdatedOn,U.DeletedOn,u.IsActive,u.CreatedBy         
FROM Tbl_WishList As U   
LEFT join Master_Country as MC on MC.CountryID = U.CountryId          
LEFT join Master_User as us on us.UserId = u.CreatedBy       
LEFT Join Master_BusinessUnit As MBU On MBU.BusinessUnitId = U.GeographyId 
INNER JOIN Master_WishListType WT on WT.WishListTypeId=U.WishListTypeId
)  
AS B WHERE             
(MoleculeName LIKE '%' + @SearchText + '%') or (WishListTyp LIKE '%' + @SearchText + '%') or (Strength LIKE '%' + @SearchText + '%') 
or (NameofVendor LIKE '%' + @SearchText + '%')) As C          
Where  (WishListTypeId=@typeId or @typeId=0) and 
RowNumber BETWEEN (@CurrentPageNumber) + 1 AND (@CurrentPageNumber) + @PageSize          
          
SET NOCOUNT OFF; 
end
GO
/****** Object:  StoredProcedure [dbo].[GetWishListByTypeId]    Script Date: 07-07-2023 15:17:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetWishListByTypeId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetWishListByTypeId] AS' 
END
GO

ALTER proc [dbo].[GetWishListByTypeId](
@WishListTypeId int=0
)
as
begin
SELECT WishListId
      ,WishListTypeId
      ,GeographyId
      ,CountryId
      ,MoleculeName
      ,Strength
      ,IsInhouseOrInLicensed
      ,DateOfFiling
      ,DateOfApproval
      ,NameofVendor
      ,VendorEvaluationRemark
      ,ReferenceDrugProduct
      ,Remarks
      ,CreatedOn
      ,UpdatedOn
      ,DeletedOn
      ,IsActive
      ,CreatedBy
  FROM Tbl_WishList
  where WishListTypeId=@WishListTypeId and IsActive=1
  end
GO


