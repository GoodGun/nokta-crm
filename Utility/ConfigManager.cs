using System; 
using System.Configuration;
using Utility;

[Serializable()]
public class ConfigManager
{
    public string pathDBSchema = "";
    public string pathUpload = "";
    public string pathReport = "";
    public string pathForLists = "";
    public string pathForEmailTemplate = "";
    public string pathForResources = "";
    public string pathForUILinks = "";
    public string CookieDomain = "localhost";
    public string VirtualPathS = "";
    public string VirtualPath = "";
    public string AdminVirtualPathS = "";
    public string AdminVirtualPath = "";
    public string ImagesPath = "";
    public string ImagesPathS = "";
    public string DefaultPageTitle = "";
    public string UploadWebPath = "";
    public string ConnectionString = "";
    public string AdminMail = "sonerars@gmail.com";
    public string ExceptionEmailTo = "";

    public string DefaultLanguage = "";
    public string AvailableLanguages = "";

    public string ApplicationName = "";
    public string ApplicationStatus = "";
    public string ApplicationPassword = "";
    public string DomainName = "";
    public string WebmasterMail = "";
    public string AdministrativeMailTo = "";
    public string AdministrativeMailCc = "";
    public string AdministrativeMailBcc = "";
    public string ProjectId = "";
    public string ProjectRelatedEmail = "";
    public string ErrorEmailSubject = "";

    public bool IsTestEnvironment = false;
    public bool AllowSendingEmail = false;

    public int DefaultPageSize = 50;
    public int MaxExportRows = 1000;
    public int MaxUploadBytes = 2000;
    public int CacheVersion = 121;

    public string SmtpUser;
    public string SmtpPassword;
    public string SmtpHostAddress;
    public string SmtpServerAddress;
    public int SmtpServerPort = 0;

    public int ThumbImageWidth = 0;
    public int ThumbImageHeight = 0;
    public int SmallImageWidth = 0;
    public int SmallImageHeight = 0;
    public int MediumImageWidth = 0;
    public int MediumImageHeight = 0;
    public int BigImageWidth = 0;
    public int BigImageHeight = 0;
    public int SliderImageWidth = 0;
    public int SliderImageHeight = 0;
    public int CategoryImageWidth = 0;
    public int CategoryImageHeight = 0;

    public bool LogCommands;
    public string CommandLoggerFilter = "";

    public static ConfigManager Current 
    { 
        get 
        { 
            return CacheUtil.Get<ConfigManager>(SavePath); 
        } 
    }
    public void Save() 
    {
        CacheUtil.Set<ConfigManager>(this, SavePath);
    }
    
    private static string _SavePath;
    private static string SavePath 
    { 
        get 
        {
            if (_SavePath == null) 
                _SavePath = ConfigurationManager.AppSettings["ConfigFilePath"]; 
            return _SavePath; 
        } 
    }
}

