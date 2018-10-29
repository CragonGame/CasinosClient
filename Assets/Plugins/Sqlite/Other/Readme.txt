This is a nice C# wrapper for Unity adopted csharp-sqlite project http://code.google.com/p/csharp-sqlite/
It's tested and works on WebPlayer, PC, MacOSX, Android, iPhone.
Main features:
- can work in memory only, no file creation is required - crucial for WebPlayer platform.
- support UTF-8 strings.
- Binary (blob) database fields are supported - you can store and read any binary data.
- you have all power of SQL language to make your project specific search through your data.

INSTALLATION

- just copy/import sqlitekit folder into your project.
- move StreamingAssets folder to Assets root folder.
- if you dont have playmaker please delete "Playmaker Integration" folder.

FILES

sqlite.dll - unity adopted c# sqlite libraty port by http://code.google.com/p/csharp-sqlite
DemoObject.cs - example object
SQLiteDB.cs - database wrapper
SQLiteQuery.cs - query wrapper

EXAMPLES


- Please open demo.unity scene and compile for your desired platform.
DemoObject.cs scene contains all possible ways to use wrapper:
1. As StreamingAsset - It means you can create database and use it in real-time on any platform.
2. As MemoryCache - If you need to make a sophisticated search with it, database 
is not required to be saved as a file.
3. As MemoryStream - It's your choice, if you need to run a project on WebPlayer as that platform
doesn't support file operations, you can declare MemoryStream and register it in
our sqlite library as a named stream. The MemoryStream will represent the sqlite
file byte-to-byte so you can upload or download it as a file over HTTP.
4. As File at Cache - at Application.persistentDataPath you can create database to collect statistics 
or organize user profiles in one file, which you can easily upload to or download from your server.
5. Copy preexists database from StreamingAsset to persistentDataPath and open from there.

KNOWN LIMITATION

On WebPlayer platform the function Application.persistentDataPath returns a non-existing folder, 
so we have no way to recognize is there a cache folder or not - it will throw an exception.

CONTACTS

orangetree.unity@gmail.com