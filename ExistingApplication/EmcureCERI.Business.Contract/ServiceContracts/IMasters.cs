using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Contract.ServiceContracts
{
    public interface IMasters
    {
        //Formulation
        int insertFormulation(Tbl_Master_Formulation entity);
        int updateFormulation(Tbl_Master_Formulation entity);
        int deleteFormulation(Tbl_Master_Formulation entity);
        IList<Tbl_Master_Formulation> GetFormulationSingleRecord(int ID);
        IList<Tbl_Master_Formulation> GetMasterFormulation();
        IList<Tbl_Master_Formulation> GetMasterFormulationForuser();
        //ProductManufacture
        int insertProductManufacture(Tbl_Master_ProductManufacture entity);
        int updateProductManufacture(Tbl_Master_ProductManufacture entity);
        int deleteProductManufacture(Tbl_Master_ProductManufacture entity);
        IList<Tbl_Master_ProductManufacture> GetProductManufactureSingleRecord(int ID);
        IList<Tbl_Master_ProductManufacture> GetMasterProductManufacture();
        IList<Tbl_Master_ProductManufacture> GetMasterProductManufactureForUser();


        //PackSize
        int insertPackSize(Tbl_Master_PackSize entity);
        int updatePackSize(Tbl_Master_PackSize entity);
        int deletePackSize(Tbl_Master_PackSize entity);
        IList<Tbl_Master_PackSize> GetPackSizeSingleRecord(int ID);
        IList<Tbl_Master_PackSize> GetMasterPackSize();
        IList<Tbl_Master_PackSize> GetMasterarPackSizeListForUser();

        //PackStyle
        int insertPackStyle(Tbl_Master_PackStyle entity);
        int updatePackStyle(Tbl_Master_PackStyle entity);
        int deletePackStyle(Tbl_Master_PackStyle entity);
        IList<Tbl_Master_PackStyle> GetPackStyleSingleRecord(int ID);
        IList<Tbl_Master_PackStyle> GetMasterPackStyle();
        IList<Tbl_Master_PackStyle> GetMasterPackStyleForUser();

        //Strength
        int insertStrength(Tbl_Master_Strength entity);
        int updateStrength(Tbl_Master_Strength entity);
        int deleteStrength(Tbl_Master_Strength entity);
        IList<Tbl_Master_Strength> GetStrengthSingleRecord(int ID);
        IList<Tbl_Master_Strength> GetMasterStrength();
        IList<Tbl_Master_Strength> GetMasterStrengthForUser();

        //Mode of shipment
        int insertModeofshipment(Tbl_Master_Modeofshipment entity);
        int updateModeofshipment(Tbl_Master_Modeofshipment entity);
        int deleteModeofshipment(Tbl_Master_Modeofshipment entity);
        IList<Tbl_Master_Modeofshipment> GetModeofshipmentSingleRecord(int ID);
        IList<Tbl_Master_Modeofshipment> GetMasterModeofshipment();
        IList<Tbl_Master_Modeofshipment> GetMasterModeofshipmentForUser();

        //Mode of Fees payment
        int insertModeofFeesPayment(Tbl_Master_ModeofFeesPayment entity);
        int updateModeofFeesPayment(Tbl_Master_ModeofFeesPayment entity);
        int deleteModeofFeesPayment(Tbl_Master_ModeofFeesPayment entity);
        IList<Tbl_Master_ModeofFeesPayment> GetModeofFeesPaymentSingleRecord(int ID);
        IList<Tbl_Master_ModeofFeesPayment> GetMasterModeofFeesPayment();
        IList<Tbl_Master_ModeofFeesPayment> GetMasterModeofFeesPaymentForUser();

        //Incoterms
        int insertIncoterms(Tbl_Master_Incoterms entity);
        int updateIncoterms(Tbl_Master_Incoterms entity);
        int deleteIncoterms(Tbl_Master_Incoterms entity);
        IList<Tbl_Master_Incoterms> GetIncotermsSingleRecord(int ID);
        IList<Tbl_Master_Incoterms> GetMasterIncoterms();
        IList<Tbl_Master_Incoterms> GetMasterIncotermsForUser();

        //DossierTemplate
        int insertDossierTemplate(Tbl_Master_DossierTemplate entity);
        int updateDossierTemplate(Tbl_Master_DossierTemplate entity);
        int deleteDossierTemplate(Tbl_Master_DossierTemplate entity);
        IList<Tbl_Master_DossierTemplate> GetDossierTemplateSingleRecord(int ID);
        IList<Tbl_Master_DossierTemplate> GetMasterDossierTemplate();
        IList<Tbl_Master_DossierTemplate> GetMasterDossierTemplateFouUser();


        //APISite
        int insertAPISite(Tbl_Master_APISite entity);
        int updateAPISite(Tbl_Master_APISite entity);
        int deleteAPISite(Tbl_Master_APISite entity);
        IList<Tbl_Master_APISite> GetAPISiteSingleRecord(int ID);
        IList<Tbl_Master_APISite> GetMasterAPISite();
        IList<Tbl_Master_APISite> GetMasterAPISiteForUser();

        //ManufacturingSite
        int insertManufacturingSite(Tbl_Master_ManufacturingSite entity);
        int updateManufacturingSite(Tbl_Master_ManufacturingSite entity);
        int deleteManufacturingSite(Tbl_Master_ManufacturingSite entity);
        IList<Tbl_Master_ManufacturingSite> GetManufacturingSiteSingleRecord(int ID);
        IList<Tbl_Master_ManufacturingSite> GetMasterManufacturingSite();
        IList<Tbl_Master_ManufacturingSite> GetMasterManufacturingSiteForUser();

        //ArtworkType
        int insertArtworkType(Tbl_Master_ArtworkType entity);
        int updateArtworkType(Tbl_Master_ArtworkType entity);
        int deleteArtworkType(Tbl_Master_ArtworkType entity);
        IList<Tbl_Master_ArtworkType> GetArtworkTypeSingleRecord(int ID);
        IList<Tbl_Master_ArtworkType> GetMasterArtworkType();
        IList<Tbl_Master_ArtworkType> GetMasterArtworkTypeForUser();


        //GMPAvailability
        int insertGMPAvailability(Tbl_Master_GMPAvailability entity);
        int updateGMPAvailability(Tbl_Master_GMPAvailability entity);
        int deleteGMPAvailability(Tbl_Master_GMPAvailability entity);
        IList<Tbl_Master_GMPAvailability> GetGMPAvailabilitySingleRecord(int ID);
        IList<Tbl_Master_GMPAvailability> GetMasterGMPAvailability();
        IList<Tbl_Master_GMPAvailability> GetMasterGMPAvailabilityForUser();

        //For DRF Enabled/Disabled
        IList<DRFListModel> GetAllDRFListForEnabledDisabled();
        int DRFEnabledDisabled(int DRFID, bool isActive);

    }
}
