using UnityEngine;
using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;

//using BuglyUnity;

public class Demo : MonoBehaviour
{

    private const string BuglyAppIDForiOS = "93b3dce5fa";
    private const string BuglyAppIDForAndroid = "900022932";

    void InitBuglySDK ()
    {

        // TODO NOT Required. Set the crash reporter type and log to report
        // BuglyAgent.ConfigCrashReporter (1, 2);

        // TODO NOT Required. Enable debug log print, please set false for release version
#if DEBUG
        BuglyAgent.ConfigDebugMode (true);
#endif
        BuglyAgent.ConfigDebugMode (true);
        // TODO NOT Required. Register log callback with 'BuglyAgent.LogCallbackDelegate' to replace the 'Application.RegisterLogCallback(Application.LogCallback)'
        // BuglyAgent.RegisterLogCallback (CallbackDelegate.Instance.OnApplicationLogCallbackHandler);

        // BuglyAgent.ConfigDefault ("Bugly", null, "ronnie", 0);

        #if UNITY_IPHONE || UNITY_IOS
        BuglyAgent.InitWithAppId (BuglyAppIDForiOS);
        #elif UNITY_ANDROID
        BuglyAgent.InitWithAppId (BuglyAppIDForAndroid);
        #endif

        // TODO Required. If you do not need call 'InitWithAppId(string)' to initialize the sdk(may be you has initialized the sdk it associated Android or iOS project),
        // please call this method to enable c# exception handler only.
        BuglyAgent.EnableExceptionHandler ();

        // TODO NOT Required. If you need to report extra data with exception, you can set the extra handler
//        BuglyAgent.SetLogCallbackExtrasHandler (MyLogCallbackExtrasHandler);

        BuglyAgent.PrintLog (LogSeverity.LogInfo, "Init the bugly sdk");
    }

//    // Extra data handler to packet data and report them with exception.
//    // Please do not do hard work in this handler 
//    Dictionary<string, string> MyLogCallbackExtrasHandler ()
//    {
//        // TODO Test log, please do not copy it
//        BuglyAgent.PrintLog (LogSeverity.Log, "extra handler");
//
//        // TODO Sample code, please do not copy it
//        Dictionary<string, string> extras = new Dictionary<string, string> ();
//        extras.Add ("ScreenSolution", string.Format ("{0}x{1}", Screen.width, Screen.height));
//        extras.Add ("deviceModel", SystemInfo.deviceModel);
//        extras.Add ("deviceName", SystemInfo.deviceName);
//        extras.Add ("deviceType", SystemInfo.deviceType.ToString ());
//
//        extras.Add ("deviceUId", SystemInfo.deviceUniqueIdentifier);
//        extras.Add ("gDId", string.Format ("{0}", SystemInfo.graphicsDeviceID));
//        extras.Add ("gDName", SystemInfo.graphicsDeviceName);
//        extras.Add ("gDVdr", SystemInfo.graphicsDeviceVendor);
//        extras.Add ("gDVer", SystemInfo.graphicsDeviceVersion);
//        extras.Add ("gDVdrID", string.Format ("{0}", SystemInfo.graphicsDeviceVendorID));
//
//        extras.Add ("graphicsMemorySize", string.Format ("{0}", SystemInfo.graphicsMemorySize));
//        extras.Add ("systemMemorySize", string.Format ("{0}", SystemInfo.systemMemorySize));
//        extras.Add ("UnityVersion", Application.unityVersion);
// 
//        BuglyAgent.PrintLog (LogSeverity.LogInfo, "Package extra data");
//        return extras;
//    }

    // Use this for initialization
    void Start ()
    {  
        BuglyAgent.PrintLog (LogSeverity.LogInfo, "Demo Start()");

        SetupGUIStyle ();

        InitBuglySDK ();

        BuglyAgent.PrintLog (LogSeverity.LogWarning, "Init bugly sdk done");
        // set tag
#if UNITY_ANDROID
        BuglyAgent.SetScene (3450);
#else
        BuglyAgent.SetScene (3261);
#endif

    }

    // Update is called once per frame
    void Update ()
    {
        #if UNITY_ANDROID
        //当用户按下手机的返回键或home键退出游戏
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
        {
            Application.Quit();
        }
        // 按返回键退出应用
        if (Input.GetKeyDown (KeyCode.Escape)) {
            Application.Quit ();
        }
        #endif

//        BuglyAgent.PrintLog (LogSeverity.LogWarning, "Update() - 界面刷新");
    }

    void OnGUI ()
    {
        // StyledGUI ();

        // set the base area
        GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));

        GUILayout.BeginVertical ();

        // set the title bar
        GUILayout.Space (20);
        GUILayout.BeginHorizontal ();
        GUILayout.FlexibleSpace ();
        GUILayout.Label ("Bugly Unity Demo", styleTitle);
        GUILayout.FlexibleSpace ();
        GUILayout.EndHorizontal ();

        GUILayout.Space (20);

        // set layout
        GUILayout.BeginVertical ();
        GUILayout.Label ("Uncaught Exceptions:", styleContent);
        GUILayout.Space (20);

        scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (Screen.width), GUILayout.Height (Screen.height - 100));

//      GUILayout.BeginArea (new Rect(20,100, Screen.width - 20, Screen.height));
        GUILayout.BeginHorizontal ();
        GUILayout.Space (40);
        selGridIntCurrent = GUILayout.SelectionGrid (selGridIntCurrent, selGridItems, 2);
        GUILayout.Space (40);
        GUILayout.EndHorizontal ();
//              GUILayout.EndArea ();
        GUILayout.EndScrollView ();

        if (selGridIntCurrent != selGridIntDefault) {
            selGridIntDefault = selGridIntCurrent;

            TrigException (selGridIntCurrent);
        }

        GUILayout.EndVertical ();

        GUILayout.EndVertical ();
        GUILayout.EndArea ();

//        BuglyAgent.PrintLog (LogSeverity.LogWarning, "OnGUI() - 绘制界面");
    }

    private void TrigException (int selGridInt)
    {
        BuglyAgent.PrintLog (LogSeverity.LogWarning, "Trigge Exception: {0}", selGridInt);

        switch (selGridInt) {
        case 0:
            ExceptionProbe.NormalException();
            break;
        case 1:
            ExceptionProbe.NormalSystemException();
            break;
        case 2:
            ExceptionProbe.ArgumentException();
            break;
        case 3:
            ExceptionProbe.ArgumentException();
            break;
        case 4:
            ExceptionProbe.FormatException();
            break;
        case 5: // ignore
            break;
        case 6:
            ExceptionProbe.MemberAccessException();
            break;
        case 7:
            ExceptionProbe.FieldAccessException();
            break;
        case 8:
            ExceptionProbe.MethodAccessException();
            break;
        case 9:
            ExceptionProbe.MissingMemberException();
            break;
        case 10:
            ExceptionProbe.MissingMethodException();
            break;
        case 11:
            ExceptionProbe.MissingFieldException();
            break;
        case 12:
            ExceptionProbe.IndexOutOfRangeException();
            break;
        case 13:
            ExceptionProbe.ArrayTypeMismatchException();
            break;
        case 14:
            ExceptionProbe.RankException();
            break;
        case 15:
            ExceptionProbe.IOException();
            break;
        case 16:
            ExceptionProbe.DirectoryNotFoundException();
            break;
        case 17:
            ExceptionProbe.FileNotFoundException();
            break;
        case 18:
            ExceptionProbe.EndOfStreamException();
            break;
        case 19:
            ExceptionProbe.FileLoadException();
            break;
        case 20:
            ExceptionProbe.PathTooLongException();
            break;
        case 21:
            ExceptionProbe.ArithmeticException();
            break;
        case 22:
            ExceptionProbe.NotFiniteNumberException();
            break;
        case 23:
            ExceptionProbe.DivideByZero();
            break;
        case 24:
            ExceptionProbe.OutOfMemory();
            break;
        case 25:
            ExceptionProbe.NRE();
            break;
        case 26:
            ExceptionProbe.InvalidCastException();
            break;
        case 27:
            ExceptionProbe.InvalidOperationException();
            break;
        case 28:
            ExceptionProbe.OverflowException();
            break;
        case 29:
            ExceptionProbe.StackOverflow();
            break;
        default:
            try {
                throwException (new System.OutOfMemoryException ("Fatal error, out of memory"));
            } catch (System.Exception e) {
                UnityEngine.Debug.LogException (e);
            }
            break;
        }

    }

//    private void findGameObjectByTag ()
//    {
//        System.Console.Write ("it will throw UnityException");
//        GameObject go = GameObject.FindWithTag ("test");
//
//        string gName = go.name;
//        System.Console.Write (gName);
//    }
//
//    private void findGameObject ()
//    {
//        System.Console.Write ("it will throw NullReferenceException");
//
//        GameObject go = GameObject.Find ("test");
//        string gName = go.name;
//
//        System.Console.Write (gName);
//    }
//    
    private void throwException (Exception e)
    {
        if (e == null)
            return;

        BuglyAgent.PrintLog (LogSeverity.LogWarning, "Throw exception: {0}", e.ToString ());

        testDeepFrame (e);
    }

    private void testDeepFrame (Exception e)
    {
        throw e;
    }
   
    private int selGridIntCurrent = 5;
    private int selGridIntDefault = -1;
    private Vector2 scrollPosition = Vector2.zero;
    private string[] selGridItems = new string[] {"Exception","SystemException","ApplicationException",
        "ArgumentException","FormatException","...",
        "MemberAccessException","FieldAccessException","MethodAccessException","MissingMemberException","MissingMethodException","MissingFieldException",
        "IndexOutOfRangeException","ArrayTypeMismatchException","RankException",
        "IOException","DirectionNotFoundException","FileNotFoundException","EndOfStreamException","FileLoadException","PathTooLongException",
        "ArithmeticException","NotFiniteNumberException","DivideByZeroException",
        "OutOfMemoryException","NullReferenceException","InvalidCastException","InvalidOperationException",
        "Overflow","StackOverflow"
    };
    private GUIStyle styleTitle;
    private GUIStyle styleContent;
    
    void SetupGUIStyle ()
    {
        styleTitle = new GUIStyle ();
        styleTitle.fontSize = 28;
        styleTitle.fontStyle = FontStyle.Bold;
        
        styleContent = new GUIStyle ();
        styleContent.fontSize = 20;
        styleContent.fontStyle = FontStyle.Italic;
    }

    private static float StandardScreenWidth = 640.0f;
    private static float StandardScreenHeight = 960.0f;
    private float guiRatioX;
    private float guiRatioY;
    private float screenWidth;
    private float screenHeight;
    private Vector3 scaleGUIs;

    void Awake ()
    {
        BuglyAgent.DebugLog ("Demo.Awake()", "Screen: {0} x {1}", Screen.width, Screen.height);

        screenWidth = Screen.width;
        screenHeight = Screen.height;
        
        guiRatioX = (float)(screenWidth / StandardScreenWidth * 1.0f);
        guiRatioY = (float)(screenHeight / StandardScreenHeight * 1.0f);
        
        scaleGUIs = new Vector3 (guiRatioX, guiRatioY, 1.0f);
    }
    
    public void StyledGUI ()
    {
//        GUI.matrix = Matrix4x4.TRS (new Vector3(scaleGUIs.x, scaleGUIs.y, 0.0f), Quaternion.identity, scaleGUIs);
//        Console.Write ("x = " + scaleGUIs.x + ", y = " + scaleGUIs.y);
        GUI.color = Color.gray;
        GUI.skin.label.fontSize = 20;
        GUI.skin.button.fontSize = 20;
    }

    private sealed class ExceptionProbe {

        public static void NormalException(){
            try {
                DivideByZero();
            } catch (Exception e) {
                throw new System.Exception("Exception, " + e.Message);
            }
        }

        public static void NormalSystemException(){
            try {
                DivideByZero();
            } catch (Exception e) {
                throw new SystemException("SystemException," +e.Message);
            }
        }

        public static void ApplicationException(){
            try {
                DivideByZero();
            } catch (Exception e) {
                throw new ApplicationException("ApplicationException," +e.Message);
            }
        }

        public static void ArgumentException(){
            try {
                NullArgument(null);
            } catch (Exception e) {
                throw new ArgumentException("ArgumentException," + e.Message);
            }
        }

        public static void FormatException(){
            decimal price = 169.32m;
            Console.WriteLine("The cost is {0:Q2}.", price);
        }

        public static void MemberAccessException(){
            try{
                MissingMemberException();
            }catch(Exception e) {
                throw new MemberAccessException("MemberAccessException," + e.Message);
            }
        }
 
        public static void MethodAccessException(){
            try{
                MissingMethodException();
            }catch(Exception e) {
                throw new MethodAccessException("MethodAccessException," + e.Message);
            }
        }

        public static void FieldAccessException(){
            try{
                MissingFieldException();
            }catch(Exception e) {
                throw new FieldAccessException("FieldAccessException," + e.Message);
            }
        }

        public static void MissingMethodException(){
            try
            {
                // Attempt to call a static DoSomething method defined in the App class.
                // However, because the App class does not define this method,
                // a MissingMethodException is thrown.
                typeof(ExceptionProbe).InvokeMember("DoSomething", BindingFlags.Static |
                                         BindingFlags.InvokeMethod, null, null, null);
            }
            catch (MissingMethodException e)
            {
                // Show the user that the DoSomething method cannot be called.
                Console.WriteLine("Unable to call the DoSomething method: {0}", e.Message);
                throw e;
            }
        }
        public static void MissingMemberException(){
            try
            {
                // Attempt to access a static AnotherField field defined in the App class.
                // However, because the App class does not define this field,
                // a MissingFieldException is thrown.
                typeof(ExceptionProbe).InvokeMember("AnotherField", BindingFlags.Static |
                                         BindingFlags.GetField, null, null, null);
            }
            catch (MissingMemberException e)
            {
                // Notice that this code is catching MissingMemberException which is the
                // base class of MissingMethodException and MissingFieldException.
                // Show the user that the AnotherField field cannot be accessed.
                Console.WriteLine("Unable to access the AnotherField field: {0}", e.Message);
                throw e;
            }
        }
        public static void MissingFieldException(){
            try
            {
                // Attempt to access a static AField field defined in the App class.
                // However, because the App class does not define this field,
                // a MissingFieldException is thrown.
                typeof(ExceptionProbe).InvokeMember("AField", BindingFlags.Static | BindingFlags.SetField,
                                         null, null, null);
            }
            catch (MissingFieldException e)
            {
                // Show the user that the AField field cannot be accessed.
                Console.WriteLine("Unable to access the AField field: {0}", e.Message);
                throw e;
            }
        }

        public static void IndexOutOfRangeException(){
            string[] array = {"array"};
            Console.Write ("{0}", array[4]);
        }

        public static void ArrayTypeMismatchException(){
            string[] names = {"Dog", "Cat", "Fish"};
            object[] objs  = (object[]) names;
            
            try 
            {
                objs[2] = "Mouse";
                
                foreach (object animalName in objs) 
                {
                    System.Console.WriteLine(animalName);
                }
            }
            catch (System.ArrayTypeMismatchException e) 
            {
                // Not reached; "Mouse" is of the correct type.
                System.Console.WriteLine("Exception Thrown.");
                throw e;
            }

        }

        public static void RankException(){
            throw new RankException ("This is RankException");
        }


        public static void IOException(){
            try
            {
                File.Open("none.txt", FileMode.Open);
            }
            catch (IOException e)
            {
                Console.WriteLine("IO");
                throw e;
            }
        }

        public static void DirectoryNotFoundException(){
            try
            {
                IOException();
            }
            catch (Exception e)
            {
                throw new DirectoryNotFoundException("DirectoryNotFoundException, " + e.Message);
            }
        }

        public static void DriveNotFoundException(){
            try
            {
                IOException();
            }
            catch (Exception e)
            {
                throw new DriveNotFoundException("DriveNotFoundException, " + e.Message);
            }
        }
        
        public static void EndOfStreamException(){
            try
            {
                IOException();
            }
            catch (Exception e)
            {
                throw new EndOfStreamException("EndOfStreamException, " + e.Message);
            }
        }

        public static void FileLoadException(){
            try
            {
                IOException();
            }
            catch (Exception e)
            {
                throw new FileLoadException("FileLoadException, " + e.Message);
            }
        }
        public static void FileNotFoundException(){
            try
            {
                File.Open("none.txt", FileMode.Open);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Not found");
                throw e;
            }
            catch (IOException e)
            {
                Console.WriteLine("IO");
                throw e;
            }
        }
        public static void PathTooLongException(){
            try
            {
                IOException();
            }
            catch (Exception e)
            {
                throw new PathTooLongException("PathTooLongException, " + e.Message);
            }
        }
        public static void PipeException(){
            try
            {
                IOException();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void ArithmeticException(){
            try {
                DivideByZero();
            } catch (Exception e) {
                throw new ArithmeticException ("ArithmeticException," + e.Message);
            }
            
        }
        public static void NotFiniteNumberException(){
            try {
                DivideByZero();
            } catch (Exception e) {
                throw new NotFiniteNumberException ("NotFiniteNumberException," + e.Message);
            }
        }
        
        public static void DivideByZero(){
            System.Console.Write ("There is divide by zero exception");
            
            int number1 = 3000;
            int number2 = 0;
            
            Console.WriteLine("{0}",number1 / number2);
        }

        public static void OverflowException(){
            int value = 780000000;
            try {
                // Square the original value.
                int square = value * value; 
                Console.WriteLine("{0} ^ 3 = {1}", value, square);
            }
            catch (OverflowException e) {
                double square = Math.Pow(value, 2);
                Console.WriteLine("Exception: {0} > {1:E}.", 
                                  square, Int32.MaxValue);
                throw e;
            } 
        }

        public static void NRE(){
            List<String> names = GetData();
            PopulateNames(names);
        }

        private static void PopulateNames(List<String> names)
        {
            String[] arrNames = { "Dakota", "Samuel", "Nikita",
                "Koani", "Saya", "Yiska", "Yumaevsky" };
            foreach (var arrName in arrNames)
                names.Add(arrName);
        }

        private static List<String> GetData() 
        {
            return null;   
            
        }

        public static void InvalidCastException(){
            bool flag = true;
            try {
                Char ch = Convert.ToChar(flag);
                Console.WriteLine("Conversion succeeded." + ch);
            }   
            catch (InvalidCastException e) {   
                Console.WriteLine("Cannot convert a Boolean to a Char.");
                throw e;
            }
        }

        public static void InvalidOperationException(){
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };
            foreach (var number in numbers) {
                int square = (int) Math.Pow(number, 2);
                Console.WriteLine("{0}^{1}", number, square);
                Console.WriteLine("Adding {0} to the collection...\n", square);
                numbers.Add(square);
            }
        }


        public static void OutOfMemory(){
            System.Console.Write ("There is OOM exception");
            string[] array = new string[100000000];
            foreach(string s in array){
                Console.Write("" + s);
            }
            OutOfMemory ();
        }
        
        public static void StackOverflow(){
            System.Console.Write ("There is StackOverflow exception");

            StackOverflow ();
        }

        
        public static  void NullArgument(string arg){
            System.Console.Write ("There is null argument exception");
            if (arg == null) {
                throw new ArgumentNullException();
            }
        }
    }
}
