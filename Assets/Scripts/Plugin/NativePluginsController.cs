using System;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class NativePluginsController : MonoBehaviour
{
    private static bool IsInit = false;
    private DateTime timeToPlay;

    void Awake()
    {
        IOSNotificationController.instance.RequestNotificationPermissions();
       // InitBilling();
    }

    void Start()
    {
        LocalNotifications();
    }

    #region LocalNotification
    private void LocalNotifications()
    {
        timeToPlay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 00, 00);
        timeToPlay = timeToPlay.AddDays(1);

        if (timeToPlay > DateTime.Now)
        {
            if (!PlayerPrefs.HasKey("TimeToPlayLocalNotification"))
            {
                TimeToPlayLocalNotification(timeToPlay);
            }
        }
        else
        {
            if (PlayerPrefs.HasKey("TimeToPlayLocalNotification"))
            {
                CancelSomeLocalNotification(PlayerPrefs.GetInt("TimeToPlayLocalNotification"));
                PlayerPrefs.DeleteKey("TimeToPlayLocalNotification");

                TimeToPlayLocalNotification(timeToPlay);
            }
            else
            {
                TimeToPlayLocalNotification(timeToPlay);
            }
        }
    }

    public static void TimeToPlayLocalNotification(DateTime _startTime)
    {
        ISN_LocalNotification notification = new ISN_LocalNotification(_startTime, "Have a minute to relax, start play.", true);
        //notification.SetData("some_test_data");
        //notification.SetSoundName("purchase_ok.wav");
        //notification.SetBadgesNumber(1);
        notification.Schedule();

        PlayerPrefs.SetInt("TimeToPlayLocalNotification", notification.Id);
    }

    public static void LifeFullLocalNotification(DateTime _startTime)
    {
        ISN_LocalNotification notification = new ISN_LocalNotification(
            _startTime, "Your lifes is full! Let's start to play.", true);
        notification.Schedule();

        PlayerPrefs.SetInt("LifeFullLocalNotification", notification.Id);
    }

    public static void HintRefillLocalNotification(DateTime _startTime)
    {
        ISN_LocalNotification notification = new ISN_LocalNotification(
            _startTime, "Your hint is restored! Let's start to play.", true);
        notification.Schedule();

        PlayerPrefs.SetInt("HintRefillLocalNotification", notification.Id);
    }

    public static void CancelAllLocalNotifications()
    {
        IOSNotificationController.instance.CancelAllLocalNotifications();
        IOSNativeUtility.SetApplicationBagesNumber(0);
    }

    public static void CancelSomeLocalNotification(int _idNotification)
    {
        IOSNotificationController.instance.CancelLocalNotificationById(_idNotification);
    }
    #endregion

    #region In-App Purchase
    public static void InitBilling()
    {
        if (!IsInit)
        {
           /* IOSInAppPurchaseManager.instance.AddProductId("com.turilin.mhog.coins5");
            IOSInAppPurchaseManager.instance.AddProductId("com.turilin.mhog.coins10");
            IOSInAppPurchaseManager.instance.AddProductId("com.turilin.mhog.coins20");
            IOSInAppPurchaseManager.instance.AddProductId("com.turilin.mhog.coins50");
            IOSInAppPurchaseManager.instance.AddProductId("com.turilin.mhog.coins100");
            IOSInAppPurchaseManager.instance.AddProductId("com.turilin.mhog.coins200");*/

            IOSInAppPurchaseManager.OnStoreKitInitComplete += OnStoreKitInitComplete;
            IOSInAppPurchaseManager.OnTransactionComplete += OnTransactionComplete;

            IsInit = true;
        }

        IOSInAppPurchaseManager.instance.LoadStore();
    }

    public static void BuyItem(string _productId)
    {
        IOSInAppPurchaseManager.Instance.BuyProduct(_productId);
    }

    private static void UnlockProducts(string productIdentifier)
    {
        ShopController shopCtrl = GameObject.FindObjectOfType<ShopController>();

        switch (productIdentifier)
        {
            case "com.turilin.mhog.coins5":
                shopCtrl.BuyPearls(1);
                break;
            case "com.turilin.mhog.coins10":
                shopCtrl.BuyPearls(1);
                break;
            case "com.turilin.mhog.coins20":
                shopCtrl.BuyPearls(1);
                break;
            case "com.turilin.mhog.coins50":
                shopCtrl.BuyPearls(1);
                break;
            case "com.turilin.mhog.coins100":
                shopCtrl.BuyPearls(1);
                break;
            case "com.turilin.mhog.coins200":
                shopCtrl.BuyPearls(1);
                break;
        }
    }

    private static void OnTransactionComplete(IOSStoreKitResult result)
    {
        switch (result.State)
        {
            case InAppPurchaseState.Purchased:
            case InAppPurchaseState.Restored:
                //Our product been succsesly purchased or restored
                //So we need to provide content to our user depends on productIdentifier
                UnlockProducts(result.ProductIdentifier);
                break;
            case InAppPurchaseState.Deferred:
                //iOS 8 introduces Ask to Buy, which lets parents approve any purchases initiated by children
                //You should update your UI to reflect this deferred state, and expect another Transaction Complete  to be called again with a new transaction state 
                //reflecting the parent’s decision or after the transaction times out. Avoid blocking your UI or gameplay while waiting for the transaction to be updated.
                break;
            case InAppPurchaseState.Failed:
                //Our purchase flow is failed.
                //We can unlock intrefase and repor user that the purchase is failed. 
                IOSNativePopUpManager.showMessage("Transaction Failed", "Error code: " + result.Error.Code + "\n" + "Error description:" + result.Error.Description);
                break;
        }
    }

    private static void OnStoreKitInitComplete(ISN_Result result)
    {
        if (result.IsSucceeded)
        {
            int avaliableProductsCount = 0;
            foreach (IOSProductTemplate tpl in IOSInAppPurchaseManager.instance.Products)
            {
                if (tpl.IsAvaliable)
                {
                    avaliableProductsCount++;
                }
            }
        }
        else
        {
            IOSNativePopUpManager.showMessage("StoreKit Init Failed", "Error code: " + result.Error.Code + "\n" + "Error description:" + result.Error.Description);
        }
    }
    #endregion
}
