--USE [EmcureNPDUAT]
GO
CREATE or alter PROCEDURE [dbo].[SP_GetCurrencyByUser]     
(    
@UserId INT = 0     
)    
AS    
BEGIN    
 Select A.CurrencyID,A.CurrencyCode, A.CurrencyName from Master_Currency As A    
Inner Join Master_CurrencyCountryMapping As B On A.CurrencyId = B.CurrencyId    
Inner Join Master_UserCountryMapping As C On C.CountryId = B.CountryId    
Where C.UserId = @UserId And A.IsActive = 1    
UNION  
SELECT A.CurrencyID,A.CurrencyCode, A.CurrencyName   
from Master_Currency As A    
Inner Join Master_CurrencyCountryMapping As B On A.CurrencyId = B.CurrencyId  

Select CurrencyId,CurrencyName from Master_Currency_Convert

END    



GO
/****** Object:  Table [dbo].[Master_Currency_Convert]    Script Date: 17-01-2024 01:01:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Currency_Convert](
	[Id] [float] NULL,
	[CurrencyId] [nvarchar](255) NULL,
	[CurrencyName] [nvarchar](255) NULL,
	[CurrencySymbol] [nvarchar](255) NULL,
	[CurrencyCodeId] [nvarchar](255) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (1, N'ALL', N'Albanian Lek', N'Lek', N'ALL')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (2, N'XCD', N'East Caribbean Dollar', N'$', N'XCD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (3, N'EUR', N'Euro', N'€', N'EUR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (4, N'BBD', N'Barbadian Dollar', N'$', N'BBD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (5, N'BTN', N'Bhutanese Ngultrum', NULL, N'BTN')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (6, N'BND', N'Brunei Dollar', N'$', N'BND')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (7, N'XAF', N'Central African CFA Franc', NULL, N'XAF')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (8, N'CUP', N'Cuban Peso', N'$', N'CUP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (9, N'USD', N'United States Dollar', N'$', N'USD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (10, N'FKP', N'Falkland Islands Pound', N'£', N'FKP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (11, N'GIP', N'Gibraltar Pound', N'£', N'GIP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (12, N'HUF', N'Hungarian Forint', N'Ft', N'HUF')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (13, N'IRR', N'Iranian Rial', N'﷼', N'IRR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (14, N'JMD', N'Jamaican Dollar', N'J$', N'JMD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (15, N'AUD', N'Australian Dollar', N'$', N'AUD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (16, N'LAK', N'Lao Kip', N'₭', N'LAK')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (17, N'LYD', N'Libyan Dinar', NULL, N'LYD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (18, N'MKD', N'Macedonian Denar', N'ден', N'MKD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (19, N'XOF', N'West African CFA Franc', NULL, N'XOF')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (20, N'NZD', N'New Zealand Dollar', N'$', N'NZD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (21, N'OMR', N'Omani Rial', N'﷼', N'OMR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (22, N'PGK', N'Papua New Guinean Kina', NULL, N'PGK')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (23, N'RWF', N'Rwandan Franc', NULL, N'RWF')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (24, N'WST', N'Samoan Tala', NULL, N'WST')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (25, N'RSD', N'Serbian Dinar', N'Дин.', N'RSD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (26, N'SEK', N'Swedish Krona', N'kr', N'SEK')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (27, N'TZS', N'Tanzanian Shilling', N'TSh', N'TZS')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (28, N'AMD', N'Armenian Dram', NULL, N'AMD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (29, N'BSD', N'Bahamian Dollar', N'$', N'BSD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (30, N'BAM', N'Bosnia And Herzegovina Konvertibilna Marka', N'KM', N'BAM')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (31, N'CVE', N'Cape Verdean Escudo', NULL, N'CVE')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (32, N'CNY', N'Chinese Yuan', N'¥', N'CNY')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (33, N'CRC', N'Costa Rican Colon', N'₡', N'CRC')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (34, N'CZK', N'Czech Koruna', N'Kč', N'CZK')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (35, N'ERN', N'Eritrean Nakfa', NULL, N'ERN')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (36, N'GEL', N'Georgian Lari', NULL, N'GEL')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (37, N'HTG', N'Haitian Gourde', NULL, N'HTG')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (38, N'INR', N'Indian Rupee', N'₹', N'INR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (39, N'JOD', N'Jordanian Dinar', NULL, N'JOD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (40, N'KRW', N'South Korean Won', N'₩', N'KRW')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (41, N'LBP', N'Lebanese Lira', N'£', N'LBP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (42, N'MWK', N'Malawian Kwacha', NULL, N'MWK')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (43, N'MRO', N'Mauritanian Ouguiya', NULL, N'MRO')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (44, N'MZN', N'Mozambican Metical', NULL, N'MZN')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (45, N'ANG', N'Netherlands Antillean Gulden', N'ƒ', N'ANG')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (46, N'PEN', N'Peruvian Nuevo Sol', N'S/.', N'PEN')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (47, N'QAR', N'Qatari Riyal', N'﷼', N'QAR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (48, N'STD', N'Sao Tome And Principe Dobra', NULL, N'STD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (49, N'SLL', N'Sierra Leonean Leone', NULL, N'SLL')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (50, N'SOS', N'Somali Shilling', N'S', N'SOS')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (51, N'SDG', N'Sudanese Pound', NULL, N'SDG')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (52, N'SYP', N'Syrian Pound', N'£', N'SYP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (53, N'AOA', N'Angolan Kwanza', NULL, N'AOA')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (54, N'AWG', N'Aruban Florin', N'ƒ', N'AWG')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (55, N'BHD', N'Bahraini Dinar', NULL, N'BHD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (56, N'BZD', N'Belize Dollar', N'BZ$', N'BZD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (57, N'BWP', N'Botswana Pula', N'P', N'BWP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (58, N'BIF', N'Burundi Franc', NULL, N'BIF')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (59, N'KYD', N'Cayman Islands Dollar', N'$', N'KYD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (60, N'COP', N'Colombian Peso', N'$', N'COP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (61, N'DKK', N'Danish Krone', N'kr', N'DKK')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (62, N'GTQ', N'Guatemalan Quetzal', N'Q', N'GTQ')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (63, N'HNL', N'Honduran Lempira', N'L', N'HNL')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (64, N'IDR', N'Indonesian Rupiah', N'Rp', N'IDR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (65, N'ILS', N'Israeli New Sheqel', N'₪', N'ILS')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (66, N'KZT', N'Kazakhstani Tenge', N'лв', N'KZT')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (67, N'KWD', N'Kuwaiti Dinar', NULL, N'KWD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (68, N'LSL', N'Lesotho Loti', NULL, N'LSL')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (69, N'MYR', N'Malaysian Ringgit', N'RM', N'MYR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (70, N'MUR', N'Mauritian Rupee', N'₨', N'MUR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (71, N'MNT', N'Mongolian Tugrik', N'₮', N'MNT')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (72, N'MMK', N'Myanma Kyat', NULL, N'MMK')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (73, N'NGN', N'Nigerian Naira', N'₦', N'NGN')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (74, N'PAB', N'Panamanian Balboa', N'B/.', N'PAB')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (75, N'PHP', N'Philippine Peso', N'₱', N'PHP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (76, N'RON', N'Romanian Leu', N'lei', N'RON')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (77, N'SAR', N'Saudi Riyal', N'﷼', N'SAR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (78, N'SGD', N'Singapore Dollar', N'$', N'SGD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (79, N'ZAR', N'South African Rand', N'R', N'ZAR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (80, N'SRD', N'Surinamese Dollar', N'$', N'SRD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (81, N'TWD', N'New Taiwan Dollar', N'NT$', N'TWD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (82, N'TOP', N'Paanga', NULL, N'TOP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (83, N'VEF', N'Venezuelan Bolivar', NULL, N'VEF')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (84, N'DZD', N'Algerian Dinar', NULL, N'DZD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (85, N'ARS', N'Argentine Peso', N'$', N'ARS')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (86, N'AZN', N'Azerbaijani Manat', N'ман', N'AZN')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (87, N'BYR', N'Belarusian Ruble', N'p.', N'BYR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (88, N'BOB', N'Bolivian Boliviano', N'$b', N'BOB')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (89, N'BGN', N'Bulgarian Lev', N'лв', N'BGN')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (90, N'CAD', N'Canadian Dollar', N'$', N'CAD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (91, N'CLP', N'Chilean Peso', N'$', N'CLP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (92, N'CDF', N'Congolese Franc', NULL, N'CDF')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (93, N'DOP', N'Dominican Peso', N'RD$', N'DOP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (94, N'FJD', N'Fijian Dollar', N'$', N'FJD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (95, N'GMD', N'Gambian Dalasi', NULL, N'GMD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (96, N'GYD', N'Guyanese Dollar', N'$', N'GYD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (97, N'ISK', N'Icelandic Króna', N'kr', N'ISK')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (98, N'IQD', N'Iraqi Dinar', NULL, N'IQD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (99, N'JPY', N'Japanese Yen', N'¥', N'JPY')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (100, N'KPW', N'North Korean Won', N'₩', N'KPW')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (101, N'LVL', N'Latvian Lats', N'Ls', N'LVL')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (102, N'CHF', N'Swiss Franc', N'Fr.', N'CHF')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (103, N'MGA', N'Malagasy Ariary', NULL, N'MGA')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (104, N'MDL', N'Moldovan Leu', NULL, N'MDL')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (105, N'MAD', N'Moroccan Dirham', NULL, N'MAD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (106, N'NPR', N'Nepalese Rupee', N'₨', N'NPR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (107, N'NIO', N'Nicaraguan Cordoba', N'C$', N'NIO')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (108, N'PKR', N'Pakistani Rupee', N'₨', N'PKR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (109, N'PYG', N'Paraguayan Guarani', N'Gs', N'PYG')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (110, N'SHP', N'Saint Helena Pound', N'£', N'SHP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (111, N'SCR', N'Seychellois Rupee', N'₨', N'SCR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (112, N'SBD', N'Solomon Islands Dollar', N'$', N'SBD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (113, N'LKR', N'Sri Lankan Rupee', N'₨', N'LKR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (114, N'THB', N'Thai Baht', N'฿', N'THB')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (115, N'TRY', N'Turkish New Lira', NULL, N'TRY')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (116, N'AED', N'UAE Dirham', NULL, N'AED')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (117, N'VUV', N'Vanuatu Vatu', NULL, N'VUV')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (118, N'YER', N'Yemeni Rial', N'﷼', N'YER')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (119, N'AFN', N'Afghan Afghani', N'؋', N'AFN')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (120, N'BDT', N'Bangladeshi Taka', NULL, N'BDT')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (121, N'BRL', N'Brazilian Real', N'R$', N'BRL')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (122, N'KHR', N'Cambodian Riel', N'៛', N'KHR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (123, N'KMF', N'Comorian Franc', NULL, N'KMF')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (124, N'HRK', N'Croatian Kuna', N'kn', N'HRK')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (125, N'DJF', N'Djiboutian Franc', NULL, N'DJF')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (126, N'EGP', N'Egyptian Pound', N'£', N'EGP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (127, N'ETB', N'Ethiopian Birr', NULL, N'ETB')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (128, N'XPF', N'CFP Franc', NULL, N'XPF')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (129, N'GHS', N'Ghanaian Cedi', NULL, N'GHS')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (130, N'GNF', N'Guinean Franc', NULL, N'GNF')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (131, N'HKD', N'Hong Kong Dollar', N'$', N'HKD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (132, N'XDR', N'Special Drawing Rights', NULL, N'XDR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (133, N'KES', N'Kenyan Shilling', N'KSh', N'KES')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (134, N'KGS', N'Kyrgyzstani Som', N'лв', N'KGS')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (135, N'LRD', N'Liberian Dollar', N'$', N'LRD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (136, N'MOP', N'Macanese Pataca', NULL, N'MOP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (137, N'MVR', N'Maldivian Rufiyaa', NULL, N'MVR')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (138, N'MXN', N'Mexican Peso', N'$', N'MXN')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (139, N'NAD', N'Namibian Dollar', N'$', N'NAD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (140, N'NOK', N'Norwegian Krone', N'kr', N'NOK')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (141, N'PLN', N'Polish Zloty', N'zł', N'PLN')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (142, N'RUB', N'Russian Ruble', N'руб', N'RUB')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (143, N'SZL', N'Swazi Lilangeni', NULL, N'SZL')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (144, N'TJS', N'Tajikistani Somoni', NULL, N'TJS')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (145, N'TTD', N'Trinidad and Tobago Dollar', N'TT$', N'TTD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (146, N'UGX', N'Ugandan Shilling', N'USh', N'UGX')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (147, N'UYU', N'Uruguayan Peso', N'$U', N'UYU')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (148, N'VND', N'Vietnamese Dong', N'₫', N'VND')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (149, N'TND', N'Tunisian Dinar', NULL, N'TND')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (150, N'UAH', N'Ukrainian Hryvnia', N'₴', N'UAH')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (151, N'UZS', N'Uzbekistani Som', N'лв', N'UZS')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (152, N'TMT', N'Turkmenistan Manat', NULL, N'TMT')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (153, N'GBP', N'British Pound', N'£', N'GBP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (154, N'ZMW', N'Zambian Kwacha', NULL, N'ZMW')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (155, N'BTC', N'Bitcoin', N'BTC', N'BTC')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (156, N'BYN', N'New Belarusian Ruble', N'p.', N'BYN')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (157, N'BMD', N'Bermudan Dollar', NULL, N'BMD')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (158, N'GGP', N'Guernsey Pound', NULL, N'GGP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (159, N'CLF', N'Chilean Unit Of Account', NULL, N'CLF')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (160, N'CUC', N'Cuban Convertible Peso', NULL, N'CUC')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (161, N'IMP', N'Manx pound', NULL, N'IMP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (162, N'JEP', N'Jersey Pound', NULL, N'JEP')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (163, N'SVC', N'Salvadoran Colón', NULL, N'SVC')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (164, N'ZMK', N'Old Zambian Kwacha', NULL, N'ZMK')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (165, N'XAG', N'Silver (troy ounce)', NULL, N'XAG')
GO
INSERT [dbo].[Master_Currency_Convert] ([Id], [CurrencyId], [CurrencyName], [CurrencySymbol], [CurrencyCodeId]) VALUES (166, N'ZWL', N'Zimbabwean Dollar', NULL, N'ZWL')
GO
