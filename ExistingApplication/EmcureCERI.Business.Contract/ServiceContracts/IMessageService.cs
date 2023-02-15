namespace EmcureCERI.Business.Contract
{
   
    public interface IMessageService
    {

        string GetApprovalMessage(string afn, string userName, string password, string url, string admin);
        string GetRejectedMessage(string afn, string userName, string url, string message, string admin);
        string GetActivatedMessage(string afn, string userName, string url, string admin);
        string GetDeactivatedMessage(string afn, string userName, string url, string message, string admin);
        string GetUserPassMessage(string afn, string userName, string password, string url);
        string GetForgotPasswordMessage(string afn, string callbackUrl);
        string GetAcknowledgement(string afn, string userName, string url);
        string GetAdminAcknowledgementBDFModification(string adminName, string prescriberName, string patientName);
        string GetAdminAcknowledgement(string adminName, string prescriberName, string url);
        string GetPatientReject(string prescriber, string patient, string message, string form);
        string GetPatientApprovedICF(string prescriber, string patient);
        string GetPatientApprovedBDF(string prescriber, string patient);
        string SendMessageToAdmin(string admin, string prescriber, string patient, string form);
        string GetPrescriberAcknowledgementICF(string prescriber, string url, string adminEmail);
        string GetPrescriberAcknowledgementBDF(string prescriber, string url, string adminEmail);
        string GetPrescriberAcknowledgementFUF(string prescriber, string url, string adminEmail);
        string ContactUs(string name, string email, string query);
    }
}

