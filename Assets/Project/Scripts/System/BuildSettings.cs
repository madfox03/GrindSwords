public enum TargetMarket
{
    GOOGLE = 0,
    AMAZON = 1,
    IOS = 2
}

public class BuildSettings
{

    public static bool isDebug = false;
    public static bool isFree = true;
    public static TargetMarket Market = TargetMarket.GOOGLE;

    private static string nameGoogleFree = "War Craft Machine";

    private static string bundleGoogleFree = "";//"com.gamefirst.spinnerhuntergo";
    private static string AppMetricaKeyGoogleFree = ""; //"c5dbd793-c348-4bfd-9768-c9070767e3f8";
    private static string AppodealAppKeyGoogleFree = ""; // "91fdc8d003baff13f8b7ff28da1db63e7b6237a938739d87";
    private static string publicKeyGoogleFree = ""; // "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAjhZDVqrDl0fHcpaDSN71lRdoGoTCF0OesSq5hhAWVUxchT8p3MlL2ynq7f33NNU9o46lbIZkHVD3jvHbaiHwa4Qvp7p7fphTTFUCk87BkP7aszo0CD4IM6y2j60l54okcq9dijkG83cQB8D0c8cEJd4owzZeajlbIwlCjnlx770j0TwdHJPaNUZ7KcqNiOMpLhn431obfuuWKGxKv45J5EyxaZvrSLjruA32KUhmA6GxUaVgiQeOn9ENlMScyFV9dUzHxpIS0gOQgl5qa5dhYThBeXNlJOx1T9L8iiei/ZbjINKKRhngnTmpzhWPb8XTOLq1eDHzNr/DCsFbbk9EnQIDAQAB";


    private static string nameAmazonFree = "";
    private static string bundleAmazonFree = "";
    private static string AppMetricaKeyAmazonFree = "";
    private static string AppodealAppKeyAmazonFree = "";

    private static string nameIOSFree = "";
    private static string bundleIOSFree = "";
    private static string AppMetricaKeyIOSFree = "";
    private static string AppodealAppKeyIOSFree = "";

    private static string AppMetricaTest = ""; //"5dece649-4b34-406f-90b3-008c4c237b4b";

    public static string AppodealAppKey
    {
        get
        {
            switch (Market)
            {
                case TargetMarket.GOOGLE: return AppodealAppKeyGoogleFree;
                case TargetMarket.AMAZON: return AppodealAppKeyAmazonFree;
                case TargetMarket.IOS: return AppodealAppKeyIOSFree;
            }

            return "";
        }
    }

    public static string AppMetricaKey
    {
        get
        {
            if (isDebug) return AppMetricaTest;

            switch (Market)
            {
                case TargetMarket.GOOGLE: return AppMetricaKeyGoogleFree;
                case TargetMarket.AMAZON: return AppMetricaKeyAmazonFree;
                case TargetMarket.IOS: return AppMetricaKeyIOSFree;
            }
            return "";
        }
    }

    public static string bundle
    {
        get
        {
            switch (Market)
            {
                case TargetMarket.GOOGLE: return bundleGoogleFree;
                case TargetMarket.AMAZON: return bundleAmazonFree;
                case TargetMarket.IOS: return bundleIOSFree;
            }

            return "";
        }
    }

    public static string productName
    {
        get
        {
            switch (Market)
            {
                case TargetMarket.GOOGLE: return nameGoogleFree;
                case TargetMarket.AMAZON: return nameAmazonFree;
                case TargetMarket.IOS: return nameIOSFree;
            }

            return "";
        }
    }

    public static string bundleGoogle
    {
        get { return bundleGoogleFree; } // bundleGooglePaid; }
    }

    public static string publicKeyGoogle
    {
        get { return publicKeyGoogleFree; } // publicKeyGooglePaid; }
    }

    public static string bundleAmazon
    {
        get { return bundleAmazonFree; }
    }

    public static string bundleIOS
    {
        get { return bundleIOSFree; }
    }


}


