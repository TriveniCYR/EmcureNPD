 -- exec stp_npd_AfterApproveRejectActions @PIDFID=208, @StatusId =7           
CREATE OR ALTER PROCEDURE [dbo].[stp_npd_AfterApproveRejectActions]                        
(            
	@PIDFID BIGINT,            
	@StatusId INT         
)                      
AS                        
BEGIN   
	IF (@StatusId = 7) 
	BEGIN
		 INSERT INTO PIDF_PBF_Reference_Product_detail (PIDFID, BusinessUnitId, RFDBrand, RFDApplicant, RFDCountryId, RFDIndication, RFDInnovators, 
														RFDInitialRevenuePotential, RFDPriceDiscounting, RFDCommercialBatchSize)
		 SELECT pb.PIDFId, pb.BusinessUnitId, pb.RFDBrand, pb.RFDApplicant, pb.RFDCountryId, pb.RFDIndication, pb.RFDInnovators, NULL, NULL, NULL
		 FROM dbo.PIDF_BusinessUnit  pb
		 WHERE pb.PIDFId = @PIDFID
		 
		 UNION
		 
		 SELECT p.PIDFID, p.BusinessUnitId, p.RFDBrand, p.RFDApplicant, p.RFDCountryId, p.RFDIndication, p.RFDInnovators, NULL, NULL, NULL
		 FROM dbo.PIDF  p
		 WHERE p.PIDFId = @PIDFID

		 
	END
END 
