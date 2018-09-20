/*******************************************************************************
 * Copyright 2012-2014 One Platform Foundation
 *
 *       Licensed under the Apache License, Version 2.0 (the "License");
 *       you may not use this file except in compliance with the License.
 *       You may obtain a copy of the License at
 *
 *           http://www.apache.org/licenses/LICENSE-2.0
 *
 *       Unless required by applicable law or agreed to in writing, software
 *       distributed under the License is distributed on an "AS IS" BASIS,
 *       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *       See the License for the specific language governing permissions and
 *       limitations under the License.
 ******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace OnePF
{
    /**
     * Android billing implentation
     */
    public class OpenIAB_Android
#if UNITY_ANDROID
 : IOpenIAB
#endif
    {
        public static readonly string STORE_GOOGLE;
        public static readonly string STORE_AMAZON;
        public static readonly string STORE_SAMSUNG;
        public static readonly string STORE_NOKIA;
        public static readonly string STORE_SKUBIT;
        public static readonly string STORE_SKUBIT_TEST;
        public static readonly string STORE_YANDEX;
        public static readonly string STORE_APPLAND;
        public static readonly string STORE_SLIDEME;
        public static readonly string STORE_APTOIDE;

#if UNITY_ANDROID
        private static AndroidJavaObject _plugin;

        IntPtr ConvertToStringJNIArray(string[] array)
        {
            IntPtr stringClass = AndroidJNI.FindClass("java/lang/String");
            IntPtr initialString = AndroidJNI.NewStringUTF("");
            IntPtr javaStringArray = AndroidJNI.NewObjectArray(array.Length, stringClass, initialString);

            for (int i = 0; i < array.Length; ++i)
            {
                AndroidJNI.SetObjectArrayElement(javaStringArray, i, AndroidJNI.NewStringUTF(array[i]));
            }

            return javaStringArray;
        }

        static OpenIAB_Android()
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                STORE_GOOGLE = "STORE_GOOGLE";
                STORE_AMAZON = "STORE_AMAZON";
                STORE_SAMSUNG = "STORE_SAMSUNG";
                STORE_NOKIA = "STORE_NOKIA";
                STORE_SKUBIT = "STORE_SKUBIT";
                STORE_SKUBIT_TEST = "STORE_SKUBIT_TEST";
                STORE_YANDEX = "STORE_YANDEX";
                STORE_APPLAND = "STORE_APPLAND";
                STORE_SLIDEME = "STORE_SLIDEME";
                STORE_APTOIDE = "STORE_APTOIDE";
                return;
            }

            AndroidJNI.AttachCurrentThread();

            // Find the plugin instance
            using (var pluginClass = new AndroidJavaClass("org.onepf.openiab.UnityPlugin"))
            {
                _plugin = pluginClass.CallStatic<AndroidJavaObject>("instance");
            }

            using (var pluginClass = new AndroidJavaClass("org.onepf.oms.OpenIabHelper"))
            {
                STORE_GOOGLE =      pluginClass.GetStatic<string>("NAME_GOOGLE");
                STORE_AMAZON =      pluginClass.GetStatic<string>("NAME_AMAZON");
                STORE_SAMSUNG =     pluginClass.GetStatic<string>("NAME_SAMSUNG");
                STORE_NOKIA =       pluginClass.GetStatic<string>("NAME_NOKIA");
                STORE_SKUBIT =      pluginClass.GetStatic<string>("NAME_SKUBIT");
                STORE_SKUBIT_TEST = pluginClass.GetStatic<string>("NAME_SKUBIT_TEST");
                STORE_YANDEX =      pluginClass.GetStatic<string>("NAME_YANDEX");
                STORE_APPLAND =     pluginClass.GetStatic<string>("NAME_APPLAND");
                STORE_SLIDEME =     pluginClass.GetStatic<string>("NAME_SLIDEME");
                STORE_APTOIDE =     pluginClass.GetStatic<string>("NAME_APTOIDE");
            }         
        }

        private bool IsDevice()
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                //OpenIAB.EventManager.SendMessage("OnBillingNotSupported", "editor mode");
                return false;
            }
            return true;
        }

        private AndroidJavaObject CreateJavaHashMap(Dictionary<string, string> storeKeys)
        {
            var j_HashMap = new AndroidJavaObject("java.util.HashMap");
            IntPtr method_Put = AndroidJNIHelper.GetMethodID(j_HashMap.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

            if (storeKeys != null)
            {
                object[] args = new object[2];
                foreach (KeyValuePair<string, string> kvp in storeKeys)
                {
                    using (AndroidJavaObject k = new AndroidJavaObject("java.lang.String", kvp.Key))
                    {
                        using (AndroidJavaObject v = new AndroidJavaObject("java.lang.String", kvp.Value))
                        {
                            args[0] = k;
                            args[1] = v;
                            AndroidJNI.CallObjectMethod(j_HashMap.GetRawObject(),
                                method_Put, AndroidJNIHelper.CreateJNIArgArray(args));
                        }
                    }
                }
            }
            return j_HashMap;
        }

        public void init(Options options)
        {
            if (!IsDevice())
            {
                // Fake init process in the editor. For test purposes
                OpenIAB.EventManager.SendMessage("OnBillingSupported", "");
                return;
            }

            using (var j_optionsBuilder = new AndroidJavaObject("org.onepf.oms.OpenIabHelper$Options$Builder"))
            {
                var clazz = j_optionsBuilder.GetRawClass();
                var objPtr = j_optionsBuilder.GetRawObject();

                j_optionsBuilder.Call<AndroidJavaObject>("setDiscoveryTimeout", options.discoveryTimeoutMs)
                                .Call<AndroidJavaObject>("setCheckInventory", options.checkInventory)
                                .Call<AndroidJavaObject>("setCheckInventoryTimeout", options.checkInventoryTimeoutMs)
                                .Call<AndroidJavaObject>("setVerifyMode", (int) options.verifyMode)
                                .Call<AndroidJavaObject>("setStoreSearchStrategy", (int) options.storeSearchStrategy);

                if (options.samsungCertificationRequestCode > 0)
                    j_optionsBuilder.Call<AndroidJavaObject>("setSamsungCertificationRequestCode", options.samsungCertificationRequestCode);

                foreach (var pair in options.storeKeys)
                    j_optionsBuilder.Call<AndroidJavaObject>("addStoreKey", pair.Key, pair.Value);

                var addPreferredStoreNameMethod = AndroidJNI.GetMethodID(clazz, "addPreferredStoreName", "([Ljava/lang/String;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;");
                var prms = new jvalue[1];
                prms[0].l = ConvertToStringJNIArray(options.prefferedStoreNames);
                AndroidJNI.CallObjectMethod(objPtr, addPreferredStoreNameMethod, prms);

                var addAvailableStoreNameMethod = AndroidJNI.GetMethodID(clazz, "addAvailableStoreNames", "([Ljava/lang/String;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;");
                prms = new jvalue[1];
                prms[0].l = ConvertToStringJNIArray(options.availableStoreNames);
                AndroidJNI.CallObjectMethod(objPtr, addAvailableStoreNameMethod, prms);

                // Build options instance
                var buildMethod = AndroidJNI.GetMethodID(clazz, "build", "()Lorg/onepf/oms/OpenIabHelper$Options;");
                var j_options = AndroidJNI.CallObjectMethod(objPtr, buildMethod, new jvalue[0]);

                // UnityPlugin.initWithOptions(OpenIabHelper.Options options);
                var initWithOptionsMethod = AndroidJNI.GetMethodID(_plugin.GetRawClass(), "initWithOptions", "(Lorg/onepf/oms/OpenIabHelper$Options;)V");
                prms = new jvalue[1];
                prms[0].l = j_options;
                AndroidJNI.CallVoidMethod(_plugin.GetRawObject(), initWithOptionsMethod, prms);
            }
        }

        public void init(Dictionary<string, string> storeKeys = null)
        {
            if (!IsDevice()) return;

            if (storeKeys != null)
            {
                AndroidJavaObject j_storeKeys = CreateJavaHashMap(storeKeys);
                _plugin.Call("init", j_storeKeys);
                j_storeKeys.Dispose();
            }
        }

        public void mapSku(string sku, string storeName, string storeSku)
        {
            if (!IsDevice()) return;

            _plugin.Call("mapSku", sku, storeName, storeSku);
        }

        public void unbindService()
        {
            if (IsDevice())
            {
                _plugin.Call("unbindService");
            }
        }

        public bool areSubscriptionsSupported()
        {
            if (!IsDevice())
            {
                // Fake result for editor mode
                return true;
            }
            return _plugin.Call<bool>("areSubscriptionsSupported");
        }

        public void queryInventory()
        {
            if (!IsDevice())
            {
                return;
            }
            IntPtr methodId = AndroidJNI.GetMethodID(_plugin.GetRawClass(), "queryInventory", "()V");
            AndroidJNI.CallVoidMethod(_plugin.GetRawObject(), methodId, new jvalue[] { });
        }

        public void queryInventory(string[] skus)
        {
            queryInventory(skus, skus);
        }

        private void queryInventory(string[] inAppSkus, string[] subsSkus)
        {
            if (!IsDevice())
            {
                return;
            }

            jvalue[] args = new jvalue[2];
            args[0].l = ConvertToStringJNIArray(inAppSkus);
            args[1].l = ConvertToStringJNIArray(subsSkus);

            IntPtr methodId = AndroidJNI.GetMethodID(_plugin.GetRawClass(), "queryInventory", "([Ljava/lang/String;[Ljava/lang/String;)V");
            AndroidJNI.CallVoidMethod(_plugin.GetRawObject(), methodId, args);
        }

        public void purchaseProduct(string sku, string developerPayload = "")
        {
            if (!IsDevice())
            {
                // Fake purchase in editor mode
                OpenIAB.EventManager.SendMessage("OnPurchaseSucceeded", Purchase.CreateFromSku(sku, developerPayload).Serialize());
                return;
            }
            _plugin.Call("purchaseProduct", sku, developerPayload);
        }

        public void purchaseSubscription(string sku, string developerPayload = "")
        {
            if (!IsDevice())
            {
                // Fake purchase in editor mode
                OpenIAB.EventManager.SendMessage("OnPurchaseSucceeded", Purchase.CreateFromSku(sku, developerPayload).Serialize());
                return;
            }
            _plugin.Call("purchaseSubscription", sku, developerPayload);
        }

        public void consumeProduct(Purchase purchase)
        {
            if (!IsDevice())
            {
                // Fake consume in editor mode
                OpenIAB.EventManager.SendMessage("OnConsumePurchaseSucceeded", purchase.Serialize());
                return;
            }
            _plugin.Call("consumeProduct", purchase.Serialize());
        }

        public void restoreTransactions()
        {
        }

        public bool isDebugLog()
        {
            return _plugin.Call<bool>("isDebugLog");
        }

        public void enableDebugLogging(bool enabled)
        {
            _plugin.Call("enableDebugLogging", enabled);
        }

        public void enableDebugLogging(bool enabled, string tag)
        {
            _plugin.Call("enableDebugLogging", enabled, tag);
        }
#else
		static OpenIAB_Android() {
            STORE_GOOGLE = "STORE_GOOGLE";
            STORE_AMAZON = "STORE_AMAZON";
            STORE_SAMSUNG = "STORE_SAMSUNG";
            STORE_NOKIA = "STORE_NOKIA";
            STORE_YANDEX = "STORE_YANDEX";
		}
#endif
    }
}