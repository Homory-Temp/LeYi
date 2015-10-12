using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;

namespace LY.Service.ResourceConvert
{
    public partial class HomoryResourceConvertService : ServiceBase
    {
        private FileSystemWatcher watcher;

        private static dynamic _engine;

        public static dynamic engine
        {
            get
            {
                if (_engine == null)
                {
                    Assembly eLib = Assembly.LoadFile(ConfigurationManager.AppSettings["LibraryEngine"]);
                    Type db = eLib.GetType("STSdb4.Database.STSdb");
                    _engine = db.GetMethod("FromFile").Invoke(null, new[] { ConfigurationManager.AppSettings["LibraryEngineFile"] });
                }
                return _engine;
            }
        }

        public class ConvertingResource
        {
            public string Target { get; set; }
            public bool Started { get; set; }
        }

        public HomoryResourceConvertService()
        {
            InitializeComponent();
        }

        static Func<KeyValuePair<string, ConvertingResource>, bool> whereClause = o => o.Value.Started = false;

        public void StartConvert()
        {
            var table = engine.OpenXTable<string, ConvertingResource>("Video");
            Dictionary<string, ConvertingResource> dict = new Dictionary<string, ConvertingResource>();
            foreach (var pair in table)
            {
                if (!pair.Value.Started)
                    dict.Add(pair.Key, pair.Value);
            }
            foreach (var pair in dict)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(RealConvert));
                thread.Start(pair);
            }
        }

        protected bool ActualConvert(string source, string target)
        {
            try
            {
                string temp = string.Format("{0}\\{1}", ConfigurationManager.AppSettings["ResourceTempPath"], target.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Last());
                Assembly eLib = Assembly.LoadFile(ConfigurationManager.AppSettings["LibraryConverter"]);
                dynamic c = eLib.CreateInstance("NReco.VideoConverter.FFMpegConverter");
                c.ConvertMedia(source, temp, ConfigurationManager.AppSettings["ResourceConvertedExtension"].Substring(1));
                File.Move(temp, target);
                return true;
            }
            catch (Exception exception)
            {
                Log("Error", exception.StackTrace.ToString(CultureInfo.InvariantCulture));
                return false;
            }
        }

        protected void RealConvert(object pair)
        {
            try
            {
                string key = ((KeyValuePair<string, ConvertingResource>)pair).Key;
                ConvertingResource value = ((KeyValuePair<string, ConvertingResource>)pair).Value;
                var table = engine.OpenXTable<string, ConvertingResource>("Video");
                value.Started = true;
                table[key] = value;
                engine.Commit();
                if (File.Exists(value.Target))
                {
                    table.Delete(key);
                    engine.Commit();
                    Log("Succeeded", string.Format("{0} --> {1}", key, value.Target));
                }
                else
                {
                    if (ActualConvert(key, value.Target))
                    {
                        table.Delete(key);
                        engine.Commit();
                        Log("Succeeded", string.Format("{0} --> {1}", key, value.Target));
                    }
                    else
                    {
                        value.Started = false;
                        table[key] = value;
                        engine.Commit();
                        Log("Failed", string.Format("{0} --> {1}", key, value.Target));
                    }
                }
            }
            catch (Exception exception)
            {
                Log("Error", exception.StackTrace.ToString(CultureInfo.InvariantCulture));
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Log("Service", "Start");
                watcher = new FileSystemWatcher();
                watcher.BeginInit();
                watcher.IncludeSubdirectories = true;
                watcher.Path = ResourcePath;
                watcher.Created += Watcher_Created;
                watcher.EnableRaisingEvents = true;
                watcher.EndInit();
                Log("Watching", "Start");
                Log("Calculating", "Start");
                DirectoryInfo info = new DirectoryInfo(ResourcePath);
                var dirs = info.GetDirectories("视频", SearchOption.AllDirectories);
                foreach (var dir in dirs)
                {
                    var files = dir.GetFiles().Where(o =>
                                    o.Name.EndsWith(".avi", StringComparison.OrdinalIgnoreCase) ||
                                    o.Name.EndsWith(".mpg", StringComparison.OrdinalIgnoreCase) ||
                                    o.Name.EndsWith(".mpeg", StringComparison.OrdinalIgnoreCase) ||
                                    o.Name.EndsWith(".rm", StringComparison.OrdinalIgnoreCase) ||
                                    o.Name.EndsWith(".rmvb", StringComparison.OrdinalIgnoreCase) ||
                                    o.Name.EndsWith(".wmv", StringComparison.OrdinalIgnoreCase)).ToList();
                    var table = engine.OpenXTable<string, ConvertingResource>("Video");
                    foreach (var file in files)
                    {
                        var path = file.FullName;
                        var target = path.Substring(0, path.LastIndexOf('.')) + TagetExtension;
                        if (!File.Exists(target))
                        {
                            table[path] = new ConvertingResource { Target = target, Started = false };
                            Log("Todo", string.Format("{0} --> {1}", path, target));
                        }
                    }
                    engine.Commit();
                }
                Log("Calculating", "Calculated");
                StartConvert();
            }
            catch (Exception exception)
            {
                try { watcher.Dispose(); } catch { }
                Log("Error", exception.StackTrace.ToString(CultureInfo.InvariantCulture));
            }
        }

        protected string ResourcePath
        {
            get
            {
                return ConfigurationManager.AppSettings["ResourcePath"];
            }
        }

        protected string TagetExtension
        {
            get
            {
                return ConfigurationManager.AppSettings["ResourceConvertedExtension"];
            }
        }

        protected void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                var path = e.FullPath;

                if (!path.EndsWith(".avi", StringComparison.OrdinalIgnoreCase) && !path.EndsWith(".mpg", StringComparison.OrdinalIgnoreCase) && !path.EndsWith(".mpeg", StringComparison.OrdinalIgnoreCase) && !path.EndsWith(".rm", StringComparison.OrdinalIgnoreCase) && !path.EndsWith(".rmvb", StringComparison.OrdinalIgnoreCase) && !path.EndsWith(".wmv", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                var segments = path.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Reverse();
                var source = segments.First();
                var category = segments.Skip(1).Take(1).Single();
                if (category == "视频")
                {
                    var target = path.Substring(0, path.LastIndexOf('.')) + TagetExtension;
                    if (!File.Exists(target))
                    {
                        var table = engine.OpenXTable<string, ConvertingResource>("Video");
                        table[path] = new ConvertingResource { Target = target, Started = false };
                        Log("Todo", string.Format("{0} --> {1}", path, target));
                        engine.Commit();
                        StartConvert();
                    }
                }
            }
            catch (Exception exception)
            {
                try { watcher.Dispose(); } catch { }
                Log("Error", exception.StackTrace.ToString(CultureInfo.InvariantCulture));
            }
        }

        protected override void OnShutdown()
        {
            try { watcher.Dispose(); } catch { }
            try { engine.Close(); } catch { }
        }

        protected override void OnStop()
        {
            try { watcher.Dispose(); } catch { }
            try { engine.Close(); } catch { }
            Log("Watching", "Stop");
            Log("Service", "Stop");
        }

        protected void Log(string key, string content)
        {
            var path = string.Format(ConfigurationManager.AppSettings["ResourceConvertLog"], DateTime.Today.ToString("yyyyMMdd"));
            var line = string.Format("Time: {0}; Key: {1}; Content: {2}.{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), key,
                content, Environment.NewLine);
            File.AppendAllText(path, line);
        }
    }
}
