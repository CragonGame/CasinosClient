//
//  UniWebViewLogger.cs
//  Created by Wang Wei(@onevcat) on 2017-04-11.
//
//  This file is a part of UniWebView Project (https://uniwebview.com)
//  By purchasing the asset, you are allowed to use this code in as many as projects 
//  you want, only if you publish the final products under the name of the same account
//  used for the purchase. 
//
//  This asset and all corresponding files (such as source code) are provided on an 
//  “as is” basis, without warranty of any kind, express of implied, including but not 
//  limited to the warranties of merchantability, fitness for a particular purpose, and 
//  noninfringement. In no event shall the authors or copyright holders be liable for any 
//  claim, damages or other liability, whether in action of contract, tort or otherwise, 
//  arising from, out of or in connection with the software or the use of other dealing in the software.
//

/// <summary>
/// A leveled logger which could log UniWebView related messages in 
/// both development environment and final product.
/// </summary>
public class UniWebViewLogger {
    /// <summary>
    /// Logger level.
    /// </summary>
    public enum Level {
        /// <summary>
        /// Lowest level. When set to `Verbose`, the logger will log out all messages.
        /// </summary>
        Verbose = 0,

        /// <summary>
        /// Debug level. When set to `Debug`, the logger will log out most of messages up to this level.
        /// </summary>
        Debug = 10,

        /// <summary>
        /// Info level. When set to `Info`, the logger will log out up to info messages.
        /// </summary>
        Info = 20,

        /// <summary>
        /// Critical level. When set to `Critical`, the logger will only log out errors or exceptions.
        /// </summary>
        Critical = 80,
        
        /// <summary>
        /// Off level. When set to `Off`, the logger will log out nothing.
        /// </summary>
        Off = 99
    }

    private static UniWebViewLogger instance;
    private Level level;
    
    /// <summary>
    /// Current level of this logger. All messages above current level will be logged out.
    /// Default is `Critical`, which means the logger only prints errors and exceptions.
    /// </summary>
    public Level LogLevel {
        get { return level; }
        set {
            Log(Level.Off, "Setting UniWebView logger level to: " + value);
            level = value;
            UniWebViewInterface.SetLogLevel((int)value);
        }
    }

    private UniWebViewLogger(Level level) {
        this.level = level;
    }

    /// <summary>
    /// Instance of the UniWebView logger across the process. Normally you should use this for logging purpose
    /// in UniWebView, instead of creating a new logger yourself.
    /// </summary>
    public static UniWebViewLogger Instance {
        get {
            if (instance == null) {
                instance = new UniWebViewLogger(Level.Critical);
            }
            return instance;
        }
    }

    /// <summary>
    /// Log a verbose message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void Verbose(string message) { Log(Level.Verbose, message); }

    /// <summary>
    /// Log a debug message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void Debug(string message) { Log(Level.Debug, message); }

    /// <summary>
    /// Log an info message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void Info(string message) { Log(Level.Info, message); }

    /// <summary>
    /// Log a critical message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void Critical(string message) { Log(Level.Critical, message); }

    private void Log(Level level, string message) {
        if (level >= this.LogLevel) {
            var logMessage = "<UniWebView> " + message;
            if (level == Level.Critical) {
                UnityEngine.Debug.LogError(logMessage);
            } else {
                UnityEngine.Debug.Log(logMessage);
            }
        }
    }
}